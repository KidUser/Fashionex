using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashinexShoppingCart.Models;

namespace FashinexShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // FashinexShoppingCartDBEntities ce = new FashinexShoppingCartDBEntities();
           List<PRODUCT> products= new List<PRODUCT>();
            using(FashioNEXDBEntities ce = new FashioNEXDBEntities())
            {
                var a= ce.PRODUCTS.Where(pr=>pr.IsActive.ToLower().Equals("y")).Select(p => 
                    new PRODUCT() {
                        ProductID=p.ProductID, 
                        ProductName=p.ProductName,
                        Price=p.Price                        
                    });
            }
            return View(products);
        }

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
    }
}