using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int id);
        User GetUser(string name);
        bool UserExists(string name);
        ICollection<Review> GetReviewsOfUser(int id);
        bool Save();
        bool CreateUser(User user);
        bool DeleteUser(User user);
    }
}
