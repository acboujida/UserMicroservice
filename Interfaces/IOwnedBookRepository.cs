using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IOwnedBookRepository
    {
        ICollection<OwnedBook> GetOwnedBooks();
        OwnedBook GetOwnedBook(int id);
        bool OwnedBookExists(int id);
        bool IsUserOwner(int id, string userId);
        bool CreateOwnedBook(OwnedBook ownedBook);
        bool UpdateOwnedBook(OwnedBook ownedBook);
        bool DeleteOwnedBook(int id);
        bool Save();
        User GetOwnerOfBook(int ownedBookId);
    }
}
