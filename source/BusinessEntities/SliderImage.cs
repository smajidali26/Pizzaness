using System;
using System.Data;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// SliderImage Class.
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
	/// 		<description>9/29/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class SliderImage
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the SliderImage class.
		/// </summary>
		public SliderImage()
		{
		}

		/// <summary>
		/// Initializes a new instance of the SliderImage class.
		/// </summary>
		public SliderImage(Int32 SliderImageId, String ImageName, String Description, Boolean IsEnabled)
		{
			this.SliderImageId = SliderImageId;
			this.ImageName = ImageName;
			this.Description = Description;
			this.IsEnabled = IsEnabled;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the SliderImageId value.
		/// </summary>
		public virtual Int32 SliderImageId { get; set; }

		/// <summary>
		/// Gets or sets the ImageName value.
		/// </summary>
		public virtual String ImageName { get; set; }

        /// <summary>
        /// Gets or sets the ImagePath value.
        /// </summary>
        public virtual String ImagePath { get; set; }

		/// <summary>
		/// Gets or sets the Description value.
		/// </summary>
		public virtual String Description { get; set; }

		/// <summary>
		/// Gets or sets the IsEnabled value.
		/// </summary>
		public virtual Boolean IsEnabled { get; set; }
		#endregion
	}
}