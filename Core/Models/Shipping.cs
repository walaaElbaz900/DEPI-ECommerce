namespace ECommerce.Models
{
    // Models/Shipping.cs
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Shipping
    {
        [Key]
        public int ShippingID { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [StringLength(50)]
        public string TrackingNumber { get; set; }

        [Required]
        [StringLength(20)]
        public ShippingStatus ShippingStatus { get; set; } // "Pending", "Shipped", "Delivered"

        public DateTime? EstimatedDeliveryDate { get; set; }

        // Navigation Property
        public Order? Order { get; set; }
    }
}
