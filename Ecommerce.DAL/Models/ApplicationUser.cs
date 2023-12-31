using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ecommerce.DAL
{
    public class ApplicationUser:IdentityUser
    {

        public string Fname { get; set; }

        public string Lname { get; set; }

        public bool isAgree { get; set; }

        //public virtual ICollection<Product>? Products { get; set; }

    }
}
