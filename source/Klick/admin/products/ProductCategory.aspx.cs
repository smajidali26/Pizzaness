using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessService;
using BusinessEntities;
using Telerik.Web.UI;

public partial class admin_products_ProductCategory : BasePage
{
    private KlickEntities entities = new KlickEntities();
    private ProductCategoryManager _categoryManager = new ProductCategoryManager();

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
            grdProductCategory.DataBind();
        }
    }

    protected void grdProductCategory_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.PerformInsertCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtCategoryName") as RadTextBox;
                RadTextBox description = editedItem.FindControl("txtDescription") as RadTextBox;
                CheckBox enable = editedItem.FindControl("txtEnabled") as CheckBox;
                RadTextBox displayOrder = editedItem.FindControl("txtDisplayOrder") as RadTextBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                CheckBox txtDisplayOnHomePage = editedItem.FindControl("txtDisplayOnHomePage") as CheckBox;
                CheckBox txtDisplayOnWebSite = editedItem.FindControl("txtDisplayOnWebSite") as CheckBox;
                CheckBox txtDisplayOnDesktop = editedItem.FindControl("txtDisplayOnDesktop") as CheckBox;

                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        ProductCategory category = new ProductCategory();
                        category.Name = name.Text;
                        category.Description = description.Text;
                        category.IsActive = enable.Checked;
                        category.DisplayOrder = Convert.ToInt16(displayOrder.Text);
                        category.DisplayOnHomePage = txtDisplayOnHomePage.Checked;
                        category.WebCategory = txtDisplayOnWebSite.Checked;
                        category.DesktopCategory = txtDisplayOnDesktop.Checked;
                        string filename = string.Empty;
                        if (txtImage.HasFile)
                            filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(txtImage.FileName);

                        category.ImagePath = filename;
                        entities.ProductCategories.Add(category);
                        int value = entities.SaveChanges();
                        if(value> 0)
                        {
                            if (txtImage.HasFile)
                            {
                                
String path = Server.MapPath("~/Products/Category/");
                                txtImage.SaveAs(path + filename);
                                ThumbImage(path + filename, path + "S_" + filename, 60, 60);
                                ThumbImage(path + filename, path + "M_" + filename, 200, 110);
                                ThumbImage(path + filename, path + "L_" + filename, 280, 180);
                            }
                        }
                        ShowMessage("Product category has been saved successfully.", MessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        //RadAjaxManager1.Alert("Error occured while updating Category Record. Error Detail ->" + ex.Message);
                    }
                }
            }
        }
        else if (e.CommandName == RadGrid.UpdateCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                RadTextBox name = editedItem.FindControl("txtCategoryName") as RadTextBox;
                RadTextBox description = editedItem.FindControl("txtDescription") as RadTextBox;
                CheckBox enable = editedItem.FindControl("txtEnabled") as CheckBox;
                RadTextBox displayOrder = editedItem.FindControl("txtDisplayOrder") as RadTextBox;
                FileUpload txtImage = editedItem.FindControl("txtImage") as FileUpload;
                CheckBox txtDisplayOnHomePage = editedItem.FindControl("txtDisplayOnHomePage") as CheckBox;
                CheckBox txtDisplayOnWebSite = editedItem.FindControl("txtDisplayOnWebSite") as CheckBox;
                CheckBox txtDisplayOnDesktop = editedItem.FindControl("txtDisplayOnDesktop") as CheckBox;
                Label error = editedItem.FindControl("error") as Label;
                
                if (!string.IsNullOrEmpty(name.Text))
                {
                    try
                    {
                        Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                        short id = (short)key["CategoryID"];
                        ProductCategory category = (from c in entities.ProductCategories
                                                    where c.CategoryID == id
                                                    select c).FirstOrDefault();
                        if (category != null)
                        {
                            using (TransactionScope transaction = new TransactionScope())
                            {
                                category.Name = name.Text;
                                category.Description = description.Text;
                                category.IsActive = enable.Checked;
                                category.DisplayOrder = Convert.ToInt16(displayOrder.Text);
                                category.DisplayOnHomePage = txtDisplayOnHomePage.Checked;
                                category.WebCategory = txtDisplayOnWebSite.Checked;
                                category.DesktopCategory = txtDisplayOnDesktop.Checked;
                                string filename = string.Empty;
                                if (txtImage.HasFile)
                                {
                                    filename = Guid.NewGuid().ToString() +
                                               System.IO.Path.GetExtension(txtImage.FileName);

                                    category.ImagePath = filename;
                                }
                                int value = entities.SaveChanges();
                                transaction.Complete();

                                if(value>0)
                                {
                                    if (txtImage.HasFile)
                                    {
                                        String path = Server.MapPath("~/Products/Category/");
                                        txtImage.SaveAs(path + filename);
                                        ThumbImage(path + filename, path + "S_" + filename, 60, 60);
                                        ThumbImage(path + filename, path + "M_" + filename, 200, 110);
                                        ThumbImage(path + filename, path + "L_" + filename, 280, 180);
                                    }
                                }
                                ShowMessage("Product category has been saved successfully.", MessageType.Success);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //RadAjaxManager1.Alert("Error occured while updating Category Record. Error Detail ->" + ex.Message);
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
        else if (e.CommandName == RadGrid.DeleteCommandName)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            Int16 id = (Int16)item.GetDataKeyValue("CategoryID");
            try
            {
                int result = _categoryManager.DeleteProductCategory(id);
                if (result > 0)
                {
                    BindData();
                }
            }
            catch (Exception ex)
            {
                txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            }
            
        }
    }

    protected void grdProductCategory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        try
        {
            List<ProductCategory> categories = (from c in entities.ProductCategories
                                                orderby c.DisplayOrder
                                                select c).ToList();
            grdProductCategory.DataSource = categories;
        }
        catch (Exception ex)
        {
            txtError.Text = "Error occured while binding categories data. Error Detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        }
    }
}