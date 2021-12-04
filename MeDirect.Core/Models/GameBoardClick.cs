using System;
using System.Collections.Generic;
using System.Text;

namespace MeDirect.Core.Models
{
    public class GameBoardClick
    {
        public List<BoardRow> BoardRows { get; set; }

        public int ClickX { get; set; }

        public int ClickY { get; set; }

        public bool IsBoardComplated { get; set; } = false;
    }
}
