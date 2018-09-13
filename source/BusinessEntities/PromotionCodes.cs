using System;
using System.Data;
using Core;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// PromotionCode Class.
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
	/// 		<description>11/7/2013</description>
	/// 		<description>Created</description>
	/// 	</item>
	/// </list>
	/// </remarks>
	#endregion

	[Serializable]
	public class PromotionCodes : BaseClass
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the PromotionCode class.
		/// </summary>
		public PromotionCodes()
		{
		}

		
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the PromotionCodeId value.
		/// </summary>
		public virtual Int32 PromotionCodeId { get; set; }

		/// <summary>
		/// Gets or sets the PromotionName value.
		/// </summary>
		public virtual String PromotionName { get; set; }

		/// <summary>
		/// Gets or sets the PromotionCode value.
		/// </summary>
		public virtual String PromotionCode { get; set; }

		/// <summary>
		/// Gets or sets the TypeOfPromotion value.
		/// </summary>
		public virtual PromotionType TypeOfPromotion { get; set; }

		/// <summary>
		/// Gets or sets the StartDate value.
		/// </summary>
		public virtual DateTime? StartDate { get; set; }

		/// <summary>
		/// Gets or sets the EndDate value.
		/// </summary>
		public virtual DateTime? EndDate { get; set; }

		/// <summary>
		/// Gets or sets the PromotionValue value.
		/// </summary>
		public virtual Double PromotionValue { get; set; }

        /// <summary>
        /// Gets or sets the PromotionValue value.
        /// </summary>
        public virtual Double PromoValueUsed { get; set; }

        /// <summary>
        /// Gets or sets the CodeUsageCounter value.
		/// </summary>
        public virtual Int32? CodeUsageCounter { get; set; }
        
		#endregion
        
	}
}