using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class HistOrder
    {

        //store everything about the order & product 
        [Key]
        public int HistOrderId { get; set; }

        [Required]
        public int HistProductId { get; set; }

        [Required]
        public string HistName { get; set; }

        public string HistDescription { get; set; }

        public string HistShortDescription { get; set; }

        public string HistImage { get; set; }

        public double HistPrice { get; set; }

        public int HistQuantity { get; set; }

    }
}
