using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.DAL.IdentityApplication
{
    public class ApplicationUser:IdentityUser
    {
        // Any Additional Properties that is not in IdentityUser
        public string? Image { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public int age { get; set; }
    }
}
