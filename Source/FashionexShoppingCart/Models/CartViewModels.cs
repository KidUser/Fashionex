using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CrossoverShoppingCart.Models
{
    [Serializable]
    public class CartViewModels
    {
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }  

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }               
        
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get { return Quantity * Price; } }

    }
}
