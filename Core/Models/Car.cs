namespace ECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using Microsoft.AspNetCore.Http;

    public class Car
    {
        [Key]
        public int CarID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(30)]
        public string Category { get; set; } // "Sedan", "SUV", etc.

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RentalPricePerDay { get; set; }

        public int Mileage { get; set; }

        [Required]
        [StringLength(20)]
        public string FuelType { get; set; }

        [Required]
        [StringLength(20)]
        public string TransmissionType { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public AvailabilityStatus AvailabilityStatus { get; set; } // "Available", "Sold", "Rented"

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        //[RegularExpression(@".+\.(jpg|png)", ErrorMessage = "Only .jpg and .png files are allowed")]
        public string ImagesUrl { get; set; }

        [NotMapped]
        public IFormFile CarImage { get; set; }
    }
}
