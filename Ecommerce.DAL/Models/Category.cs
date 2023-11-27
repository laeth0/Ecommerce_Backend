using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ecommerce.DAL
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [DisplayName("Category Name")]//show in the label in html page
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }



    }
}
