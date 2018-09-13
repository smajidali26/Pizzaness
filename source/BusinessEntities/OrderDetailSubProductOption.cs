using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OrderDetailSubProductOption Class.
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
	public class OrderDetailSubProductOption
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OrderDetailSubProductOption class.
		/// </summary>
		public OrderDetailSubProductOption()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderDetailSubProductOption class.
		/// </summary>
		public OrderDetailSubProductOption(Int64 OrderDetailSubProductOptionId, Int32 OrderDetailSubProductId, Int32 ProductOptionId, String ProductOptionName, Double Price)
		{
			this.OrderDetailSubProductOptionId = OrderDetailSubProductOptionId;
			this.OrderDetailSubProductId = OrderDetailSubProductId;
			this.ProductOptionId = ProductOptionId;
			this.ProductOptionName = ProductOptionName;
			this.Price = Price;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OrderDetailSubProductOptionId value.
		/// </summary>
		public virtual Int64 OrderDetailSubProductOptionId { get; set; }

		/// <summary>
		/// Gets or sets the OrderDetailSubProductId value.
		/// </summary>
		public virtual Int32 OrderDetailSubProductId { get; set; }

		/// <summary>
		/// Gets or sets the ProductOptionId value.
		/// </summary>
		public virtual Int32 ProductOptionId { get; set; }

		/// <summary>
		/// Gets or sets the ProductOptionName value.
		/// </summary>
		public virtual String ProductOptionName { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Double Price { get; set; }
        
		#endregion

        #region Non Table Properties

        public virtual String ProductOptionTypeName { get; set; }

        #endregion
		#region Overrides
		/// <summary>
		/// Returns the Primary Key of the object.
		/// </summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return "[OrderDetailSubProductOption] " + this.OrderDetailSubProductOptionId.ToString();
		}

		/// <summary>
		/// Returns true if the Ids of the two instances are equal.
		/// </summary>
		/// <param name="ObjectToCompare">The other object instance.</param>
		/// <returns>String</returns>
		public override Boolean Equals(Object ObjectToCompare)
		{
			if(ObjectToCompare == null) return false;
			OrderDetailSubProductOption otherObject = ObjectToCompare as OrderDetailSubProductOption;
			if (otherObject == null) return false;
			return OrderDetailSubProductOption.Equals(this, otherObject);
		}

		/// <summary>
		/// Returns the GetHashCode() method of the Primary Key member.
		/// </summary>
		/// <returns>String</returns>
		public override Int32 GetHashCode()
		{
			return this.OrderDetailSubProductOptionId.GetHashCode();
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