using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class ProductJson
    {
        [Key]

        public int ProductId { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int Quantity { get; set; }

        public List<int> Shops { get; set; }

        public string User { get; set; }

        public bool IsApi { get; set; } = false;

    }
}
