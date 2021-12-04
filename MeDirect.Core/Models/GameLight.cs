using System;
using System.Collections.Generic;
using System.Text;

namespace MeDirect.Core.Models
{
    public class GameLight
    {
        public Guid Id { get; set; }
        public Guid GameSettingId { get; set; }
        public int LightOpenX { get; set; }
        public int LightOpenY { get; set; }
    }
}
