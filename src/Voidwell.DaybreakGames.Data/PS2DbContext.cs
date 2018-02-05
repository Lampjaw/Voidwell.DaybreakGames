﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Voidwell.DaybreakGames.Data.Models;
using Voidwell.DaybreakGames.Data.Models.Planetside;
using Voidwell.DaybreakGames.Data.Models.Planetside.Events;

namespace Voidwell.DaybreakGames.Data
{
    public class PS2DbContext : DbContext
    {
        public PS2DbContext(DbContextOptions<PS2DbContext> options)
            : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterLifetimeStat> CharacterLifetimeStats { get; set; }
        public DbSet<CharacterLifetimeStatByFaction> CharacterLifetimeStatsByFaction { get; set; }
        public DbSet<CharacterStat> CharacterStats { get; set; }
        public DbSet<CharacterStatByFaction> CharacterStatByFactions { get; set; }
        public DbSet<CharacterTime> CharacterTimes { get; set; }
        public DbSet<CharacterUpdateQueue> CharacterUpdateQueue { get; set; }
        public DbSet<CharacterWeaponStat> CharacterWeaponStats { get; set; }
        public DbSet<CharacterWeaponStatByFaction> CharacterWeaponStatByFactions { get; set; }
        public DbSet<FacilityLink> FacilityLinks { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<MapHex> MapHexs { get; set; }
        public DbSet<MapRegion> MapRegions { get; set; }
        public DbSet<MetagameEventCategory> MetagameEventCategories { get; set; }
        public DbSet<MetagameEventState> MetagameEventStates { get; set; }
        public DbSet<Outfit> Outfits { get; set; }
        public DbSet<OutfitMember> OutfitMembers { get; set; }
        public DbSet<PlayerSession> PlayerSessions { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFaction> VehicleFactions { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Zone> Zones { get; set; }

        public DbSet<AchievementEarned> AchievementEarnedEvents { get; set; }
        public DbSet<BattlerankUp> BattleRankUpEvents { get; set; }
        public DbSet<ContinentLock> ContinentLockEvents { get; set; }
        public DbSet<ContinentUnlock> ContinentUnlockEvents { get; set; }
        public DbSet<Death> EventDeaths { get; set; }
        public DbSet<FacilityControl> EventFacilityControls { get; set; }
        public DbSet<GainExperience> GainExperienceEvents { get; set; }
        public DbSet<MetagameEvent> MetagameEventEvents { get; set; }
        public DbSet<PlayerFacilityCapture> PlayerFacilityCaptureEvents { get; set; }
        public DbSet<PlayerFacilityDefend> PlayerFacilityDefendEvents { get; set; }
        public DbSet<PlayerLogin> PlayerLoginEvents { get; set; }
        public DbSet<PlayerLogout> PlayerLogoutEvents { get; set; }
        public DbSet<VehicleDestroy> EventVehicleDestroys { get; set; }

        public DbSet<UpdaterScheduler> UpdaterScheduler { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var applyGenericMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(builder, new object[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }

            /*
            builder.ApplyConfiguration(new AlertConfiguration());
            builder.ApplyConfiguration(new CharacterConfiguration());
            builder.ApplyConfiguration(new CharacterLifetimeStatConfiguration());
            builder.ApplyConfiguration(new CharacterLifetimeStatByFactionConfiguration());
            builder.ApplyConfiguration(new CharacterStatConfiguration());
            builder.ApplyConfiguration(new CharacterStatByFactionConfiguration());
            builder.ApplyConfiguration(new CharacterTimeConfiguration());
            builder.ApplyConfiguration(new CharacterWeaponStatConfiguration());
            builder.ApplyConfiguration(new CharacterWeaponStatByFactionConfiguration());
            builder.ApplyConfiguration(new EventAchievementEarnedConfiguration());
            builder.ApplyConfiguration(new EventBattlerankUpConfiguration());
            builder.ApplyConfiguration(new EventContinentLockConfiguration());
            builder.ApplyConfiguration(new EventContinentUnlockConfiguration());
            builder.ApplyConfiguration(new EventDeathConfiguration());
            builder.ApplyConfiguration(new EventFacilityControlConfiguration());
            builder.ApplyConfiguration(new EventGainExperienceConfiguration());
            builder.ApplyConfiguration(new EventPlayerFacilityCaptureConfiguration());
            builder.ApplyConfiguration(new EventPlayerFacilityDefendConfiguration());
            builder.ApplyConfiguration(new EventMetagameEventConfiguration());
            builder.ApplyConfiguration(new EventPlayerLoginConfiguration());
            builder.ApplyConfiguration(new EventPlayerLogoutConfiguration());
            builder.ApplyConfiguration(new EventVehicleDestroyConfiguration());
            builder.ApplyConfiguration(new PlayerSessionConfiguration());
            builder.ApplyConfiguration(new ProfileConfiguration());
            builder.ApplyConfiguration(new TitleConfiguration());
            builder.ApplyConfiguration(new FacilityLinkConfiguration());
            builder.ApplyConfiguration(new FactionConfiguration());
            builder.ApplyConfiguration(new ItemConfiguration());
            builder.ApplyConfiguration(new ItemCategoryConfiguration());
            builder.ApplyConfiguration(new MapHexConfiguration());
            builder.ApplyConfiguration(new MapRegionConfiguration());
            builder.ApplyConfiguration(new MetagameEventCategoryConfiguration());
            builder.ApplyConfiguration(new MetagameEventStateConfiguration());
            builder.ApplyConfiguration(new OutfitConfiguration());
            builder.ApplyConfiguration(new OutfitMemberConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new VehicleFactionConfiguration());
            builder.ApplyConfiguration(new WorldConfiguration());
            builder.ApplyConfiguration(new ZoneConfiguration());
            */
        }
    }
}
