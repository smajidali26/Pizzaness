using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductChildProductOption Class.
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
	/// 		<description>8/28/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class ProductChildProductOption
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductChildProductOption class.
		/// </summary>
		public ProductChildProductOption()
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ProductChildProductOptionId value.
		/// </summary>
		public virtual Int32 ProductChildProductOptionId { get; set; }

		/// <summary>
		/// Gets or sets the ComboId value.
		/// </summary>
		public virtual Int32 ComboId { get; set; }

		/// <summary>
		/// Gets or sets the OptionId value.
		/// </summary>
		public virtual Int32 OptionId { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Double Price { get; set; }
		#endregion

        #region Methods
        


        #endregion
    }
}