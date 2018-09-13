using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using Core;
namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OrderDetails Class.
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
    public class OrderDetails : BaseClass
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OrderDetails class.
		/// </summary>
		public OrderDetails()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderDetails class.
		/// </summary>
		public OrderDetails(Int64 OrderDetailID, Int64 OrderID, Int32 ProductID, Int32 Quantity, Double Price, Int32 ParentProductId, String RecipientName, String Comments)
		{
			this.OrderDetailID = OrderDetailID;
			this.OrderID = OrderID;
			this.ProductID = ProductID;
			this.Quantity = Quantity;
			this.Price = Price;
			this.ParentProductId = ParentProductId;
			this.RecipientName = RecipientName;
			this.Comments = Comments;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OrderDetailID value.
		/// </summary>
		public virtual Int64 OrderDetailID { get; set; }

		/// <summary>
		/// Gets or sets the OrderID value.
		/// </summary>
		public virtual Int64 OrderID { get; set; }

		/// <summary>
		/// Gets or sets the ProductID value.
		/// </summary>
		public virtual Int32 ProductID { get; set; }

		/// <summary>
		/// Gets or sets the Quantity value.
		/// </summary>
		public virtual Int32 Quantity { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Double Price { get; set; }

		/// <summary>
		/// Gets or sets the ParentProductId value.
		/// </summary>
		public virtual Int32 ParentProductId { get; set; }

		/// <summary>
		/// Gets or sets the RecipientName value.
		/// </summary>
		public virtual String RecipientName { get; set; }

		/// <summary>
		/// Gets or sets the Comments value.
		/// </summary>
		public virtual String Comments { get; set; }

        /// <summary>
        /// Gets or sets the IsGroupProduct value.
        /// </summary>
        public virtual bool IsGroupProduct { get; set; }

        /// <summary>
        /// OrderDetailOptionXml
        /// </summary>
        public virtual String OrderDetailOptionXml { get; set; }

        /// <summary>
        /// OrderDetailAdOnsXml
        /// </summary>
        public virtual String OrderDetailAdOnsXml { get; set; }

        /// <summary>
        /// Gets or sets the ProductName value.
        /// </summary>
        public virtual String ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProductImage value.
        /// </summary>
        public virtual String ProductImage { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName value.
        /// </summary>
        public virtual String CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the CrustType value.
        /// </summary>
        public virtual String CrustType { get; set; }

        /// <summary>
        /// Gets or sets the ItemTotal value.
        /// </summary>
        public virtual Double ItemTotal { get; set; }
		#endregion

        #region Non Table Properties

        [XmlIgnore]
        public List<OrderDetailSubProduct> OrderDetailSubProducts { get; set; }

        public String OrderDetailSubProductsXml { get; set; }
        #endregion
    }
}