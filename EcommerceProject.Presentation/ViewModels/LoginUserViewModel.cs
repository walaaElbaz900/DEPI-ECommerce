using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Presentation.ViewModels
{
    public class LoginUserViewModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me!!")]
        public bool RememberMe { get; set; }
    }
}
