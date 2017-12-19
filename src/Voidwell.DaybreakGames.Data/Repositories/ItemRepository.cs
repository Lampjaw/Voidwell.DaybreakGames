﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voidwell.DaybreakGames.Data.Models.Planetside;

namespace Voidwell.DaybreakGames.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDbContextHelper _dbContextHelper;

        public ItemRepository(IDbContextHelper dbContextHelper)
        {
            _dbContextHelper = dbContextHelper;
        }

        public async Task<IEnumerable<DbItem>> FindItemsByIdsAsync(IEnumerable<string> itemIds)
        {
            using (var dbContext = _dbContextHelper.Create())
            {
                return await dbContext.Items.Where(i => itemIds.Contains(i.Id))
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<DbItem>> FindItemsByNameAsync(string name, int limit)
        {
            using (var dbContext = _dbContextHelper.Create())
            {
                return await dbContext.Items.Where(i => i.Name.ToLower().Contains(name.ToLower()))
                    .Take(limit)
                    .ToListAsync();
            }
        }

        public async Task UpsertRangeAsync(IEnumerable<DbItem> entities)
        {
            using (var dbContext = _dbContextHelper.Create())
            {
                var storeEntities = await dbContext.Items.Where(a => entities.Any(e => e.Id == a.Id)).AsNoTracking().ToListAsync();

                foreach (var entity in entities)
                {
                    var storeEntity = storeEntities.FirstOrDefault(a => a.Id == entity.Id);
                    if (storeEntity == null)
                    {
                        dbContext.Items.Add(entity);
                    }
                    else
                    {
                        storeEntity = entity;
                        dbContext.Items.Update(storeEntity);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpsertRangeAsync(IEnumerable<DbItemCategory> entities)
        {
            using (var dbContext = _dbContextHelper.Create())
            {
                foreach (var entity in entities)
                {
                    var storeEntity = await dbContext.ItemCategories.AsNoTracking().SingleOrDefaultAsync(a => a.Id == entity.Id);
                    if (storeEntity == null)
                    {
                        dbContext.ItemCategories.Add(entity);
                    }
                    else
                    {
                        storeEntity = entity;
                        dbContext.ItemCategories.Update(storeEntity);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}