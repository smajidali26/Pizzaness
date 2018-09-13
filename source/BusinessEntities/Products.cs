using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// Products Class.
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
    [XmlRoot("Products")]
	public class Products
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the Products class.
		/// </summary>
		public Products()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Products class.
		/// </summary>
		public Products(Int32 ProductID, Int16 CategoryID, String Name, String Description, String Image, String ImagePath, Boolean IsActive, Boolean IsSpecial)
		{
			this.ProductID = ProductID;
			this.CategoryID = CategoryID;
			this.Name = Name;
			this.Description = Description;
			this.Image = Image;
			this.ImagePath = ImagePath;
			this.IsActive = IsActive;
			this.IsSpecial = IsSpecial;
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the ProductID value.
		/// </summary>
		public virtual Int32 ProductID { get; set; }

		/// <summary>
		/// Gets or sets the CategoryID value.
		/// </summary>
		public virtual Int16 CategoryID { get; set; }

		/// <summary>
		/// Gets or sets the Name value.
		/// </summary>
		public virtual String Name { get; set; }

		/// <summary>
		/// Gets or sets the Description value.
		/// </summary>
		public virtual String Description { get; set; }

		/// <summary>
		/// Gets or sets the Image value.
		/// </summary>
		public virtual String Image { get; set; }

		/// <summary>
		/// Gets or sets the ImagePath value.
		/// </summary>
		public virtual String ImagePath { get; set; }

		/// <summary>
		/// Gets or sets the IsActive value.
		/// </summary>
		public virtual Boolean IsActive { get; set; }

		/// <summary>
		/// Gets or sets the IsSpecial value.
		/// </summary>
		public virtual Boolean IsSpecial { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOrder value.
        /// </summary>
        public virtual Int16 DisplayOrder { get; set; }
		#endregion

        #region Non Table Properties
        /// <summary>
        /// Gets or sets the DefaultBranchProductPrice value.
        /// </summary>
        public virtual Double DefaultBranchProductPrice { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName value.
        /// </summary>
        public virtual String CategoryName { get; set; }

        public virtual Int32 Quantity { get; set; }

        public virtual Double UnitPrice { get; set; }

        public virtual String OptionTypeInProductXml { get; set; }

        public virtual String AdonTypeInProducctXml { get; set; }

        public virtual Int64 BranchProductID { get; set; }

        public virtual Int16 NumberOfFreeTopping { get; set; }

        public bool IsCustomizable { get; set; }
        
        public virtual Int32 ComboId { get; set; }
        [XmlIgnore]
        public ICollection<OptionTypesInProduct> OptionTypesInProductList { get; set; }
        
        [XmlIgnore]
        public ICollection<AdOnTypeInProduct> AdOnTypeInProductList { get; set; }
        
        [XmlIgnore]
        public ProductActivation ProductActivationObject { get; set; }

        [XmlIgnore]
        public String ProductActivationObj { get; set; }
        #endregion
    }
}