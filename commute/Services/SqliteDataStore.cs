using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace commutr.Services
{
    public class SqliteDataStore<T> : IDataStore<T> where T : class, IIdentifiable, new()
    {
        private readonly SQLiteAsyncConnection connection;
        public SqliteDataStore()
        {
            connection = DataContext.GetConnection();
            connection.CreateTableAsync<T>();
        }

        public async Task<int> AddItemAsync(T item)
        {
            return await connection.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            var item = new T {Id = id};
            return await connection.DeleteAsync(item);
        }

        public async Task<T> GetItemAsync(int id)
        {
            return await connection.Table<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            var table = typeof(T).Name;
            var result = await connection.QueryAsync<T>(@"SELECT * FROM " + table);
            return result;
        }

        public async Task<int> UpdateItemAsync(T item)
        {
            return await connection.UpdateAsync(item);
        }
    }
}
