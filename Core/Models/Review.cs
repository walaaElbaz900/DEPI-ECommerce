namespace ECommerce.Models
{
    // Models/Review.cs
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    //using Microsoft.AspNetCore.Identity;

    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public string UserId { get; set; } // Changed to string for IdentityUser, no navigation in here i will handle it in DAL

        [ForeignKey(nameof(Car))]
        public int CarID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Car? Car { get; set; }
    }
}
