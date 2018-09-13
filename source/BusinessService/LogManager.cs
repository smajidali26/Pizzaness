using System;
using DataAccess;

namespace BusinessService
{
    public class LogManager
    {

        #region DataAccess

        private IDBHelper _dbHandler = null;

        public IDBHelper DBHandler
        {
            get
            {
                if (_dbHandler == null)
                {
                    _dbHandler = DBHelper.CreateInstance(DatabaseSettings.ConnectionString);
                }

                return _dbHandler;
            }
        }

        #endregion

        #region Constructor

        public LogManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Records a log entry
        /// </summary>
        /// <param name="thread">Session ID</param>
        /// <param name="level">Exception or Log</param>
        /// <param name="logger">Log Activity Name</param>
        /// <param name="message">Message to record against Log Activity (if applicable)</param>
        /// <param name="exception">Exception to record against Log Activity (if applicable)</param>
        /// <returns>Save Status</returns>
        public bool SaveLogData(string thread, string level, string logger, string message = null,
                                string exception = null)
        {
            try
            {

                #region Parameters

                IParameter[] parameters = new Parameter[]
                    {
                        new Parameter("@Thread", thread),
                        new Parameter("@Level", level),
                        new Parameter("@Logger", logger),
                        new Parameter("@Message", message),
                        new Parameter("@Exception", exception),

                    };

                #endregion


                object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Log_AddNewLogEntry]",
                                                        parameters);
                return (int) result > 0;
            }
            catch (Exception)
            {
                // If not logged, do nothing
                return false;
            }

            #endregion
        }
    }
}