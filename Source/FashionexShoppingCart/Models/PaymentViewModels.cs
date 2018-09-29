using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CrossoverShoppingCart.Models
{
    [Serializable]
    public class PaymentViewModels 
    {
        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public string CardExpiryDate { get; set; }

        [Required]
        [Display(Name = "Credit Card(Y/N)")]
        public bool CardTypeCode { get; set; }
        
        [Required]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        public List<CartViewModels> cartDetailModel { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentFailureMessage { get; set; }

    }
}
