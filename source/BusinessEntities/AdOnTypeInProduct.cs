using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// AdOnTypeInProduct Class.
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
    [XmlRoot(ElementName = "AdonTypesInProduct")]
	public class AdOnTypeInProduct
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the AdOnTypeInProduct class.
		/// </summary>
		public AdOnTypeInProduct()
		{
		}

		/// <summary>
		/// Initializes a new instance of the AdOnTypeInProduct class.
		/// </summary>
		public AdOnTypeInProduct(Int64 ProductsAdOnTypeId, Int64 BrachProductID, Int16 AdonTypeID, Boolean Enable, Decimal Price)
		{
			this.ProductsAdOnTypeId = ProductsAdOnTypeId;
			this.BrachProductID = BrachProductID;
			this.AdonTypeID = AdonTypeID;
			this.Enable = Enable;
			this.Price = Price;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the ProductsAdOnTypeId value.
		/// </summary>
		public virtual Int64 ProductsAdOnTypeId { get; set; }

		/// <summary>
		/// Gets or sets the BrachProductID value.
		/// </summary>
		public virtual Int64 BrachProductID { get; set; }

		/// <summary>
		/// Gets or sets the AdonTypeID value.
		/// </summary>
		public virtual Int16 AdonTypeID { get; set; }

		/// <summary>
		/// Gets or sets the Enable value.
		/// </summary>
		public virtual Boolean Enable { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
		public virtual Decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the DisplayFormat value.
        /// </summary>
        public virtual Int16 DisplayFormat { get; set; }
		#endregion

        #region Non Table Properties
        //public String ProductAdonsXml { get; set; }

        /// <summary>
        /// Gets or sets the AdOnTypeName value.
        /// </summary>
        public String AdOnTypeName { get; set; }

        /// <summary>
        /// Gets or sets the IsFreeAdonType value.
        /// </summary>
        public bool IsFreeAdonType { get; set; }

        /// <summary>
        /// Gets or sets the Adons value.
        /// </summary>
        [XmlArrayItem(typeof(Adon))]
        public List<Adon> Adons { get; set; }
        #endregion
    }
}