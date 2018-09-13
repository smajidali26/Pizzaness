using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductCategories Class.
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
	public class ProductCategories
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the ProductCategories class.
		/// </summary>
		public ProductCategories()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProductCategories class.
		/// </summary>
		public ProductCategories(Int16 CategoryID, String Name, String Description, Boolean IsActive)
		{
			this.CategoryID = CategoryID;
			this.Name = Name;
			this.Description = Description;
			this.IsActive = IsActive;
		}
		#endregion

		#region Properties
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
		/// Gets or sets the IsActive value.
		/// </summary>
		public virtual Boolean IsActive { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOrder value.
        /// </summary>
        public virtual Int16 DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the ImagePath value.
        /// </summary>
        public virtual String ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOnHomePage value.
        /// </summary>
        public virtual Boolean DisplayOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets the WebCategory value.
        /// </summary>
        public virtual Boolean WebCategory { get; set; }

        /// <summary>
        /// Gets or sets the DesktopCategory value.
        /// </summary>
        public virtual Boolean DesktopCategory { get; set; }

	    #endregion
	}
}