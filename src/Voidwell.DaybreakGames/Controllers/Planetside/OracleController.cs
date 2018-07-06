﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voidwell.DaybreakGames.Data.Models.Planetside;
using Voidwell.DaybreakGames.Models;
using Voidwell.DaybreakGames.Services.Planetside;

namespace Voidwell.DaybreakGames.Controllers.Planetside
{
    [Route("ps2/oracle")]
    public class OracleController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IWeaponService _weaponService;

        public OracleController(IItemService itemService, IWeaponService weaponService)
        {
            _itemService = itemService;
            _weaponService = weaponService;
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> GetOracleCategory(string categoryId)
        {
            List<int> categoryIds;
            if (categoryId == "all")
            {
                categoryIds = OracleCategoryMap.SelectMany(a => a.Value).Distinct().ToList();
            }
            else if (OracleCategoryMap.ContainsKey(categoryId))
            {
                categoryIds = OracleCategoryMap[categoryId].ToList();
            }
            else
            {
                return BadRequest($"Invalid category '{categoryId}'");
            }

            var weapons = await _itemService.GetItemsByCategoryIds(categoryIds);

            var simpleWeapons = weapons.Select(item =>
            {
                var name = FormatCategoryName(item, weapons);
                return new SimpleItem { Id = item.Id, Name = name };
            });

            return Ok(simpleWeapons);
        }

        [HttpGet("stats/{statId}")]
        public async Task<ActionResult> GetOracleStats(string statId, [FromQuery(Name = "q")]string sWeaponIds)
        {
            if (!OracleStatTransforms.ContainsKey(statId))
            {
                return BadRequest($"Invalid stat type '{statId}'");
            }

            var weaponIds = sWeaponIds.Split(",").Select(a => int.Parse(a));
            if (!weaponIds.Any())
            {
                return BadRequest("Must select at least one weapon");
            }

            var stats = await _weaponService.GetOracleStatsFromWeaponByDateAsync(weaponIds, DateTime.MinValue, DateTime.UtcNow.Date.AddDays(-1).AddSeconds(-1));

            var oracleStat = stats.ToDictionary(a => a.Key, a => a.Value.Select(v => new OracleStat { Period = v.Date, Value = OracleStatTransforms[statId](v) }));

            return Ok(oracleStat);
        }

        private static string FormatCategoryName(Item item, IEnumerable<Item> items)
        {
            var name = item.Name;

            if (items.Count(a => a.Name == name) > 1)
            {
                switch (item.FactionId)
                {
                    case 1: //VS
                        name = "VS " + name;
                        break;
                    case 2: //NC
                        name = "NC " + name;
                        break;
                    case 3: //TR
                        name = "TR " + name;
                        break;
                }
            }

            switch (item.ItemCategoryId)
            {
                case 110: //Galaxy Left
                    name += " (Left)";
                    break;
                case 111: //Galaxy Tail
                    name += " (Tail)";
                    break;
                case 112: //Galaxy Right
                    name += " (Right)";
                    break;
                case 113: //Galaxy Top
                    name += " (Top)";
                    break;
                case 129: //Sunderer Front
                    name += " (Front)";
                    break;
                case 130: //Sunderer Rear
                    name += " (Back)";
                    break;
            }

            return name;
        }

        private static readonly Dictionary<string, Func<DailyWeaponStats, float>> OracleStatTransforms = new Dictionary<string, Func<DailyWeaponStats, float>>()
        {
            { "kills", a => a.Kills },
            { "uniques", a => a.Uniques },
            { "kpu", a => a.Kpu },
            { "avg-br", a => a.AvgBr },
            { "hkills" , a => a.Headshots },
            { "headshot-percent" , a => a.Headshots / a.Kills },
            { "q4-kills" , a => a.Q4Kills },
            { "q4-uniques" , a => a.Q4Uniques },
            { "q4-headshots" , a => a.Q4Headshots },
            { "q4-headshots-percent" , a => a.Q4Headshots / a.Q4Kills },
            { "q4-kpu", a => a.Q4Kpu },
            { "q3-kpu", a => a.Q3Kpu },
            { "q2-kpu", a => a.Q2Kpu },
            { "q1-kpu", a => a.Q1Kpu }
        };

        private static readonly Dictionary<string, IEnumerable<int>> OracleCategoryMap = new Dictionary<string, IEnumerable<int>>() {
            { "melee", new[] { 2 } },
            { "sidearms", new[] { 3, 24 } },
            { "shotguns", new[] { 4 } },
            { "smg", new[] { 5 } },
            { "lmg", new[] { 6 } },
            { "assault-rifles", new[] { 7 } },
            { "carbines", new[] { 8 } },
            { "sniper-rifles", new[] { 11 } },
            { "scout-rifles", new[] { 12 } },
            { "battle-rifles", new[] { 19 } },
            { "rocket-launchers", new[] { 13 } },
            { "es-heavy-gun", new[] { 14 } },
            { "av-max", new[] { 9, 21 } },
            { "ai-max", new[] { 10, 22 } },
            { "aa-max", new[] { 20, 23 } },
            { "grenades", new[] { 17 } },
            { "explosives", new[] { 18 } },
            { "harasser", new[] { 114 } },
            { "liberator", new[] { 115, 116, 117 } },
            { "lightning", new[] { 118 } },
            { "mbt-primary", new[] { 120, 124, 132 } },
            { "mbt-secondary", new[] { 119, 123, 131 } },
            { "esf", new[] { 121, 122, 125, 126, 127, 128 } },
            { "turrets", new[] { 102, 104 } },
            { "flash", new[] { 109 } },
            { "sunderer", new[] { 129, 130 } },
            { "galaxy", new[] { 110, 111, 112, 113 } },
            { "valkyrie", new[] { 138 } },
            { "ant", new[] { 144 } }
        };
    }
}
