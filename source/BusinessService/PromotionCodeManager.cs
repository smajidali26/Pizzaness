using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BusinessEntities;
using System.Data;
using Core;

namespace BusinessService
{
    public class PromotionCodeManager
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

        public PromotionCodeManager()
        {
        }

        #endregion

        #region Methods

        public int AddPromotionCode(PromotionCodes promotionCode)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                 new Parameter("@PromotionName",promotionCode.PromotionName )
                ,new Parameter("@PromotionCode",promotionCode.PromotionCode )
                ,new Parameter("@TypeOfPromotion",Convert.ToInt16(promotionCode.TypeOfPromotion))
                ,new Parameter("@StartDate",promotionCode.StartDate )
                ,new Parameter("@EndDate",promotionCode.EndDate )
                ,new Parameter("@PromotionValue",promotionCode.PromotionValue )
                ,new Parameter("@CodeUsageCounter",promotionCode.CodeUsageCounter )
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[PromotionCode_AddPromotionCode]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public int UpdatePromotionCode(PromotionCodes promotionCode)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                 new Parameter("@PromotionCodeId",promotionCode.PromotionCodeId )
                ,new Parameter("@PromotionName",promotionCode.PromotionName )
                ,new Parameter("@PromotionCode",promotionCode.PromotionCode )
                ,new Parameter("@TypeOfPromotion",Convert.ToInt16(promotionCode.TypeOfPromotion))
                ,new Parameter("@StartDate",promotionCode.StartDate )
                ,new Parameter("@EndDate",promotionCode.EndDate )
                ,new Parameter("@PromotionValue",promotionCode.PromotionValue )
                ,new Parameter("@CodeUsageCounter",promotionCode.CodeUsageCounter )
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[PromotionCode_UpdatePromotionCode]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        /// <summary>
        /// Get PromotionCodes by PromotionCodeId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public PromotionCodes GetPromotionCodesById(int promotionCodeId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@PromotionCodeId",promotionCodeId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[PromotionCode_GetPromotionCodeById]", parameters);
            return BaseEntityController.FillEntity<PromotionCodes>(reader);
        }

        /// <summary>
        /// Get PromotionCodes by PromotionCode
        /// </summary>
        /// <param name="promotionCode"></param>
        /// <returns></returns>
        public PromotionCodes GetPromotionCodeByCode(String promotionCode)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@PromotionCode",promotionCode )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[PromotionCode_GetPromotionCodeByCode]", parameters);
            return BaseEntityController.FillEntity<PromotionCodes>(reader);
        }

        /// <summary>
        /// Get Promotion code list
        /// </summary>
        /// <param name="promotionName"></param>
        /// <param name="promotionCode"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ICollection<PromotionCodes> GetPromotionCode(String promotionName,String promotionCode,Int32 pageNumber,Int32 pageSize)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@PromotionName",promotionName )
                ,new Parameter("@PromotionCode",promotionCode )
                ,new Parameter("@PageNumber",pageNumber )
                ,new Parameter("@PageSize",pageSize )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[PromotionCode_GetPromotionCode]", parameters);
            return BaseEntityController.FillEntities<PromotionCodes>(reader);
        }

        /// <summary>
        /// Delete promotion code
        /// </summary>
        /// <param name="promotionCodeId">Promotion Code Id</param>
        /// <returns></returns>
        public int DeletePromotionCode(int promotionCodeId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@PromotionCodeId",promotionCodeId )
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[PromotionCode_DeletePromotionCode]", parameters);
            return Convert.ToInt32(result.ToString());
        }
        #endregion
    }
}
