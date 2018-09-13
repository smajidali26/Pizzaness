using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    [Serializable]
    public class Report
    {
        /// <summary>
        /// Gets or sets the DateField value.
        /// </summary>
        public virtual DateTime DateField { get; set; }

        /// <summary>
        /// Gets or sets the ProductID value.
        /// </summary>
        public virtual Double OrderTotal { get; set; }
    }
}
