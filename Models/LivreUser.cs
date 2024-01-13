namespace UserMicroservice.Models
{
    public class LivreUser
    {
        public User User { get; set; }
        public Livre Livre { get; set; }
        public int LivreId { get; set; }
        public int UserId { get; set; }
        public Annonce Annonce { get; set; }
    }
}
