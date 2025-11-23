namespace RoxfortNeptun.Models
{
    public interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string NeptunKod { get; set; }
        string Password { get; set; }
        Houses House { get; set; }
        UserType UserType { get; }
    }

    public enum UserType
    {
        Student = 0,
        Teacher = 1
    }
}