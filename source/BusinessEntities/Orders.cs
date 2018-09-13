using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Core;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// Orders Class.
	/// </summary>
	/// <remarks>
	/// <h3>Changes</h3>
	/// <list type="table">
	/// 	<listheader>
	/// 		<th>Author</th>
	/// 		<th>Date</th>
	/// 		<th>Details</th>
	/// 	</listheader>
	/// 	<item>
	/// 		<term>Majid Ali</term>
	/// 		<description>5/24/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
    public class Orders : BaseClass
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the Orders class.
		/// </summary>
		public Orders()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Orders class.
		/// </summary>
		public Orders(Int64 OrderID, DateTime OrderDate, DateTime DeliveryDate, Int64 ContactInfoId, OrderStatus OrderStatusID, Int32 BranchID, Double OrderTotal, Double DeliveryCharges, String DeliveryAddress, OrderType OrderTypeID, Double Discount, Double TaxPercentage, Boolean IsDeleted)
		{
			this.OrderID = OrderID;
			this.OrderDate = OrderDate;
			this.DeliveryDate = DeliveryDate;
			this.ContactInfoId = ContactInfoId;
			this.OrderStatusID = OrderStatusID;
			this.BranchID = BranchID;
			this.OrderTotal = OrderTotal;
			this.DeliveryCharges = DeliveryCharges;
			this.DeliveryAddress = DeliveryAddress;
			this.OrderTypeID = OrderTypeID;
			this.Discount = Discount;
			this.TaxPercentage = TaxPercentage;
			this.IsDeleted = IsDeleted;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OrderID value.
		/// </summary>
		public virtual Int64 OrderID { get; set; }

		/// <summary>
		/// Gets or sets the OrderDate value.
		/// </summary>
		public virtual DateTime OrderDate { get; set; }

		/// <summary>
		/// Gets or sets the DeliveryDate value.
		/// </summary>
		public virtual DateTime? DeliveryDate { get; set; }

		/// <summary>
		/// Gets or sets the ContactInfoId value.
		/// </summary>
		public virtual Int64 ContactInfoId { get; set; }

		/// <summary>
		/// Gets or sets the OrderStatusID value.
		/// </summary>
		public virtual OrderStatus OrderStatusID { get; set; }

		/// <summary>
		/// Gets or sets the BranchID value.
		/// </summary>
		public virtual Int32 BranchID { get; set; }

		/// <summary>
		/// Gets or sets the OrderTotal value.
		/// </summary>
		public virtual Double OrderTotal { get; set; }

		/// <summary>
		/// Gets or sets the DeliveryCharges value.
		/// </summary>
		public virtual Double DeliveryCharges { get; set; }

		/// <summary>
		/// Gets or sets the DeliveryAddress value.
		/// </summary>
		public virtual String DeliveryAddress { get; set; }

		/// <summary>
		/// Gets or sets the OrderTypeID value.
		/// </summary>
		public virtual OrderType OrderTypeID { get; set; }

		/// <summary>
		/// Gets or sets the Discount value.
		/// </summary>
		public virtual Double Discount { get; set; }

		/// <summary>
		/// Gets or sets the TaxPercentage value.
		/// </summary>
		public virtual Double TaxPercentage { get; set; }

		/// <summary>
		/// Gets or sets the IsDeleted value.
		/// </summary>
		public virtual Boolean IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the IsPaid value.
        /// </summary>
        public virtual Boolean IsPaid { get; set; }

        /// <summary>
        /// Gets or sets the PaymentMethod value.
        /// </summary>
        public virtual PaymentType PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the LineTip value.
        /// </summary>
        public virtual Double LineTip { get; set; }

        /// <summary>
        /// Gets or sets the PromotionCodeId value.
        /// </summary>
        public virtual Int32? PromotionCodeId { get; set; }

        /// <summary>
        /// Gets or sets the PromotionValueUsed value.
        /// </summary>
        public virtual double PromotionValueUsed { get; set; }

		#endregion

        #region Virtual Properties

        public virtual ICollection<OrderDetails> OrderDetailsList { get; set; }

        public virtual String OrderDetailXml { get; set; }

        public virtual String CustomerName { get; set; }

        public virtual String CustomerEmail { get; set; }

        public virtual String CustomerTelephone { get; set; }

        public virtual String CustomerMobile { get; set; }
        #endregion
    }
}