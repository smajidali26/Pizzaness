using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// Branches Class.
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
	public class Branches
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the Branches class.
		/// </summary>
		public Branches()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Branches class.
		/// </summary>
		public Branches(Int32 BranchID, String Title, String Address, String City, String State, String Zip, String Phone, String Fax, DateTime WorkingHourStart, DateTime WorkingHourEnd, Decimal TaxPercentage, Boolean IsDeliveryEnabled, Decimal DeliveryCharges, Boolean IsActive)
		{
			this.BranchID = BranchID;
			this.Title = Title;
			this.Address = Address;
			this.City = City;
			this.State = State;
			this.Zip = Zip;
			this.Phone = Phone;
			this.Fax = Fax;
			this.WorkingHourStart = WorkingHourStart;
			this.WorkingHourEnd = WorkingHourEnd;
			this.TaxPercentage = TaxPercentage;
			this.IsDeliveryEnabled = IsDeliveryEnabled;
			this.DeliveryCharges = DeliveryCharges;
			this.IsActive = IsActive;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the BranchID value.
		/// </summary>
		public virtual Int32 BranchID { get; set; }

		/// <summary>
		/// Gets or sets the Title value.
		/// </summary>
		public virtual String Title { get; set; }

		/// <summary>
		/// Gets or sets the Address value.
		/// </summary>
		public virtual String Address { get; set; }

		/// <summary>
		/// Gets or sets the City value.
		/// </summary>
		public virtual String City { get; set; }

		/// <summary>
		/// Gets or sets the State value.
		/// </summary>
		public virtual String State { get; set; }

		/// <summary>
		/// Gets or sets the Zip value.
		/// </summary>
		public virtual String Zip { get; set; }

		/// <summary>
		/// Gets or sets the Phone value.
		/// </summary>
		public virtual String Phone { get; set; }

		/// <summary>
		/// Gets or sets the Fax value.
		/// </summary>
		public virtual String Fax { get; set; }

		/// <summary>
		/// Gets or sets the WorkingHourStart value.
		/// </summary>
		public virtual DateTime WorkingHourStart { get; set; }

		/// <summary>
		/// Gets or sets the WorkingHourEnd value.
		/// </summary>
		public virtual DateTime WorkingHourEnd { get; set; }

		/// <summary>
		/// Gets or sets the TaxPercentage value.
		/// </summary>
		public virtual Decimal TaxPercentage { get; set; }

		/// <summary>
		/// Gets or sets the IsDeliveryEnabled value.
		/// </summary>
		public virtual Boolean IsDeliveryEnabled { get; set; }

		/// <summary>
		/// Gets or sets the DeliveryCharges value.
		/// </summary>
		public virtual Decimal DeliveryCharges { get; set; }

		/// <summary>
		/// Gets or sets the IsActive value.
		/// </summary>
		public virtual Boolean IsActive { get; set; }
		#endregion
	}
}