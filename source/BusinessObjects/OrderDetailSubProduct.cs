//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessObjects
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetailSubProduct
    {
        public int OrderDetailSubProductId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string RecipientName { get; set; }
        public string Comments { get; set; }
        public long OrderDetailID { get; set; }
    
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
