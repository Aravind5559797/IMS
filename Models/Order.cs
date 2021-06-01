using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{

    //for the sake of the sample project , orders are made using ajax calls 
    public class Order
    {

        //ORDER META DATA 


        //Order Id (Primary key)
        [Key]
        public int OrderId { get; set; }

        //Identifies shop
        public int HistShopId { get; set; }

        public string HistShopName { get; set; }

        [Required]
        //order date and time (send via javascript ajax)
        public string CreatedDate { get; set; }


        [Required]
        //address of the customer 
        public string Address { get; set; }


        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        //ACTUAL DATA FOR ORDER 
        public virtual List<HistOrder> Orders { get; set; }

        //total price 
        public int OrderTotalPrice { get; set; }


        //[Required]
        public virtual User User {get; set; }

}
}
