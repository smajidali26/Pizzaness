using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessService;
using BusinessEntities;
using Telerik.Web.UI;

public partial class admin_Settings_Sliders : BasePage
{
    #region Private Members
    private SliderImageManager _sliderImageManager = new SliderImageManager();
    #endregion

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            grdSliderImage.DataBind();
        }
    }

    protected void grdSliderImage_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }

    protected void grdSliderImage_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.PerformInsertCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtImageName") as RadTextBox;
                RadTextBox description = editedItem.FindControl("txtDescription") as RadTextBox;
                RadAsyncUpload image = editedItem.FindControl("txtImage") as RadAsyncUpload;
                CheckBox chk = editedItem.FindControl("txtIsEnabled") as CheckBox;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text) && image.UploadedFiles.Count > 0)
                {
                    try
                    {
                        SliderImage slider = new SliderImage();
                        slider.ImageName = name.Text.Trim();
                        slider.Description = description.Text.Trim();
                        slider.ImagePath = image.UploadedFiles[0].FileName;
                        slider.IsEnabled = chk.Checked;
                        _sliderImageManager.AddUpdateImageSlider(slider);
                        image.UploadedFiles[0].SaveAs(Server.MapPath("~/Sliders/" + slider.ImagePath));
                    }
                    catch (Exception ex)
                    {
                        RadAjaxManager1.Alert("Error occured while Adding new Record. Error Detail ->" + ex.Message);
                    }
                }
                else
                {
                    RadAjaxManager1.Alert("Image Name and Image File are required.");
                }
            }
        }
        else if (e.CommandName == RadGrid.UpdateCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                
                RadTextBox name = editedItem.FindControl("txtImageName") as RadTextBox;
                RadTextBox description = editedItem.FindControl("txtDescription") as RadTextBox;
                RadAsyncUpload image = editedItem.FindControl("txtImage") as RadAsyncUpload;
                CheckBox chk = editedItem.FindControl("txtIsEnabled") as CheckBox;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        SliderImage slider = new SliderImage();
                        slider.SliderImageId = (Int32)key["SliderImageId"];
                        slider.ImageName = name.Text.Trim();
                        slider.Description = description.Text.Trim();
                        if (image.UploadedFiles.Count > 0)
                            slider.ImagePath = image.UploadedFiles[0].FileName;
                        else
                            slider.ImagePath = String.Empty;
                        slider.IsEnabled = chk.Checked;
                        _sliderImageManager.AddUpdateImageSlider(slider);
                        if (image.UploadedFiles.Count > 0)
                            image.UploadedFiles[0].SaveAs(Server.MapPath("~/Sliders/" + slider.ImagePath));
                    }
                    catch (Exception ex)
                    {
                        RadAjaxManager1.Alert("Error occured while Adding new Record. Error Detail ->" + ex.Message);
                    }
                }
                else
                {
                    RadAjaxManager1.Alert("Image Name and Image File are required.");
                }
                BindData();
            }
        }
        else if (e.CommandName == RadGrid.DeleteCommandName)
        {
             GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                Int32 id = (Int32)key["SliderImageId"];
                Int32 result = _sliderImageManager.DeleteSliderImage(id);
                if (result > 0)
                    BindData();
            }
        }
    }

    #endregion

    #region Private Methods

    private void BindData()
    {
        grdSliderImage.DataSource = _sliderImageManager.GetSliders();
    }

    #endregion
}