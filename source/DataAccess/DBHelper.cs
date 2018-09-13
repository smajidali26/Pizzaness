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
    /// <summary>
    /// DB Helper Class for interacting with DB
    /// </summary>
    [Serializable]
    public class DBHelper : IDBHelper
    {
        #region DATA MEMBERS

        /// <summary>
        /// Factory Instance
        /// </summary>
        [NonSerialized]
        private DbProviderFactory mFactory;
        /// <summary>
        /// DB Command
        /// </summary>
        private DbCommand _DbCommand { get; set; }
        /// <summary>
        /// DB Command Data Member
        /// </summary>
        public DbCommand DbCommand
        {
            get
            {
                return _DbCommand;
            }
            set
            {
                _DbCommand = value;
            }
        }
        /// <summary>
        /// Transaction Private Data Member 
        /// </summary>
        [NonSerialized]
        private DbTransaction mTransaction;
        /// <summary>
        /// Is Transacton Active
        /// </summary>
        private bool mIsTransactionActive;
        /// <summary>
        /// Connection string 
        /// </summary>
        private String S_CONNECTION = "";
        /// <summary>
        /// Provider Name
        /// </summary>
        private static String S_PROVIDER
        {
            get
            {
                return DatabaseSettings.DatabaseProvider;
            }
        }
        /// <summary>
        /// Db parameter
        /// </summary>
        [NonSerialized]
        private DbParameter mParameter = null;
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Function to intialize connection string and provider
        /// </summary>
        /// <param name="connectionStringSectionName"></param>
        private void Init(String connectionString)
        {
            S_CONNECTION = connectionString;
            mFactory = DbProviderFactories.GetFactory(S_PROVIDER);
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        private DBHelper()
        {
            this.Init(DatabaseSettings.ConnectionString);
        }
        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="connectionStringSectionName"> Connection String Name</param>
        private DBHelper(String connectionString)
        {
            this.Init(connectionString);
        }
        /// <summary>
        /// This function is used for creating the instance of DB helper
        /// </summary>
        /// <returns>DB Helper</returns>
        public static DBHelper CreateInstance()
        {
            return new DBHelper();
        }
        /// <summary>
        /// This function is used for creating the instance of DB helper
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <returns>DB Helper</returns>
        public static DBHelper CreateInstance(String connectionString)
        {
            return new DBHelper(connectionString);
        }
        #endregion

        #region METHODS
        #region GENERAL
        /// <summary>
        /// This function is used to Open Database Connection
        /// </summary>
        public DbConnection OpenConnection()
        {
            #region Commented Code
            //if (mConnection != null)
            //{
            //    if (mConnection.State == ConnectionState.Open)
            //    {
            //        return;
            //mConnection.Close();   
            //    }
            //}
            //DbConnection mConnection
            #endregion

            DbConnection connection = mFactory.CreateConnection();
            connection.ConnectionString = S_CONNECTION;
            connection.Open();
            return connection;
        }
        /// <summary>
        /// This function is used to Close Database Connection
        /// </summary>
        public void CloseConnection(DbConnection connection)
        {
            //check for an open connection            
            try
            {
                if ((connection != null) && (connection.State == ConnectionState.Open))
                {
                    connection.Close();
                }
            }

            catch
            {
                //catch any SQL server data provider generated error messag
                throw;// new DbException();
            }

            finally
            {
                //if (mConnection !=null)
                //mConnection.Dispose();
            }
        }
        /// <summary>
        /// This function is used to Handle Transaction Events
        /// </summary>
        /// <param name="pTransactionType">Transaction Type</param>
        public void TransactionHandler(TransactionType pTransactionType)
        {
            DbConnection connection = OpenConnection();
            switch (pTransactionType)
            {
                case TransactionType.Open:  //open a transaction
                    try
                    {
                        mTransaction = connection.BeginTransaction();
                        mIsTransactionActive = true;
                    }
                    catch (InvalidOperationException oErr)
                    {
                        throw new InvalidOperationException("Transaction Handler - " + oErr.Message);
                    }
                    break;

                case TransactionType.Commit:  //commit the transaction
                    if (null != mTransaction.Connection)
                    {
                        try
                        {
                            mTransaction.Commit();
                            mIsTransactionActive = false;
                        }
                        catch (InvalidOperationException oErr)
                        {
                            throw new InvalidOperationException("Transaction Handler - " + oErr.Message);
                        }
                    }
                    break;

                case TransactionType.Rollback:  //rollback the transaction
                    try
                    {
                        if (mIsTransactionActive)
                        {
                            mTransaction.Rollback();
                        }
                        mIsTransactionActive = false;
                    }
                    catch (InvalidOperationException oErr)
                    {
                        throw new InvalidOperationException("Transaction Handler - " + oErr.Message);
                    }
                    break;
            }
        }
        /// <summary>
        /// This function is used to Prepare Command For Execution
        /// </summary>
        /// <param name="connection">Connection</param>
        /// <param name="pUseTransaction">Use Transaction</param>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        private DbCommand PrepareCommand(DbConnection connection, bool pUseTransaction, CommandType pCommandType, string pCommandText)
        {
            DbCommand = mFactory.CreateCommand();
            DbCommand.Connection = connection;
            DbCommand.CommandText = pCommandText;
            DbCommand.CommandType = pCommandType;

            if (pUseTransaction)
                DbCommand.Transaction = mTransaction;

            return DbCommand;
        }
        /// <summary>
        /// This function is used to prepare command for data set 
        /// </summary>      
        /// <param name="connection">Connection</param>
        /// <param name="pUseTransaction">Use Transaction</param>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        private DbCommand PrepareCommandDataSet(DbConnection connection, bool pUseTransaction, CommandType pCommandType, string pCommandText)
        {

            connection = mFactory.CreateConnection();
            connection.ConnectionString = S_CONNECTION; // Set Connection String

            DbCommand = mFactory.CreateCommand();
            DbCommand.Connection = connection; // Set Command Connection

            DbCommand.CommandText = pCommandText; // Set Command Name
            DbCommand.CommandType = pCommandType; // Set Command type

            if (pUseTransaction)
                DbCommand.Transaction = mTransaction; // Set Commant Transaction

            return DbCommand;
        }
        /// <summary>
        /// This function is used to Prepare Command For Execution
        /// </summary>
        /// <param name="connection">Connection</param>
        /// <param name="pUseTransaction">Use Transaction</param>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters</param>
        private DbCommand PrepareCommand(DbConnection connection, bool pUseTransaction, CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {
            DbCommand = mFactory.CreateCommand();
            DbCommand.Connection = connection; // Set Command Connection
            DbCommand.CommandText = pCommandText; // Set Command Name
            DbCommand.CommandType = pCommandType; // Set Command type

            if (pUseTransaction)
                DbCommand.Transaction = mTransaction; // Set Commant Transaction

            if (pParameters != null)
                CreateDBParameters(DbCommand, pParameters); // Add Command parameters

            return DbCommand;
        }
        /// <summary>
        /// This function is used to prepare command for data set 
        /// </summary>
        /// <param name="pUseTransaction">Use Transacion</param>
        /// <param name="pUseTransaction">Use Transaction</param>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        private DbCommand PrepareCommandDataSet(DbConnection connection, bool pUseTransaction, CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {

            connection = mFactory.CreateConnection();
            connection.ConnectionString = S_CONNECTION; // Set Command Connection

            DbCommand = mFactory.CreateCommand();
            DbCommand.Connection = connection;
            DbCommand.CommandText = pCommandText; // Set Command Name
            DbCommand.CommandType = pCommandType; // Set Command type

            if (pUseTransaction)
                DbCommand.Transaction = mTransaction;

            if (pParameters != null)
                CreateDBParameters(DbCommand, pParameters); // Add Command parameters
            return DbCommand;
        }
        /// <summary>
        /// This function is used to Create Parameters for the Command For Execution
        /// </summary>
        /// <param name="dbCommand">DBCommand</param>
        /// <param name="pParameters">Parameters</param>
        private void CreateDBParameters(DbCommand dbCommand, IParameter[] pParameters)
        {

            for (int i = 0; i < pParameters.Length; i++)
            {
                //check the parameter
                Parameter oParam = (Parameter)pParameters[i];
                mParameter = dbCommand.CreateParameter();
                mParameter.ParameterName = oParam.Name;
                mParameter.Value = oParam.Value;
                mParameter.Direction = oParam.Direction;

                //if size is specified, use this - otherwise use default
                if (oParam.Size > 0)
                {
                    mParameter.Size = oParam.Size;
                }

                dbCommand.Parameters.Add(mParameter);
            }
        }

        /// <summary>
        /// This method encodes parameter
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>
        /// 
        //Azhar converted to protected for Rules
        /// <summary>
        /// This method encodes parameter
        /// </summary>
        /// <param name="value">string text</param>
        /// <returns>string</returns>
        //protected string EncodeParameter(string value)
        //{
        //    if (value == null)
        //    {
        //        throw new ArgumentNullException("value");
        //    }


        //    if (value.StartsWith("~~~") == true)
        //        return value;
        //    else
        //    {
        //        return System.Text.RegularExpressions.Regex.Replace(value, @"(--)|(xp_)|(\s*(exec)((\s)|(&nbsp;))+)|(<((\s)|(&nbsp;))*(script)>)|(&lt;((\s)|(&nbsp;))*script(&gt;))|(<((\s)|(&nbsp;))*(script)((\s)|(&nbsp;))+>?)|(&lt;((\s)|(&nbsp;))*script((\s)|(&nbsp;)|(&gt;))+)|(&lt;/((\s)|(&nbsp;))*(script)((\s)|(&nbsp;)|(&gt;))*)|(</((\s)|(&nbsp;))*(script)((\s)|(&nbsp;))*>)|(%3c)|(%3e)/i", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    }
        //}        

        #endregion

        #region EXCEUTE NON_QUERY
        /// <summary>
        /// This function is used to Create Parameters for the Command For Execution
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>No of Rows Affected</returns>
        public int ExecuteNonQuery(CommandType pCommandType, string pCommandText)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, false, pCommandType, pCommandText);
                return DbCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>        
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pUseTransaction">Flag for Transaction</param>
        /// <returns>No of Rows Affected</returns>
        public int ExecuteNonQuery(CommandType pCommandType, string pCommandText, bool pUseTransaction)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, pUseTransaction, pCommandType, pCommandText);
                int val = DbCommand.ExecuteNonQuery();
                return val;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();

                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>      
        /// <returns>No of Rows Affected</returns>
        public int ExecuteNonQuery(CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, false, pCommandType, pCommandText, pParameters);
                return DbCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>        
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>
        /// <param name="pUseTransaction">Flag for transaction</param>
        /// <returns>No of Rows Affected</returns>
        public int ExecuteNonQuery(CommandType pCommandType, string pCommandText, IParameter[] pParameters, bool pUseTransaction)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, pUseTransaction, pCommandType, pCommandText, pParameters);
                return DbCommand.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to get the log of stored procedure call  and it should be used in profiling ang logging only 
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>      
        /// <returns>No of Rows Affected</returns>
        public int ExecuteNonQueryWithoutLog(CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, false, pCommandType, pCommandText, pParameters);
                return DbCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();
                CloseConnection(connection);
            }
        }
        #endregion

        #region EXECUTE READER
        /// <summary>
        /// This function is used to fetch data using Data Reader	
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>Data Reader</returns>
        public DbDataReader ExecuteReader(CommandType pCommandType, string pCommandText)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, false, pCommandType, pCommandText);
                DbDataReader dr = DbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DbCommand.Parameters.Clear();
                return dr;
            }
            catch
            {
                CloseConnection(connection);
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();
                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to fetch data using Data Reader	
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>
        /// <returns>Data Reader</returns>
        public DbDataReader ExecuteReader(CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, false, pCommandType, pCommandText, pParameters);
                return DbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                CloseConnection(connection);
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        #endregion

        #region EXECUTE DATASET
        /// <summary>
        /// This function is used to fetch data using Data Adapter	
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>Data Set</returns>
        public DataSet DataAdapter(CommandType pCommandType, string pCommandText)
        {

            DbConnection connection = null;
            DbDataAdapter dda = null;
            DataSet ds = null;
            try
            {
                dda = mFactory.CreateDataAdapter();
                PrepareCommandDataSet(connection, false, pCommandType, pCommandText);
                dda.SelectCommand = DbCommand;
                ds = new DataSet();
                ds.Locale = CultureInfo.InvariantCulture;
                dda.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();

                //Closing Connections
                CloseConnection(connection);

                //Logging Data Base Call Configurabel from web.config
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }

            return ds;
        }
        /// <summary>
        /// This function is used to fetch data using DataSet
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters Collection</param>
        /// <returns>Data Set</returns>
        public DataSet DataAdapter(CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {

            DbConnection connection = null;
            DbDataAdapter dda = null;
            DataSet ds = new DataSet();

            try
            {
                dda = mFactory.CreateDataAdapter();
                PrepareCommandDataSet(connection, false, pCommandType, pCommandText, pParameters);
                dda.SelectCommand = DbCommand;
                ds.Locale = CultureInfo.InvariantCulture;
                dda.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();

                //Closing Connections
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }

            return ds;
        }
        #endregion

        #region EXECUTE SCALAR METHODS
        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="pCommandType">Coomand Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(CommandType pCommandType, string pCommandText)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();

                PrepareCommand(connection, false, pCommandType, pCommandText);

                object val = DbCommand.ExecuteScalar();

                return val;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();

                //Closing Connection
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters</param>        
        /// <returns>Object</returns>
        public object ExecuteScalar(CommandType pCommandType, string pCommandText, IParameter[] pParameters)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, false, pCommandType, pCommandText, pParameters);
                return DbCommand.ExecuteScalar();

            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();

                //Closing Connection
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="pCommandType">Command Type</param>
        /// <param name="pCommandText">Command Text</param>
        /// <param name="pParameters">Parameters</param>
        /// <param name="pUseTransaction">Use Transaction</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(CommandType pCommandType, string pCommandText, IParameter[] pParameters, bool pUseTransaction)
        {
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();
                PrepareCommand(connection, pUseTransaction, pCommandType, pCommandText, pParameters);
                return DbCommand.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (DbCommand != null)
                    DbCommand.Dispose();
                CloseConnection(connection);

                //Logging Data Base Call
                //DatabaseLog.AddDatabaseLog(DbCommand);
            }
        }
        #endregion
        #endregion
    }
}


