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
    
    public partial class OptionType
    {
        public OptionType()
        {
            this.OptionTypesInProducts = new HashSet<OptionTypesInProduct>();
            this.ProductOptions = new HashSet<ProductOption>();
        }
    
        public short OptionTypeID { get; set; }
        public string OptionTypeName { get; set; }
        public string OptionDisplayText { get; set; }
    
        public virtual ICollection<OptionTypesInProduct> OptionTypesInProducts { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}