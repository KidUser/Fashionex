//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FashinexShoppingCart.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SHIPPING_TRACKING_DETAILS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SHIPPING_TRACKING_DETAILS()
        {
            this.ORDERS = new HashSet<ORDER>();
        }
    
        public string SHIPPING_TRACKINGID { get; set; }
        public string SHIPPERID { get; set; }
        public System.DateTime SHIPDATE { get; set; }
        public System.DateTime EXPECTEDDATE { get; set; }
        public string DELIVERY_STATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER> ORDERS { get; set; }
        public virtual SHIPPER SHIPPER { get; set; }
    }
}
