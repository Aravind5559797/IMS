using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.ViewModels
{
    public class Dashboard
    {
        public List<Shop> Shops { get; set; }

        public List<Order> Orders { get; set; }

        public int ProductNum   { get; set; }

        public List<Platform> Platforms { get; set; }
    }
}
