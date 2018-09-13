using System;
using System.Data;
using System.Xml.Serialization;
using Core;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// OrderDetailOptions Class.
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
    [XmlRoot(ElementName = "OrderDetailOption")]
    public class OrderDetailOptions : BaseClass
	{
		#region Construction
		/// <summary>
		/// Initializes a new (no-args) instance of the OrderDetailOptions class.
		/// </summary>
		public OrderDetailOptions()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderDetailOptions class.
		/// </summary>
		public OrderDetailOptions(Int64 OderDetailId, Int32 ProductOptionId, String ProductOptionName, Double Price)
		{
			this.OderDetailId = OderDetailId;
			this.ProductOptionId = ProductOptionId;
			this.ProductOptionName = ProductOptionName;
			this.Price = Price;
		}
		#endregion

		#region Properties
        /// <summary>
        /// Gets or sets the OrderDetailOptionId value.
        /// </summary>
        public virtual Int64 OrderDetailOptionId { get; set; }

		/// <summary>
		/// Gets or sets the OderDetailId value.
		/// </summary>
		public virtual Int64 OderDetailId { get; set; }

		/// <summary>
		/// Gets or sets the ProductOptionId value.
		/// </summary>
		public virtual Int32 ProductOptionId { get; set; }

		/// <summary>
		/// Gets or sets the ProductOptionName value.
		/// </summary>
        public virtual String ProductOptionName { get; set; }

		/// <summary>
		/// Gets or sets the Price value.
		/// </summary>
        public virtual Double Price { get; set; }
		#endregion

        #region Non Table Properties

        public virtual String ProductOptionTypeName { get; set; }

	    #endregion
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            OrderDetailOptionId = Convert.ToInt64(reader.ReadElementString("OrderDetailOptionId"));
            OderDetailId = Convert.ToInt64(reader.ReadElementString("OderDetailId"));
            ProductOptionId = Convert.ToInt32(reader.ReadElementString("ProductOptionId"));
            ProductOptionName = reader.ReadElementString("ProductOptionName");
            Price = Convert.ToDouble(reader.ReadElementString("Price"));
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("OrderDetailOptionId", OrderDetailOptionId.ToString());
            writer.WriteElementString("OderDetailId", OderDetailId.ToString());
            writer.WriteElementString("ProductOptionId", ProductOptionId.ToString());
            writer.WriteElementString("ProductOptionName", ProductOptionName);
            writer.WriteElementString("Price", Price.ToString());
        }
    }
}