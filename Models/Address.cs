﻿using System.ComponentModel.DataAnnotations;

namespace CanineConnect.Models
{
    public class Address
    {

        public int Id { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public string? Country { get; set; }
    }
}
