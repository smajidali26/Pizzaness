using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductAdon Class.
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
	public class ProductAdon
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductAdon class.
		/// </summary>
		public ProductAdon()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProductAdon class.
		/// </summary>
		public ProductAdon(Int64 ProductAdOnID, Int64 ProductAdonTypeID, Int16 AdonID, Int16 DefaultSelected, Boolean Enable)
		{
			this.ProductAdOnID = ProductAdOnID;
			this.ProductAdonTypeID = ProductAdonTypeID;
			this.AdonID = AdonID;
			this.DefaultSelected = DefaultSelected;
			this.Enable = Enable;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ProductAdOnID value.
		/// </summary>
		public virtual Int64 ProductAdOnID { get; set; }

		/// <summary>
		/// Gets or sets the ProductAdonTypeID value.
		/// </summary>
		public virtual Int64 ProductAdonTypeID { get; set; }

		/// <summary>
		/// Gets or sets the AdonID value.
		/// </summary>
		public virtual Int16 AdonID { get; set; }

		/// <summary>
		/// Gets or sets the DefaultSelected value.
		/// </summary>
		public virtual Int16 DefaultSelected { get; set; }

		/// <summary>
		/// Gets or sets the Enable value.
		/// </summary>
		public virtual Boolean Enable { get; set; }
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the Primary Key of the object.
		/// </summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return "[ProductAdon] " + this.ProductAdOnID.ToString();
		}

		/// <summary>
		/// Returns true if the Ids of the two instances are equal.
		/// </summary>
		/// <param name="ObjectToCompare">The other object instance.</param>
		/// <returns>String</returns>
		public override Boolean Equals(Object ObjectToCompare)
		{
			if(ObjectToCompare == null) return false;
			ProductAdon otherObject = ObjectToCompare as ProductAdon;
			if (otherObject == null) return false;
			return ProductAdon.Equals(this, otherObject);
		}

		/// <summary>
		/// Returns the GetHashCode() method of the Primary Key member.
		/// </summary>
		/// <returns>String</returns>
		public override Int32 GetHashCode()
		{
			return this.ProductAdOnID.GetHashCode();
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