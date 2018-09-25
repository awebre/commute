﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace commutr.Services
{
    public interface IDataStore<T> where T : class, IIdentifiable, new()
    {
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync();
    }
}
