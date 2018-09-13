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
    public class SliderImageManager
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

        public SliderImageManager()
        {
        }

        #endregion

        #region Methods


        public Int32 AddUpdateImageSlider(SliderImage sliderImage)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                 new Parameter("@SliderImageId",sliderImage.SliderImageId )
                ,new Parameter("@ImageName",sliderImage.ImageName )
                ,new Parameter("@ImagePath",sliderImage.ImagePath )
                ,new Parameter("@Description",sliderImage.Description )
                ,new Parameter("@IsEnabled",sliderImage.IsEnabled )
            };

            #endregion
            int result = DBHandler.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "[SliderImage_AddUpdateSliderImage]", parameters);
            return result;
        }

        public ICollection<SliderImage> GetSliders()
        {
            #region Parameters

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[SliderImage_GetSliders]", null);
            return BaseEntityController.FillEntities<SliderImage>(reader);
        }

        public ICollection<SliderImage> GetHomePageSliders()
        {
            #region Parameters

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[SliderImage_GetHomePageSliders]", null);
            return BaseEntityController.FillEntities<SliderImage>(reader);
        }

        public Int32 DeleteSliderImage(Int32 sliderImageId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                 new Parameter("@SliderImageId",sliderImageId )
            };

            #endregion
            int result = DBHandler.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "[SliderImage_DeleteSliderImage]", parameters);
            return result;
        }
        #endregion
    }
}
