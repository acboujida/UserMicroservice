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

        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.OrderBy(u => u.Id).ToList();
        }
    }
}