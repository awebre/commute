using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace commute.Services
{
    public class SqliteDataStore<T> : IDataStore<T> where T : class, IIdentifiable, new()
    {
        private readonly SQLiteAsyncConnection connection;
        public SqliteDataStore()
        {
            connection = DataContext.GetConnection();
        }

        public async Task<int> AddItemAsync(T item)
        {
            return await connection.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            var item = default(T);
            item.Id = id;
            return await connection.DeleteAsync(item);
        }

        public async Task<T> GetItemAsync(int id)
        {
            return await connection.Table<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            return await connection.QueryAsync<T>($"SELECT * FROM {nameof(T)}");
        }

        public async Task<int> UpdateItemAsync(T item)
        {
            return await connection.UpdateAsync(item);
        }
    }
}
