using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Presentation.ViewModels
{
    public class AddRoleViewModel
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
