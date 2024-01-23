using Microsoft.AspNetCore.Identity;
using UserMicroservice.Data;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;

namespace UserMicroservice.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User GetUser(string id)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByName(string username)
        {
            return _dataContext.Users.FirstOrDefault(u => ((u.UserName.ToUpper().Trim() == username.ToUpper().Trim())));
        }

        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.OrderBy(u => u.Id).ToList();
        }

        public bool UserExists(string id)
        {
            return _dataContext.Users.Any(u => u.Id == id);
        }

        public bool UserExistsByName(string username)
        {
            return _dataContext.Users.Any(u => ((u.UserName.ToUpper().Trim() == username.ToUpper().Trim())));
        }

        public ICollection<Review> GetReviewsOfUser(string id)
        {
            return _dataContext.Reviews.Where(r => r.User.Id == id).OrderBy(r => r.Id).ToList();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public bool CreateUser(User user)
        {
            _dataContext.Add(user);

            return Save();
        }

        public bool DeleteUser(User user)
        {
            _dataContext.Remove(user);

            return Save();
        }

        public ICollection<OwnedBook> GetOwnedBooksOfUser(string id)
        {
            return _dataContext.OwnedBooks.Where(b => b.OwnerId == id).ToList();
        }

        public ICollection<BorrowedBook> GetBorrowedBooksOfUser(string id)
        {
            return _dataContext.BorrowedBooks.Where(b => b.BorrowerId == id).ToList();
        }
    }
}