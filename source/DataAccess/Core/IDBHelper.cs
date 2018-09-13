using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Configuration;
using System.Globalization;

namespace DataAccess
{
    public interface IDBHelper
    {

        #region Data Members
        DbCommand DbCommand { get; set; }
        #endregion 

        #region METHODS
        #region GENERAL
        /// <summary>
        /// This function is used to Open Database Connection
        /// </summary>
        DbConnection OpenConnection();
        /// <summary>
        /// This function is used to Close Database Connection
        /// </summary>
        void CloseConnection(DbConnection connection);
        /// <summary>
        /// This function is used to Handle Transaction Events
        /// </summary>
        /// <param name="pTransactionType"></param>
        void TransactionHandler(TransactionType pTransactionType);
        /// <summary>
        /// This function is used to Prepare Command For Execution
        /// </summary>
        /// <param name="pUseTransaction"></param>
        /// <param name="pCommandType"></param>
        /// <param name="pCommandText"></param>
        #endregion 

        #region EXCEUTE NON_QUERY
        //New parameter in overloaded method should be at the end
        /// <summary>
        /// This function is used to Create Parameters for the Command For Execution
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>No of rows affected</returns>
        int ExecuteNonQuery(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>
        /// <param name="pUseTransaction">Flag for Transaction</param>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>No of rows affected</returns>
        int ExecuteNonQuery(CommandType pCommandType, string pCommandText,bool pUseTransaction);
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>        
        /// <returns>No of rows affected</returns>
        int ExecuteNonQuery(CommandType pCommandType, string pCommandText, IParameter[] pParameters);
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>       
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters</param>        
        /// <param name="pUseTransaction">Flag for transaction</param>
        /// <returns>No of rows affected</returns>
        int ExecuteNonQuery(CommandType pCommandType, string pCommandText, IParameter[] pParameters, bool pUseTransaction);
        /// <summary>
        /// This function is used to get the log of stored procedure call  and it should be used in profiling ang logging only 
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>      
        /// <returns>No of Rows Affected</returns>
        int ExecuteNonQueryWithoutLog(CommandType pCommandType, string pCommandText, IParameter[] pParameters);
        #endregion 

        #region EXECUTE READER
        /// <summary>
        /// This function is used to fetch data using Data Reader	
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>Data Reader</returns>
        DbDataReader ExecuteReader(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// This function is used to fetch data using Data Reader	
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>
        /// <returns>Data Reader</returns>
        DbDataReader ExecuteReader(CommandType pCommandType, string pCommandText, IParameter[] pParameters);
        #endregion 

        #region EXECUTE DATASET
        /// <summary>
        /// This function is used to fetch data using Data Adapter	
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>DataSet</returns>
        DataSet DataAdapter(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// This function is used to fetch data using DataSet
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>
        /// <returns>DataSet</returns>
        DataSet DataAdapter(CommandType pCommandType, string pCommandText, IParameter[] pParameters);
        #endregion 

        #region EXECUTE SCALAR METHODS
        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>Object</returns>
        object ExecuteScalar(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Comand Text</param>
        /// <param name="pParameters">Parameters Collection</param>       
        /// <returns></returns>
        object ExecuteScalar(CommandType pCommandType, string pCommandText, IParameter[] pParameters);
        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>        
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>
        /// <param name="pUseTransaction">Flag for Transaction</param>
        /// <returns></returns>
        object ExecuteScalar(CommandType pCommandType, string pCommandText, IParameter[] pParameters, bool pUseTransaction);
        #endregion 

        #endregion
    }
}
