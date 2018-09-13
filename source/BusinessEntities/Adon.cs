using System;
using System.Data;
using System.Xml.Serialization;
using Core;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// Adon Class.
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
    [XmlRoot(ElementName = "Adon")]
	public class Adon : BaseClass
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the Adon class.
		/// </summary>
		public Adon()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Adon class.
		/// </summary>
		public Adon(Int16 AdOnID, String AdOnName, Int16 AdonType)
		{
			this.AdOnID = AdOnID;
			this.AdOnName = AdOnName;
			this.AdonType = AdonType;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the AdOnID value.
		/// </summary>
		public virtual Int16 AdOnID { get; set; }

		/// <summary>
		/// Gets or sets the AdOnName value.
		/// </summary>
		public virtual String AdOnName { get; set; }

		/// <summary>
		/// Gets or sets the AdonType value.
		/// </summary>
		public virtual Int16 AdonType { get; set; }

        public virtual Int16 DefaultSelected { get; set; }

        /// <summary>
        /// Get or Set ImageName value
        /// </summary>
        public virtual String ImageName { get; set; }
		#endregion

        #region Non Table Properties

        public virtual Int16 DisplayFormat { get; set; }

        /// <summary>
        /// Gets or sets the AdOnTypeName value.
        /// </summary>
        public virtual String AdOnTypeName { get; set; }
        #endregion
    }
}