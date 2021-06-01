using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.UnusedModels
{
    public class ProductAndQuantityOrdered
    {
        //[Key]
        //public int Id { get; set; }

        //[ForeignKey("ProductId")]
        //[DisplayName("Product Type")]
        ////load the product id (foreignkey)
        //public Product ProductId { get; set; }

        //[Required]
        ////the amt of product ordered
        //public int Quantity { get; set; }


        ////Load the product object 
        //public Product Product { get; set; }

        ////not good , tied to product , historical data not saved when product is changed
        ////take snape shot of the product (when it was ordered)
    }
}
