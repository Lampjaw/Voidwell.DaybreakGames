﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Voidwell.DaybreakGames.Data.Migrations
{
    public partial class initialPS2DbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterUpdateQueue",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterUpdateQueue", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "EventAchievementEarned",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    AchievementId = table.Column<string>(nullable: true),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAchievementEarned", x => new { x.Timestamp, x.CharacterId });
                });

            migrationBuilder.CreateTable(
                name: "EventBattlerankUp",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    BattleRank = table.Column<int>(nullable: false),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventBattlerankUp", x => new { x.Timestamp, x.CharacterId });
                });

            migrationBuilder.CreateTable(
                name: "EventContinentLock",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    WorldId = table.Column<string>(nullable: false),
                    ZoneId = table.Column<string>(nullable: false),
                    MetagameEventId = table.Column<string>(nullable: true),
                    PopulationNc = table.Column<float>(nullable: false),
                    PopulationTr = table.Column<float>(nullable: false),
                    PopulationVs = table.Column<float>(nullable: false),
                    TriggeringFaction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventContinentLock", x => new { x.Timestamp, x.WorldId, x.ZoneId });
                });

            migrationBuilder.CreateTable(
                name: "EventContinentUnkock",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    WorldId = table.Column<string>(nullable: false),
                    ZoneId = table.Column<string>(nullable: false),
                    MetagameEventId = table.Column<string>(nullable: true),
                    TriggeringFaction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventContinentUnkock", x => new { x.Timestamp, x.WorldId, x.ZoneId });
                });

            migrationBuilder.CreateTable(
                name: "EventFacilityControl",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    WorldId = table.Column<string>(nullable: false),
                    FacilityId = table.Column<string>(nullable: false),
                    DurationHeld = table.Column<int>(nullable: false),
                    NewFactionId = table.Column<string>(nullable: true),
                    OldFactionId = table.Column<string>(nullable: true),
                    OutfitId = table.Column<string>(nullable: true),
                    ZoneControlNc = table.Column<float>(nullable: false),
                    ZoneControlTr = table.Column<float>(nullable: false),
                    ZoneControlVs = table.Column<float>(nullable: false),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFacilityControl", x => new { x.Timestamp, x.WorldId, x.FacilityId });
                });

            migrationBuilder.CreateTable(
                name: "EventGainExperience",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    ExperienceId = table.Column<string>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    LoadoutId = table.Column<string>(nullable: true),
                    OtherId = table.Column<string>(nullable: true),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGainExperience", x => new { x.Timestamp, x.CharacterId, x.ExperienceId });
                });

            migrationBuilder.CreateTable(
                name: "EventMetagameEvent",
                columns: table => new
                {
                    MetagameId = table.Column<string>(nullable: false),
                    ExperienceBonus = table.Column<int>(nullable: false),
                    InstanceId = table.Column<string>(nullable: true),
                    MetagameEventId = table.Column<string>(nullable: true),
                    MetagameEventState = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneControlNc = table.Column<float>(nullable: false),
                    ZoneControlTr = table.Column<float>(nullable: false),
                    ZoneControlVs = table.Column<float>(nullable: false),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMetagameEvent", x => x.MetagameId);
                });

            migrationBuilder.CreateTable(
                name: "EventPlayerFacilityCapture",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    FacilityId = table.Column<string>(nullable: false),
                    OutfitId = table.Column<string>(nullable: true),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPlayerFacilityCapture", x => new { x.Timestamp, x.CharacterId, x.FacilityId });
                });

            migrationBuilder.CreateTable(
                name: "EventPlayerFacilityDefend",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    FacilityId = table.Column<string>(nullable: false),
                    OutfitId = table.Column<string>(nullable: true),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPlayerFacilityDefend", x => new { x.Timestamp, x.CharacterId, x.FacilityId });
                });

            migrationBuilder.CreateTable(
                name: "EventPlayerLogin",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    WorldId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPlayerLogin", x => new { x.Timestamp, x.CharacterId });
                });

            migrationBuilder.CreateTable(
                name: "EventPlayerLogout",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    WorldId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPlayerLogout", x => new { x.Timestamp, x.CharacterId });
                });

            migrationBuilder.CreateTable(
                name: "EventVehicleDestroy",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    AttackerCharacterId = table.Column<string>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    AttackerLoadoutId = table.Column<string>(nullable: true),
                    AttackerVehicleId = table.Column<string>(nullable: false),
                    AttackerWeaponId = table.Column<string>(nullable: true),
                    FacilityId = table.Column<string>(nullable: true),
                    FactionId = table.Column<string>(nullable: true),
                    VehicleId = table.Column<string>(nullable: false),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventVehicleDestroy", x => new { x.Timestamp, x.AttackerCharacterId, x.CharacterId });
                });

            migrationBuilder.CreateTable(
                name: "FacilityLink",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FacilityIdA = table.Column<string>(nullable: true),
                    FacilityIdB = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityLink", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faction",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CodeTag = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserSelectable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapHex",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HexType = table.Column<string>(nullable: true),
                    MapRegionId = table.Column<string>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    XPos = table.Column<int>(nullable: false),
                    YPos = table.Column<int>(nullable: false),
                    ZoneId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapHex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapRegion",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FacilityId = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    FacilityType = table.Column<string>(nullable: true),
                    FacilityTypeId = table.Column<string>(nullable: true),
                    XPos = table.Column<float>(nullable: false),
                    YPos = table.Column<float>(nullable: false),
                    ZPos = table.Column<float>(nullable: false),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetagameEventCategory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ExperienceBonus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetagameEventCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetagameEventState",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetagameEventState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSession",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    LoginDate = table.Column<DateTime>(nullable: false),
                    LogoutDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FactionId = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProfileTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PS2UpdaterScheduler",
                columns: table => new
                {
                    ServiceName = table.Column<string>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PS2UpdaterScheduler", x => x.ServiceName);
                });

            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    CostResourceId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "World",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_World", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HexSize = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FactionId = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true),
                    IsVehicleWeapon = table.Column<bool>(nullable: false),
                    ItemCategoryId = table.Column<string>(nullable: true),
                    ItemTypeId = table.Column<string>(nullable: true),
                    MaxStackSize = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    WorldId = table.Column<string>(nullable: false),
                    MetagameInstanceId = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LastFactionNc = table.Column<float>(nullable: false),
                    LastFactionTr = table.Column<float>(nullable: false),
                    LastFactionVs = table.Column<float>(nullable: false),
                    MetagameEventId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartFactionNc = table.Column<float>(nullable: false),
                    StartFactionTr = table.Column<float>(nullable: false),
                    StartFactionVs = table.Column<float>(nullable: false),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => new { x.WorldId, x.MetagameInstanceId });
                    table.ForeignKey(
                        name: "FK_Alert_MetagameEventCategory_MetagameEventId",
                        column: x => x.MetagameEventId,
                        principalTable: "MetagameEventCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFaction",
                columns: table => new
                {
                    VehicleId = table.Column<string>(nullable: false),
                    FactionId = table.Column<string>(nullable: false),
                    DbVehicleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFaction", x => new { x.VehicleId, x.FactionId });
                    table.ForeignKey(
                        name: "FK_VehicleFaction_Vehicle_DbVehicleId",
                        column: x => x.DbVehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BattleRank = table.Column<int>(nullable: false),
                    BattleRankPercentToNext = table.Column<int>(nullable: false),
                    CertsEarned = table.Column<int>(nullable: false),
                    FactionId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TitleId = table.Column<string>(nullable: true),
                    WorldId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Faction_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Faction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Title_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_World_WorldId",
                        column: x => x.WorldId,
                        principalTable: "World",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterStat",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: false),
                    AchievementCount = table.Column<int>(nullable: true),
                    AssistCount = table.Column<int>(nullable: true),
                    Deaths = table.Column<int>(nullable: true),
                    DominationCount = table.Column<int>(nullable: true),
                    FacilityCaptureCount = table.Column<int>(nullable: true),
                    FacilityDefendedCount = table.Column<int>(nullable: true),
                    FireCount = table.Column<int>(nullable: true),
                    HitCount = table.Column<int>(nullable: true),
                    KilledBy = table.Column<int>(nullable: true),
                    Kills = table.Column<int>(nullable: true),
                    MedalCount = table.Column<int>(nullable: true),
                    PlayTime = table.Column<int>(nullable: true),
                    RevengeCount = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    SkillPoints = table.Column<int>(nullable: true),
                    WeaponDamageGiven = table.Column<int>(nullable: true),
                    WeaponDamageTakenBy = table.Column<int>(nullable: true),
                    WeaponDeaths = table.Column<int>(nullable: true),
                    WeaponFireCount = table.Column<int>(nullable: true),
                    WeaponHeadshots = table.Column<int>(nullable: true),
                    WeaponHitCount = table.Column<int>(nullable: true),
                    WeaponKilledBy = table.Column<int>(nullable: true),
                    WeaponKills = table.Column<int>(nullable: true),
                    WeaponPlayTime = table.Column<int>(nullable: true),
                    WeaponScore = table.Column<int>(nullable: true),
                    WeaponVehicleKills = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterStat", x => new { x.CharacterId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_CharacterStat_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterStat_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterStatByFaction",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: false),
                    DominationCountNC = table.Column<int>(nullable: true),
                    DominationCountTR = table.Column<int>(nullable: true),
                    DominationCountVS = table.Column<int>(nullable: true),
                    FacilityCaptureCountNC = table.Column<int>(nullable: true),
                    FacilityCaptureCountTR = table.Column<int>(nullable: true),
                    FacilityCaptureCountVS = table.Column<int>(nullable: true),
                    KilledByNC = table.Column<int>(nullable: true),
                    KilledByTR = table.Column<int>(nullable: true),
                    KilledByVS = table.Column<int>(nullable: true),
                    KillsNC = table.Column<int>(nullable: true),
                    KillsTR = table.Column<int>(nullable: true),
                    KillsVS = table.Column<int>(nullable: true),
                    RevengeCountNC = table.Column<int>(nullable: true),
                    RevengeCountTR = table.Column<int>(nullable: true),
                    RevengeCountVS = table.Column<int>(nullable: true),
                    WeaponDamageGivenNC = table.Column<int>(nullable: true),
                    WeaponDamageGivenTR = table.Column<int>(nullable: true),
                    WeaponDamageGivenVS = table.Column<int>(nullable: true),
                    WeaponDamageTakenByNC = table.Column<int>(nullable: true),
                    WeaponDamageTakenByTR = table.Column<int>(nullable: true),
                    WeaponDamageTakenByVS = table.Column<int>(nullable: true),
                    WeaponHeadshotsNC = table.Column<int>(nullable: true),
                    WeaponHeadshotsTR = table.Column<int>(nullable: true),
                    WeaponHeadshotsVS = table.Column<int>(nullable: true),
                    WeaponKilledByNC = table.Column<int>(nullable: true),
                    WeaponKilledByTR = table.Column<int>(nullable: true),
                    WeaponKilledByVS = table.Column<int>(nullable: true),
                    WeaponKillsNC = table.Column<int>(nullable: true),
                    WeaponKillsTR = table.Column<int>(nullable: true),
                    WeaponKillsVS = table.Column<int>(nullable: true),
                    WeaponVehicleKillsNC = table.Column<int>(nullable: true),
                    WeaponVehicleKillsTR = table.Column<int>(nullable: true),
                    WeaponVehicleKillsVS = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterStatByFaction", x => new { x.CharacterId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_CharacterStatByFaction_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterStatByFaction_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterTime",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastLoginDate = table.Column<DateTime>(nullable: false),
                    LastSaveDate = table.Column<DateTime>(nullable: false),
                    MinutesPlayed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTime", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_CharacterTime_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterWeaponStat",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    VehicleId = table.Column<string>(nullable: false),
                    DamageGiven = table.Column<int>(nullable: true),
                    DamageTakenBy = table.Column<int>(nullable: true),
                    Deaths = table.Column<int>(nullable: true),
                    FireCount = table.Column<int>(nullable: true),
                    Headshots = table.Column<int>(nullable: true),
                    HitCount = table.Column<int>(nullable: true),
                    KilledBy = table.Column<int>(nullable: true),
                    Kills = table.Column<int>(nullable: true),
                    PlayTime = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    VehicleKills = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterWeaponStat", x => new { x.CharacterId, x.ItemId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_CharacterWeaponStat_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterWeaponStat_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterWeaponStat_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterWeaponStatByFaction",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    VehicleId = table.Column<string>(nullable: false),
                    DamageGivenNC = table.Column<int>(nullable: true),
                    DamageGivenTR = table.Column<int>(nullable: true),
                    DamageGivenVS = table.Column<int>(nullable: true),
                    DamageTakenByNC = table.Column<int>(nullable: true),
                    DamageTakenByTR = table.Column<int>(nullable: true),
                    DamageTakenByVS = table.Column<int>(nullable: true),
                    HeadshotsNC = table.Column<int>(nullable: true),
                    HeadshotsTR = table.Column<int>(nullable: true),
                    HeadshotsVS = table.Column<int>(nullable: true),
                    KilledByNC = table.Column<int>(nullable: true),
                    KilledByTR = table.Column<int>(nullable: true),
                    KilledByVS = table.Column<int>(nullable: true),
                    KillsNC = table.Column<int>(nullable: true),
                    KillsTR = table.Column<int>(nullable: true),
                    KillsVS = table.Column<int>(nullable: true),
                    VehicleKillsNC = table.Column<int>(nullable: true),
                    VehicleKillsTR = table.Column<int>(nullable: true),
                    VehicleKillsVS = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterWeaponStatByFaction", x => new { x.CharacterId, x.ItemId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_CharacterWeaponStatByFaction_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterWeaponStatByFaction_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterWeaponStatByFaction_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Outfit",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FactionId = table.Column<string>(nullable: true),
                    LeaderCharacterId = table.Column<string>(nullable: true),
                    MemberCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    WorldId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outfit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outfit_Faction_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Faction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfit_Character_LeaderCharacterId",
                        column: x => x.LeaderCharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outfit_World_WorldId",
                        column: x => x.WorldId,
                        principalTable: "World",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventDeath",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    AttackerCharacterId = table.Column<string>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    AttackerFireModeId = table.Column<string>(nullable: true),
                    AttackerLoadoutId = table.Column<string>(nullable: true),
                    AttackerOutfitId = table.Column<string>(nullable: true),
                    AttackerVehicleId = table.Column<string>(nullable: true),
                    AttackerWeaponId = table.Column<string>(nullable: true),
                    CharacterLoadoutId = table.Column<string>(nullable: true),
                    CharacterOutfitId = table.Column<string>(nullable: true),
                    IsHeadshot = table.Column<bool>(nullable: false),
                    WorldId = table.Column<string>(nullable: true),
                    ZoneId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDeath", x => new { x.Timestamp, x.AttackerCharacterId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_EventDeath_Character_AttackerCharacterId",
                        column: x => x.AttackerCharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventDeath_Outfit_AttackerOutfitId",
                        column: x => x.AttackerOutfitId,
                        principalTable: "Outfit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDeath_Vehicle_AttackerVehicleId",
                        column: x => x.AttackerVehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDeath_Item_AttackerWeaponId",
                        column: x => x.AttackerWeaponId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDeath_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventDeath_Outfit_CharacterOutfitId",
                        column: x => x.CharacterOutfitId,
                        principalTable: "Outfit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutfitMember",
                columns: table => new
                {
                    CharacterId = table.Column<string>(nullable: false),
                    MemberSinceDate = table.Column<DateTime>(nullable: false),
                    OutfitId = table.Column<string>(nullable: false),
                    Rank = table.Column<string>(nullable: true),
                    RankOrdinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitMember", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_OutfitMember_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutfitMember_Outfit_OutfitId",
                        column: x => x.OutfitId,
                        principalTable: "Outfit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alert_MetagameEventId",
                table: "Alert",
                column: "MetagameEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_FactionId",
                table: "Character",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_TitleId",
                table: "Character",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_WorldId",
                table: "Character",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterStat_ProfileId",
                table: "CharacterStat",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterStatByFaction_ProfileId",
                table: "CharacterStatByFaction",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeaponStat_ItemId",
                table: "CharacterWeaponStat",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeaponStat_Kills",
                table: "CharacterWeaponStat",
                column: "Kills");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeaponStat_VehicleId",
                table: "CharacterWeaponStat",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeaponStatByFaction_ItemId",
                table: "CharacterWeaponStatByFaction",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeaponStatByFaction_VehicleId",
                table: "CharacterWeaponStatByFaction",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDeath_AttackerCharacterId",
                table: "EventDeath",
                column: "AttackerCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDeath_AttackerOutfitId",
                table: "EventDeath",
                column: "AttackerOutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDeath_AttackerVehicleId",
                table: "EventDeath",
                column: "AttackerVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDeath_AttackerWeaponId",
                table: "EventDeath",
                column: "AttackerWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDeath_CharacterId",
                table: "EventDeath",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDeath_CharacterOutfitId",
                table: "EventDeath",
                column: "CharacterOutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemCategoryId",
                table: "Item",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfit_FactionId",
                table: "Outfit",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfit_LeaderCharacterId",
                table: "Outfit",
                column: "LeaderCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfit_WorldId",
                table: "Outfit",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitMember_OutfitId",
                table: "OutfitMember",
                column: "OutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSession_CharacterId_LoginDate_LogoutDate",
                table: "PlayerSession",
                columns: new[] { "CharacterId", "LoginDate", "LogoutDate" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFaction_DbVehicleId",
                table: "VehicleFaction",
                column: "DbVehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "CharacterStat");

            migrationBuilder.DropTable(
                name: "CharacterStatByFaction");

            migrationBuilder.DropTable(
                name: "CharacterTime");

            migrationBuilder.DropTable(
                name: "CharacterUpdateQueue");

            migrationBuilder.DropTable(
                name: "CharacterWeaponStat");

            migrationBuilder.DropTable(
                name: "CharacterWeaponStatByFaction");

            migrationBuilder.DropTable(
                name: "EventAchievementEarned");

            migrationBuilder.DropTable(
                name: "EventBattlerankUp");

            migrationBuilder.DropTable(
                name: "EventContinentLock");

            migrationBuilder.DropTable(
                name: "EventContinentUnkock");

            migrationBuilder.DropTable(
                name: "EventDeath");

            migrationBuilder.DropTable(
                name: "EventFacilityControl");

            migrationBuilder.DropTable(
                name: "EventGainExperience");

            migrationBuilder.DropTable(
                name: "EventMetagameEvent");

            migrationBuilder.DropTable(
                name: "EventPlayerFacilityCapture");

            migrationBuilder.DropTable(
                name: "EventPlayerFacilityDefend");

            migrationBuilder.DropTable(
                name: "EventPlayerLogin");

            migrationBuilder.DropTable(
                name: "EventPlayerLogout");

            migrationBuilder.DropTable(
                name: "EventVehicleDestroy");

            migrationBuilder.DropTable(
                name: "FacilityLink");

            migrationBuilder.DropTable(
                name: "MapHex");

            migrationBuilder.DropTable(
                name: "MapRegion");

            migrationBuilder.DropTable(
                name: "MetagameEventState");

            migrationBuilder.DropTable(
                name: "OutfitMember");

            migrationBuilder.DropTable(
                name: "PlayerSession");

            migrationBuilder.DropTable(
                name: "PS2UpdaterScheduler");

            migrationBuilder.DropTable(
                name: "VehicleFaction");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "MetagameEventCategory");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Outfit");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Faction");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropTable(
                name: "World");
        }
    }
}
