using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OptionTypesInProduct Class.
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
    [XmlRoot(ElementName = "OptionTypesInProduct")]
	public class OptionTypesInProduct
	{
        #region Construction
        /// <summary>
        /// Initializes a new (no-args) instance of the OptionTypesInProduct class.
        /// </summary>
        public OptionTypesInProduct()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ProductsOptionTypeId value.
        /// </summary>
        public Int64 ProductsOptionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the OptionTypeID value.
        /// </summary>
        public Int16 OptionTypeID { get; set; }

        /// <summary>
        /// Gets or sets the BranchProductID value.
        /// </summary>
        public Int64 BranchProductID { get; set; }

        /// <summary>
        /// Gets or sets the IsMultiSelect value.
        /// </summary>
        public Boolean IsMultiSelect { get; set; }

        /// <summary>
        /// Gets or sets the IsSamePrice value.
        /// </summary>
        public Boolean IsSamePrice { get; set; }

        /// <summary>
        /// Gets or sets the IsAdonPriceVary value.
        /// </summary>
        public Boolean IsAdonPriceVary { get; set; }

        /// <summary>
        /// Gets or sets the IsProductPriceChangeType value.
        /// </summary>
        public Boolean IsProductPriceChangeType { get; set; }
        #endregion

        #region Non Table Properties

        /// <summary>
        /// Gets or sets the BranchID value.
        /// </summary>
        [XmlIgnore]
        public Int32 BranchID { get; set; }

        /// <summary>
        /// Gets or sets the ProductID value.
        /// </summary>
        [XmlIgnore]
        public Int32 ProductID { get; set; }

        /// <summary>
        /// Gets or sets the ProductOptionsXml value.
        /// </summary>
        [XmlIgnore]
        public String ProductOptionsXml { get; set; }

        /// <summary>
        /// Gets or sets the ProductOptionsXml value.
        /// </summary>
        [XmlArrayItem(typeof(ProductOptions))]
        public List<ProductOptions> ProductOptionsList { get; set; }

        /// <summary>
        /// Gets or sets the OptionTypeName value.
        /// </summary>
        public String OptionTypeName { get; set; }
        #endregion
	}
}