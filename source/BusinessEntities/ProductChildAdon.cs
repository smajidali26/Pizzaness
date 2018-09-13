using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductChildAdon Class.
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
	/// 		<description>9/4/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class ProductChildAdon
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductChildAdon class.
		/// </summary>
		public ProductChildAdon()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProductChildAdon class.
		/// </summary>
		public ProductChildAdon(Int32 ProductChildAdonId, Int32 ComboId, Int16 AdonId, Int16 DefaultSelected)
		{
			this.ProductChildAdonId = ProductChildAdonId;
			this.ComboId = ComboId;
			this.AdonId = AdonId;
			this.DefaultSelected = DefaultSelected;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ProductChildAdonId value.
		/// </summary>
		public virtual Int32 ProductChildAdonId { get; set; }

		/// <summary>
		/// Gets or sets the ComboId value.
		/// </summary>
		public virtual Int32 ComboId { get; set; }

		/// <summary>
		/// Gets or sets the AdonId value.
		/// </summary>
		public virtual Int16 AdonId { get; set; }

		/// <summary>
		/// Gets or sets the DefaultSelected value.
		/// </summary>
		public virtual Int16 DefaultSelected { get; set; }
		#endregion
	}
}