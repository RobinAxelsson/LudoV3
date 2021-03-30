using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoEngine.Models
{
    public class GameResult //many-to-many
    {
        public int Id { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<int> Score { get; set; }
    }
}
