using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RoxfortNeptun.Models
{
    [Table("StudnetTaskCass")]
    public class StudentClassTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, NotNull]
        public int StudentId { get; set; }  // Hallgató ID-ja

        [Indexed, NotNull]
        public int ClassTaskId { get; set; } // Óra/Feladat ID-ja

        public DateTime EnrollmentDate { get; set; } = DateTime.Now; // Felvétel dátuma

        public bool IsActive { get; set; } = true; // Aktív-e még

        // Navigation properties (opcionális)
        [Ignore]
        public Students Student { get; set; }

        [Ignore]
        public ClassTask ClassTask { get; set; }
    }
}
