using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoxfortNeptun.Models;

namespace RoxfortNeptun.Services
{
    public class DbService : IDbService
    {
        private readonly IDbContext _dbContext;

        public DbService(IDbContext context)
        {
            this._dbContext = context;
        }

        public async Task<bool> InitializeAsync()
        {
            return await _dbContext.InitializeAsync();
        }

        public async Task<int> InsertDemoDataAsync()
        {
            return await _dbContext.InsertDemoDataAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new()
        {
            return await _dbContext.GetAllAsync<T>();
        }

        public async Task<T> GetByIdAsync<T>(string id) where T : class, new()
        {
            return await _dbContext.GetByIdASync<T>(id);
        }

        public async Task<int> CreateAsync<T>(T item) where T : class, new()
        {
            return await _dbContext.CreateAsync<T>(item);
        }

        public async Task<int> UpdateAsync<T>(T item) where T : class, new()
        {
            return await _dbContext.UpdateAsync<T>(item);
        }

        public async Task<int> DeleteAsync<T>(T item) where T : class, new()
        {
            return await _dbContext.DeleteAsync<T>(item);
        }
    }
}
