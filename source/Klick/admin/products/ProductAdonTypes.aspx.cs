using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using BusinessEntities;
using BusinessService;

public partial class admin_products_ProductAdonTypes : BasePage
{

    #region Private Members
    private AdonTypeManager _adonTypeManager = new AdonTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            grdProductAdonType.DataBind();
        }

    }

    private void BindData()
    {
        try
        {
            grdProductAdonType.DataSource = _adonTypeManager.GetAdonType(grdProductAdonType.CurrentPageIndex, grdProductAdonType.PageSize);
        }
        catch (Exception ex)
        {
            txtError.Text = "Error occured while binding AdOns Type data. Error Detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        }
    }

    protected void grdProductAdonType_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }

    protected void grdProductAdonType_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.PerformInsertCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtAdOnTypeName") as RadTextBox;
                CheckBox chk = editedItem.FindControl("txtFree") as CheckBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text) )
                {
                    try
                    {                       
                        AdonType adOnType = new AdonType();
                        adOnType.AdOnTypeName = name.Text;
                        adOnType.IsFreeAdonType = chk.Checked;
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        adOnType.ImageName = filename;
                        Int16 result = _adonTypeManager.AddAdonType(adOnType);
                        if (result > 0)
                        {
                            if (txtImage.HasFile)
                            {
                                String path = Server.MapPath("~/Products/AdonTypeImage/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + filename, 60, 60);
                            }
                            name.Text = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                       // RadAjaxManager1.Alert("Error occured while Adding new Record. Error Detail ->" + ex.Message);
                    }
                }
            }
        }
        else if (e.CommandName == RadGrid.UpdateCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtAdOnTypeName") as RadTextBox;
                CheckBox chk = editedItem.FindControl("txtFree") as CheckBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                Label error = editedItem.FindControl("error") as Label;
                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                        short id = (short)key["AdOnTypeId"];
                        AdonType adOnType = new AdonType();
                        adOnType.AdOnTypeId = id;
                        adOnType.AdOnTypeName = name.Text;
                        adOnType.IsFreeAdonType = chk.Checked;
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        adOnType.ImageName = filename;
                        Int32 result = _adonTypeManager.UpdateAdonType(adOnType);
                        if (result > 0)
                        {
                            if (txtImage.HasFile)
                            {
                                String path = Server.MapPath("~/Products/AdonTypeImage/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + filename, 60, 60);
                            }
                            name.Text = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        //RadAjaxManager1.Alert("Error occured while updating Record. Error Detail ->" + ex.Message);
                    }

                }
                else
                {
                    error.Text = "Enter required valid values.";
                    e.Canceled = true;
                }
                BindData();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridEditFormItem editedItem = (GridEditFormItem)btn.NamingContainer;// access the EditFormItem  
       
        if (btn.CommandName == RadGrid.PerformInsertCommandName)
        {
            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtAdOnTypeName") as RadTextBox;
                CheckBox chk = editedItem.FindControl("txtFree") as CheckBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        AdonType adOnType = new AdonType();
                        adOnType.AdOnTypeName = name.Text;
                        adOnType.IsFreeAdonType = chk.Checked;
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        adOnType.ImageName = filename;
                        Int16 result = _adonTypeManager.AddAdonType(adOnType);
                        if (result > 0)
                        {
                            if (txtImage.HasFile)
                            {
                                String path = Server.MapPath("~/Products/AdonTypeImage/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + filename, 60, 60);
                            }
                            name.Text = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                       // RadAjaxManager1.Alert("Error occured while Adding new Record. Error Detail ->" + ex.Message);
                    }
                }
            }
        }
        else if (btn.CommandName == RadGrid.UpdateCommandName)
        {
            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtAdOnTypeName") as RadTextBox;
                CheckBox chk = editedItem.FindControl("txtFree") as CheckBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                HiddenField hdAdonTypeId = editedItem.FindControl("hdAdonTypeId") as HiddenField;
                Label error = editedItem.FindControl("error") as Label;
                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        Int16 adonTypeId = 0;
                        Int16.TryParse(hdAdonTypeId.Value, out adonTypeId);
                        AdonType adOnType = new AdonType();
                        adOnType.AdOnTypeId = adonTypeId;
                        adOnType.AdOnTypeName = name.Text;
                        adOnType.IsFreeAdonType = chk.Checked;
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        adOnType.ImageName = filename;
                        Int32 result = _adonTypeManager.UpdateAdonType(adOnType);
                        if (result > 0)
                        {
                            if (txtImage.HasFile)
                            {
                                String path = Server.MapPath("~/Products/AdonTypeImage/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + filename, 60, 60);
                            }
                            name.Text = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        //RadAjaxManager1.Alert("Error occured while updating Record. Error Detail ->" + ex.Message);
                    }

                }
                else
                {
                    error.Text = "Enter required valid values.";
                }
                BindData();
            }
        }
    }
}