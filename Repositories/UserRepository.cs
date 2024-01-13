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

        public User GetUser(int id)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUser(string name)
        {
            return _dataContext.Users.FirstOrDefault(u => ((u.LastName.ToUpper().Trim() == name.ToUpper().Trim()) || (u.FirstName.ToUpper().Trim() == name.ToUpper().Trim())));
        }

        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.OrderBy(u => u.Id).ToList();
        }

        public bool UserExists(int id)
        {
            return _dataContext.Users.Any(u => u.Id == id);
        }

        public bool UserExists(string name)
        {
            return _dataContext.Users.Any(u => ((u.LastName.ToUpper().Trim() == name.ToUpper().Trim()) || (u.FirstName.ToUpper().Trim() == name.ToUpper().Trim())));
        }

        public ICollection<Review> GetReviewsOfUser(int id)
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
    }
}