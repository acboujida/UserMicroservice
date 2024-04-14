namespace UserMicroservice.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public OwnedBook OwnedBook { get; set; }
        public int OwnedBookId { get; set; }
        public User Borrower { get; set; }
        public string BorrowerId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}