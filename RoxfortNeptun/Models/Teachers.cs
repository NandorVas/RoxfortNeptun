using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RoxfortNeptun.Models
{
    [Table("Teachers")]
    class Teachers
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [StringLength(6), Unique, NotNull]
        public string Neptunkod { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Houses WhichHousesHead { get; set; }

        public Teachers()
        {
            
        }

        public Teachers(string neptun)
        {
            this.Neptunkod = neptun;
        }


        public Teachers(string name, string neptun)
            :this(neptun)
        {
            this.Name = name;
            this.WhichHousesHead = Houses.None;
            this.Password = string.Empty; //ha empty, akkor még nem volt beállítva jelszó, még nm jelentkeztek be
        }
        public Teachers(string name, string neptun, Houses houses)
            :this(name, neptun)
        {
            this.WhichHousesHead = houses;
        }
    }
}
