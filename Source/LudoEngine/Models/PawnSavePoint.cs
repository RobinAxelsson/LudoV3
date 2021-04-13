using LudoEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.Models
{
    public class PawnSavePoint
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public TeamColor Color { get; set; }
        public int PlayerType { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}
