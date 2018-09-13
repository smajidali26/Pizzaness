using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OrderDetailSubProductId Class.
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
	/// 		<description>9/10/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class OrderDetailSubProduct
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OrderDetailSubProductId class.
		/// </summary>
		public OrderDetailSubProduct()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderDetailSubProductId class.
		/// </summary>
		public OrderDetailSubProduct(Int32 OrderDetailSubProductId, Int32 ProductId, Int32 Quantity, Double Price, String RecipientName, String Comments, Int64 OrderDetailID)
		{
			this.OrderDetailSubProductId = OrderDetailSubProductId;
			this.ProductId = ProductId;
			this.Quantity = Quantity;
			this.Price = Price;
			this.RecipientName = RecipientName;
			this.Comments = Comments;
			this.OrderDetailID = OrderDetailID;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OrderDetailSubProductId value.
		/// </summary>
		public virtual Int32 OrderDetailSubProductId { get; set; }

		/// <summary>
		/// Gets or sets the ProductId value.
		/// </summary>
		public virtual Int32 ProductId { get; set; }

		/// <summary>
		/// Gets or sets the Quantity value.
		/// </summary>
		public virtual Int32 Quantity { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Double Price { get; set; }

		/// <summary>
		/// Gets or sets the RecipientName value.
		/// </summary>
		public virtual String RecipientName { get; set; }

		/// <summary>
		/// Gets or sets the Comments value.
		/// </summary>
		public virtual String Comments { get; set; }

		/// <summary>
		/// Gets or sets the OrderDetailID value.
		/// </summary>
		public virtual Int64 OrderDetailID { get; set; }

        /// <summary>
        /// Gets or sets the CrustType value.
        /// </summary>
        public virtual String CrustType { get; set; }

	    #endregion

        #region Non Table Properties
        public String OrderDetailSubProductOptionsXml {get;set;}
        
        [XmlIgnore]
		public List<OrderDetailSubProductOption>  OrderDetailSubProductOptions {get;set;}

        public String OrderDetailSubProductAdonsXml {get;set;}
        
        [XmlIgnore]
        public List<OrderDetailSubProductAdon> OrderDetailSubProductAdons { get; set; }

        public String ProductName { get; set; }
	    
        #endregion
	}
}