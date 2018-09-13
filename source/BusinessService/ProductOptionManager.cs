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
    public class ProductOptionManager
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

        public ProductOptionManager()
        {
        }

        #endregion

        #region Methods
        public Int32 AddProductOption(OptionTypesInProduct productOption)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",productOption.BranchID),
                new Parameter("@ProductId",productOption.ProductID),
                new Parameter("@OptionTypeId",productOption.OptionTypeID),
                new Parameter("@IsMultiSelect",productOption.IsMultiSelect),
                new Parameter("@IsSamePrice",productOption.IsSamePrice),
                new Parameter("@IsAdonPriceVary",productOption.IsAdonPriceVary),
                new Parameter("@IsProductPriceChangeType",productOption.IsProductPriceChangeType),
                new Parameter("@ProductOptionInProductsXml",productOption.ProductOptionsXml)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[ProductOptionsInProducts_AddProductOptionsInProducts ]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public ICollection<OptionType> GetOptionTypeByProductId(int productId)
        {
            ICollection<OptionType> list = null;
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ProductId",productId)
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Product_GetOptionTypeByProductId]", parameters);
            list = BaseEntityController.FillEntities<OptionType>(reader);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!String.IsNullOrEmpty(list.ElementAt(i).ProductOptionXml))
                    {
                        list.ElementAt(i).ProductOptionList = Utility.XmlToObjectList<ProductOptions>(list.ElementAt(i).ProductOptionXml, "/ProductOptions/ProductOption");
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
