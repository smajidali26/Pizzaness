using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// AdonType Class.
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
	public class AdonType
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the AdonType class.
		/// </summary>
		public AdonType()
		{
		}

		/// <summary>
		/// Initializes a new instance of the AdonType class.
		/// </summary>
		public AdonType(Int16 AdOnTypeId, String AdOnTypeName, Boolean IsFreeAdonType)
		{
			this.AdOnTypeId = AdOnTypeId;
			this.AdOnTypeName = AdOnTypeName;
			this.IsFreeAdonType = IsFreeAdonType;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the AdOnTypeId value.
		/// </summary>
		public virtual Int16 AdOnTypeId { get; set; }

		/// <summary>
		/// Gets or sets the AdOnTypeName value.
		/// </summary>
		public virtual String AdOnTypeName { get; set; }

		/// <summary>
		/// Gets or sets the IsFreeAdonType value.
		/// </summary>
		public virtual Boolean IsFreeAdonType { get; set; }

        /// <summary>
        /// Get or Set ImageName value
        /// </summary>
        public virtual String ImageName { get; set; }
		#endregion

        #region Non Table Properties
        [XmlIgnore]
        public String ProductAdonXml { get; set; }
        
        [XmlIgnore]
        public ICollection<Adon> ProductAdonList { get; set; }

        /// <summary>
        /// This is Non Table Property. It is taken from AdOnTypeInProduct Table.
        /// </summary>
        public Decimal Price { get; set; }
        #endregion
    }
}