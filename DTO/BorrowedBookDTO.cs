using UserMicroservice.Models;

namespace UserMicroservice.DTO
{
    public class BorrowedBookDTO
    {
        public int Id { get; set; }
        public string BorrowerId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
