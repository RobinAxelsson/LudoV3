using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoAPI.Enum;

namespace LudoAPI.Models
{
    public class Pawn
    {
        public int PlayerID { get; set; }
        public TeamColor Color { get; set; }
        public int? Position { get; set; }
    }
}
