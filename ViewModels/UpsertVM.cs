using IMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.ViewModels
{
    public class UpsertVM
    {
        public Shop Shop { get; set; }

        public IEnumerable<SelectListItem> Platforms { get; set; }


    }
}
