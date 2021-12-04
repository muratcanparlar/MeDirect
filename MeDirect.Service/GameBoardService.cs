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
        private readonly MeDirectDbContext _dbContext;
        public GameBoardService(MeDirectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task<GameSetting> CreateGameSetting(GameSetting gameSetting)
        {
            await _dbContext.AddAsync(gameSetting);
            await _dbContext.SaveChangesAsync();
            return gameSetting;
        }

        public async Task DeleteGameSetting(Guid settingId)
        {
            var entity = _dbContext.Set<GameSetting>().Where(x => x.Id == settingId).FirstOrDefault();
            _dbContext.Set<GameSetting>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GameSetting> GetGameSettings()
        {
            var gameSetting= await  _dbContext.Set<GameSetting>().FirstOrDefaultAsync();
            return gameSetting;
        }

        public async Task UpdateGameSetting(GameSetting gameSetting)
        {
            
            var settingToBeUpdated = await _dbContext.Set<GameSetting>().SingleOrDefaultAsync(x => x.Id == gameSetting.Id);
            settingToBeUpdated.Size = gameSetting.Size;
            _dbContext.Set<GameSetting>().Update(settingToBeUpdated);
            await _dbContext.SaveChangesAsync();
        }



        async Task<GameSetting> TakeGameBoardSize()
        {
            var result = await _dbContext.Set<GameSetting>().FirstOrDefaultAsync();

            if (result == null)
            {
                result.Size = 5;//default size;
            }

            return result;
        }

        async Task<List<GameLight>> TakeOpenLights(Guid gameSettingId)
        {

            var result = await _dbContext.Set<GameLight>().Where(x => x.GameSettingId == gameSettingId).ToListAsync();
            return result;
        }


        public async Task<List<BoardRow>> DrawGameBoard()
        {
            var gamesetting = await TakeGameBoardSize();
            var openLight = await TakeOpenLights(gamesetting.Id);
            var hasOpenLight = false;
            if (openLight != null)
            {
                hasOpenLight = true;
            }
            List<BoardRow> boardRows = new List<BoardRow>();
            for (int j = 0; j < gamesetting.Size; j++)
            {
                int lightIndex = new Random().Next(4);
                List<BoardCol> boardCol = new List<BoardCol>();
                for (int i = 0; i < gamesetting.Size; i++)
                {
                    bool lightOn = false;
                    if (hasOpenLight)
                    {
                        int indx = openLight.FindIndex(x => x.LightOpenX == i && x.LightOpenY == j);
                        if (indx > -1)
                        {
                            lightOn = true;
                            openLight.RemoveAt(indx);
                        }
                    }
                    boardCol.Add(new BoardCol { col = lightOn });
                }
                boardRows.Add(new BoardRow { Columns = boardCol });
            }

            return boardRows;
        }

        public GameBoardClick ClickBoard(GameBoardClick  gameBoardClick)
        {
            var result=SwitchLights(gameBoardClick.BoardRows,gameBoardClick.ClickY, gameBoardClick.ClickX);
            gameBoardClick.BoardRows = result;
            gameBoardClick.IsBoardComplated = IsBoardComplated(gameBoardClick.BoardRows);
            return gameBoardClick;
        }

        bool IsBoardComplated(List<BoardRow> BoardRows)
        {
            bool isComplated = true;
            for (int y = 0; y < BoardRows.Count; y++)
            {
                for (int x = 0; x < BoardRows.Count; x++)
                {
                    if (BoardRows[y].Columns[x].col == false)
                    {
                        isComplated = false;
                    }
                }
            }
            return isComplated;
        }


        List<BoardRow> SwitchLights(List<BoardRow> BoardRows ,int ClickY, int ClickX)
        {
            BoardRows[ClickY].Columns[ClickX].col = !BoardRows[ClickY].Columns[ClickX].col;

            if (ClickY > 0)
            {
                BoardRows[ClickY - 1].Columns[ClickX].col = !BoardRows[ClickY - 1].Columns[ClickX].col;
            }
            if (ClickY < BoardRows.Count - 1)
            {
                BoardRows[ClickY + 1].Columns[ClickX].col = !BoardRows[ClickY + 1].Columns[ClickX].col;
            }
            if (ClickX > 0)
            {
                BoardRows[ClickY].Columns[ClickX - 1].col = !BoardRows[ClickY].Columns[ClickX - 1].col;
            }
            if (ClickX < BoardRows.Count - 1)
            {
                BoardRows[ClickY].Columns[ClickX + 1].col = !BoardRows[ClickY].Columns[ClickX + 1].col;
            }

            return BoardRows;
        }


    }

        
    
}
