using System.ComponentModel.DataAnnotations;

namespace CanineConnect.Models
{
    public class Application
    {
        public int Id { get; set; }
        [Required]
        public bool Approved { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int DogListingId { get; set; }
        public DogListing? DogListing { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? Salary { get; set; }
        public int? Animals { get; set; }
        public int? Dependents { get; set; }
    }
}
