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

        /// <summary>
        /// Add new game settings to database
        /// </summary>
        /// <param name="gameSetting"></param>
        /// <returns></returns>
        public async  Task<GameSetting> CreateGameSetting(GameSetting gameSetting)
        {
            await _dbContext.AddAsync(gameSetting);
            await _dbContext.SaveChangesAsync();
            return gameSetting;
        }
        /// <summary>
        /// Delete Game Setting from database
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task DeleteGameSetting(Guid settingId)
        {
            var entity = _dbContext.Set<GameSetting>().Where(x => x.Id == settingId).FirstOrDefault();
            _dbContext.Set<GameSetting>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Listing the data of game setting from database
        /// </summary>
        /// <returns></returns>
        public async Task<GameSetting> GetGameSettings()
        {
            var gameSetting= await  _dbContext.Set<GameSetting>().FirstOrDefaultAsync();
            return gameSetting;
        }
        /// <summary>
        /// Update game setting data, which is in the database.
        /// </summary>
        /// <param name="gameSetting"></param>
        /// <returns></returns>
        public async Task UpdateGameSetting(GameSetting gameSetting)
        {
            
            var settingToBeUpdated = await _dbContext.Set<GameSetting>().SingleOrDefaultAsync(x => x.Id == gameSetting.Id);
            settingToBeUpdated.Size = gameSetting.Size;
            _dbContext.Set<GameSetting>().Update(settingToBeUpdated);
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// This Methos calls the Api for Take Board Size. Default Size=5
        /// </summary>
        /// <returns></returns>
        async Task<GameSetting> TakeGameBoardSize()
        {
            var result = await _dbContext.Set<GameSetting>().FirstOrDefaultAsync();

            if (result == null)
            {
                result.Size = 5;//default size;
            }

            return result;
        }

        /// <summary>
        /// This method is connecting to the database for take which ligths are oppening when the creating gameboard.
        /// </summary>
        /// <param name="gameSettingId"></param>
        /// <returns></returns>
        async Task<List<GameLight>> TakeOpenLights(Guid gameSettingId)
        {

            var result = await _dbContext.Set<GameLight>().Where(x => x.GameSettingId == gameSettingId).ToListAsync();
            return result;
        }

        /// <summary>
        /// This method is connected to the database for learning which lights are open when the creating game board.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This Method works after click the board by user. Firstly, clicked cell and  the four adjacent lights are toggled.
        /// Nextly, is checked the board if all switched turn off.
        /// </summary>
        /// <param name="gameBoardClick"></param>
        /// <returns></returns>
        public GameBoardClick ClickBoard(GameBoardClick  gameBoardClick)
        {
            var result=SwitchLights(gameBoardClick.BoardRows,gameBoardClick.ClickY, gameBoardClick.ClickX);
            gameBoardClick.BoardRows = result;
            gameBoardClick.IsBoardComplated = IsBoardComplated(gameBoardClick.BoardRows);
            return gameBoardClick;
        }

        /// <summary>
        /// This method is check the board if all lights are turned off.
        /// </summary>
        /// <param name="BoardRows"><list type="BoardRow"></list></param>
        /// <returns></returns>
        bool IsBoardComplated(List<BoardRow> BoardRows)
        {
            bool isComplated = true;
            for (int y = 0; y < BoardRows.Count; y++)
            {
                for (int x = 0; x < BoardRows.Count; x++)
                {
                    if (BoardRows[y].Columns[x].col == true)
                    {
                        isComplated = false;
                    }
                }
            }
            return isComplated;
        }

        /// <summary>
        /// This method is switched the lights, which are adjacent of clicked light, status. 
        /// if adjacent light is on, it will turned it off.  or if adjacent light is off it will turned it on.
        /// </summary>
        /// <param name="BoardRows">The Game Board of Rows</param>
        /// <param name="ClickY">Clicked Ligth of Y coordinate</param>
        /// <param name="ClickX">Clicked Ligth of X coordinate</param>
        /// <returns></returns>
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

        public async Task<List<GameLight>> GameTurnOnLigths(Guid gameSettingId)
        {
           return await  _dbContext.GameLights.Where(x => x.GameSettingId == gameSettingId).ToListAsync();
        }

        public async Task<GameLight> AddTurnOnLigth(GameLight gameLight)
        {
            gameLight.Id = Guid.NewGuid();
            await _dbContext.Set<GameLight>().AddAsync(gameLight);
            var result=_dbContext.SaveChangesAsync();
            return gameLight;

        }

        public async Task DeleteTurnOnLigth(Guid gameLightId)
        {
           var removedEntity = await _dbContext.Set<GameLight>().Where(x=>x.Id==gameLightId).FirstOrDefaultAsync();
            if (removedEntity != null)
            {
                 _dbContext.Set<GameLight>().Remove(removedEntity);
                 _dbContext.SaveChanges();
            }
        }
    }

        
    
}
