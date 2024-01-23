using UserMicroservice.DTO;
using UserMicroservice.Models;

namespace UserMicroservice.Mapper
{
    public class MappingFactory
    {
        BorrowedBookDTO CreateBorrowedBookDTO(BorrowedBook book)
        {
            return new BorrowedBookDTO
            {
                BookId = book.Book.BookId,
                Title = book.Book.Title,
                Description = book.Book.Description,
                Id = book.Id,
                BorrowDate = book.BorrowDate,
                ReturnDate = book.ReturnDate
            };
    
        }   
    }
}
