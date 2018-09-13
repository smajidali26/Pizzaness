using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace BusinessEntities
{
    [Serializable]
    public class PreOrderPromo : BaseClass
    {
        #region Properties

        /// <summary>
        /// Gets or sets the PreOrderPromoCode value.
        /// </summary>
        public virtual string PreOrderPromoCode { get; set; }

        /// <summary>
        /// Gets or sets the PreOrderPromoValue value.
        /// </summary>
        public virtual double PreOrderPromoValue { get; set; }

        /// <summary>
        /// Gets or sets the PromotionCodeId value.
        /// </summary>
        public virtual Int32? PromotionCodeId { get; set; }

        #endregion
    }
}
