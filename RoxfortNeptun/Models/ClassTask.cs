using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    class ClassTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfStuds { get; set; }
        public string Place { get; set; }
        public int Teacher { get; set; }
        public bool IsClass { get; set; } = true;
        public TimeSpan ?StartTime { get; set; } // need to parse
        public TimeSpan ?EndTime { get; set; } // need to parse
        public TimeSpan ?Duration => EndTime - StartTime;

        public ClassTask(string name, int numofstuds, string place, int teacher, bool ?isCLass, TimeSpan ?startTime, TimeSpan ?endTime)
        {
            this.Name = name;
            this.NumOfStuds = numofstuds;
            this.Place = place;
            this.Teacher = teacher;
            //this.StartTime = startTime;
            //this.EndTime = endTime;

            if (isCLass == false)
            {
                this.IsClass = false;
            }

            if (startTime == null && endTime != null)
            {
                startTime = endTime - TimeSpan.FromHours(1);
            }
            if (endTime == null && startTime != null)
            {
                endTime = startTime + TimeSpan.FromHours(1);
            }
        }
    }
}
