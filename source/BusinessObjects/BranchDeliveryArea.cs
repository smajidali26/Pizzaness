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
    
    public partial class BranchDeliveryArea
    {
        public long BranchDeliveryID { get; set; }
        public int BranchID { get; set; }
        public int ZipCode { get; set; }
    
        public virtual Branch Branch { get; set; }
    }
}
