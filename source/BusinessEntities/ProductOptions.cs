using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;

namespace BusinessEntities
{
	#region Comments
	/// <summary>
	/// ProductOptions Class.
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
    [XmlRoot(ElementName = "ProductOption")]
    public class ProductOptions : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Construction
        /// <summary>
		/// Initializes a new (no-args) instance of the ProductOptions class.
		/// </summary>
		public ProductOptions()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProductOptions class.
		/// </summary>
		public ProductOptions(Int32 OptionID, String OptionName, Int16 OptionTypeId)
		{
			this.OptionID = OptionID;
			this.OptionName = OptionName;
			this.OptionTypeId = OptionTypeId;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the OptionID value.
		/// </summary>
		public Int32 OptionID { get; set; }

		/// <summary>
		/// Gets or sets the OptionName value.
		/// </summary>
		public String OptionName { get; set; }

		/// <summary>
		/// Gets or sets the OptionTypeId value.
		/// </summary>
		public Int16 OptionTypeId { get; set; }

        #endregion

        #region Non Table Properties

        /// <summary>
        /// Gets or sets the Price value.
        /// </summary>
        public Double Price { get; set; }

        /// <summary>
        /// Gets or sets the OptionDisplayText value.
        /// </summary>
        public String OptionDisplayText { get; set; }

        private bool _isMultiSelect;
        /// <summary>
        /// Gets or sets the OptionDisplayText value.
        /// </summary>
        public bool IsMultiSelect { get { return _isMultiSelect; } set { _isMultiSelect = value; OnPropertyChanged("IsMultiSelect"); } }

        private bool _isSamePrice;
        /// <summary>
        /// Gets or sets the OptionDisplayText value.
        /// </summary>
        public bool IsSamePrice { get { return _isSamePrice; } set { _isSamePrice = value; OnPropertyChanged("IsSamePrice"); } }

        /// <summary>
        /// Gets or sets the ToppingPrice value.
        /// </summary>
        public Double ToppingPrice { get; set; }

		#endregion

        #region Methods
        
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}