using System;
using System.IO;
using SQLite;

namespace commutr.Services
{
    public static class DataContext
    {
        private static SQLiteAsyncConnection connection;
        public static SQLiteAsyncConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "commute.db"));
            }
            return connection;
        }
    }
}
