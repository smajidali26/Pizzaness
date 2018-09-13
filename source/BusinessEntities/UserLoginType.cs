using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// UserLoginType Class.
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
	public class UserLoginType
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the UserLoginType class.
		/// </summary>
		public UserLoginType()
		{
		}

		/// <summary>
		/// Initializes a new instance of the UserLoginType class.
		/// </summary>
		public UserLoginType(Int16 LoginTypeId, String LoginTypeName)
		{
			this.LoginTypeId = LoginTypeId;
			this.LoginTypeName = LoginTypeName;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the LoginTypeId value.
		/// </summary>
		public virtual Int16 LoginTypeId { get; set; }

		/// <summary>
		/// Gets or sets the LoginTypeName value.
		/// </summary>
		public virtual String LoginTypeName { get; set; }
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the Primary Key of the object.
		/// </summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return "[UserLoginType] " + this.LoginTypeId.ToString();
		}

		/// <summary>
		/// Returns true if the Ids of the two instances are equal.
		/// </summary>
		/// <param name="ObjectToCompare">The other object instance.</param>
		/// <returns>String</returns>
		public override Boolean Equals(Object ObjectToCompare)
		{
			if(ObjectToCompare == null) return false;
			UserLoginType otherObject = ObjectToCompare as UserLoginType;
			if (otherObject == null) return false;
			return UserLoginType.Equals(this, otherObject);
		}

		/// <summary>
		/// Returns the GetHashCode() method of the Primary Key member.
		/// </summary>
		/// <returns>String</returns>
		public override Int32 GetHashCode()
		{
			return this.LoginTypeId.GetHashCode();
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