using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// BranchDeliveryArea Class.
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
	public class BranchDeliveryArea
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the BranchDeliveryArea class.
		/// </summary>
		public BranchDeliveryArea()
		{
		}

		/// <summary>
		/// Initializes a new instance of the BranchDeliveryArea class.
		/// </summary>
		public BranchDeliveryArea(Int64 BranchDeliveryID, Int32 BranchID, Int32 ZipCode)
		{
			this.BranchDeliveryID = BranchDeliveryID;
			this.BranchID = BranchID;
			this.ZipCode = ZipCode;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the BranchDeliveryID value.
		/// </summary>
		public virtual Int64 BranchDeliveryID { get; set; }

		/// <summary>
		/// Gets or sets the BranchID value.
		/// </summary>
		public virtual Int32 BranchID { get; set; }

		/// <summary>
		/// Gets or sets the ZipCode value.
		/// </summary>
		public virtual Int32 ZipCode { get; set; }
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the Primary Key of the object.
		/// </summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return "[BranchDeliveryArea] " + this.BranchDeliveryID.ToString();
		}

		/// <summary>
		/// Returns true if the Ids of the two instances are equal.
		/// </summary>
		/// <param name="ObjectToCompare">The other object instance.</param>
		/// <returns>String</returns>
		public override Boolean Equals(Object ObjectToCompare)
		{
			if(ObjectToCompare == null) return false;
			BranchDeliveryArea otherObject = ObjectToCompare as BranchDeliveryArea;
			if (otherObject == null) return false;
			return BranchDeliveryArea.Equals(this, otherObject);
		}

		/// <summary>
		/// Returns the GetHashCode() method of the Primary Key member.
		/// </summary>
		/// <returns>String</returns>
		public override Int32 GetHashCode()
		{
			return this.BranchDeliveryID.GetHashCode();
		}
		#endregion

		#region CRUD Methods
		/// <summary>
		/// Load a single record.
		/// </summary>
		public virtual void Load() { throw new NotImplementedException(); }

		/// <summary>
		/// Load all records.
		/// </summary>
		public virtual void LoadAll() { throw new NotImplementedException(); }

		/// <summary>
		/// Insert a new record.
		/// </summary>
		public virtual void Insert() { throw new NotImplementedException(); }

		/// <summary>
		/// Update existing record.
		/// </summary>
		public virtual void Update() { throw new NotImplementedException(); }

		/// <summary>
		/// Delete existing record.
		/// </summary>
		public virtual void Delete() { throw new NotImplementedException(); }
		#endregion
	}
}