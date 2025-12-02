using SQLite;
using System;

namespace RoxfortNeptun.Models
{
    [Table("ClassTasks")]
    public class ClassTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfStuds { get; set; }
        public string Place { get; set; }
        public int Teacher { get; set; }
        public bool IsClass { get; set; } = true;
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public TimeSpan? Duration => EndTime - StartTime;

        // Compatibility convenience (older XAML used Room)
        public string Room => Place;

        // UI-friendly formatted strings
        public string StartTimeDisplay => StartTime.HasValue ? StartTime.Value.ToString(@"hh\:mm") : string.Empty;
        public string EndTimeDisplay => EndTime.HasValue ? EndTime.Value.ToString(@"hh\:mm") : string.Empty;

        public ClassTask(string name, int numofstuds, string place, int teacher, bool? isClass, TimeSpan? startTime, TimeSpan? endTime)
        {
            Name = name;
            NumOfStuds = numofstuds;
            Place = place;
            Teacher = teacher;
            IsClass = isClass ?? true;

            if (startTime == null && endTime != null)
            {
                startTime = endTime - TimeSpan.FromHours(1);
            }

            if (endTime == null && startTime != null)
            {
                endTime = startTime + TimeSpan.FromHours(1);
            }

            StartTime = startTime;
            EndTime = endTime;
        }

        public ClassTask()
        {
        }
    }
}
