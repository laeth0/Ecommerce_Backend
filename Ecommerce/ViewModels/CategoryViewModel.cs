using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class CategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [DisplayName("Category Name")]//show in the label in html page
        public string CategoryName { get; set; }

        public virtual ICollection<Product>? Products { get; set; }


    }
}
