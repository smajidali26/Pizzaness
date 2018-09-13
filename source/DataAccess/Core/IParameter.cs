using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{ 
    /// <summary>
    /// Contract of Parameter Class for paramters of DB command
    /// </summary>
    /// 
    //Defining Template for thia Param
    public interface IParameter
    {
        #region DATA MEMBERS
        string Name { get; set; }
        object Value { get; set; }
        ParameterDirection Direction { get; set; }
        #endregion 
    }
}
