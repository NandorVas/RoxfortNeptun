using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    public class RoxfortDb
    {
        SQLite.SQLiteOpenFlags Flags =
       SQLite.SQLiteOpenFlags.ReadWrite | 
       SQLite.SQLiteOpenFlags.Create;

        string databasePath =
        Path.Combine(FileSystem.Current.AppDataDirectory, "Roxfort.db3");

        SQLiteAsyncConnection db;

        public RoxfortDb()
        {
            db = new SQLiteAsyncConnection(databasePath, Flags);
        }

        public async Task<bool> InitializeDatabaseAsync()
        {
            try
            {
                // Táblák létrehozása
                await db.CreateTableAsync<Students>();
                await db.CreateTableAsync<Teachers>();
                await db.CreateTableAsync<ClassTask>();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization failed: {ex.Message}");
                return false;
            }
        }
    }
}
