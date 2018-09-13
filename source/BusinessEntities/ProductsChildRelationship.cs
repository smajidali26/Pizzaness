using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductsChildRelationship Class.
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
	public class ProductsChildRelationship
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductsChildRelationship class.
		/// </summary>
		public ProductsChildRelationship()
		{
		}
		#endregion

		#region Properties

        /// <summary>
        /// Gets or sets the ComboId value.
        /// </summary>
        public virtual Int32 ComboId { get; set; }

		/// <summary>
		/// Gets or sets the ParentProductsId value.
		/// </summary>
		public virtual Int32 ParentProductsId { get; set; }

		/// <summary>
		/// Gets or sets the ChildProductId value.
		/// </summary>
		public virtual Int32 ChildProductId { get; set; }

        /// <summary>
        /// Gets or sets the Quantity value.
        /// </summary>
        public virtual Int16 Quantity { get; set; }

        /// <summary>
        /// Gets or sets the UnitPrice value.
        /// </summary>
        public virtual Double UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the NumberOfFreeTopping value.
        /// </summary>
        public virtual Int16 NumberOfFreeTopping { get; set; }

        /// <summary>
        /// Gets or sets the NumberOfFreeTopping value.
        /// </summary>
        public virtual bool IsCustomizable { get; set; }

		#endregion
	}
}