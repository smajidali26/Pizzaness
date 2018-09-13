using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductsInBranches Class.
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
	public class ProductsInBranches
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductsInBranches class.
		/// </summary>
		public ProductsInBranches()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProductsInBranches class.
		/// </summary>
		public ProductsInBranches(Int64 BranchProductID, Int32 ProductID, Int32 BranchID, Boolean Enable)
		{
			this.BranchProductID = BranchProductID;
			this.ProductID = ProductID;
			this.BranchID = BranchID;
			this.Enable = Enable;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the BranchProductID value.
		/// </summary>
		public virtual Int64 BranchProductID { get; set; }

		/// <summary>
		/// Gets or sets the ProductID value.
		/// </summary>
		public virtual Int32 ProductID { get; set; }

		/// <summary>
		/// Gets or sets the BranchID value.
		/// </summary>
		public virtual Int32 BranchID { get; set; }

		/// <summary>
		/// Gets or sets the Enable value.
		/// </summary>
		public virtual Boolean Enable { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Double Price { get; set; }
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the Primary Key of the object.
		/// </summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return "[ProductsInBranches] " + this.BranchProductID.ToString();
		}

		/// <summary>
		/// Returns true if the Ids of the two instances are equal.
		/// </summary>
		/// <param name="ObjectToCompare">The other object instance.</param>
		/// <returns>String</returns>
		public override Boolean Equals(Object ObjectToCompare)
		{
			if(ObjectToCompare == null) return false;
			ProductsInBranches otherObject = ObjectToCompare as ProductsInBranches;
			if (otherObject == null) return false;
			return ProductsInBranches.Equals(this, otherObject);
		}

		/// <summary>
		/// Returns the GetHashCode() method of the Primary Key member.
		/// </summary>
		/// <returns>String</returns>
		public override Int32 GetHashCode()
		{
			return this.BranchProductID.GetHashCode();
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