﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voidwell.Cache;
using Voidwell.DaybreakGames.Data.Models.Planetside;
using Voidwell.DaybreakGames.Data.Repositories;
using Voidwell.DaybreakGames.Models;
using Voidwell.DaybreakGames.CensusStream.Models;
using Microsoft.Extensions.Logging;
using Voidwell.DaybreakGames.App;

namespace Voidwell.DaybreakGames.Services.Planetside
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IMetagameEventService _metagameEventService;
        private readonly ICombatReportService _combatReportService;
        private readonly IMapService _mapService;
        private readonly IWorldMonitor _worldMonitor;
        private readonly ICache _cache;

        private enum METAGAME_EVENT_STATE
        {
            STARTED = 135,
            RESTARTED = 136,
            CANCELED = 137,
            ENDED = 138,
            XPCHANGE = 139
        }

        private const string _cacheKey = "ps2.alert";
        private readonly TimeSpan _cacheAlertsExpiration = TimeSpan.FromMinutes(1);
        private readonly TimeSpan _cacheAlertExpiration = TimeSpan.FromMinutes(5);

        public AlertService(IAlertRepository alertRepository, IMetagameEventService metagameEventService,
            ICombatReportService combatReportService, IMapService mapService, IWorldMonitor worldMonitor,
            ICache cache, ILogger<AlertService> logger)
        {
            _alertRepository = alertRepository;
            _metagameEventService = metagameEventService;
            _combatReportService = combatReportService;
            _mapService = mapService;
            _worldMonitor = worldMonitor;
            _cache = cache;
        }

        public async Task<IEnumerable<Alert>> GetAlerts(int pageNumber, int? worldId = null, int limit = 10)
        {
            var cacheKey = $"{_cacheKey}_alerts_{pageNumber}:{worldId}";

            var alerts = await _cache.GetAsync<IEnumerable<Alert>>(cacheKey);
            if (alerts != null)
            {
                return alerts;
            }

            alerts = await _alertRepository.GetAlerts(pageNumber, limit, worldId);

            if (alerts != null && alerts.Any())
            {
                await _cache.SetAsync(cacheKey, alerts, _cacheAlertsExpiration);
            }

            return alerts;
        }

        public Task<IEnumerable<Alert>> GetActiveAlertsByWorldId(int worldId)
        {
            return _alertRepository.GetActiveAlertsByWorldId(worldId);
        }

        public async Task<AlertResult> GetAlert(int worldId, int instanceId)
        {
            var cacheKey = $"{_cacheKey}_alert_{worldId}_{instanceId}";

            var alertResult = await _cache.GetAsync<AlertResult>(cacheKey);
            if (alertResult != null)
            {
                return alertResult;
            }

            var alert = await _alertRepository.GetAlert(worldId, instanceId);
            if (alert?.ZoneId == null || !alert.StartDate.HasValue)
            {
                return null;
            }

            var combatReportTask = _combatReportService.GetCombatReport(alert.WorldId, alert.ZoneId.Value, alert.StartDate.Value, alert.EndDate);
            var zoneSnapshotTask = _mapService.GetZoneSnapshotByMetagameEvent(worldId, instanceId);

            await Task.WhenAll(combatReportTask, zoneSnapshotTask);

            if (combatReportTask.Result == null)
            {
                return null;
            }

            var neuturalScore = 0.0f;
            if (alert.MetagameEvent?.Type == 1 || alert.MetagameEvent?.Type == 8 || alert.MetagameEvent?.Type == 9)
            {
                neuturalScore = 100 - (alert.LastFactionVs.GetValueOrDefault() + alert.LastFactionVs.GetValueOrDefault() + alert.LastFactionTr.GetValueOrDefault() + alert.LastFactionNs.GetValueOrDefault());
            }

            alertResult = new AlertResult
            {
                WorldId = alert.WorldId,
                ZoneId = alert.ZoneId,
                MetagameInstanceId = alert.MetagameInstanceId,
                MetagameEventId = alert.MetagameEventId,
                StartDate = alert.StartDate,
                EndDate = alert.EndDate,
                StartFactionVS = alert.StartFactionVs.GetValueOrDefault(),
                StartFactionNC = alert.StartFactionNc.GetValueOrDefault(),
                StartFactionTR = alert.StartFactionTr.GetValueOrDefault(),
                StartFactionNS = alert.StartFactionNs.GetValueOrDefault(),
                LastFactionVS = alert.LastFactionVs.GetValueOrDefault(),
                LastFactionNC = alert.LastFactionNc.GetValueOrDefault(),
                LastFactionTR = alert.LastFactionTr.GetValueOrDefault(),
                LastFactionNS = alert.LastFactionNs.GetValueOrDefault(),
                MetagameEvent = alert.MetagameEvent,
                Log = combatReportTask.Result,
                Score = new[] {
                    neuturalScore,
                    alert.LastFactionVs.GetValueOrDefault(),
                    alert.LastFactionNc.GetValueOrDefault(),
                    alert.LastFactionTr.GetValueOrDefault(),
                    alert.LastFactionNs.GetValueOrDefault()
                },
                ServerId = alert.WorldId.ToString(),
                MapId = alert.ZoneId.ToString(),
                ZoneSnapshot = zoneSnapshotTask.Result?.Ownership
            };

            await _cache.SetAsync(cacheKey, alertResult, _cacheAlertExpiration);

            return alertResult;
        }

        public async Task ProcessMetagameEvent(MetagameEvent metagameEvent)
        {
            var eventState = Enum.Parse<METAGAME_EVENT_STATE>(metagameEvent.MetagameEventState);
            if (eventState == METAGAME_EVENT_STATE.STARTED || eventState == METAGAME_EVENT_STATE.RESTARTED)
            {
                await StartAlert(metagameEvent);
            }
            else if (eventState == METAGAME_EVENT_STATE.ENDED || eventState == METAGAME_EVENT_STATE.CANCELED)
            {
                await EndAlert(metagameEvent);
            }
        }

        private async Task StartAlert(MetagameEvent metagameEvent)
        {
            var category = await _metagameEventService.GetMetagameEvent(metagameEvent.MetagameEventId);

            if (metagameEvent.ZoneId == null)
            {
                metagameEvent.ZoneId = category?.ZoneId;
            }

            if (metagameEvent.ZoneId != null)
            {
                var zoneAlert = new ZoneAlertState(metagameEvent.Timestamp, metagameEvent.InstanceId, category);
                _worldMonitor.UpdateZoneAlert(metagameEvent.WorldId, (int)metagameEvent.ZoneId, zoneAlert);
            }

            var dataModel = new Alert
            {
                WorldId = metagameEvent.WorldId,
                ZoneId = metagameEvent.ZoneId,
                MetagameInstanceId = metagameEvent.InstanceId,
                MetagameEventId = metagameEvent.MetagameEventId,
                StartDate = metagameEvent.Timestamp,
                EndDate = metagameEvent.Timestamp + (category?.Duration ?? TimeSpan.FromMinutes(45)),
                StartFactionVs = metagameEvent.FactionVs,
                StartFactionNc = metagameEvent.FactionNc,
                StartFactionTr = metagameEvent.FactionTr,
                LastFactionVs = metagameEvent.FactionVs,
                LastFactionNc = metagameEvent.FactionNc,
                LastFactionTr = metagameEvent.FactionTr
            };

            var tasks = new List<Task>
            {
                _alertRepository.AddAsync(dataModel)
            };

            if (dataModel.ZoneId != Constants.KoltyrZoneId)
            {
                tasks.Add(CreateAlertZoneSnapshot(metagameEvent));
            }

            await Task.WhenAll(tasks);
        }

        private async Task EndAlert(MetagameEvent metagameEvent)
        {
            var alert = await _alertRepository.GetAlert(metagameEvent.WorldId, metagameEvent.InstanceId);


            if (metagameEvent.ZoneId != null || alert?.ZoneId != null)
            {
                var zoneId = metagameEvent.ZoneId ?? alert.ZoneId;
                _worldMonitor.UpdateZoneAlert(metagameEvent.WorldId, (int)zoneId);
            }

            if (alert == null)
            {
                return;
            }

            alert.EndDate = metagameEvent.Timestamp;
            alert.LastFactionVs = metagameEvent.FactionVs;
            alert.LastFactionNc = metagameEvent.FactionNc;
            alert.LastFactionTr = metagameEvent.FactionTr;

            await _alertRepository.UpdateAsync(alert);
        }

        private async Task CreateAlertZoneSnapshot(MetagameEvent metagameEvent)
        {
            if (metagameEvent.ZoneId != null)
            {
                var zoneOwnership = await _worldMonitor.RefreshZoneOwnership(metagameEvent.WorldId, metagameEvent.ZoneId.Value);
                //var zoneOwnership = _worldMonitor.GetZoneOwnership(metagameEvent.WorldId, metagameEvent.ZoneId.Value);
                if (zoneOwnership != null && zoneOwnership.Any())
                {
                    await _mapService.CreateZoneSnapshot(metagameEvent.WorldId, metagameEvent.ZoneId.Value, metagameEvent.Timestamp, metagameEvent.InstanceId, zoneOwnership);
                }
            }
        }
    }
}
