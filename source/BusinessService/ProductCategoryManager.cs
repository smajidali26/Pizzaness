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
    public class ProductCategoryManager
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

        public ProductCategoryManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get All product categories by branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public ICollection<ProductCategories> GetAllProductCategories(int branchId,bool displayOnWeb)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",branchId ),
                new Parameter("@DisplayOnWeb",displayOnWeb )
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[ProductCategories_GetAllProductCategories]", parameters);
            return BaseEntityController.FillEntities<ProductCategories>(reader);
        }

        /// <summary>
        /// Get All product categories by branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public ICollection<ProductCategories> GetProductCategories()
        {
            #region Parameters

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[ProductCategories_GetProductCategories]", null);
            return BaseEntityController.FillEntities<ProductCategories>(reader);
        }

        /// <summary>
        /// Delete  Product category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Int32 DeleteProductCategory(Int16 categoryId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductCategoryId",categoryId )
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[ProductCategories_DeleteProductCategory]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        /// <summary>
        /// Get ProductCategory By Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ProductCategories GetProductCategoryById(Int16 categoryId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@CategoryId",categoryId )
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[ProductCategories_GetProductCategoryById]", parameters);
            return BaseEntityController.FillEntity<ProductCategories>(reader);
        }

        /// <summary>
        /// Get All product categories by branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public ICollection<ProductCategories> GetHomePageProductCategories(int branchId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",branchId )
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[ProductCategories_GetHomePageProductCategories]", parameters);
            return BaseEntityController.FillEntities<ProductCategories>(reader);
        }
        #endregion
    }
}
