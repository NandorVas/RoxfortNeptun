
namespace RoxfortNeptun.Services
{
    public interface IDbService
    {
        Task<int> CreateAsync<T>(T item) where T : class, new();
        Task<int> DeleteAsync<T>(T item) where T : class, new();
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new();
        Task<T> GetByIdAsync<T>(string id) where T : class, new();
        Task<bool> InitializeAsync();
        Task<int> InsertDemoDataAsync();
        Task<int> UpdateAsync<T>(T item) where T : class, new();
    }
}