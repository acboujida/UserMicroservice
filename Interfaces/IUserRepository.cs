using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(string id);
        bool UserExists(string id);
        User GetUserByName(string name);
        bool UserExistsByName(string name);
        ICollection<Review> GetReviewsOfUser(string id);
        bool Save();
        bool CreateUser(User user);
        bool DeleteUser(User user);
        ICollection<OwnedBook> GetOwnedBooksOfUser(string id);
        ICollection<BorrowedBook> GetBorrowedBooksOfUser(string id);
    }
}
