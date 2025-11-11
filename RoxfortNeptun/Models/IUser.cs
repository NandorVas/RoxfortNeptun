using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Models
{
    public enum UserType
    {
        Student = 0,
        Teacher = 1
    }

    public interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string Neptunkod { get; set; }
        string Password { get; set; }
        UserType Type { get; set; }
        Houses Houses { get; set; }
    }
}
