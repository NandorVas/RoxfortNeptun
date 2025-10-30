
namespace RoxfortNeptun.Models
{
    public interface IDbContext
    {
        Task<int> CreateAsync<T>(T item) where T : class, new();
        Task<int> DeleteAsync<T>(T item) where T : class, new();
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new();
        Task<T> GetByIdASync<T>(object id) where T : class, new();
        Task<bool> InitializeAsync();
        Task<int> InsertDemoDataAsync();
        Task<int> UpdateAsync<T>(T item) where T : class, new();
    }
}