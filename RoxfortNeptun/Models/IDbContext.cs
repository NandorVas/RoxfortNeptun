
namespace RoxfortNeptun.Models
{
    public interface IDbContext
    {
        Task<int> CreateAsync<T>(T item) where T : IUser, new();
        Task<int> DeleteAsync<T>(T item) where T : IUser, new();
        Task<IEnumerable<T>> GetAllAsync<T>() where T : IUser, new();
        Task<T> GetByIdASync<T>(string neptunKod) where T : IUser, new();
        Task<bool> InitializeAsync();
        Task<int> InsertDemoDataAsync();
        Task<int> UpdateAsync<T>(T item) where T : IUser, new();
        Task<IEnumerable<ClassTask>> GetClassTasksAsync(int studentId);
        Task<int> UpdateClassTaskAsync(ClassTask task);
        Task<int> InsertClassTaskAsync(ClassTask task, int? enrollStudentId = null);
        Task<int> InsertStudentClassTaskAsync(StudentClassTask enrollment);
    }
}