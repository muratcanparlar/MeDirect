using MeDirect.Core.Models;
using MeDirect.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeDirect.Service
{
    public class GameBoardService : IGameBoardService
    {
        public List<BoardRow> CreateGameBoard(int sizeX,int sizeY)
        {

            List<BoardRow> boardRows = new List<BoardRow>();
            for (int j = 0; j < sizeY; j++)
            {
                int lightIndex = new Random().Next(4);
                List<BoardCol> boardCol = new List<BoardCol>();
                for (int i = 0; i < sizeX; i++)
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

    }
}
