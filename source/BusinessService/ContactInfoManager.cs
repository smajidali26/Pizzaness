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
    public class ContactInfoManager
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

        public ContactInfoManager()
        {
        }

        #endregion

        #region Methods

        public Int64 AddContactInfo(ContactInfo contactInfo,Int16 userTypeId)
        {
            #region Parameters
            String addressXml = Utility.CollectionXml<ContactAddresses>(contactInfo.ContactAddressList, "ContactAddressesDataSet", "ContactAddressesDataTable");
            IParameter[] parameters = new Parameter[]{
                new Parameter("@Title",contactInfo.Title),
                new Parameter("@FirstName",contactInfo.FirstName),
                new Parameter("@LastName",contactInfo.LastName),
                new Parameter("@Email",contactInfo.Email),
                new Parameter("@Telephone",contactInfo.Telephone),
                new Parameter("@Mobile",contactInfo.Mobile),
                new Parameter("@UserTypeId",userTypeId),
                new Parameter("@ContactAddress",addressXml)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[ContactInfo_AddContactInfo]", parameters);
            return Convert.ToInt32(result);
        }

        public Int32 UpdateContactInfo(ContactInfo contactInfo)
        {
            #region Parameters
            String addressXml = Utility.CollectionXml<ContactAddresses>(contactInfo.ContactAddressList, "ContactAddressesDataSet", "ContactAddressesDataTable");
            IParameter[] parameters = new Parameter[]{
                new Parameter("@ContactInfoId",contactInfo.ContactInfoId),
                new Parameter("@FirstName",contactInfo.FirstName),
                new Parameter("@LastName",contactInfo.LastName),
                new Parameter("@Email",contactInfo.Email),
                new Parameter("@Telephone",contactInfo.Telephone),
                new Parameter("@Mobile",contactInfo.Mobile),
                new Parameter("@ContactAddress",addressXml)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[ContactInfo_UpdateContactInfo]", parameters);
            return Convert.ToInt32(result);
        }

        public ContactInfo GetContactInfoByEmailOrPhone(bool isEmail, String filterValue)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@FilterValue",filterValue),
                new Parameter("@IsEmail",isEmail)
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[ContactInfo_GetContactInfoByEmailOrPhone]", parameters);
            return BaseEntityController.FillEntity<ContactInfo>(reader);   
        }
        #endregion
    }
}
