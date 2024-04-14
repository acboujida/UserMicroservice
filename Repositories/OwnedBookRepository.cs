using UserMicroservice.Data;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;
using System.Linq;
using System.Collections.Generic;

namespace UserMicroservice.Repositories
{
    public class OwnedBookRepository : IOwnedBookRepository
    {
        private readonly DataContext _dataContext;

        public OwnedBookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public OwnedBook GetOwnedBook(int id)
        {
            return _dataContext.OwnedBooks.FirstOrDefault(o => o.Id == id);
        }

        public ICollection<OwnedBook> GetOwnedBooks()
        {
            return _dataContext.OwnedBooks.OrderBy(o => o.Id).ToList();
        }

        public bool OwnedBookExists(int id)
        {
            return _dataContext.OwnedBooks.Any(o => o.Id == id);
        }

        public bool CreateOwnedBook(OwnedBook ownedBook)
        {
            _dataContext.Add(ownedBook);
            return Save();
        }

        public bool UpdateOwnedBook(OwnedBook ownedBook)
        {
            _dataContext.Update(ownedBook);
            return Save();
        }

        public bool DeleteOwnedBook(int id)
        {
            var ownedBook = _dataContext.OwnedBooks.FirstOrDefault(o => o.Id == id);
            if (ownedBook != null)
            {
                _dataContext.Remove(ownedBook);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0;
        }

        public bool IsUserOwner(int id, string userId)
        {
            return _dataContext.OwnedBooks.Any(o => o.Id == id && o.OwnerId == userId);
        }

        public User GetOwnerOfBook(int ownedBookId)
        {
            return _dataContext.OwnedBooks.Where(ob => ob.Id == ownedBookId).Select(ob => ob.Owner).FirstOrDefault();
        }
    }
}
