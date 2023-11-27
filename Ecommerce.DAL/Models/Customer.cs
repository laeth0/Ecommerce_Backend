using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="User name is Reauired")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "User Email is Required")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "User Password is Required")]
        public string CustomerPassword { get; set; }

        [Required(ErrorMessage = "User Phone is Required")]
        public string CustomerPhone { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
