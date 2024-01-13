using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IUserRepository
    {
        public ICollection<User> GetUsers();
    }
}
