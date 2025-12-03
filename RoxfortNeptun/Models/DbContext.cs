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
                var classTaskCount = await _connection.Table<ClassTask>().CountAsync();
                var enrollmentCount = await _connection.Table<StudentClassTask>().CountAsync();
                var teacherCount = await _connection.Table<Teachers>().CountAsync();

                // If any critical table is empty, run seeding that only adds missing rows
                if (studentCount == 0 || classTaskCount == 0 || enrollmentCount == 0 || teacherCount == 0)
                {
                    var result = await InsertDemoDataAsync();
                    if (result < 0)
                    {
                        System.Diagnostics.Debug.WriteLine("InsertDemoDataAsync failed.");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeAsync error: {ex.Message}");
                return false;
            }
        }

        public async Task<int> InsertDemoDataAsync()
        {
            try
            {
                // Insert teachers only if table empty
                var teacherCount = await _connection.Table<Teachers>().CountAsync();
                DemoTeachers(teacherCount);

                // Insert students only if table empty
                var studentCount = await _connection.Table<Students>().CountAsync();
                DemoStudents(studentCount);

                // Insert class tasks only if table empty
                var classTaskCount = await _connection.Table<ClassTask>().CountAsync();
                DemoClassTasks(classTaskCount);

                // Insert enrollments only if table empty. Use persisted rows to build relationships.
                var enrollmentCount = await _connection.Table<StudentClassTask>().CountAsync();
                DemoEnrollments(enrollmentCount);

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

            var studClassTasks = await _connection.Table<StudentClassTask>().ToListAsync();
            var classTasks = await _connection.Table<ClassTask>().ToListAsync();
            var studs = await _connection.Table<Students>().ToListAsync();
            var teachers = await _connection.Table<Teachers>().ToListAsync();

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

        public async Task<int> InsertClassTaskAsync(ClassTask task, int? enrollStudentId = null)
        {
            return await _connection.InsertAsync(task);
        }

        public async Task<int> UpdateClassTaskAsync(ClassTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (task == null) throw new ArgumentNullException(nameof(task));
            return await _connection.UpdateAsync(task);
        }
        public async Task<int> InsertStudentClassTaskAsync(StudentClassTask enrollment)
        {
            return await _connection.InsertAsync(enrollment);
        }

        private async void DemoTeachers(int teacherCount)
        {
            if (teacherCount == 0)
            {
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
            }
        }

        private async void DemoStudents(int studentCount)
        {
            if (studentCount == 0)
            {
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
            }
        }

        private async void DemoClassTasks(int classTaskCount)
        {
            if (classTaskCount == 0)
            {
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
            }
        }

        private async void DemoEnrollments(int enrollmentCount)
        {
            if (enrollmentCount == 0)
            {
                var studs = await _connection.Table<Students>().ToListAsync();
                var tasks = await _connection.Table<ClassTask>().ToListAsync();

                Students ByNeptun(string neptun) => studs.First(s => s.NeptunKod == neptun);
                ClassTask ByName(string name) => tasks.First(t => t.Name == name);

                var demoEnrollments = new List<StudentClassTask>
                    {
                        // Harry Potter
                        new StudentClassTask(ByNeptun("HPOTTR").Id, ByName("Bájitaltan").Id),
                        new StudentClassTask(ByNeptun("HPOTTR").Id, ByName("Átváltoztatástan").Id),
                        new StudentClassTask(ByNeptun("HPOTTR").Id, ByName("Gyógynövénytan").Id),

                        // Hermione
                        new StudentClassTask(ByNeptun("HGRNGR").Id, ByName("Bájitaltan").Id),
                        new StudentClassTask(ByNeptun("HGRNGR").Id, ByName("Átváltoztatástan").Id),
                        new StudentClassTask(ByNeptun("HGRNGR").Id, ByName("Varázslattan").Id),
                        new StudentClassTask(ByNeptun("HGRNGR").Id, ByName("Gyógynövénytan").Id),

                        // Draco
                        new StudentClassTask(ByNeptun("DMALFY").Id, ByName("Bájitaltan").Id),
                        new StudentClassTask(ByNeptun("DMALFY").Id, ByName("Varázslattan").Id)
                    };

                foreach (var enrollment in demoEnrollments)
                {
                    await _connection.InsertAsync(enrollment);
                }
            }
        }
    }
}
