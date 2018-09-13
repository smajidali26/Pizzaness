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
    public class BranchManager
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

        public BranchManager()
        {
        }

        #endregion

        #region Methods

        
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

        public ICollection<Branches> GetAllBranch()
        {
            #region Parameters

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Branch_GetAllBranch]", null);
            return BaseEntityController.FillEntities<Branches>(reader);
        }

        public Branches GetBranchById(Int32 branchId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",branchId )
            };

            #endregion
            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Branch_GetBranchById]", parameters);
            return BaseEntityController.FillEntity<Branches>(reader);
        }

        public bool IsStoreClose(Int32 branchId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@BranchId",branchId )
            };

            #endregion
            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Branch_IsStoreClose]", parameters);
            return (bool)result;
        }

        #endregion
    }
}
