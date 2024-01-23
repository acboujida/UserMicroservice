using Microsoft.AspNetCore.Identity;

namespace UserMicroservice.Models
{
    public class User : IdentityUser
    {
        public string ProfilePhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
        public ICollection<OwnedBook> OwnedBooks { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}