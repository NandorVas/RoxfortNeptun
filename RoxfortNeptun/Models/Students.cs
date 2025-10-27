using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    public enum Houses
    {
        None = 0,
        Gryffindor,
        Hufflepuff,
        Ravenclaw,
        Slytherin
    }

    public class Students
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(6)]
        public string NeptunKod { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Houses House { get; set; }

        public Students(string name, string neptunKod)
        {
            this.Name = name;
            this.NeptunKod = neptunKod;
            this.DateOfBirth = DateTime.MinValue;
            this.House = Houses.None;
            this.Password = string.Empty; //ha empty, akkor még nem volt beállítva jelszó, még nem jelentkeztek be
        }

        public Students(string name, string neptunKod, DateTime birthdate)
            :this(name, neptunKod)
        {
            this.DateOfBirth = birthdate;
        }

        public Students(string name, string neptunKod, Houses house)
            :this(name, neptunKod)
        {
            this.House = house;
        }

        public Students(string name, string neptunKod, DateTime birthdate, Houses house)
            : this(name, neptunKod, birthdate)
        {
            this.House = house;
        }

        public Students()
        {
            
        }
    }
}
