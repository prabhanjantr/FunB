//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FunB.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblShoppingcart
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public decimal Product_Price { get; set; }
        public int CustomerId { get; set; }
    
        public virtual product product { get; set; }
        public virtual User User { get; set; }
    }
}
