using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CanineConnect.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int SenderId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? Sender { get; set; }

        [Required]
        public int ReceiverId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? Receiver { get; set; }
    }
}
