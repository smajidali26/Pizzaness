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
    
    public partial class Order_GetOrder_Result
    {
        public long OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public long ContactInfoId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerTelephone { get; set; }
        public string CustomerMobile { get; set; }
        public int OrderStatusID { get; set; }
        public int BranchID { get; set; }
        public double OrderTotal { get; set; }
        public Nullable<double> DeliveryCharges { get; set; }
        public string DeliveryAddress { get; set; }
        public short OrderTypeID { get; set; }
        public Nullable<double> Discount { get; set; }
        public double TaxPercentage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<int> TotalRows { get; set; }
    }
}
