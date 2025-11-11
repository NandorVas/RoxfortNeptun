    using SQLite;
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

    [Table("Students")]
    public class Students: IUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(6), Unique, NotNull]
        public string Neptunkod { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Houses Houses { get; set; }
        public UserType Type { get; set; } = UserType.Student;

        public Students(string name, string neptunKod)
        {
            this.Name = name;
            this.Neptunkod = neptunKod;
            this.DateOfBirth = DateTime.MinValue;
            this.Houses = Houses.None;
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
            this.Houses = house;
        }

        public Students(string name, string neptunKod, DateTime birthdate, Houses house)
            : this(name, neptunKod, birthdate)
        {
            this.Houses = house;
        }

        public Students()
        {
            
        }
    }
}
