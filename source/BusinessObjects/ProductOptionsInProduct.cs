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
    
    public partial class ProductOptionsInProduct
    {
        public long ProductOptionsInProductID { get; set; }
        public long ProductsOptionTypeId { get; set; }
        public int ProductOptionID { get; set; }
        public bool Enabled { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> ToppingPrice { get; set; }
        public Nullable<short> DisplayOrder { get; set; }
    
        public virtual OptionTypesInProduct OptionTypesInProduct { get; set; }
        public virtual ProductOption ProductOption { get; set; }
    }
}
