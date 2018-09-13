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
    public class ProductInBranchManager
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

        public ProductInBranchManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add or Update Product in Branch
        /// </summary>
        /// <param name="productInBranch"></param>
        /// <returns></returns>
        public Int32 AddUpdateProductInBranch(ProductsInBranches productInBranch)
        {
            #region Parameters
            
            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductID",productInBranch.ProductID),
                new Parameter("@BranchID",productInBranch.BranchID),
                new Parameter("@Enable",productInBranch.Enable),
                new Parameter("@Price",productInBranch.Price)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[BranchProduct_AddUpdateProductInBranch]", parameters);
            return Convert.ToInt32(result);
        }

        public ProductsInBranches GetProductInBranchById(Int64 branchProductID)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchProductID",branchProductID)
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[BranchProduct_GetProductInBranchById]", parameters);
            return BaseEntityController.FillEntity<ProductsInBranches>(reader);
        }

        #endregion
    }
}
