using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrossoverShoppingCart.CustomFilters;
using CrossoverShoppingCart.Models;

namespace CrossoverShoppingCart.Controllers
{
    public class ProductController : Controller
    {        
        // GET: /Product/
        [AuthorizeLog(Roles = "Operator")]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Cart");
        }

        //
        // GET: /Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Product/Create
        [AuthorizeLog(Roles = "Operator")]
        public ActionResult Create()
        {
            return View(new ProductViewModels());
        }

        //
        // POST: /Product/Create
        [HttpPost]
        [AuthorizeLog(Roles = "Operator")]
        public ActionResult Create(ProductViewModels newproduct)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    using (CrossOverShoppingCartDBEntities ce = new CrossOverShoppingCartDBEntities())
                    {
                        ce.Products.Add(new Product() {
                            ProductName = newproduct.ProductName,
                            Price=newproduct.Price,
                            IsActive= newproduct.IsActive?"Y":"N",
                            Cr_datetime= DateTime.Now,
                            Mo_datetime = DateTime.Now
                        });
                        ce.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
            return View(newproduct);
           
        }

        public ActionResult Error()
        {
            
            return View();
        }
        //
        // GET: /Product/Edit/5
        [AuthorizeLog(Roles = "Operator")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        [AuthorizeLog(Roles = "Operator")]
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
        // GET: /Product/Delete/5
        [AuthorizeLog(Roles = "Operator")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Product/Delete/5
        [HttpPost]
        [AuthorizeLog(Roles = "Operator")]
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
