using MeDirect.Core.Models;
using MeDirect.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeDirect.Data.Repositories
{
    public class GameSettingRepository:Repository<GameSetting>,IGameSettingRepository
    {
        public GameSettingRepository(MeDirectDbContext context):base(context)
        {

        }
    }
}
