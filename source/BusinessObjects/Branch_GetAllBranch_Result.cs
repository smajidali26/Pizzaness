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
    
    public partial class Branch_GetAllBranch_Result
    {
        public int BranchID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public System.DateTime WorkingHourStart { get; set; }
        public System.DateTime WorkingHourEnd { get; set; }
        public decimal TaxPercentage { get; set; }
        public bool IsDeliveryEnabled { get; set; }
        public Nullable<decimal> DeliveryCharges { get; set; }
        public bool IsActive { get; set; }
    }
}