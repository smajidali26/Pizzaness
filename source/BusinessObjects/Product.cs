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
    
    public partial class Product
    {
        public Product()
        {
            this.ProductsInBranches = new HashSet<ProductsInBranch>();
        }
    
        public int ProductID { get; set; }
        public short CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsSpecial { get; set; }
        public Nullable<short> DisplayOrder { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductsInBranch> ProductsInBranches { get; set; }
    }
}
