using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class CustomerViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "User name is Reauired")]
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
