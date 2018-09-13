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
using BusinessService;
using BusinessEntities;

public partial class admin_products_ProducAdOns : BasePage
{
    #region Private Members
    private AdonManager _adonManager = new AdonManager();
    private AdonTypeManager _adonTypeManager = new AdonTypeManager();
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            grdProductAdon.DataBind();
        }

    }

    protected void grdProductAdon_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindData();
    }

    protected void grdProductAdon_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditFormItem item = e.Item as GridEditFormItem;
            RadComboBox ddlPAdOnType = item.FindControl("txtAdonType") as RadComboBox;
            ddlPAdOnType.DataSource = _adonTypeManager.GetAllAdonType();       
        }
        else if (e.Item is GridGroupHeaderItem)
        {
            
            GridGroupHeaderItem item = e.Item as GridGroupHeaderItem;
            
        }
    }

    protected void grdProductAdon_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.DeleteCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                Label error = editedItem.FindControl("error") as Label;

                try
                {
                    Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                    short id = (short)key["AdOnID"];
                    _adonManager.Deletedon(id);
                }
                catch (Exception ex)
                {
                    error.Text = ex.Message;
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
                RadTextBox name = editedItem.FindControl("txtAdOnName") as RadTextBox;
                RadComboBox adonType = editedItem.FindControl("txtAdonType") as RadComboBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        Adon adon = new Adon();
                        adon.AdOnName = name.Text;
                        adon.AdonType = short.Parse(adonType.SelectedValue);
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        adon.ImageName = filename;
                        Int16 result = _adonManager.AddAdon(adon);
                        if (result > 0)
                        {
                            if (txtImage.HasFile)
                            {
                                String path = Server.MapPath("~/Products/AdonImage/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + filename, 60, 60);
                            }
                            name.Text = string.Empty;
                            ShowMessage("Adon has been added successfully.", MessageType.Success);
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
                RadTextBox name = editedItem.FindControl("txtAdOnName") as RadTextBox;
                RadComboBox adonType = editedItem.FindControl("txtAdonType") as RadComboBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                HiddenField hdAdonId = editedItem.FindControl("hdAdonId") as HiddenField;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        Int16 adonId = 0;
                        Int16.TryParse(hdAdonId.Value, out adonId);
                        Adon adon = new Adon();
                        adon.AdOnID = adonId;
                        adon.AdOnName = name.Text;
                        adon.AdonType = short.Parse(adonType.SelectedValue);
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        adon.ImageName = filename;
                        Int32 result = _adonManager.UpdateAdon(adon);
                        if (result > 0)
                        {
                            if (txtImage.HasFile)
                            {
                                String path = Server.MapPath("~/Products/AdonImage/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + filename, 60, 60);
                            }
                            name.Text = string.Empty;
                            ShowMessage("Adon has been updated successfully.", MessageType.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        error.Text = ex.Message;
                    }
                }
                BindData();
            }
        }
    }

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        grdProductAdon.CurrentPageIndex = 0;
        BindData();
        grdProductAdon.DataBind();
    }

    protected void buttonClear_Click(object sender, EventArgs e)
    {
        txtSearchAdonName.Text = string.Empty;
        grdProductAdon.CurrentPageIndex = 0;
        BindData();
        grdProductAdon.DataBind();
    }

    #endregion

    #region Private Methods
    
    private void BindData()
    {
        try
        {
            ICollection<Adon> list = _adonManager.GetAdon(txtSearchAdonName.Text, grdProductAdon.CurrentPageIndex + 1, grdProductAdon.PageSize);
            if (list != null && list.Count > 0)
                grdProductAdon.VirtualItemCount = list.ElementAt(0).TotalRows;
            grdProductAdon.DataSource = list;            
        }
        catch (Exception ex)
        {
            txtError.Text = "Error occured while binding AdOns data. Error Detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        }
    }

    #endregion
    
}