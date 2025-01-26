using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CanineConnect.Models
{
    [Index(nameof(UserId), IsUnique = true)]
    public class Shelter
    {
        public int Id { get; set; }
        [Required]
        public string? ShelterName { get; set; }
        public string? Description { get; set; }

        public string? FacebookURL { get; set; }
        public string? InstagramURL { get; set; }
        public string? TwitterURL { get; set; }
        public string? WebsiteURL { get; set; }

        [Required]
        public int UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? User { get; set; }
    }
}
