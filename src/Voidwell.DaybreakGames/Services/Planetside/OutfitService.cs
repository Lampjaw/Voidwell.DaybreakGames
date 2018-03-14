﻿using System.Threading.Tasks;
using Voidwell.DaybreakGames.Data.Models.Planetside;
using Voidwell.DaybreakGames.CensusServices;
using System.Collections.Generic;
using Voidwell.DaybreakGames.Data.Repositories;
using Microsoft.Extensions.Logging;
using Voidwell.DaybreakGames.Models;
using Voidwell.Cache;
using System;
using System.Linq;
using Voidwell.DaybreakGames.Census.Exceptions;
using Voidwell.DaybreakGames.CensusServices.Models;
using System.Threading;

namespace Voidwell.DaybreakGames.Services.Planetside
{
    public class OutfitService : IOutfitService
    {
        private readonly IOutfitRepository _outfitRepository;
        private readonly CensusOutfit _censusOutfit;
        private readonly CensusCharacter _censusCharacter;
        private readonly ICache _cache;
        private readonly ILogger<OutfitService> _logger;

        private readonly string _cacheKey = "ps2.outfit";
        private readonly TimeSpan _cacheOutfitExpiration = TimeSpan.FromMinutes(10);
        private readonly TimeSpan _cacheOutfitMemberExpiration = TimeSpan.FromMinutes(10);
        private readonly TimeSpan _cacheOutfitDetailsExpiration = TimeSpan.FromMinutes(30);
        private readonly TimeSpan _cacheOutfitMemberDetailsExpiration = TimeSpan.FromMinutes(30);

        public OutfitService(IOutfitRepository outfitRepository, CensusOutfit censusOutfit, CensusCharacter censusCharacter, ICache cache, ILogger<OutfitService> logger)
        {
            _outfitRepository = outfitRepository;
            _censusOutfit = censusOutfit;
            _censusCharacter = censusCharacter;
            _cache = cache;
            _logger = logger;
        }

        public Task<IEnumerable<Outfit>> FindOutfits(IEnumerable<string> outfitIds)
        {
            return _outfitRepository.GetOutfitsByIdsAsync(outfitIds);
        }

        private readonly KeyedSemaphoreSlim _outfitLock = new KeyedSemaphoreSlim();

        public async Task<Outfit> GetOutfit(string outfitId)
        {
            Outfit outfit = null;

            using (await _outfitLock.WaitAsync(outfitId))
            {
                var cacheKey = $"{_cacheKey}_outfit_{outfitId}";

                outfit = await _cache.GetAsync<Outfit>(cacheKey);
                if (outfit != null)
                {
                    return outfit;
                }

                outfit = await _outfitRepository.GetOutfitAsync(outfitId);
                if (outfit == null)
                {
                    try
                    {
                        outfit = await UpdateOutfit(outfitId);
                    }
                    catch (CensusConnectionException)
                    {
                        return null;
                    }
                }

                if (outfit != null)
                {
                    await _cache.SetAsync(cacheKey, outfit, _cacheOutfitExpiration);
                }
            }

            return outfit;
        }

        public async Task<OutfitDetails> GetOutfitDetails(string outfitId)
        {
            var cacheKey = $"{_cacheKey}_details_{outfitId}";

            var details = await _cache.GetAsync<OutfitDetails>(cacheKey);
            if (details != null)
            {
                return details;
            }

            var outfit = await GetOutfitDetailsAsync(outfitId);
            if (outfit == null)
            {
                return null;
            }

            details = new OutfitDetails
            {
                Name = outfit.Name,
                Alias = outfit.Alias,
                CreatedDate = outfit.CreatedDate,
                FactionId = outfit.FactionId,
                FactionName = outfit.Faction?.Name,
                MemberCount = outfit.MemberCount,
                WorldId = outfit.WorldId,
                WorldName = outfit.World?.Name,
                LeaderCharacterId = outfit.LeaderCharacterId,
                LeaderName = outfit.LeaderCharacter?.Name
            };

            await _cache.SetAsync(cacheKey, details, _cacheOutfitDetailsExpiration);

            return details;
        }

        public async Task<Outfit> GetOutfitDetailsAsync(string outfitId)
        {
            var outfit = await TryGetOutfitFull(outfitId);

            if (outfit != null)
            {
                return outfit;
            }

            await UpdateOutfit(outfitId);

            return await TryGetOutfitFull(outfitId);
        }

        public async Task<IEnumerable<OutfitMemberDetails>> GetOutfitMembers(string outfitId)
        {
            var cacheKey = $"{_cacheKey}_member_details_{outfitId}";

            var memberDetails = await _cache.GetAsync<IEnumerable<OutfitMemberDetails>> (cacheKey);
            if (memberDetails != null)
            {
                return memberDetails;
            }

            var members = await _outfitRepository.GetOutfitMembersAsync(outfitId);

            memberDetails = members.Where(a => a.Character.Time != null).Select(a =>
            {
                return new OutfitMemberDetails
                {
                    CharacterId = a.CharacterId,
                    MemberSinceDate = a.MemberSinceDate.Value,
                    Rank = a.Rank,
                    RankOrdinal = a.RankOrdinal.Value,
                    Name = a.Character?.Name,
                    BattleRank = a.Character?.BattleRank,
                    LastLoginDate = a.Character?.Time?.LastLoginDate,
                    LifetimeStats = new OutfitMemberDetailsStats
                    {
                        FacilityCaptureCount = a.Character.LifetimeStats.FacilityCaptureCount,
                        FacilityDefendedCount = a.Character.LifetimeStats.FacilityDefendedCount,
                        WeaponKills = a.Character.LifetimeStats.WeaponKills,
                        WeaponDeaths = a.Character.LifetimeStats.WeaponDeaths,
                        WeaponFireCount = a.Character.LifetimeStats.WeaponFireCount,
                        WeaponHeadshots = a.Character.LifetimeStats.WeaponHeadshots,
                        WeaponHitCount = a.Character.LifetimeStats.WeaponHitCount,
                        WeaponPlayTime = a.Character.LifetimeStats.WeaponPlayTime,
                        WeaponScore = a.Character.LifetimeStats.WeaponScore,
                        WeaponVehicleKills = a.Character.LifetimeStats.WeaponVehicleKills,
                        AssistCount = a.Character.LifetimeStats.AssistCount,
                        RevengeCount = a.Character.LifetimeStats.RevengeCount,
                        DominationCount = a.Character.LifetimeStats.DominationCount
                    }
                };
            });

            await _cache.SetAsync(cacheKey, memberDetails, _cacheOutfitMemberDetailsExpiration);

            return memberDetails;
        }

        public Task<IEnumerable<Outfit>> LookupOutfitsByName(string name, int limit = 12)
        {
            return _outfitRepository.GetOutfitsByNameAsync(name, limit);
        }

        public async Task<Outfit> UpdateOutfit(string outfitId)
        {
            var outfit = await _censusOutfit.GetOutfit(outfitId);

            if (outfit == null)
            {
                return null;
            }

            var leader = await _censusCharacter.GetCharacter(outfit.LeaderCharacterId);

            var dataModel = new Outfit
            {
                Id = outfit.OutfitId,
                Name = outfit.Name,
                Alias = outfit.Alias,
                CreatedDate = outfit.TimeCreated,
                LeaderCharacterId = outfit.LeaderCharacterId,
                MemberCount = outfit.MemberCount,
                FactionId = leader.FactionId,
                WorldId = leader.WorldId
            };

            return await _outfitRepository.UpsertAsync(dataModel);
        }

        private readonly KeyedSemaphoreSlim _outfitMembershipLock = new KeyedSemaphoreSlim();

        public async Task<OutfitMember> UpdateCharacterOutfitMembership(string characterId)
        {
            OutfitMember outfitMember = null;

            using (await _outfitMembershipLock.WaitAsync(characterId))
            {
                var cacheKey = $"{_cacheKey}_member_{characterId}";

                outfitMember = await _cache.GetAsync<OutfitMember>(cacheKey);
                if (outfitMember != null)
                {
                    return outfitMember.OutfitId != null ? outfitMember : null;
                }

                CensusOutfitMemberModel membership = null;

                try
                {
                    membership = await _censusCharacter.GetCharacterOutfitMembership(characterId);
                }
                catch (CensusConnectionException)
                {
                    return null;
                }

                if (membership == null)
                {
                    await _outfitRepository.RemoveOutfitMemberAsync(characterId);
                    await _cache.SetAsync(cacheKey, new OutfitMember(), _cacheOutfitMemberExpiration);
                    return null;
                }

                var outfit = await GetOutfit(membership.OutfitId);
                if (outfit == null)
                {
                    _logger.LogError(84624, $"Unable to resolve outfit {membership.OutfitId} for character {characterId}");
                    await _cache.SetAsync(cacheKey, new OutfitMember(), _cacheOutfitMemberExpiration);
                    return null;
                }

                outfitMember = new OutfitMember
                {
                    OutfitId = membership.OutfitId,
                    CharacterId = membership.CharacterId,
                    MemberSinceDate = membership.MemberSinceDate,
                    Rank = membership.Rank,
                    RankOrdinal = membership.RankOrdinal
                };

                await _cache.SetAsync(cacheKey, outfitMember, _cacheOutfitMemberExpiration);
                outfitMember = await _outfitRepository.UpsertAsync(outfitMember);
            }

            return outfitMember;
        }

        private Task<Outfit> TryGetOutfitFull(string outfitId)
        {
            return _outfitRepository.GetOutfitDetailsAsync(outfitId);
        }
    }
}
