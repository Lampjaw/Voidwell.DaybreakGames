﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Voidwell.DaybreakGames.Data.Models.Planetside;

namespace Voidwell.DaybreakGames.Services.Planetside
{
    public interface IProfileService : IUpdateable
    {
        Task<IEnumerable<DbProfile>> GetAllProfiles();
        Task RefreshStore();
    }
}