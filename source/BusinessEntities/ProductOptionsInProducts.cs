using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductOptionsInProducts Class.
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
	public class ProductOptionsInProducts
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductOptionsInProducts class.
		/// </summary>
		public ProductOptionsInProducts()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProductOptionsInProducts class.
		/// </summary>
		public ProductOptionsInProducts(Int64 ProductOptionsInProductID, Int64 ProductsOptionTypeId, Int32 ProductOptionID, Boolean Enabled, Decimal Price, Decimal ToppingPrice)
		{
			this.ProductOptionsInProductID = ProductOptionsInProductID;
			this.ProductsOptionTypeId = ProductsOptionTypeId;
			this.ProductOptionID = ProductOptionID;
			this.Enabled = Enabled;
			this.Price = Price;
			this.ToppingPrice = ToppingPrice;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ProductOptionsInProductID value.
		/// </summary>
		public virtual Int64 ProductOptionsInProductID { get; set; }

		/// <summary>
		/// Gets or sets the ProductsOptionTypeId value.
		/// </summary>
		public virtual Int64 ProductsOptionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the ProductOptionID value.
		/// </summary>
		public virtual Int32 ProductOptionID { get; set; }

		/// <summary>
		/// Gets or sets the Enabled value.
		/// </summary>
		public virtual Boolean Enabled { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Decimal? Price { get; set; }

		/// <summary>
		/// Gets or sets the ToppingPrice value.
		/// </summary>
		public virtual Decimal? ToppingPrice { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOrder value.
        /// </summary>
        public virtual Int16 DisplayOrder { get; set; }
		#endregion
    }
}