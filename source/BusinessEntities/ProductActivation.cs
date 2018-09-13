using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductActivation Class.
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
	/// 		<description>10/7/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class ProductActivation
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductActivation class.
		/// </summary>
		public ProductActivation()
		{
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ProductActivationId value.
		/// </summary>
		public virtual Int32 ProductActivationId { get; set; }

		/// <summary>
		/// Gets or sets the ProductID value.
		/// </summary>
		public virtual Int32 ProductID { get; set; }

		/// <summary>
		/// Gets or sets the Days value.
		/// </summary>
		public virtual String Days { get; set; }

		/// <summary>
		/// Gets or sets the DisplayOnFullDay value.
		/// </summary>
		public virtual Boolean DisplayOnFullDay { get; set; }

		/// <summary>
		/// Gets or sets the StartTime value.
		/// </summary>
        public virtual String StartTime { get; set; }

		/// <summary>
		/// Gets or sets the EndTime value.
		/// </summary>
        public virtual String EndTime { get; set; }

		#endregion
	}
}