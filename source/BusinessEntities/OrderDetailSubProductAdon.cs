using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OrderDetailSubProductAdon Class.
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
	public class OrderDetailSubProductAdon
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OrderDetailSubProductAdon class.
		/// </summary>
		public OrderDetailSubProductAdon()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderDetailSubProductAdon class.
		/// </summary>
		public OrderDetailSubProductAdon(Int32 OrderDetailSubProductAdonId, Int32 OrderDetailSubProductId, Int16 AdOnId, String AdonName, Int16 SelectedAdonOption, Boolean IsDoubleSelected)
		{
			this.OrderDetailSubProductAdonId = OrderDetailSubProductAdonId;
			this.OrderDetailSubProductId = OrderDetailSubProductId;
			this.AdOnId = AdOnId;
			this.AdonName = AdonName;
			this.SelectedAdonOption = SelectedAdonOption;
			this.IsDoubleSelected = IsDoubleSelected;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OrderDetailSubProductAdonId value.
		/// </summary>
		public virtual Int32 OrderDetailSubProductAdonId { get; set; }

		/// <summary>
		/// Gets or sets the OrderDetailSubProductId value.
		/// </summary>
		public virtual Int32 OrderDetailSubProductId { get; set; }

		/// <summary>
		/// Gets or sets the AdOnId value.
		/// </summary>
		public virtual Int16 AdOnId { get; set; }

		/// <summary>
		/// Gets or sets the AdonName value.
		/// </summary>
		public virtual String AdonName { get; set; }

		/// <summary>
		/// Gets or sets the SelectedAdonOption value.
		/// </summary>
		public virtual Int16 SelectedAdonOption { get; set; }

		/// <summary>
		/// Gets or sets the IsDoubleSelected value.
		/// </summary>
		public virtual Boolean IsDoubleSelected { get; set; }

        /// <summary>
        /// Gets or sets the Price value.
        /// </summary>
        public virtual Double Price { get; set; }

		#endregion

        #region Non Table Properties
        /// <summary>
		/// Gets or sets the AdonTypeName value.
		/// </summary>
		public virtual String AdonTypeName { get; set; }
        #endregion
    }
}