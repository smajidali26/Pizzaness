using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class BaseClass
    {
        /// <summary>
        /// Date of Creation
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Date of modification
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// Date of deletion
        /// </summary>
        public DateTime DeletedOn { get; set; }
        /// <summary>
        /// Deleted by
        /// </summary>
        public int DeletedBy { get; set; }
        /// <summary>
        /// Gets or sets Total Rows
        /// </summary>
        public virtual int TotalRows { get; set; }
    }
}
