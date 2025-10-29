﻿using CommunityToolkit.Mvvm.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    public class DbContext : IDbContext
    {
        private SQLiteAsyncConnection _connection;

        public DbContext()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Roxfort.db3");
            var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            _connection = new SQLiteAsyncConnection(databasePath, flags);
        }

        public async Task<bool> InitializeAsync()
        {
            try
            {
                await _connection.CreateTableAsync<Students>();
                await _connection.CreateTableAsync<Teachers>();
                await _connection.CreateTableAsync<ClassTask>();

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
                    new Teachers("Perselus Piton") { Neptunkod = "PPITON", WhichHousesHead = Houses.Slytherin },
                    new Teachers("Albus Dumbledore") { Neptunkod = "ADUMBL", WhichHousesHead = Houses.None },
                    new Teachers("Minerva McGalagony") { Neptunkod = "MMCGAL", WhichHousesHead = Houses.Gryffindor },
                    new Teachers("Filius Fricsik") { Neptunkod = "FFRICSI", WhichHousesHead = Houses.Ravenclaw },
                    new Teachers("Pomona Bimba") { Neptunkod = "PBIMBA", WhichHousesHead = Houses.Hufflepuff }
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

                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Demo data insertion failed: {ex.Message}");
                return -1;
            }
        }

    }
}
