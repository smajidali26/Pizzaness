using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// Enum for Trnasaction
    /// </summary>
    public enum TransactionType : int
    {
        None = 0,
        Open = 1,
        Commit = 2,
        Rollback = 3
    }
}
