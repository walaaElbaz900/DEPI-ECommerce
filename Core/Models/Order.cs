namespace ECommerce.Models
{
    // Models/Order.cs
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string UserId { get; set; } // Changed to string for IdentityUser, no navigation in here i will handle it in DAL

        [ForeignKey(nameof(Car))]
        public int CarID { get; set; }

        [Required]
        [StringLength(20)]
        public OrderType OrderType { get; set; } // "Purchase", "Rental"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public PaymentStatus PaymentStatus { get; set; } // "Pending", "Paid", "Failed"

        [Required]
        public OrderStatus OrderStatus { get; set; } // "Processing", "Completed", "Cancelled"

        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }

        [ForeignKey(nameof(Promotion))]
        public int? PromotionId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Promotion? Promotion { get; set; }
        public Car? Car { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
