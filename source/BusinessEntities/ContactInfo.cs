using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ContactInfo Class.
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
	public class ContactInfo
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ContactInfo class.
		/// </summary>
		public ContactInfo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ContactInfo class.
		/// </summary>
		public ContactInfo(Int64 ContactInfoId, String Title, String FirstName, String LastName, String Email, String Telephone, String Mobile, DateTime CreatedOn, DateTime ModifiedOn)
		{
			this.ContactInfoId = ContactInfoId;
			this.Title = Title;
			this.FirstName = FirstName;
			this.LastName = LastName;
			this.Email = Email;
			this.Telephone = Telephone;
			this.Mobile = Mobile;
			this.CreatedOn = CreatedOn;
			this.ModifiedOn = ModifiedOn;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ContactInfoId value.
		/// </summary>
		public virtual Int64 ContactInfoId { get; set; }

		/// <summary>
		/// Gets or sets the Title value.
		/// </summary>
		public virtual String Title { get; set; }

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

		/// <summary>
		/// Gets or sets the CreatedOn value.
		/// </summary>
		public virtual DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the ModifiedOn value.
		/// </summary>
		public virtual DateTime ModifiedOn { get; set; }
		#endregion

        #region Virtual Properties
        [XmlIgnore]
        public ICollection<ContactAddresses> ContactAddressList { get; set; }

        public String ContactAddressXml { get; set; }

        #endregion
    }
}