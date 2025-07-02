using System.ComponentModel.DataAnnotations;

namespace Orders_part.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Shipping address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
