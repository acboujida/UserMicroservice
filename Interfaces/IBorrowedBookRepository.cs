using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IBorrowedBookRepository
    {
        ICollection<BorrowedBook> GetBorrowedBooks();
        BorrowedBook GetBorrowedBook(int id);
        bool BorrowedBookExists(int id);
        User GetBorrowerOfBook(int id);
        bool BorrowBook(BorrowedBook borrowedBook);
        bool IsUserBorrower(int id, string userId);
        bool UpdateBorrowedBook(BorrowedBook borrowedBook);
        bool ReturnBook(int id);
        bool Save();
    }
}
