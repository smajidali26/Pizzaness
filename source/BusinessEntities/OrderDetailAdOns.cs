using System;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OrderDetailAdOns Class.
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
    [XmlRoot(ElementName = "OrderDetailAdOn")]
	public class OrderDetailAdOns
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OrderDetailAdOns class.
		/// </summary>
		public OrderDetailAdOns()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderDetailAdOns class.
		/// </summary>
		public OrderDetailAdOns(Int64 OrderDetailId, Int16 AdOnId, String AdonName, SelectedOption SelectedAdonOption, Boolean IsDoubleSelected)
		{
			this.OrderDetailId = OrderDetailId;
			this.AdOnId = AdOnId;
			this.AdonName = AdonName;
			this.SelectedAdonOption = SelectedAdonOption;
			this.IsDoubleSelected = IsDoubleSelected;
		}
		#endregion

		#region Properties
        /// <summary>
        /// Gets or sets the OrderDetailAdonId value.
        /// </summary>
        public virtual Int64 OrderDetailAdonId { get; set; }

		/// <summary>
		/// Gets or sets the OrderDetailId value.
		/// </summary>
		public virtual Int64 OrderDetailId { get; set; }

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
        public virtual SelectedOption SelectedAdonOption { get; set; }

		/// <summary>
		/// Gets or sets the IsDoubleSelected value.
		/// </summary>
		public virtual Boolean IsDoubleSelected { get; set; }

        /// <summary>
        /// Gets or sets the AdonTypeName value.
        /// </summary>
        public virtual String AdonTypeName { get; set; }
		#endregion
                
    }
}