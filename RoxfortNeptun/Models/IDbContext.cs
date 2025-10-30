
namespace RoxfortNeptun.Models
{
    public interface IDbContext
    {
        Task<bool> InitializeAsync();
        Task<int> InsertDemoDataAsync();
    }
}