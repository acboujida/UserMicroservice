using UserMicroservice.Data;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;
using System.Linq;
using System.Collections.Generic;

namespace UserMicroservice.Repositories
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private readonly DataContext _dataContext;

        public BorrowedBookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public BorrowedBook GetBorrowedBook(int id)
        {
            return _dataContext.BorrowedBooks.FirstOrDefault(b => b.Id == id);
        }

        public ICollection<BorrowedBook> GetBorrowedBooks()
        {
            return _dataContext.BorrowedBooks.OrderBy(b => b.Id).ToList();
        }

        public bool BorrowedBookExists(int id)
        {
            return _dataContext.BorrowedBooks.Any(b => b.Id == id);
        }

        public User GetBorrowerOfBook(int id)
        {
            return _dataContext.BorrowedBooks.Where(b => b.Id == id).Select(b => b.Borrower).FirstOrDefault();
        }

        public bool BorrowBook(BorrowedBook borrowedBook)
        {
            _dataContext.Add(borrowedBook);
            return Save();
        }

        public bool ReturnBook(int id)
        {
            var borrowedBook = _dataContext.BorrowedBooks.FirstOrDefault(b => b.Id == id);
            if (borrowedBook != null)
            {
                _dataContext.Remove(borrowedBook);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0;
        }

        public bool IsUserBorrower(int id, string userId)
        {
            return _dataContext.BorrowedBooks.Any(b => b.Id == id && b.BorrowerId == userId);
        }

        public bool UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            _dataContext.Update(borrowedBook);
            return Save();
        }
    }
}
