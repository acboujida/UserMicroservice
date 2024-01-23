using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
