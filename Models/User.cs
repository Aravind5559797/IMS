using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class User
    {

        //retrieve it from firebase 
        [Key] 
        public string UserIdentifier { get; set; }

        [Required]
        public string Name { get; set; }

       
        //Shops owned 
        public virtual  List<Shop> Shops { get; set; }

    }
}
