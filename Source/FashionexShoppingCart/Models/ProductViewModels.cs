using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CrossoverShoppingCart.Models
{
    [Serializable]
    public class ProductViewModels 
    {
        public int ProductID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        public bool IsActive { get; set; }        

    }
}
