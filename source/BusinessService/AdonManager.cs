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
    public class AdonManager
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

        public AdonManager()
        {
        }

        #endregion

        #region Methods

        public Int16 AddAdon(Adon adon)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnName",adon.AdOnName ),
                new Parameter("@AdonType",adon.AdonType),
                new Parameter("@ImageName",adon.ImageName)
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Adon_AddAdon]", parameters);
            return Convert.ToInt16(result.ToString());
        }

        public Int32 UpdateAdon(Adon adon)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnID",adon.AdOnID),
                new Parameter("@AdOnName",adon.AdOnName),
                new Parameter("@AdonType",adon.AdonType),
                new Parameter("@ImageName",adon.ImageName)
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Adon_UpdateAdon]", parameters);
            return Convert.ToInt32(result.ToString());
        }


        public Adon GetAdonById(Int16 adonId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnID",adonId)
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Adon_GetAdonById]", parameters);
            return BaseEntityController.FillEntity<Adon>(reader);
        }

        public ICollection<Adon> GetAdon(String adonName, Int32 pageNumber, Int32 pageSize)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnName",adonName)
                ,new Parameter("@PageNumber",pageNumber)
                ,new Parameter("@PageSize",pageSize)
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Adon_GetAdon]", parameters);
            return BaseEntityController.FillEntities<Adon>(reader);
        }

        public ICollection<Adon> GetAdonsByAdonTypeId(Int16 adonTypeId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnType",adonTypeId)
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Adon_GetAdonsByAdonTypeId]", parameters);
            return BaseEntityController.FillEntities<Adon>(reader);
        }

        public Int32 Deletedon(Int16 adonId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@AdOnID",adonId)
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Adon_Deletedon]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        #endregion
    }
}
