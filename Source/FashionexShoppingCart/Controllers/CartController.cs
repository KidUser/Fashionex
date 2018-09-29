using CrossoverShoppingCart.CustomFilters;
using CrossoverShoppingCart.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace CrossoverShoppingCart.Controllers
{
    public class CartController : Controller
    {

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            var cartOnHold = null as List<ShoppingCart>;
            if (Request.IsAuthenticated)
            {
                using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
                {
                    List<ShoppingCart> prevCart = ce.ShoppingCarts.Where(u => u.UserName == (User.Identity).Name).ToList();
                    System.Web.HttpContext.Current.Session["userShoppingCart"] = prevCart;
                    System.Web.HttpContext.Current.Session["userShoppingCartCount"] = prevCart.Count;
                }
            }
            else
            {
                cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
                if (cartOnHold == null)
                {
                    cartOnHold = new List<ShoppingCart>();
                    System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;// check from db
                    System.Web.HttpContext.Current.Session["userShoppingCartCount"] = 0;// check from db
                }
            }
            List<Product> products = new List<Product>();
            using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
            {
                var a = ce.Products.ToList();
                var b = a.Where(pr => pr.IsActive.ToLower().Equals("y")).Select(p =>
                    new Product()
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        Price = p.Price
                    }).ToList();
                products.AddRange(a);
            }
            return View(products);
        }

        public ActionResult Add(int id)
        {
            var cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            var cartCount = (int)(System.Web.HttpContext.Current.Session["userShoppingCartCount"]);
            var existingProduct = cartOnHold.Where(p => p.ProductID == id).Any();
            using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
            {
                var products = ce.Products.ToList();
                if (cartOnHold.Where(p => p.ProductID == id).Any())
                {
                    cartOnHold.Where(i => i.ProductID == id).First().ProductQuantity += 1;
                    if (Request.IsAuthenticated)
                    {
                        ce.ShoppingCarts.Where(i => i.ProductID == id).First().ProductQuantity += 1;
                        ce.SaveChanges();
                    }
                }
                else
                {
                    var product = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && (pr.ProductID == id)).FirstOrDefault();
                    cartOnHold.Add(new ShoppingCart() { ProductID = product.ProductID, ProductQuantity = 1, UserName = (User.Identity).Name });
                    if (Request.IsAuthenticated)
                    {
                       // var crt = new ShoppingCart() { ProductID = product.ProductID, ProductQuantity = 1, UserName = (User.Identity).Name };
                        var crt2 = ce.ShoppingCarts.Create();
                        crt2.ProductID = product.ProductID;
                        crt2.ProductQuantity = 1;
                        crt2.UserName = (User.Identity).Name;
                        crt2.Cr_datetime = DateTime.Now;
                        crt2.Mo_datetime = DateTime.Now;
                        ce.ShoppingCarts.Add(crt2);
                        ce.SaveChanges();
                    }
                    System.Web.HttpContext.Current.Session["userShoppingCartCount"] = ++cartCount;
                }
                System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;
                return View("Index", products);
            }

            //return View();
        }

        //
        // GET: /Cart/Details/5
        public ActionResult CheckoutDetails()
        {
            var cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
            {
                var cartItemIds = cartOnHold.Select(c => c.ProductID).ToList();
                var products = ce.Products.ToList();
                var viewCartProducts = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && cartItemIds.Contains(pr.ProductID)).ToList();

                List<CartViewModels> carDetModel = new List<CartViewModels>();
                foreach (var item in cartOnHold)
                {
                    var product = viewCartProducts.Find(p => p.ProductID == item.ProductID);
                    var cvm = new CartViewModels();
                    cvm.ProductID = product.ProductID;
                    cvm.ProductName = product.ProductName;
                    cvm.Quantity = item.ProductQuantity;
                    cvm.Price = product.Price;
                    carDetModel.Add(cvm);
                }
                decimal cartTotalPrice = 0;
                carDetModel.ForEach(i => { cartTotalPrice += i.TotalPrice; });
                ViewBag.TotalPrice = cartTotalPrice;
                return View("CheckoutDetails", carDetModel);
            }
        }

        public ActionResult InstantAdd(int id)
        {
            var cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            var cartCount = (int)(System.Web.HttpContext.Current.Session["userShoppingCartCount"]);
            var existingProduct = cartOnHold.Where(p => p.ProductID == id).Any();
            using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
            {
                var products = ce.Products.ToList();
                if (cartOnHold.Where(p => p.ProductID == id).Any())
                {
                    cartOnHold.Where(i => i.ProductID == id).First().ProductQuantity += 1;
                    if (Request.IsAuthenticated)
                    {
                        ce.ShoppingCarts.Where(i => i.ProductID == id).First().ProductQuantity += 1;
                        ce.SaveChanges();
                    }
                }
                else
                {
                    var product = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && (pr.ProductID == id)).FirstOrDefault();
                    cartOnHold.Add(new ShoppingCart() { ProductID = product.ProductID, ProductQuantity = 1, UserName = (User.Identity).Name });
                    if (Request.IsAuthenticated)
                    {
                        ce.ShoppingCarts.Add(new ShoppingCart() { ProductID = product.ProductID, ProductQuantity = 1, UserName = (User.Identity).Name });
                        ce.SaveChanges();
                    }
                    System.Web.HttpContext.Current.Session["userShoppingCartCount"] = ++cartCount;
                }
                System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;
                return RedirectToAction("CheckoutDetails", "Cart");
            }

            //return View();
        }
        public ActionResult InstantDelete(int id)
        {
            var cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            var cartCount = (int)(System.Web.HttpContext.Current.Session["userShoppingCartCount"]);
            var existingProduct = cartOnHold.Where(p => p.ProductID == id).Any();
            using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
            {
                var products = ce.Products.ToList();
                if (cartOnHold.Where(p => p.ProductID == id).Any())
                {
                    cartOnHold.Where(i => i.ProductID == id).First().ProductQuantity -= 1;
                    if (Request.IsAuthenticated)
                    {
                        ce.ShoppingCarts.Where(i => i.ProductID == id).First().ProductQuantity -= 1;
                        ce.SaveChanges();
                    }
                }
                var remItems = cartOnHold.Where(p => p.ProductQuantity == 0).ToList();
                if (remItems.Any())
                {
                    foreach (var item in remItems)
                    {
                        cartOnHold.Remove(item);
                        if (Request.IsAuthenticated)
                        {
                            var rItem= ce.ShoppingCarts.Where(i=>i.ProductID==item.ProductID).First();
                            ce.ShoppingCarts.Remove(rItem);
                            ce.SaveChanges();
                        }
                    }

                    System.Web.HttpContext.Current.Session["userShoppingCartCount"] = --cartCount;
                }
                System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;

                return RedirectToAction("CheckoutDetails", "Cart");
            }
        }

        //
        // GET: /Cart/Create
        [AuthorizeLog(Roles = "ApplicationUser")]
        public ActionResult ProceedPayment()
        {
            var cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            if (cartOnHold != null && cartOnHold.Count > 0)
            {
                using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
                {
                    var cartItemIds = cartOnHold.Select(c => c.ProductID).ToList();
                    var products = ce.Products.ToList();
                    var viewCartProducts = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && cartItemIds.Contains(pr.ProductID)).ToList();

                    List<CartViewModels> carDetModel = new List<CartViewModels>();
                    foreach (var item in cartOnHold)
                    {
                        var product = viewCartProducts.Find(p => p.ProductID == item.ProductID);
                        var cvm = new CartViewModels();
                        cvm.ProductName = product.ProductName;
                        cvm.Quantity = item.ProductQuantity;
                        cvm.Price = product.Price;
                        carDetModel.Add(cvm);
                    }
                    decimal cartTotalPrice = 0;
                    carDetModel.ForEach(i => { cartTotalPrice += i.TotalPrice; });
                    //ViewBag.TotalPrice = cartTotalPrice;
                    PaymentViewModels pvm = new PaymentViewModels();
                    pvm.cartDetailModel = carDetModel;
                    pvm.PaymentAmount = cartTotalPrice;
                    return View("ProceedPayment", pvm);
                }
            }
            else
            {
                return RedirectToAction("CheckoutDetails", "Cart");
            }
        }


        protected string paymetUrlBase = "http://localhost:12260/api/Payment/";

        //
        // POST: /Cart/Create
        [HttpPost]
        [AuthorizeLog(Roles = "ApplicationUser")]
        public ActionResult ProceedPayment(PaymentViewModels payObj)
        {
            if (payObj != null && ModelState.IsValid)
            {

                BinaryFormatter bf = new BinaryFormatter();
                var bytPayObj = null as byte[];
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, payObj);
                    bytPayObj = ms.ToArray();
                }
                try
                {
                    WebClient client = new WebClient();
                    string url = paymetUrlBase;

                    client.UseDefaultCredentials = true;
                    //client.UploadData(paymetUrlBase, bytPayObj);
                    client.DownloadString(url);
                    //get the latest payment-id for the user action
                    var paymentTransactionId = 12345;
                    //confirm the result
                    string result = JsonConvert.DeserializeObject<string>(client.DownloadString(url + paymentTransactionId));

                    if (result != null && result.ToLower().Contains("success"))
                    {      
                        return RedirectToAction("OrderConformation");//,payObj);
                    }
                    else
                    {
                        payObj.PaymentFailureMessage = "Payment unsuccessful! please try again.";
                        return View(payObj);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Something went wrong!!! Sorry Try again.";
                    return View("Error");
                }

            }
            var cartOnHold = (List<ShoppingCart>)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            if (cartOnHold != null && cartOnHold.Count > 0)
            {
                using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
                {
                    var cartItemIds = cartOnHold.Select(c => c.ProductID).ToList();
                    var products = ce.Products.ToList();
                    var viewCartProducts = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && cartItemIds.Contains(pr.ProductID)).ToList();

                    List<CartViewModels> carDetModel = new List<CartViewModels>();
                    foreach (var item in cartOnHold)
                    {
                        var product = viewCartProducts.Find(p => p.ProductID == item.ProductID);
                        var cvm = new CartViewModels();
                        cvm.ProductName = product.ProductName;
                        cvm.Quantity = item.ProductQuantity;
                        cvm.Price = product.Price;
                        carDetModel.Add(cvm);
                    }
                    decimal cartTotalPrice = 0;
                    carDetModel.ForEach(i => { cartTotalPrice += i.TotalPrice; });
                    payObj.cartDetailModel = carDetModel;
                    payObj.PaymentAmount = cartTotalPrice;
                }
            }
            return View(payObj);
        }

        public ActionResult OrderConformation()
        {
            ViewBag.Message = "Congrats! Order conformed. Thank you for being with us.";
            using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
            {
                List<ShoppingCart> prevCart = ce.ShoppingCarts.Where(u => u.UserName == (User.Identity).Name).ToList();
                ce.ShoppingCarts.RemoveRange(prevCart);
                ce.SaveChanges();
            }
            Session.Clear();
            return View();
        }

        public ActionResult OrderHistory()
        {

            return View();
        }
        //
        // GET: /Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
