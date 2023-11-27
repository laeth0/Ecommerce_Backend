using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage="Product Name is Required")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }


        public string? ImagePath { get; set; }


        [Required(ErrorMessage = "Product Category is Required")]
        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
