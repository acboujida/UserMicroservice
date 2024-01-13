namespace UserMicroservice.Models
{
    public class Annonce
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Owner Owner { get; set; }
    }
}