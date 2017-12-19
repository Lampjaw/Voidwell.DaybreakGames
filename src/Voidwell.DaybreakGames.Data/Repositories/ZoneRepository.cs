﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voidwell.DaybreakGames.Data.Models.Planetside;

namespace Voidwell.DaybreakGames.Data.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly IDbContextHelper _dbContextHelper;

        public ZoneRepository(IDbContextHelper dbContextHelper)
        {
            _dbContextHelper = dbContextHelper;
        }

        public async Task<IEnumerable<DbZone>> GetAllZonesAsync()
        {
            using (var dbcontext = _dbContextHelper.Create())
            {
                return await dbcontext.Zones.ToListAsync();
            }
        }

        public async Task UpsertRangeAsync(IEnumerable<DbZone> entities)
        {
            using (var dbContext = _dbContextHelper.Create())
            {
                foreach (var entity in entities)
                {
                    var storeEntity = await dbContext.Zones.AsNoTracking().SingleOrDefaultAsync(a => a.Id == entity.Id);
                    if (storeEntity == null)
                    {
                        dbContext.Zones.Add(entity);
                    }
                    else
                    {
                        storeEntity = entity;
                        dbContext.Zones.Update(storeEntity);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}