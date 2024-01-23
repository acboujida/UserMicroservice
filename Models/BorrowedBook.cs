namespace UserMicroservice.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public OwnedBook Book { get; set; }
        public string BorrowerId { get; set; }
        public User Borrower { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}