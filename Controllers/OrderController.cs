using IMS.Data;
using IMS.Models;
using IMS.SessionModels;
using IMS.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrderController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Order> orders = _db.Order;

            return View(orders);
        }

        //GET ALL ORDERS API  HAS USER ID VARIABLE 
        public IActionResult AllOrders()
        {

            var userId = "TestUser123";
            IEnumerable<Order> orders = _db.Order.Include(i => i.Orders).Where(i=>i.User.UserIdentifier==userId);


            var response = Newtonsoft.Json.JsonConvert.SerializeObject(orders, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(response);
        }

        //ADD ORDERS TO DATABASE API  //HAS USERID VARIABLE
        [HttpPost]
        public IActionResult AddOrder([FromBody] Order custOrder)
        {
            if (ModelState.IsValid)
            {
                var userId = "TestUser123";


                custOrder.User = _db.User.FirstOrDefault(i=>i.UserIdentifier==userId);

                _db.Order.Add(custOrder);

                _db.SaveChanges();

                var response = Newtonsoft.Json.JsonConvert.SerializeObject(custOrder, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
                return Ok(response);
            }
            else
            {
                var errorList = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                );

                var response = Newtonsoft.Json.JsonConvert.SerializeObject(errorList, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

                return BadRequest(response);
            }
        }

        public IActionResult DisplayOrder(int? id)
        {
            var userId = "TestUser123";
            Order orders = _db.Order.Include(i => i.Orders).Where(i=>i.User.UserIdentifier==userId).FirstOrDefault(i => i.OrderId == id);

            return View(orders.Orders);
        }

        //DISPLAY ORDER API 

        public IActionResult DisplayOrderAPI(int? id)
        {
            var userId = "TestUser123";
            Order orders = _db.Order.Include(i => i.Orders).Where(i => i.User.UserIdentifier == userId).FirstOrDefault(i => i.OrderId == id);

            if (orders == null)
            {
                return NotFound();
            }

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(orders.Orders, Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });


            return Ok(response);



        }




        //DELETE API
        public IActionResult DeleteOrder(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            Order order = _db.Order.Include(i => i.Orders).FirstOrDefault(i => i.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            _db.Order.Remove(order);

            _db.SaveChanges();

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(order, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Ok(response);
        }
    }
}