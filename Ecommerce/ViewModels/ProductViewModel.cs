using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class ProductViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }


        public string? ImageURL { get; set; }

        public IFormFile? Image { get; set; } 

        [Required(ErrorMessage = "Product Category is Required")]
        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<Customer>? Customers { get; set; }
    }

}
