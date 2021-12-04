using MeDirect.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeDirect.Core.Services
{
    public interface IGameBoardService
    {
        Task<GameSetting> GetGameSettings();
        Task<List<BoardRow>> DrawGameBoard();
        Task<GameSetting> CreateGameSetting(GameSetting gameSetting);
        Task UpdateGameSetting(GameSetting gameSetting);
        Task DeleteGameSetting(Guid settingId);
        GameBoardClick ClickBoard(GameBoardClick gameBoardClick);
    }
}

