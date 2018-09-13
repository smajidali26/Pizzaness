using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Telerik.Web.UI;
using System.Configuration;
using System.Transactions;
using System.Data;

public partial class admin_products_OptionTypeList : BasePage
{
    private KlickEntities entities = new KlickEntities();

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            grdOptionTypes.DataBind();
        }
    }

    private void BindData()
    {
        try
        {
            List<OptionType> categories = (from c in entities.OptionTypes
                                           select c).ToList();
            grdOptionTypes.DataSource = categories;
        }
        catch (Exception ex)
        {
            txtError.Text = "Error occured while binding option types data. Error Detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        }
    }

    protected void grdOptionTypes_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }
    protected void grdOptionTypes_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.PerformInsertCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtOptionTypeName") as RadTextBox;
                RadTextBox displaytext = editedItem.FindControl("txtOptionTypeDispllayText") as RadTextBox;
                Label error = editedItem.FindControl("error") as Label;
                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(displaytext.Text))
                {
                    try
                    {
                        OptionType optionType = new OptionType();
                        optionType.OptionTypeName = name.Text;
                        optionType.OptionDisplayText = displaytext.Text;

                        entities.OptionTypes.Add(optionType);
                        int value = entities.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        RadAjaxManager1.Alert("Error occured while adding option type. Error Detail ->" + ex.Message);
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
        else if (e.CommandName == RadGrid.UpdateCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtOptionTypeName") as RadTextBox;
                RadTextBox displaytext = editedItem.FindControl("txtOptionTypeDispllayText") as RadTextBox;
                Label error = editedItem.FindControl("error") as Label;
                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(displaytext.Text))
                {
                    try
                    {
                        Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                        short id = (short)key["OptionTypeID"];
                        OptionType optiontype = (from ot in entities.OptionTypes
                                                    where ot.OptionTypeID == id
                                                    select ot).FirstOrDefault();
                        if (optiontype != null)
                        {
                            using (TransactionScope transaction = new TransactionScope())
                            {
                                optiontype.OptionTypeName = name.Text;
                                optiontype.OptionDisplayText = displaytext.Text;                                
                                entities.SaveChanges();
                                transaction.Complete();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RadAjaxManager1.Alert("Error occured while updating option type. Error Detail ->" + ex.Message);
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
}