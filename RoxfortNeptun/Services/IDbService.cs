using RoxfortNeptun.Models;

namespace RoxfortNeptun.Services
{
    public interface IDbService
    {
        Task<int> CreateAsync<T>(T item) where T : IUser, new();
        Task<int> DeleteAsync<T>(T item) where T : IUser, new();
        Task<IEnumerable<T>> GetAllAsync<T>() where T : IUser, new();
        Task<T> GetByIdAsync<T>(string neptunKod) where T : IUser, new();
        Task<bool> InitializeAsync();
        Task<int> InsertDemoDataAsync();
        Task<int> UpdateAsync<T>(T item) where T : IUser, new();
        Task<int> GetTableCountsAsync();
    }
}