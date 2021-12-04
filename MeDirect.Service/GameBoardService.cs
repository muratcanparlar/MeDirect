using MeDirect.Core;
using MeDirect.Core.Models;
using MeDirect.Core.Services;
using MeDirect.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeDirect.Service
{
    public class GameBoardService : IGameBoardService
    {
        //private readonly IUnitOfWork _unitOfWork;
        //public GameBoardService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private readonly MeDirectDbContext _dbContext;
        public GameBoardService(MeDirectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task<GameSetting> CreateGameSetting(GameSetting gameSetting)
        {
            //await _unitOfWork.GameSettings.AddAsync(gameSetting);
            //var res = await _unitOfWork.CommitAsync();//Todo:Check Commit Result. 
            //return gameSetting;
            await _dbContext.AddAsync(gameSetting);
            await _dbContext.SaveChangesAsync();
            return gameSetting;
        }

        public async Task<List<BoardRow>> DrawGameBoard()
        {
            int size = await TakeGameBoardSize();

            List<BoardRow> boardRows = new List<BoardRow>();
            for (int j = 0; j < size; j++)
            {
                int lightIndex = new Random().Next(4);
                List<BoardCol> boardCol = new List<BoardCol>();
                for (int i = 0; i < size; i++)
                {
                    bool lightOn = false;
                    if (lightIndex == i)
                    {
                        lightOn = true;
                    }

                    boardCol.Add(new BoardCol { col = lightOn });
                }
                boardRows.Add(new BoardRow { Columns = boardCol });
            }

            return boardRows;
        }

        public async Task DeleteGameSetting(Guid settingId)
        {
            //var app = await _unitOfWork.GameSettings.GetByIdAsync(settingId);
            //_unitOfWork.GameSettings.Remove(app);

            //await _unitOfWork.CommitAsync();
            var entity = _dbContext.Set<GameSetting>().Where(x => x.Id == settingId).FirstOrDefault();
            _dbContext.Set<GameSetting>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GameSetting> GetGameSettings()
        {
            return await  _dbContext.Set<GameSetting>().FirstOrDefaultAsync();
            //return await _unitOfWork.GameSettings.GetAllAsync();
        }

        public async Task UpdateGameSetting(GameSetting gameSetting)
        {
            //GameSetting settingToBeUpdated = await _unitOfWork.GameSettings.GetByIdAsync(gameSetting.Id);
            //settingToBeUpdated.Size = gameSetting.Size;
            //await _unitOfWork.CommitAsync();

            var settingToBeUpdated = await _dbContext.Set<GameSetting>().SingleOrDefaultAsync(x => x.Id == gameSetting.Id);

            settingToBeUpdated.Size = gameSetting.Size;

            _dbContext.Set<GameSetting>().Update(settingToBeUpdated);
            await _dbContext.SaveChangesAsync();
        }

        async Task<int> TakeGameBoardSize()
        {
            //var result = await _unitOfWork.GameSettings.SingleOrDefaultAsync(x => x.Size > 0);

            var result = await _dbContext.Set<GameSetting>().FirstOrDefaultAsync();

            int size = 5;//default size;

            if (result != null)
            {
                size = result.Size;
            }
            return size;
        }

       
    }

        
    
}
