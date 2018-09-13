using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Core;
using DataAccess;
using System.Data;

namespace BusinessService
{
    public class UsersManager
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

        public UsersManager()
        {
        }

        #endregion

        #region Methods


        public ContactAddresses GetUserAddressByUserId(Int64 userLoginId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@UserLoginId",userLoginId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Users_GetUsersBillingAddressByUserId]", parameters);
            return BaseEntityController.FillEntity<ContactAddresses>(reader);
        }

        public UserLogin ValidateUser(String username, String password)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@Username",username ),
                new Parameter("@Password",password )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Users_ValidateUser]", parameters);
            return BaseEntityController.FillEntity<UserLogin>(reader);
        }
        #endregion
    }
}
