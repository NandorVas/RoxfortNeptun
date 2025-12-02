using CommunityToolkit.Mvvm.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    public class DbContext: IDbContext
    {
        private SQLiteAsyncConnection _connection;

        public DbContext()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Roxfort.db3");
            var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            _connection = new SQLiteAsyncConnection(databasePath, flags);
        }

        public DbContext(SQLiteAsyncConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<bool> InitializeAsync()
        {
            try
            {
                await _connection.CreateTableAsync<Students>();
                await _connection.CreateTableAsync<Teachers>();
                await _connection.CreateTableAsync<ClassTask>();
                await _connection.CreateTableAsync<StudentClassTask>();

                var studentCount = await _connection.Table<Students>().CountAsync();
                if (studentCount == 0)
                {
                    await InsertDemoDataAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send("Error", ex.Message);
                return false;
            }
        }

        public async Task<int> InsertDemoDataAsync()
        {
            try
            {
                // Demo tanárok
                var demoTeachers = new List<Teachers>
                {
                    new Teachers("Perselus Piton") { NeptunKod = "PPITON", House = Houses.Slytherin },
                    new Teachers("Albus Dumbledore") { NeptunKod = "ADUMBL", House = Houses.None },
                    new Teachers("Minerva McGalagony") { NeptunKod   = "MMCGAL", House = Houses.Gryffindor },
                    new Teachers("Filius Fricsik") { NeptunKod = "FFRICSI", House = Houses.Ravenclaw },
                    new Teachers("Pomona Bimba") { NeptunKod = "PBIMBA", House = Houses.Hufflepuff }
                };

                foreach (var teacher in demoTeachers)
                {
                    await _connection.InsertAsync(teacher);
                }

                // Demo hallgatók
                var demoStudents = new List<Students>
                {
                    new Students("Harry Potter", "HPOTTR", new DateTime(1980, 7, 31), Houses.Gryffindor),
                    new Students("Hermione Granger", "HGRNGR", new DateTime(1979, 9, 19), Houses.Gryffindor),
                    new Students("Ron Weasley", "RWEASL", new DateTime(1980, 3, 1), Houses.Gryffindor),
                    new Students("Draco Malfoy", "DMALFY", new DateTime(1980, 6, 5), Houses.Slytherin),
                    new Students("Neville Longbottom", "NLONGB", new DateTime(1980, 7, 30), Houses.Gryffindor)
                    {
                        Password = "" // Első bejelentkezésre
                    },
                    new Students("Luna Lovegood", "LLOVEG", new DateTime(1981, 2, 13), Houses.Ravenclaw),
                    new Students("Cedric Diggory", "CDIGGO", new DateTime(1977, 9, 1), Houses.Hufflepuff)
                };

                foreach (var student in demoStudents)
                {
                    await _connection.InsertAsync(student);
                }

                // Demo órák/feladatok
                var demoClassTasks = new List<ClassTask>
                {
                    // Órák
                    new ClassTask("Bájitaltan", 20, "B012", 1, true, new TimeSpan(9, 0, 0), new TimeSpan(10, 30, 0)),
                    new ClassTask("Átváltoztatástan", 25, "Nagy Terem", 3, true, new TimeSpan(11, 0, 0), new TimeSpan(12, 30, 0)),
                    new ClassTask("Varázslattan", 18, "Varázsló szoba", 4, true, new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0)),
                    new ClassTask("Gyógynövénytan", 15, "Üvegház", 5, true, new TimeSpan(10, 0, 0), new TimeSpan(11, 30, 0)),
                    
                    // Feladatok (nem órák)
                    new ClassTask("Bájital házi feladat", 1, "Otthon", 1, false, null, DateTime.Now.AddDays(7).TimeOfDay),
                    new ClassTask("Átváltoztatás esszé", 1, "Könyvtár", 3, false, null, DateTime.Now.AddDays(5).TimeOfDay)
                };

                foreach (var task in demoClassTasks)
                {
                    await _connection.InsertAsync(task);
                }

                var demoEnrollments = new List<StudentClassTask>
                {
                    // Harry Potter felvételei
                    new StudentClassTask { StudentId = 1, ClassTaskId = 1 }, // Bájitaltan
                    new StudentClassTask { StudentId = 1, ClassTaskId = 2 }, // Átváltoztatástan
                    new StudentClassTask { StudentId = 1, ClassTaskId = 4 }, // Gyógynövénytan
    
                    // Hermione felvételei
                    new StudentClassTask { StudentId = 2, ClassTaskId = 1 }, // Bájitaltan
                    new StudentClassTask { StudentId = 2, ClassTaskId = 2 }, // Átváltoztatástan
                    new StudentClassTask { StudentId = 2, ClassTaskId = 3 }, // Varázslattan
                    new StudentClassTask { StudentId = 2, ClassTaskId = 4 }, // Gyógynövénytan
    
                    // Draco felvételei
                    new StudentClassTask { StudentId = 4, ClassTaskId = 1 }, // Bájitaltan
                    new StudentClassTask { StudentId = 4, ClassTaskId = 3 }, // Varázslattan
                };

                foreach (var enrollment in demoEnrollments)
                {
                    await _connection.InsertAsync(enrollment);
                }

                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Demo data insertion failed: {ex.Message}");
                return -1;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : IUser, new()
        {
            return await _connection.Table<T>().ToListAsync();
        }

        public async Task<T> GetByIdASync<T>(string neptunKod) where T : IUser, new()
        {
            return await _connection.Table<T>().Where(x => x.NeptunKod == neptunKod).FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync<T>(T item) where T : IUser, new()
        {
            return await _connection.InsertAsync(item);
        }

        public async Task<int> UpdateAsync<T>(T item) where T : IUser, new()
        {
            return await _connection.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync<T>(T item) where T : IUser, new()
        {
            return await _connection.DeleteAsync(item);
        }

        private async Task<List<int>> GetTasksForStudentAsync(int studentId)
        {
            var tasks = await _connection.Table<StudentClassTask>().Where(p => studentId == p.StudentId).ToListAsync();

            var taskIds = tasks.Select(t => t.ClassTaskId).ToList();

            return taskIds;
        }

        private async Task<List<ClassTask>> GetClassTasksByIdsAsync(int studentId)
        {
            var taskIds = await GetTasksForStudentAsync(studentId);
            var classTasks = await _connection.Table<ClassTask>()
                .Where(ct => taskIds.Contains(ct.Id))
                .ToListAsync();

            return classTasks;
        }

        public async Task<IEnumerable<ClassTask>> GetClassTasksAsync(int studentId)
        {
            return await GetClassTasksByIdsAsync(studentId); // already returns List<ClassTask>
        }

        public async Task<int> GetTableCountsAsync()
        {
            var students = await _connection.Table<Students>().CountAsync();
            var classTasks = await _connection.Table<ClassTask>().CountAsync();
            var enrollments = await _connection.Table<StudentClassTask>().CountAsync();
            return (students);
        }

        public async Task<List<StudentClassTask>> GetEnrollmentsForStudentAsync(int studentId)
        {
            return await _connection.Table<StudentClassTask>().Where(p => p.StudentId == studentId).ToListAsync();
        }

        public async Task<List<ClassTask>> GetClassTasksForStudentByJoinAsync(int studentId)
        {
            // Single SQL join to avoid LINQ translation issues and to validate relationship rows
            var sql = "SELECT ct.* FROM ClassTasks ct INNER JOIN StudentClassTask sct ON ct.Id = sct.ClassTaskId WHERE sct.StudentId = ?";
            return await _connection.QueryAsync<ClassTask>(sql, studentId);
        }
    }
}
