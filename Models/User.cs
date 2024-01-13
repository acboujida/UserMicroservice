namespace UserMicroservice.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Promo { get; set; }
        public ICollection<Review> Reviews { get; set; }
        //public ICollection<UserUser> Friends { get; set; }
        public ICollection<LivreUser> LivreUsers { get; set; }
    }
}