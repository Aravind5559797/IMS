using IMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.ViewModels
{
    public class ProductUpsertVM
    {

        public Product Product { get; set; }

        public IEnumerable<SelectListItem> Shops { get; set; }


        public List<string> CurrentShopIds { get; set; }

    }
}
