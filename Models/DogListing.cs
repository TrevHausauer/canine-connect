using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanineConnect.Models
{
    public class DogListing
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } 
        [Required]
        public string? Sex { get; set; }
        [Column(TypeName="decimal(6, 1)")]
        public decimal? Weight { get; set; }
        public string? Breed { get; set; }
        public DateOnly Age { get; set; }
        public bool Avaliable { get; set; } = true;
        public string? Description { get; set; }
        public byte[]? ThumbnailImage { get; set; }
        //public byte[][]? AdditionalImages { get; set; }

        [Required]
        public int ShelterId { get; set; }
        public Shelter? Shelter { get; set; }
    }
}
