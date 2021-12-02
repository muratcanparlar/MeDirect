using MeDirect.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeDirect.Core.Services
{
    public interface IGameBoardService
    {
        List<BoardRow> CreateGameBoard(int sizeX, int sizeY);
    }
}
