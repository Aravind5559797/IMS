using IMS.Data;
using IMS.Models;
using IMS.SessionModels;
using IMS.Utility;
using IMS.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IMS.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShopController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Shop> shops;

            if (HttpContext.Session.Get<UserSessionInfo>(WC.UserSession) != null)
            {
                var userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;

                var user = _db.User.Include(u => u.Shops).ThenInclude(u => u.Platform).FirstOrDefault(u => u.UserIdentifier == userId);

                shops = user.Shops;
            }
            else
            {
                shops = null;
            }

            //pass the list of shops after checking session that the user is logged in & retrieve the uid
            return View(shops);
        }

        //RETREIVE ALL SHOPS API  //HAS A USERID VARIABLE

        public IActionResult AllShops()
        {
            //prestend that user has logged in
            var userId = "TestUser123";

            var user = _db.User.AsNoTracking().Include(u => u.Shops).ThenInclude(u => u.Platform).FirstOrDefault(u => u.UserIdentifier == userId);

            var shops = user.Shops;

            if (shops == null)
            {
                return NotFound();
            }

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(shops, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Ok(response);
        }

        //UPSERT API //DONT NEED TO HAVE USERID VARAIBLE
        [HttpPost]
        public IActionResult UpsertAPI([FromBody] ShopJson shopJson)
        {
            //shopJson does not have  the userid
            if (shopJson.ShopId == 0)
            {
                if (ModelState.IsValid)
                {
                    //add shop
                    Shop newShop = new Shop()
                    {
                        ShopName = shopJson.ShopName,

                        Description = shopJson.Description,

                        User = _db.User.FirstOrDefault(i => i.UserIdentifier == shopJson.User),

                        Platform = _db.Platform.FirstOrDefault(i => i.PlatformId == shopJson.PlatformId),

                        Link = shopJson.Link,

                        Products = new List<Product>()
                    };

                    if (shopJson.Products != null)
                    {
                        foreach (int pId in shopJson.Products.ToList())
                        {
                            var pToAdd = _db.Product.FirstOrDefault(i => i.ProductId == pId);
                            if (pToAdd != null)
                            {
                                newShop.Products.Add(pToAdd);
                            }
                            else
                            {
                                shopJson.Products.Remove(pId);
                            }
                        }
                    }

                    _db.Shop.Add(newShop);

                    _db.SaveChanges();

                    shopJson.ShopId = newShop.ShopId;
                    var response = Newtonsoft.Json.JsonConvert.SerializeObject(shopJson, Formatting.None,
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

                    return BadRequest(errorList);
                }
            }
            else
            {
                //update shop
                if (ModelState.IsValid)
                {
                    var oldShop = _db.Shop.Include(i => i.Platform).Include(i => i.Products).Include(i => i.User).FirstOrDefault(i => i.ShopId == shopJson.ShopId);
                    oldShop.ShopName = shopJson.ShopName;

                    oldShop.Description = shopJson.Description;

                    oldShop.Platform = _db.Platform.FirstOrDefault(i => i.PlatformId == shopJson.PlatformId);

                    oldShop.Link = shopJson.Link;

                    oldShop.Products = new List<Product>();

                    if (shopJson.Products != null)
                    {
                        foreach (int pId in shopJson.Products)
                        {
                            var pToAdd = _db.Product.FirstOrDefault(i => i.ProductId == pId);
                            if (pToAdd != null)
                            {
                                oldShop.Products.Add(pToAdd);
                            }
                        }
                    }

                    _db.SaveChanges();

                    var response = Newtonsoft.Json.JsonConvert.SerializeObject(shopJson, Formatting.None,
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

                    return BadRequest(errorList);
                }
            }
        }

        //GET_UPSERT
        public IActionResult Upsert(int? id)
        {
            UpsertVM upsertVM = new UpsertVM()
            {
                Shop = new Shop(),
                Platforms = _db.Platform.Select(i => new SelectListItem()
                {
                    Text = i.PlatformName,
                    Value = i.PlatformId.ToString()
                }
                  )
            };

            if (id == null)
            {
                //insert
                return View(upsertVM);
            }
            else
            {
                upsertVM.Shop = _db.Shop.Include(i => i.Platform).Include(i => i.Platform).FirstOrDefault(i => i.ShopId == id);

                if (upsertVM.Shop == null)
                {
                    return NotFound();
                }

                return View(upsertVM);
            }
        }

        //POST_UPSERT

        //states the the action is for POST request
        [HttpPost]
        //appends an anit-frogery token to form data (for security)
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UpsertVM upsertVM)
        {
            //var shop=upsertVM.Shop;
            if (ModelState.IsValid)//checks if the rules defined for the model is valid
            {
                var userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;

                if (upsertVM.Shop.ShopId == 0)
                {
                    upsertVM.Shop.User = _db.User.Find(userId);

                    _db.Shop.Add(upsertVM.Shop);

                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    //retrieve object from database. We are adding AsNoTracking as there are 2 objects with the same Id
                    //when we update the database , ERC will not know which object to be used for update
                    //hence , we shal disable tracking for object retreived from the database
                    //var obj = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);

                    _db.Shop.Update(upsertVM.Shop);

                    _db.SaveChanges();

                    return RedirectToAction("Index");
                    //return View(productVM);
                }
            }

            upsertVM = new UpsertVM()
            {
                Shop = new Shop(),
                Platforms = _db.Platform.Select(i => new SelectListItem()
                {
                    Text = i.PlatformName,
                    Value = i.PlatformId.ToString()
                }
                  )
            };

            return View(upsertVM);//we return the obj back to the view to display the error messages to be displayed
        }

        //GET DELETE
        public IActionResult Delete(int? id)
        {
            //if the id is not assinged , it will be null as it is a nullable field
            //Hence we will check check the value of id
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //retrieves result based on the Primary key provided
            //include the category property value    //find the Product that matches the ID
            var obj = _db.Shop.Include(u => u.Platform).FirstOrDefault(u => u.ShopId == id);

            if (obj == null)
            {
                return NotFound();
            }

            //if the record is found , the obj is passed to the Delete view
            return View(obj);
        }

        //POST DELETE

        [HttpPost]
        //appends an anit-frogery token to form data (for security)
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? ShopId)
        {
            //Find retreies result based on the Primary key provided

            var obj = _db.Shop.Find(ShopId);
            if (obj == null)
            {
                return NotFound();
            }

            //remove Product object to the database
            _db.Shop.Remove(obj);
            //save the changes
            _db.SaveChanges();
            // redirect to another action ; in this case, to index.
            // Since , we are redirecting to the same controller, we don't need to indicate the controller

            return RedirectToAction("Index");
        }

        //DELETE SHOP API //DONT NEED USER ID VARIABLE
        public IActionResult DeleteShop(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            Shop shop = _db.Shop.Include(i => i.Products).FirstOrDefault(i => i.ShopId == id);

            if (shop == null)
            {
                return NotFound();
            }

            _db.Shop.Remove(shop);

            _db.SaveChanges();

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(shop, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Ok(response);
        }

        //SHOW PRODUCTS

        public IActionResult ShowProducts(int? id)
        {
            var obj = _db.Shop.Include(i => i.Products).FirstOrDefault(i => i.ShopId == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj.Products);
        }

        //SHOW PRODUCTS API //DONT NEED USER ID VARIABLE

        public IActionResult AllProducts(int? id)
        {
            var obj = _db.Shop.Include(i => i.Products).FirstOrDefault(i => i.ShopId == id);

            if (obj == null || obj.Products == null)
            {
                return NotFound();
            }
            //var response = products;
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(obj.Products, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(response);
        }
    }

    public class Test
    {
        public string Description { get; set; }
    }
}