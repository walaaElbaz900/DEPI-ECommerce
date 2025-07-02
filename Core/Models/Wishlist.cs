namespace ECommerce.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Wishlist
    {
        [Key]
        public int WishlistID { get; set; }
        public string UserId { get; set; } // Changed to string for IdentityUser, no navigation in here i will handle it in DAL

        [ForeignKey(nameof(Car))]
        public int CarID { get; set; }

        [Required]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Car? Car { get; set; }
    }
}
