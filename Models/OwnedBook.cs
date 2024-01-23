namespace UserMicroservice.Models
{
    public class OwnedBook
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
