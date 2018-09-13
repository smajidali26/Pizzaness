using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessEntities;
using Core;
using DataAccess;

namespace BusinessService
{
    public class AdonTypeManager
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

        public AdonTypeManager()
        {
        }

        #endregion

        #region Methods

        public Int16 AddAdonType(AdonType adonType)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnTypeName",adonType.AdOnTypeName ),
                new Parameter("@IsFreeAdonType",adonType.IsFreeAdonType),
                new Parameter("@ImageName",adonType.ImageName)
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[AdonType_AddAdonType]", parameters);
            return Convert.ToInt16(result.ToString());
        }

        public Int32 UpdateAdonType(AdonType adonType)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnTypeId",adonType.AdOnTypeId ),
                new Parameter("@AdOnTypeName",adonType.AdOnTypeName ),
                new Parameter("@IsFreeAdonType",adonType.IsFreeAdonType),
                new Parameter("@ImageName",adonType.ImageName)
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[AdonType_UpdateAdonType]", parameters);
            return Convert.ToInt32(result.ToString());
        }


        public AdonType GetAdonTypeById(Int16 adonTypeId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnTypeId",adonTypeId)
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[AdonType_GetAdonTypeById]", parameters);
            return BaseEntityController.FillEntity<AdonType>(reader);
        }

        public ICollection<AdonType> GetAdonType(Int32 pageNumber,Int32 pageSize)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@PageNumber",pageNumber)
                ,new Parameter("@PageSize",pageSize)
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[AdonType_GetAdonType]", parameters);
            return BaseEntityController.FillEntities<AdonType>(reader);
        }

        public ICollection<AdonType> GetAllAdonType()
        {
            #region Parameters

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[AdonType_GetAllAdonType]", null);
            return BaseEntityController.FillEntities<AdonType>(reader);
        }

        public ICollection<AdonType> GetAdonByProductId(Int32 productId)
        {
            ICollection<AdonType> list = null;
            #region Parameters
            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId)
            };
            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetAdonByProductId]", parameters);
            list = BaseEntityController.FillEntities<AdonType>(reader);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list.ElementAt(i).ProductAdonXml))
                    {
                        list.ElementAt(i).ProductAdonList = Utility.XmlToObjectList<Adon>(list.ElementAt(i).ProductAdonXml, "/Adons/Adon");
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
