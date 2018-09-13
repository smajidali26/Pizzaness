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
using System.Data.Objects;

public partial class admin_products_OptionList : BasePage
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
            grdProductOptions.DataBind();
        }
    }

    private void BindData()
    {
        try
        {
            List<ProductOption> producOptions = (from po in entities.ProductOptions
                                                 select po).ToList();
            grdProductOptions.DataSource = producOptions;
        }
        catch (Exception ex)
        {
            txtError.Text = "Error occured while binding Options data. Error Detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        }
    }

    protected void grdProductOptions_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }
    protected void grdProductOptions_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.PerformInsertCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtOptionName") as RadTextBox;
                RadComboBox optionType = editedItem.FindControl("txtOptionType") as RadComboBox;
                Label error = editedItem.FindControl("error") as Label;

                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(optionType.SelectedValue))
                {
                    try
                    {
                        short id = short.Parse(optionType.SelectedValue);
                        OptionType SelectedOptionType = (from ot in entities.OptionTypes
                                                         where ot.OptionTypeID == id
                                                         select ot).FirstOrDefault();
                        if (SelectedOptionType != null)
                        {
                            ProductOption option = new ProductOption();
                            option.OptionName = name.Text;
                            option.OptionType = SelectedOptionType;
                            entities.ProductOptions.Add(option);
                            int value = entities.SaveChanges();
                            ShowMessage("Product option has been saved successfully.", MessageType.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("Error occured while adding Option. Error Detail ->" + ex.Message,MessageType.Error);
                    }
                }
                else
                {
                    ShowMessage("Enter required valid values.", MessageType.Error);
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
                RadTextBox name = editedItem.FindControl("txtOptionName") as RadTextBox;
                RadComboBox optionType = editedItem.FindControl("txtOptionType") as RadComboBox;
                Label error = editedItem.FindControl("error") as Label;
                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(optionType.SelectedValue))
                {
                    try
                    {                        
                        int id = (int)editedItem.GetDataKeyValue("OptionID");
                        short optiontypeid = short.Parse(optionType.SelectedValue);
                        ProductOption option = (from o in entities.ProductOptions
                                                 where o.OptionID == id
                                                 select o).FirstOrDefault();
                        OptionType optiontype = (from ot in entities.OptionTypes
                                                 where ot.OptionTypeID == optiontypeid
                                                 select ot).FirstOrDefault();
                        if (option != null)
                        {
                            using (TransactionScope transaction = new TransactionScope())
                            {
                                option.OptionName = name.Text;
                                option.OptionType = optiontype;
                                int value = entities.SaveChanges();
                                transaction.Complete();
                                ShowMessage("Product option has been saved successfully.", MessageType.Success);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("Error occured while updating option record. Error Detail ->" + ex.Message, MessageType.Error);
                    }

                }
                else
                {
                    ShowMessage("Enter required valid values.", MessageType.Error);
                    e.Canceled = true;
                }
                BindData();
            }
        }

    }
    protected void grdProductOptions_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }
    protected void grdProductOptions_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditFormItem item = e.Item as GridEditFormItem;
            RadComboBox ddlOptionType = item.FindControl("txtOptionType") as RadComboBox;
            ddlOptionType.DataSource = (from t in entities.OptionTypes
                                       select t).ToList();
        }
    }
}