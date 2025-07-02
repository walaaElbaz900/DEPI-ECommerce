namespace ECommerce.Models
{
    // Models/Payment.cs
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }
        public string UserId { get; set; } // Changed to string for IdentityUser, no navigation in here i will handle it in DAL

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(30)]
        public string PaymentMethod { get; set; } // "Credit Card", "PayPal", etc.

        [StringLength(100)]
        public string TransactionID { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Order Order { get; set; }
    }
}
