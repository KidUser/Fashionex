﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FashioNEXDBEntities : DbContext
    {
        public FashioNEXDBEntities()
            : base("name=FashioNEXDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<BRAND> BRANDS { get; set; }
        public virtual DbSet<CART_PAYMENT_DETAILS> CART_PAYMENT_DETAILS { get; set; }
        public virtual DbSet<Category_Master> Category_Master { get; set; }
        public virtual DbSet<ORDERDETAIL> ORDERDETAILS { get; set; }
        public virtual DbSet<ORDER> ORDERS { get; set; }
        public virtual DbSet<ORDERS_HISTORY> ORDERS_HISTORY { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PAYMENT_TYPE> PAYMENT_TYPE { get; set; }
        public virtual DbSet<Product_Category_BRANDS> Product_Category_BRANDS { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Segment_Master> Segment_Master { get; set; }
        public virtual DbSet<SHIPPER> SHIPPERS { get; set; }
        public virtual DbSet<SHIPPING_TRACKING_DETAILS> SHIPPING_TRACKING_DETAILS { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCart_Item> ShoppingCart_Item { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PRODUCTFEEDBACK> PRODUCTFEEDBACKs { get; set; }
    }
}
