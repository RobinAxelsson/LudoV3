using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.DbModel
{
    public static class DatabaseManagement
    {
        private static string ConnectionString { get; set; }
        public static void ReadConnectionString(string filepath) => ConnectionString = File.ReadAllText(filepath);

    }
}
