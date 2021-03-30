using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoEngine.Enum;

namespace LudoEngine.Models
{
    public class Pawn
    {
        public int ID { get; set; }
        public int PlayerID { get; set; }
        public TeamColor Color { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}
