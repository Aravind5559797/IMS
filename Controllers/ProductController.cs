using IMS.Data;
using IMS.Models;
using IMS.SessionModels;
using IMS.Utility;
using IMS.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IMS.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        //SEEMS FINE
        public IActionResult Index()
        {
            IEnumerable<Product> Products;
            User user; 

            if (HttpContext.Session.Get<UserSessionInfo>(WC.UserSession) != null)
            {
                var userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;

                user = _db.User.Find(userId);

                //Eager laod the shops and filer the user (shops allows us to display the shops the product is in.
                //we dont need to display the user)
                Products = _db.Product.Include(i=>i.Shops).Where(i=>i.User==user);
            }
            else
            {
                Products = null;
            }

            //pass the list of shops after checking session that the user is logged in & retrieve the uid
            return View(Products);
        }


        //GET ALL PRODUCTS API 

        //public IActionResult GetAllProducts()
        //{
        //    IEnumerable<Product> products;



        //    var user = "TestUser123";

        //    //Eager laod the shops and filer the user (shops allows us to display the shops the product is in.
        //    //we dont need to display the user)
        //    products = _db.Product.Include(i => i.Shops).Where(i => i.User.UserIdentifier == user).AsNoTracking();

        //    if (products == null)
        //    {
        //        return NotFound(); 
        //    }

        //    var response = Newtonsoft.Json.JsonConvert.SerializeObject(products, Formatting.None,
        //    new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });
            
        //    return Ok(response);


        //    //pass the list of shops after checking session that the user is logged in & retrieve the uid
          
        //}


        //[HttpPost]

        ////UPSERT API
        //public IActionResult UpsertAPI([FromBody] ProductJson productJson)
        //{

        //    var userId = "TestUser123";
        //    if (!ModelState.IsValid)
        //    {
        //        var errorList = ModelState
        //         .Where(x => x.Value.Errors.Count > 0)
        //         .ToDictionary(
        //             kvp => kvp.Key,
        //             kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
        //         );

        //        var response = Newtonsoft.Json.JsonConvert.SerializeObject(errorList, Formatting.None,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });

        //        return BadRequest(response);
        //    }
        //    if (productJson.ProductId==0 )
        //    {
        //        //add product

        //        var newProduct = new Product()
        //        {
        //            Name = productJson.Name,
        //            Description = productJson.Description,
        //            ShortDescription = productJson.ShortDescription,
        //            Image = productJson.Image,
        //            Price = productJson.Price,
        //            Quantity = productJson.Quantity,
        //            Shops = new List<Shop>(),
        //            User=_db.User.FirstOrDefault(i=>i.UserIdentifier==userId),
        //            IsApi =true
        //        };

        //        if (productJson.Shops != null)
        //        {
        //            foreach (var shopId in productJson.Shops)
        //            {
        //                var shop = _db.Shop.FirstOrDefault(i=>i.ShopId== shopId);
        //                if (shop != null)
        //                {
        //                    newProduct.Shops.Add(shop);
        //                }
        //            }
        //        }


        //        _db.Product.Add(newProduct);

        //        _db.SaveChanges();


        //        //var response = Newtonsoft.Json.JsonConvert.SerializeObject(newProduct, Formatting.None,
        //        //new JsonSerializerSettings()
        //        //{
        //        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        //});

        //        productJson.ProductId = newProduct.ProductId;


        //        var response = Newtonsoft.Json.JsonConvert.SerializeObject(productJson, Formatting.None,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });

        //        return Ok(response);


        //    }
        //    else
        //    {
        //        //update product 
        //        var oldProduct = _db.Product.Include(i=>i.Shops).FirstOrDefault(i=>i.ProductId== productJson.ProductId);
        //        if (oldProduct == null)
        //        {
        //            return NotFound(); 
        //        }

        //        oldProduct.Name = productJson.Name;
        //        oldProduct.Description = productJson.Description;
        //        oldProduct.ShortDescription = productJson.ShortDescription;
        //        oldProduct.Image = productJson.Image;
        //        oldProduct.Price = productJson.Price;
        //        oldProduct.Quantity = productJson.Quantity;
        //        oldProduct.Shops = new List<Shop>();
        //        oldProduct.IsApi = true;

        //        if (productJson.Shops != null)
        //        {
        //            foreach (var shopId in productJson.Shops)
        //            {
        //                var shop = _db.Shop.FirstOrDefault(i => i.ShopId == shopId);
        //                if (shop != null)
        //                {
        //                    oldProduct.Shops.Add(shop);
        //                }
        //            }
        //        }


        //        _db.SaveChanges();


        //        var response = Newtonsoft.Json.JsonConvert.SerializeObject(productJson, Formatting.None,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });

        //        return Ok(response);


        //    }


        //}



        //SEEMS FINE

        //GET_UPSERT
        //If it is an edit , then id will be an int otherwise ,
        //if it is a insert, then id will be null
        //public IActionResult Upsert(int? id)
        //{
        //    string userId;
        //    List<string> currentShopIds=new List<string>(); 

        //    if (HttpContext.Session.Get<UserSessionInfo>(WC.UserSession) != null)
        //    {
        //        userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;

        //        ProductUpsertVM productUpsertVM = new ProductUpsertVM()
        //        {
        //            Product = new Product(),
        //            Shops = _db.Shop.AsNoTracking().Where(i => i.User.UserIdentifier == userId).Select(i => new SelectListItem
        //            {
        //                Text = i.ShopName,
        //                Value = i.ShopId.ToString()
        //            }).ToList(),
        //            CurrentShopIds = new List<string>()
        //        };
        //        if (id == null || id==0)
        //        {
        //            //this is for create
        //            return View(productUpsertVM);
        //        }
        //        else
        //        {

        //            productUpsertVM.Product = _db.Product.AsNoTracking().Include(i=>i.Shops).FirstOrDefault(i=>i.ProductId==id);
        //            //if product is null , product is not found in the database
        //            if (productUpsertVM.Product == null)
        //            {
        //                return NotFound();
        //            }

        //            //check if shops are available, if there are shops , add the current shops present in the productUpsertVM
        //            if (productUpsertVM.Product.Shops != null)
        //            {
        //                foreach (var shop in productUpsertVM.Product.Shops)
        //                {
        //                    currentShopIds.Add(shop.ShopId.ToString()); 
        //                }

        //                productUpsertVM.CurrentShopIds = currentShopIds;
        //            }

        //                //this is for update
        //                return View(productUpsertVM);
        //        }
        //    }
        //    else
        //    {
        //        //need to login so user access key can be stored in session. We shall return the 404 for now 
        //        return NotFound();
        //    }
        //}





        ////ar selectedFruits = Request.Form.GetValues("SelectedFruits")

        ////POST UPSERT
        ////states the the action is for POST request
        //[HttpPost]
        ////appends an anit-frogery token to form data (for security)
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(ProductUpsertVM productUpsertVM)
        //{
        //    if (HttpContext.Session.Get<UserSessionInfo>(WC.UserSession) != null)
        //    {


        //        //retrieve use id from session 
        //        var userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;



        //        //retrieive all the shops that the user have 
        //        List<Shop> allShops = _db.Shop.Where(i=>i.User.UserIdentifier==userId).ToList();




        //        if (ModelState.IsValid)//checks if the rules defined for the model is valid
        //        {
        //            //retreive user from database 
        //            var user = _db.User.Find(userId);

        //            //get the file to be uploaded
        //            var files = HttpContext.Request.Form.Files;
        //            //path of the webroot
        //            string webRootPath = _webHostEnvironment.WebRootPath;



        //            if (productUpsertVM.Product.ProductId == 0)
        //            {

        //                //create new product object
        //                var product = new Product() {
        //                    Name = productUpsertVM.Product.Name,
        //                    Description = productUpsertVM.Product.Description,
        //                    ShortDescription = productUpsertVM.Product.ShortDescription,
        //                    Image = "",
        //                    Price = productUpsertVM.Product.Price,
        //                    Quantity = productUpsertVM.Product.Quantity,
        //                    Shops = new List<Shop>(),
        //                    User=user,
        //                    IsApi=false
        //                }; 

        //                //the path to which the image has to be copied to
        //                string upload = webRootPath + WC.ImagePathProducts;

        //                //generate a ui as a file name
        //                string fileName = Guid.NewGuid().ToString();

        //                //get the extension of the file
        //                string extension = Path.GetExtension(files[0].FileName);

        //                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
        //                {
        //                    files[0].CopyTo(fileStream);
        //                }

        //                product.Image = fileName + extension;


        //                //add the shops 
        //                foreach (var shop in allShops)
        //                {
        //                    string id = shop.ShopId.ToString();
        //                    if (HttpContext.Request.Form[id] == shop.ShopId.ToString())
        //                    {
        //                        productUpsertVM.CurrentShopIds.Add(shop.ShopId.ToString());


        //                        product.Shops.Add(shop);



        //                    }
        //                }



        //                _db.Product.Add(product);

        //                _db.SaveChanges();

        //                return RedirectToAction("Index");
        //            }

        //            else
        //            {


        //                var product = _db.Product.Include(i=>i.Shops).Include(i => i.User).FirstOrDefault(i => i.ProductId == productUpsertVM.Product.ProductId);

        //                product.Shops = new List<Shop>();
        //                foreach (var shop in allShops)
        //                {
        //                    string id = shop.ShopId.ToString();
        //                    if (HttpContext.Request.Form[id] == shop.ShopId.ToString())
        //                    {
        //                        productUpsertVM.CurrentShopIds.Add(shop.ShopId.ToString());



        //                        product.Shops.Add(shop);
        //                    }
        //                }


        //                product.Name = productUpsertVM.Product.Name;
        //                product.Description = productUpsertVM.Product.Description;
        //                product.ShortDescription = productUpsertVM.Product.ShortDescription;
        //                product.Price = productUpsertVM.Product.Price;
        //                product.Quantity = productUpsertVM.Product.Quantity;
        //                //product.IsApi = false;


        //                if (files.Count > 0)
        //                {
        //                    //the path to which the image has to be copied to
        //                    string upload = webRootPath + WC.ImagePathProducts;

        //                    //generate a ui as a file name
        //                    string fileName = Guid.NewGuid().ToString();

        //                    //get the extension of the file
        //                    string extension = Path.GetExtension(files[0].FileName);

        //                    //get the directory of the old image
        //                    var oldFile = Path.Combine(upload, product.Image);

        //                    //check if the file exists in the system
        //                    if (System.IO.File.Exists(oldFile))
        //                    {
        //                        //delete file from system
        //                        System.IO.File.Delete(oldFile);
        //                    }

        //                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
        //                    {
        //                        files[0].CopyTo(fileStream);
        //                    }

        //                    //add the new file name to the object received from view
        //                    productUpsertVM.Product.Image = fileName + extension;
        //                    product.Image= fileName + extension;
        //                    product.IsApi = false;
        //                }
        //                else
        //                {
        //                    //add the old file name to the object recevied from view
        //                    productUpsertVM.Product.Image = product.Image;
        //                }





        //                _db.SaveChanges();

        //                return RedirectToAction("Index");
        //                //return View(productVM);
        //            }
        //        }



        //        return View(productUpsertVM);//we return the obj back to the view to display the error messages to be displayed
        //    }


        //    //user not logged in , session expired , return 404 for now 
        //    return NotFound();
        //}


        public IActionResult Upsert(int? id)
        {
            string userId;
            List<string> currentShopIds = new List<string>();

            if (HttpContext.Session.Get<UserSessionInfo>(WC.UserSession) != null)
            {
                userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;

                ProductUpsertVM productUpsertVM = new ProductUpsertVM()
                {
                    Product = new Product(),
                    Shops = _db.Shop.Where(i => i.User.UserIdentifier == userId).Select(i => new SelectListItem
                    {
                        Text = i.ShopName,
                        Value = i.ShopId.ToString()
                    }).ToList(),
                    CurrentShopIds = new List<string>()
                };
                if (id == null || id == 0)
                {
                    //this is for create
                    return View(productUpsertVM);
                }
                else
                {

                    productUpsertVM.Product = _db.Product.Include(i => i.Shops).FirstOrDefault(i => i.ProductId == id);
                    //if product is null , product is not found in the database
                    if (productUpsertVM.Product == null)
                    {
                        return NotFound();
                    }

                    //check if shops are available, if there are shops , add the current shops present in the productUpsertVM
                    if (productUpsertVM.Product.Shops != null)
                    {
                        foreach (var shop in productUpsertVM.Product.Shops)
                        {
                            currentShopIds.Add(shop.ShopId.ToString());
                        }

                        productUpsertVM.CurrentShopIds = currentShopIds;
                    }

                    //this is for update
                    return View(productUpsertVM);
                }
            }
            else
            {
                //need to login so user access key can be stored in session. We shall return the 404 for now 
                return NotFound();
            }
        }





        //ar selectedFruits = Request.Form.GetValues("SelectedFruits")

        //POST UPSERT
        //states the the action is for POST request
        [HttpPost]
        //appends an anit-frogery token to form data (for security)
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductUpsertVM productUpsertVM)
        {

            
            if (HttpContext.Session.Get<UserSessionInfo>(WC.UserSession) != null)
            {


                //retrieve use id from session 
                var userId = HttpContext.Session.Get<UserSessionInfo>(WC.UserSession).UserId;



                //retrieive all the shops that the user have 
                List<Shop> allShops = _db.Shop.Where(i => i.User.UserIdentifier == userId).ToList();




                if (ModelState.IsValid)//checks if the rules defined for the model is valid
                {
                    //retreive user from database 
                    var user = _db.User.Find(userId);

                    //get the file to be uploaded
                    var files = HttpContext.Request.Form.Files;
                    //path of the webroot
                    string webRootPath = _webHostEnvironment.WebRootPath;



                    if (productUpsertVM.Product.ProductId == 0)
                    {

                        //create new product object
                        var product = new Product()
                        {
                            Name = productUpsertVM.Product.Name,
                            Description = productUpsertVM.Product.Description,
                            ShortDescription = productUpsertVM.Product.ShortDescription,
                            Image = "",
                            Price = productUpsertVM.Product.Price,
                            Quantity = productUpsertVM.Product.Quantity,
                            Shops = new List<Shop>(),
                            User = user,
                            IsApi = false
                        };

                        //the path to which the image has to be copied to
                        string upload = webRootPath + WC.ImagePathProducts;

                        //generate a ui as a file name
                        string fileName = Guid.NewGuid().ToString();

                        //get the extension of the file
                        string extension = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        product.Image = fileName + extension;

                        //add the shops 
                        foreach (var shop in allShops)
                        {
                            string id = shop.ShopId.ToString();
                            if (HttpContext.Request.Form[id] == shop.ShopId.ToString())
                            {
                                productUpsertVM.CurrentShopIds.Add(shop.ShopId.ToString());


                                product.Shops.Add(shop);



                            }
                        }



                        _db.Product.Add(product);

                        _db.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    else
                    {


                        var product = _db.Product.AsNoTracking().Include(i => i.Shops).Include(i => i.User).FirstOrDefault(i => i.ProductId == productUpsertVM.Product.ProductId);

                        _db.Product.Attach(productUpsertVM.Product);
                        //_db.Entry(productUpsertVM.Product).State = EntityState.Modified;


                        //product.Shops = new List<Shop>();
                        productUpsertVM.Product.Shops = new List<Shop>();
                        foreach (var shop in allShops)
                        {
                            string id = shop.ShopId.ToString();
                            if (HttpContext.Request.Form[id] == shop.ShopId.ToString())
                            {
                                productUpsertVM.CurrentShopIds.Add(shop.ShopId.ToString());

                                productUpsertVM.Product.Shops.Add(shop);

                                //product.Shops.Add(shop);
                            }
                        }
                        //productUpsertVM.Product.

                        //product.Name = productUpsertVM.Product.Name;
                        //product.Description = productUpsertVM.Product.Description;
                        //product.ShortDescription = productUpsertVM.Product.ShortDescription;
                        //product.Price = productUpsertVM.Product.Price;
                        //product.Quantity = productUpsertVM.Product.Quantity;
                        //product.IsApi = false;



                        //sadfsdfsdfsd
                        //sdfsdfsdf
                        //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf                       //sadfsdfsdfsd
                        //sdfsdfsdf


















                        //if (files.Count > 0)
                        //{
                        //    //the path to which the image has to be copied to
                        //    string upload = webRootPath + WC.ImagePathProducts;

                        //    //generate a ui as a file name
                        //    string fileName = Guid.NewGuid().ToString();

                        //    //get the extension of the file
                        //    string extension = Path.GetExtension(files[0].FileName);

                        //    //get the directory of the old image
                        //    var oldFile = Path.Combine(upload, product.Image);

                        //    //check if the file exists in the system
                        //    if (System.IO.File.Exists(oldFile))
                        //    {
                        //        //delete file from system
                        //        System.IO.File.Delete(oldFile);
                        //    }

                        //    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        //    {
                        //        files[0].CopyTo(fileStream);
                        //    }

                        //    //add the new file name to the object received from view
                        //    productUpsertVM.Product.Image = fileName + extension;
                        //    product.Image = fileName + extension;
                        //    product.IsApi = false;
                        //}
                        //else
                        //{
                        //add the old file name to the object recevied from view
                        productUpsertVM.Product.Image = product.Image;
                        //}



                        //_db.Entity(productUpsertVM.Product).State = EntityState.Modified;
                        //_db.Update(productUpsertVM.Product);
                        _db.SaveChanges();

                        return RedirectToAction("Index");
                        //return View(productVM);
                    }
                }



                return View(productUpsertVM);//we return the obj back to the view to display the error messages to be displayed
            }


            //user not logged in , session expired , return 404 for now 
            return NotFound();
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



            var obj = _db.Product.Include(id => id.Shops).FirstOrDefault(i=>i.ProductId==id);

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
        public IActionResult DeletePost(int? id)
        {

            //Find retreies result based on the Primary key provided

            var obj = _db.Product.Include(i=>i.Shops).Include(i=>i.User).FirstOrDefault(i=>i.ProductId==id);
            if (obj == null)
            {
                return NotFound();
            }



            //remove Product object to the database
            _db.Product.Remove(obj);
            //save the changes
            _db.SaveChanges();
            // redirect to another action ; in this case, to index.
            // Since , we are redirecting to the same controller, we don't need to indicate the controller

            return RedirectToAction("Index");

     
        }



        //DELETE API
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }


            Product product = _db.Product.Include(i => i.Shops).Include(i => i.User).FirstOrDefault(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Product.Remove(product);

            _db.SaveChanges();

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(product, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Ok(response);
        }












    }
}