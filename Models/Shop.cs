using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class Shop
    {

        //Primary key 
        [Key]
        [DisplayName("Shop Id")]
        public int ShopId { get; set; }

        [DisplayName("Shop Name")]
        [Required]
        public string ShopName { get; set; }

        [Required]
        public string Description { get; set; }


    
        //Owner of the shop 
        public virtual User User { get; set; }


        [DisplayName("Platform Name")]
        [Required]

        //Platform 
        public virtual int PlatformId { get; set; }



        public virtual Platform Platform { get; set; } 




        //list of products sold and quantity available 
        public virtual List<Product> Products { get; set; }

        //website link 
        public string Link { get; set; }

    }
}
