using FashinexShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashinexShoppingCart.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            var active_session = System.Web.HttpContext.Current.Session["userShoppingCart"];
            var cartOnHold = null as ShoppingCart;
            if (active_session != null)
            {

            }
            else
            {
                cartOnHold = new ShoppingCart();
                System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;// check from db
                System.Web.HttpContext.Current.Session["userShoppingCartCount"] = 0;// check from db
            }
            List<PRODUCT> products = new List<PRODUCT>();
            using (FashioNEXDBEntities ce = new FashioNEXDBEntities())
            {
                var a = ce.PRODUCTS.ToList();
                var b = a.Where(pr => pr.IsActive.ToLower().Equals("y") && pr.Quantity > 0).Select(p =>
                    new PRODUCT()
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Quantity = p.Quantity
                    }).ToList();
                products.AddRange(a);
            }
            return View(products);
        }

        public ActionResult Add(int id)
        {
            var active_session = System.Web.HttpContext.Current.Session["userShoppingCart"];
            var cartOnHold = null as ShoppingCart;
            var cartCount = 0;
            if (active_session != null)
            {
                cartOnHold = (ShoppingCart)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
                cartCount = (int)(System.Web.HttpContext.Current.Session["userShoppingCartCount"]);
            }
            else
            {
                cartOnHold = new ShoppingCart();
                System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;// check from db
                System.Web.HttpContext.Current.Session["userShoppingCartCount"] = 0;// check from db
            }
            cartOnHold.ShoppingCart_Item.Where(i => i.ProductID == id).Any();
            using (FashioNEXDBEntities ce = new FashioNEXDBEntities())
            {
                var products = ce.PRODUCTS.ToList();
                if (cartOnHold.ShoppingCart_Item.Where(p => p.ProductID == id).Any())
                {
                    var cartItem = cartOnHold.ShoppingCart_Item.Where(i => i.ProductID == id).FirstOrDefault();
                    if (cartItem != null)
                    {
                        cartItem.ProductQuantity += 1;
                    }
                }
                else
                {
                    var productItem = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && (pr.ProductID == id)).FirstOrDefault();
                    cartOnHold.ShoppingCart_Item.Add(new ShoppingCart_Item() { ProductID = productItem.ProductID, ProductQuantity = 1, PRODUCT = productItem });
                    System.Web.HttpContext.Current.Session["userShoppingCart"] = cartOnHold;
                    System.Web.HttpContext.Current.Session["userShoppingCartCount"] = ++cartCount;
                    productItem.Quantity -= 1;
                }

                cartOnHold.TotalItem = cartOnHold.ShoppingCart_Item.Sum(item => item.ProductQuantity);
                cartOnHold.TotalCost = cartOnHold.ShoppingCart_Item.Sum(item => item.ProductQuantity * item.PRODUCT.Price);
                products = ce.PRODUCTS.Where(pr => (pr.IsActive.ToLower().Equals("y")) && pr.Quantity > 0).ToList();
                ce.SaveChanges();
                return View("Index", products);
            }
            //return View();
        }

        //
        // GET: /Cart/Details/5
        public ActionResult CheckoutDetails()
        {
            var cartOnHold = (ShoppingCart)(System.Web.HttpContext.Current.Session["userShoppingCart"]);
            List<CartViewModels> carDetModel = new List<CartViewModels>();
            using (FashioNEXDBEntities ce = new FashioNEXDBEntities())
            {
                var cartItemIds = cartOnHold.ShoppingCart_Item.Select(c => c.ProductID).ToList();
                var products = ce.PRODUCTS.ToList();
                var viewCartProducts = products.Where(pr => (pr.IsActive.ToLower().Equals("y")) && cartItemIds.Contains(pr.ProductID)).ToList();

                carDetModel = new List<CartViewModels>();
                foreach (var item in cartOnHold.ShoppingCart_Item)
                {
                    var product = viewCartProducts.Find(p => p.ProductID == item.ProductID);
                    var cvm = new CartViewModels();
                    cvm.ProductName = product.ProductName;
                    cvm.Quantity = item.ProductQuantity;
                    cvm.Price = product.Price;
                    carDetModel.Add(cvm);
                }

            }
            return View("CheckoutDetails", carDetModel);
        }

        //
        // GET: /Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
