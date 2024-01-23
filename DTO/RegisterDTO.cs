using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set;}

        [Required]
        public string? Password { get; set;}

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePhoto { get; set; }
    }
}