using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OptionType Class.
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
	/// 		<description>12/7/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class OptionType
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OptionType class.
		/// </summary>
		public OptionType()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OptionType class.
		/// </summary>
		public OptionType(Int16 OptionTypeID, String OptionTypeName, String OptionDisplayText)
		{
			this.OptionTypeID = OptionTypeID;
			this.OptionTypeName = OptionTypeName;
			this.OptionDisplayText = OptionDisplayText;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OptionTypeID value.
		/// </summary>
		public virtual Int16 OptionTypeID { get; set; }

		/// <summary>
		/// Gets or sets the OptionTypeName value.
		/// </summary>
		public virtual String OptionTypeName { get; set; }

		/// <summary>
		/// Gets or sets the OptionDisplayText value.
		/// </summary>
		public virtual String OptionDisplayText { get; set; }
		#endregion

        #region Non Table Properties
        [XmlIgnore]
        public String ProductOptionXml { get; set; }
        
        [XmlIgnore]
        public ICollection<ProductOptions> ProductOptionList { get; set; }

        public bool IsMultiSelect { get; set; }

        public bool IsSamePrice { get; set; }

        public bool IsAdonPriceVary { get; set; }

        public bool IsProductPriceChangeType { get; set; }

        #endregion
    }
}