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
    public class ProductManager
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

        public ProductManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int AddProduct(Products product)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@CategoryID",product.CategoryID )
                ,new Parameter("@Name",product.Name )
                ,new Parameter("@Description",product.Description )
                ,new Parameter("@Image",product.Image )
                ,new Parameter("@ImagePath",product.ImagePath )
                ,new Parameter("@IsActive",product.IsActive )
                ,new Parameter("@IsSpecial",product.IsSpecial )
                ,new Parameter("@DisplayOrder",product.DisplayOrder )
                ,new Parameter("@Days",product.ProductActivationObject.Days )
                ,new Parameter("@DisplayOnFullDay",product.ProductActivationObject.DisplayOnFullDay)
                ,new Parameter("@StartTime",product.ProductActivationObject.StartTime )
                ,new Parameter("@EndTime",product.ProductActivationObject.EndTime )
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Product_AddProduct]", parameters);
            return Convert.ToInt32(result.ToString());
        }
        
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int UpdateProduct(Products product)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                 new Parameter("@ProductID",product.ProductID )
                ,new Parameter("@CategoryID",product.CategoryID )
                ,new Parameter("@Name",product.Name )
                ,new Parameter("@Description",product.Description )
                ,new Parameter("@Image",product.Image )
                ,new Parameter("@ImagePath",product.ImagePath )
                ,new Parameter("@IsActive",product.IsActive )
                ,new Parameter("@IsSpecial",product.IsSpecial )
                ,new Parameter("@DisplayOrder",product.DisplayOrder)
                ,new Parameter("@Days",product.ProductActivationObject.Days )
                ,new Parameter("@DisplayOnFullDay",product.ProductActivationObject.DisplayOnFullDay)
                ,new Parameter("@StartTime",product.ProductActivationObject.StartTime )
                ,new Parameter("@EndTime",product.ProductActivationObject.EndTime )
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Product_UpdateProduct]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        /// <summary>
        /// Get Product Options Type and Adons Type
        /// </summary>
        /// <param name="branchProductId"></param>
        /// <returns>Return data set</returns>
        public DataSet GetProductDetail(long branchProductId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchProductId",branchProductId )
            };

            #endregion
            object result = DBHandler.DataAdapter(System.Data.CommandType.StoredProcedure, "[Product_GetProductDetails]", parameters);
            return (DataSet)result;
        }

        /// <summary>
        /// Get Products for deal
        /// </summary>
        /// <returns></returns>
        public ICollection<Products> GetProductsForDeal()
        {            
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetProductsForDeal]", null);
            return BaseEntityController.FillEntities<Products>(reader);
        }

        /// <summary>
        /// Get Home page Products
        /// </summary>
        /// <returns></returns>
        public ICollection<Products> GetHomePageProducts()
        {
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetHomePageProducts]", null);
            return BaseEntityController.FillEntities<Products>(reader);
        }

        /// <summary>
        /// Get All products that are not added in branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public ICollection<Products> GetProductsNotInBranch(Int32 branchId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchID",branchId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetProductsNotInBranch]", parameters);
            return BaseEntityController.FillEntities<Products>(reader);
        }

        /// <summary>
        /// Get Product by ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Products GetProductById(int productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetProductById]", parameters);
            return BaseEntityController.FillEntity<Products>(reader);
        }

        public Products GetProductDetailByProductId(int productId, Int32? comboId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
                ,new Parameter("@ComboId",comboId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetProductDetailsByProductId]", parameters);
            return BaseEntityController.FillEntity<Products>(reader);
        }

        /// <summary>
        /// Get all deal child products that were added against deal
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ICollection<ProductsChildRelationship> GetDealProductsByProductId(Int32 productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetDealProductsByProductId]", parameters);
            return BaseEntityController.FillEntities<ProductsChildRelationship>(reader);
        }

        /// <summary>
        /// Get all deal child products that were added against deal
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ICollection<Products> GetDealProductsByProductIdForOrder(Int32 productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetDealProductsByProductIdForOrder]", parameters);
            return BaseEntityController.FillEntities<Products>(reader);
        }

        /// <summary>
        /// Add Combo products
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public Int32 AddComboDealProducts(ICollection<ProductsChildRelationship> products)
        {
            #region Parameters

            String ProductsXml = Core.Utility.CollectionXml<ProductsChildRelationship>(products, "ChildProductDataSet", "ChildProductDataTable");
            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductsXml",ProductsXml )
            };
            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Product_AddComboProducts]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        /// <summary>
        /// Add Options of combo deal product 
        /// </summary>
        /// <param name="productOptions"></param>
        /// <returns></returns>
        public Int32 AddProductChildProductOption(ICollection<ProductChildProductOption> productOptions)
        {
            #region Parameters

            String ProductsXml = Core.Utility.CollectionXml<ProductChildProductOption>(productOptions, "ProductOptionsDataSet", "ProductOptionsDataTable");
            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductChildProductOptionXml",ProductsXml )
            };
            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[ProductChildProductOption_AddProductChildProductOptionBulk]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public Int32 AddProductChildAdon(ICollection<ProductChildAdon> productOptions)
        {
            #region Parameters

            String ProductsXml = Core.Utility.CollectionXml<ProductChildAdon>(productOptions, "ProductAdonsDataSet", "ProductAdonsDataTable");
            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductChildAdonXml",ProductsXml )
            };
            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[ProductChildAdon_AddProductChildAdonBulk]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public ICollection<ProductOptions> GetDealOptionsByProductId(Int32 productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetDealOptionsByProductId]", parameters);
            return BaseEntityController.FillEntities<ProductOptions>(reader);
        }

        public ICollection<ProductAdon> GetDealAdonsByProductId(Int32 productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetDealAdonByProductId]", parameters);
            return BaseEntityController.FillEntities<ProductAdon>(reader);
        }

        public DataSet GetAdonsForComboProducts(Int32 productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            object result = DBHandler.DataAdapter(System.Data.CommandType.StoredProcedure, "[Product_GetAdonForDeal]", parameters);
            return (DataSet)result;
        }

        /// <summary>
        /// Get options for combo products
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public DataSet GetOptionsForComboProducts(Int32 productId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId )
            };

            #endregion
            object result = DBHandler.DataAdapter(System.Data.CommandType.StoredProcedure, "[Product_GetOptionsForDeal]", parameters);
            return (DataSet)result;
        }

        public Int32 DeleteBranchProductOptionType(long productOptionTypeId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductOptionTypeId",productOptionTypeId )
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Product_DeleteBranchProductOptionType]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public Int32 DeleteBranchProductAdonType(long productAdonTypeId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductAdonTypeId",productAdonTypeId )
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Product_DeleteBranchProductAdonType]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        /// <summary>
        /// Get all deal child products that were added against deal
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ICollection<Products> GetProductByCategoryId(Int16 categoryId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@CategoryId",categoryId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetProductByCategoryId]", parameters);
            return BaseEntityController.FillEntities<Products>(reader);
        }
        #endregion
    }
}
