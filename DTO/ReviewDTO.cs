using UserMicroservice.Models;

namespace UserMicroservice.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public float Score { get; set; }
        public DateTime PostDate { get; set; }
        public string BookId { get; set; }
    }
}
