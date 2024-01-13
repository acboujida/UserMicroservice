namespace UserMicroservice.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public ICollection<Annonce> Annonces { get; set; }
    }
}
