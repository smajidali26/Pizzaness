using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DataAccess
{
    public static class DatabaseSettings
    {


        #region "Connection Strings"

        public static String DatabaseConnectionStringName
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionStringName"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static String ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DatabaseConnectionStringName].ConnectionString;
            }
        }        

        /// <summary>
        /// Connection string Data Base Provider Name
        /// </summary>
        public static String DatabaseProvider
        {
            get
            {
                String provider = "";
                provider = ConfigurationManager.ConnectionStrings[DatabaseConnectionStringName].ProviderName;

                return provider;
            }
        }
        #endregion
    }
}
