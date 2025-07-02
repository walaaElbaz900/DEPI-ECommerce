using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Presentation.ViewModels
{
    
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int age { get; set; }
    }
}
