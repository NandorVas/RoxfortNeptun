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
    public class Teachers : IUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [StringLength(6), Unique, NotNull]
        public string NeptunKod { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Houses House { get; set; }
        public UserType UserType { get { return UserType.Teacher; } }

        public Teachers()
        {

        }

        public Teachers(string neptun)
        {
            this.NeptunKod = neptun;
        }


        public Teachers(string name, string neptun)
            : this(neptun)
        {
            this.Name = name;
            this.House = Houses.None;
            this.Password = string.Empty; //ha empty, akkor még nem volt beállítva jelszó, még nm jelentkeztek be
        }
        public Teachers(string name, string neptun, Houses houses)
            : this(name, neptun)
        {
            this.House = houses;
        }
    }
}
