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
    public class OrderManager
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

        public OrderManager()
        {
        }

        #endregion

        #region Methods

        public Int64 AddOrder(Orders order, ICollection<List<BusinessEntities.OrderDetailOptions>> OrderOptionList,
            ICollection<List<BusinessEntities.OrderDetailAdOns>> OrderAdonList)
        {
            int i = 0;
            foreach (OrderDetails orderDetail in order.OrderDetailsList)
            {
                
                if (OrderAdonList.ElementAt(i).Count > 0)
                {
                    orderDetail.OrderDetailAdOnsXml = Core.Utility.CollectionXml<OrderDetailAdOns>(OrderAdonList.ElementAt(i), "OrderDetailAdOnsDataSet", "OrderDetailAdOnsDataTable");
                }
                if (OrderOptionList.ElementAt(i).Count > 0)
                {
                    orderDetail.OrderDetailOptionXml = Core.Utility.CollectionXml<OrderDetailOptions>(OrderOptionList.ElementAt(i), "OrderDetailOptionsDataSet", "OrderDetailOptionsDataTable");
                }
                if (orderDetail.OrderDetailSubProducts != null && orderDetail.OrderDetailSubProducts.Count > 0)
                {
                    
                    foreach (OrderDetailSubProduct subProduct in orderDetail.OrderDetailSubProducts)
                    {
                        subProduct.OrderDetailSubProductAdonsXml = Core.Utility.CollectionXml<OrderDetailSubProductAdon>(subProduct.OrderDetailSubProductAdons, "OrderDetailAdOnsDataSet", "OrderDetailAdOnsDataTable");
                        subProduct.OrderDetailSubProductOptionsXml = Core.Utility.CollectionXml<OrderDetailSubProductOption>(subProduct.OrderDetailSubProductOptions, "OrderDetailOptionsDataSet", "OrderDetailOptionsDataTable");
                    }
                    orderDetail.OrderDetailSubProductsXml = Core.Utility.CollectionXml<OrderDetailSubProduct>(orderDetail.OrderDetailSubProducts, "OrderDetailSubProducts", "OrderDetailSubProduct");
                }
                i++;
            }

            String orderDetailsXml = Core.Utility.CollectionXml<OrderDetails>(order.OrderDetailsList, "OrderDetailDataSet", "OrderDetailDataTable");
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@DeliverDate",order.DeliveryDate),
                new Parameter("@ContactInfoId",order.ContactInfoId),
                new Parameter("@OrderStatusId",order.OrderStatusID),
                new Parameter("@BranchId",order.BranchID),
                new Parameter("@OrderTotal",order.OrderTotal),
                new Parameter("@DeliveryCharges",order.DeliveryCharges),
                new Parameter("@DeliveryAddress",order.DeliveryAddress),
                new Parameter("@OrderTypeId",order.OrderTypeID),
                new Parameter("@Discount",order.Discount),
                new Parameter("@TaxPercentage",order.TaxPercentage),
                new Parameter("@PaymentMethod",order.PaymentMethod),
                new Parameter("@PromotionCodeId",order.PromotionCodeId),
                new Parameter("@OrderDetailXml",orderDetailsXml),
                new Parameter("@PromotionValueUsed",order.PromotionValueUsed), 
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Orders_AddOrder]", parameters);
            return Convert.ToInt64(result.ToString());
        }
        /*
        public int UpdateCarParkingService(CarParkingService carParkingService)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@CarParkingServiceId",carParkingService.CarParkingServiceId),
                new Parameter("@ServiceName",carParkingService.ServiceName),
                new Parameter("@ServicePrice",carParkingService.ServicePrice)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[CarParkingService_UpdateCarParkingService]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public CarReservation GetCarReservationById(int carReservationId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@CarReservationId",carReservationId )
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[CarReservation_GetCarReservationById]", parameters);
            return BaseEntityController.FillEntity<CarReservation>(reader);
        }*/

        public Orders GetOrderById(Int64 orderId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@OrderId",orderId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Orders_GetOrderById]", parameters);
            return BaseEntityController.FillEntity<Orders>(reader);
        }

        public ICollection<Orders> GetOrder(int branchId,OrderStatus orderStatus, int pageNumber,int pageSize)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",branchId ),
                new Parameter("@OrderStatusId",Convert.ToInt32(orderStatus) ),
                new Parameter("@PageNumber",pageNumber ),
                new Parameter("@PageSize",pageSize )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Orders_GetOrder]", parameters);
            return BaseEntityController.FillEntities<Orders>(reader);
        }

        public ICollection<Orders> GetOrderByContactInfoId(int branchId, long contactInfoId, int pageNumber, int pageSize)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",branchId ),
                new Parameter("@ContactInfoId",contactInfoId),
                new Parameter("@PageNumber",pageNumber ),
                new Parameter("@PageSize",pageSize )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Orders_GetOrderByContactInfoId]", parameters);
            return BaseEntityController.FillEntities<Orders>(reader);
        }

        public ICollection<OrderDetails> GetOrderDetail(long orderId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@OrderId",orderId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[OrderDetail_GetOrderDetail]", parameters);
            return BaseEntityController.FillEntities<OrderDetails>(reader);
        }

        public int OrderPaid(Int64 orderId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@OrderId",orderId)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Orders_OrderPaid]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public int OrderDelivered(Int64 orderId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@OrderId",orderId)
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Orders_OrderDelivered]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public int DeleteOrder(Int64 orderId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@OrderId",orderId)
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Orders_DeleteOrder]", parameters);
            return Convert.ToInt32(result.ToString());
        }

        public DataSet SaleReport(Int16 reportType,DateTime? fromDate,DateTime? toDate)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ReportType",reportType)
                ,new Parameter("@FromDate",fromDate)
                ,new Parameter("@ToDate",toDate)
            };

            #endregion
            return DBHandler.DataAdapter(System.Data.CommandType.StoredProcedure, "[Order_SaleReport]", parameters);
        }
        #endregion
    }
}
