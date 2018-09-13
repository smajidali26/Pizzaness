using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// UserLogin Class.
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
	public class UserLogin
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the UserLogin class.
		/// </summary>
		public UserLogin()
		{
		}

		/// <summary>
		/// Initializes a new instance of the UserLogin class.
		/// </summary>
		public UserLogin(Int64 UserLoginId, String Username, String Password, Boolean Enable, Int64 ContactInfoId, Int16 UserTypeId, DateTime CreatedOn, DateTime ModifiedOn)
		{
			this.UserLoginId = UserLoginId;
			this.Username = Username;
			this.Password = Password;
			this.Enable = Enable;
			this.ContactInfoId = ContactInfoId;
			this.UserTypeId = UserTypeId;
			this.CreatedOn = CreatedOn;
			this.ModifiedOn = ModifiedOn;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the UserLoginId value.
		/// </summary>
		public virtual Int64 UserLoginId { get; set; }

		/// <summary>
		/// Gets or sets the Username value.
		/// </summary>
		public virtual String Username { get; set; }

		/// <summary>
		/// Gets or sets the Password value.
		/// </summary>
		public virtual String Password { get; set; }

		/// <summary>
		/// Gets or sets the Enable value.
		/// </summary>
		public virtual Boolean Enable { get; set; }

		/// <summary>
		/// Gets or sets the ContactInfoId value.
		/// </summary>
		public virtual Int64 ContactInfoId { get; set; }

		/// <summary>
		/// Gets or sets the UserTypeId value.
		/// </summary>
		public virtual Int16 UserTypeId { get; set; }

		/// <summary>
		/// Gets or sets the CreatedOn value.
		/// </summary>
		public virtual DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the ModifiedOn value.
		/// </summary>
		public virtual DateTime ModifiedOn { get; set; }
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the Primary Key of the object.
		/// </summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return "[UserLogin] " + this.UserLoginId.ToString();
		}

		/// <summary>
		/// Returns true if the Ids of the two instances are equal.
		/// </summary>
		/// <param name="ObjectToCompare">The other object instance.</param>
		/// <returns>String</returns>
		public override Boolean Equals(Object ObjectToCompare)
		{
			if(ObjectToCompare == null) return false;
			UserLogin otherObject = ObjectToCompare as UserLogin;
			if (otherObject == null) return false;
			return UserLogin.Equals(this, otherObject);
		}

		/// <summary>
		/// Returns the GetHashCode() method of the Primary Key member.
		/// </summary>
		/// <returns>String</returns>
		public override Int32 GetHashCode()
		{
			return this.UserLoginId.GetHashCode();
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