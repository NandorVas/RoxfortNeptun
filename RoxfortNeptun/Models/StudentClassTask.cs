using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RoxfortNeptun.Models
{
    [Table("StudentClassTask")]
    public class StudentClassTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int StudentId { get; set; }  // Hallgató ID-ja

        [Indexed]
        public int ClassTaskId { get; set; } // Óra/Feladat ID-ja

        public DateTime EnrollmentDate { get; set; } = DateTime.Now; // Felvétel dátuma

        public bool IsActive { get; set; } = true; // Aktív-e még

        // Navigation properties should be ignored by SQLite-net to avoid mapping complex types as columns
        [Ignore]
        public Students Student { get; set; }

        [Ignore]
        public ClassTask ClassTask { get; set; }

        // Konstruktorok (Students osztályhoz hasonlóan)

        // Alap: studentId + classTaskId, alapértelmezett dátummal és isActive=true
        public StudentClassTask(int studentId, int classTaskId)
        {
            StudentId = studentId;
            ClassTaskId = classTaskId;
            EnrollmentDate = DateTime.Now;
            IsActive = true;
        }

        // Student és ClassTask objektumok megadása (null-ellenőrzéssel)
        public StudentClassTask(Students student, ClassTask classTask)
            : this(student?.Id ?? throw new ArgumentNullException(nameof(student)),
                   classTask?.Id ?? throw new ArgumentNullException(nameof(classTask)))
        {
            Student = student;
            ClassTask = classTask;
        }

        // Részletes: megadható felvétel dátum és státusz is
        public StudentClassTask(int studentId, int classTaskId, DateTime enrollmentDate, bool isActive = true)
            : this(studentId, classTaskId)
        {
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
        }

        // Parameter nélküli konstruktor (szükséges ORM / deszerializáció miatt)
        public StudentClassTask()
        {
        }
    }
}
