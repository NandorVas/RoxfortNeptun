using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    class Teachers
    {
        [Key]
        public int Id { get; set; }
        [StringLength(6)]
        public string Neptunkod { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Houses WhichHousesHead { get; set; }

        public Teachers(string name)
        {
            this.Name = name;
            this.WhichHousesHead = Houses.None;
            this.Password = string.Empty; //ha empty, akkor még nem volt beállítva jelszó, még nm jelentkeztek be
        }

        public Teachers(string name, Houses houses)
            :this(name)
        {
            this.WhichHousesHead = houses;
        }

        public Teachers()
        {
            
        }
    }
}
