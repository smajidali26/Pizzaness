using System;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ContactAddresses Class.
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
    [XmlRoot(ElementName = "ContactAddress")]
	public class ContactAddresses
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ContactAddresses class.
		/// </summary>
		public ContactAddresses()
		{
		}
        
        #endregion

		#region Properties
		/// <summary>
		/// Gets or sets the AddressID value.
		/// </summary>
		public virtual Int64 AddressID { get; set; }

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
		/// Gets or sets the Country value.
		/// </summary>
		public virtual String Country { get; set; }

		/// <summary>
		/// Gets or sets the ContactInfoId value.
		/// </summary>
		public virtual Int64 ContactInfoId { get; set; }

		/// <summary>
		/// Gets or sets the AddressTypeId value.
		/// </summary>
		public virtual Int16 AddressTypeId { get; set; }

		/// <summary>
		/// Gets or sets the CreatedOn value.
		/// </summary>
		public virtual DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the ModifiedOn value.
		/// </summary>
		public virtual DateTime ModifiedOn { get; set; }
		#endregion

        #region Virtaul Properties

        /// <summary>
		/// Gets or sets the FirstName value.
		/// </summary>
		public virtual String FirstName { get; set; }

		/// <summary>
		/// Gets or sets the LastName value.
		/// </summary>
		public virtual String LastName { get; set; }

		/// <summary>
		/// Gets or sets the Email value.
		/// </summary>
		public virtual String Email { get; set; }

		/// <summary>
		/// Gets or sets the Telephone value.
		/// </summary>
		public virtual String Telephone { get; set; }

		/// <summary>
		/// Gets or sets the Mobile value.
		/// </summary>
		public virtual String Mobile { get; set; }

        #endregion
    }
}