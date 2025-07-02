using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Presentation.ViewModels
{
    public class ProfileViewModel
    {
        [HiddenInput]
        public string Id { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        
        [Display(Name = "Change Profile Picture")]
        //[RegularExpression(@".+\.(jpg|png)", ErrorMessage = "Only .jpg and .png files are allowed")]
        public IFormFile? ProfilePicture { get; set; }

        public int age { get; set; }

        public string ImageFromDB { get; set; }
    }
}
