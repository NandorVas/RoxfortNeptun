using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    class Teachers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Houses WhichHousesHead { get; set; }

        public Teachers(string name)
        {
            this.Name = name;
            this.WhichHousesHead = Houses.None;
        }

        public Teachers(string name, Houses houses)
            :this(name)
        {
            this.WhichHousesHead = houses;
        }
    }
}
