using System;
using System.Collections.Generic;
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
        public string ProductName { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
