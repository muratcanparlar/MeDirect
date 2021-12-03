using MeDirect.Core;
using MeDirect.Core.Models;
using MeDirect.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeDirect.Service
{
    public class GameBoardService : IGameBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameBoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<GameSetting> CreateGameSetting(GameSetting gameSetting)
        {
            await _unitOfWork.GameSettings.AddAsync(gameSetting);
            var res = await _unitOfWork.CommitAsync();//Todo:Check Commit Result. 
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
            var app = await _unitOfWork.GameSettings.GetByIdAsync(settingId);
            _unitOfWork.GameSettings.Remove(app);

            await _unitOfWork.CommitAsync();
        }

        public async Task<GameSetting> GetGameSettings()
        {
            return await _unitOfWork.GameSettings.SingleOrDefaultAsync(x => x.Size > 0);
            //return await _unitOfWork.GameSettings.GetAllAsync();
        }

        public async Task UpdateGameSetting(GameSetting gameSetting)
        {
            GameSetting settingToBeUpdated = await _unitOfWork.GameSettings.GetByIdAsync(gameSetting.Id);
            settingToBeUpdated.Size = gameSetting.Size;
            await _unitOfWork.CommitAsync();
        }

        async Task<int> TakeGameBoardSize()
        {
            var result = await _unitOfWork.GameSettings.SingleOrDefaultAsync(x => x.Size > 0);

            int size = 5;//default size;

            if (result != null)
            {
                size = result.Size;
            }
            return size;
        }
    }
}
