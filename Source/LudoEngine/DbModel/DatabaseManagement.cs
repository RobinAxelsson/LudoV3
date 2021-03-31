using System.IO;

namespace LudoEngine.DbModel
{
    public static class DatabaseManagement
    {
        public static string ConnectionString { get; private set; }

        public static void ReadConnectionString(string filepath)
        {
            ConnectionString = File.ReadAllText(filepath);
        }
    }

}