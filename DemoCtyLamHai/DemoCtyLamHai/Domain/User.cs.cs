using System.ComponentModel.DataAnnotations;

namespace DemoCtyLamHai.Domain
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string FullName { get; set; }


        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
