using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class Product
    {
        //primary key 
        [Key]

        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        //get the filename and extension of the image 
        public string Image { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        //add the quantity
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int Quantity { get; set; }

        //list of products sold and quantity available 
        public virtual List<Shop> Shops { get; set; }

        //user and shop conencted so no need user for it to be here 
        //Owner of the product
        public virtual User User { get; set; }

        public bool IsApi { get; set; } = false;
    }
}
