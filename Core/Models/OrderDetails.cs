namespace ECommerce.Models
{
    // Models/OrderDetail.cs
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0;

        // Navigation property
        public Order? Order { get; set; }
        public Car? Car { get; set; }

    }
}
