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

public partial class admin_products_AddProductOptions : BasePage
{
    private KlickEntities entities = new KlickEntities();
    private bool inEditMode = false;
    private ProductOption parentProductOption = null;
    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id = int.Parse(Request.QueryString["id"]);
                ProductOption option = (from po in entities.ProductOptions
                 where po.OptionID == id
                 select po).FirstOrDefault();
                txtName.Text = option.OptionName;
                         
                
                ViewState["ID"] = id;
                BindDataForUpdate();
                UpdateBtn.Visible = true;
                AddBtn.Visible = false;
            }
            else
            {
                BindData();
            }
            txtParent.Attributes.Add("onclick", "return OnClick();");
        }
    }

    private void BindData()
    {
        try
        {
            //List<ProductOption> producOptions = (from po in entities.ProductOptions
            //                                      where po.IsParentOption == false
            //                                      select po).ToList();
            //grdOptions.DataSource = producOptions;
            //grdOptions.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void BindDataForUpdate()
    {
        try
        {
             int id = int.Parse(Request.QueryString["id"]);
            // List<ProductOption> producOptions = (from po in entities.ProductOptions
            //                                      where po.IsParentOption == false && po.ProductOptionID != id
            //                                      select po).ToList();
            //grdOptions.DataSource = producOptions;
            //grdOptions.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void AddBtn_Click(object sender, EventArgs e)
    {
        try
        {
            txtError.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                ProductOption option = new ProductOption();

                option.OptionName = txtName.Text;
                
                List<ProductOption> childList = null;
                bool flag = true;
                if (txtParent.Checked)
                {
                    childList = new List<ProductOption>();
                    GridDataItemCollection coll = grdOptions.Items;
                    if (coll != null)
                    {
                        for (int i = 0; i < coll.Count; i++)
                        {
                            CheckBox chk = coll[i].FindControl("txtSelected") as CheckBox;

                            if (chk != null)
                            {
                                GridDataItem dataitem = coll[i];
                                if (chk.Checked)
                                {
                                    GridEditableItem editItem = coll[i] as GridEditableItem;
                                    int id = (int)editItem.GetDataKeyValue("ProductOptionID");
                                    ProductOption childOption = (from po in entities.ProductOptions
                                                                 where po.OptionID == id
                                                                 select po).FirstOrDefault();
                                    
                                }
                            }
                        }
                        
                    }
                }
                if (flag)
                {
                    
                    entities.ProductOptions.Add(option);

                    entities.SaveChanges();
                    SessionMessage = "Product option has been saved successfully.";
                    Response.Redirect("ProductOptions.aspx");
                }
            }
            else
                txtError.Text = "You must provide all valid values.";
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductOptions.aspx");
    }
        
    protected void grdOptions_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (inEditMode)
        {
            if (e.Item is GridDataItem)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                ProductOption option = e.Item.DataItem as ProductOption;
                CheckBox chk = item.FindControl("txtSelected") as CheckBox;
               
            }
        }
    }

    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        txtError.Text = string.Empty;
        if (!string.IsNullOrEmpty(txtName.Text))
        {
            if (ViewState["ID"] != null)
            {
                int id = (int)ViewState["ID"];
                bool flag = true;
                ProductOption option = (from po in entities.ProductOptions
                                        where po.OptionID == id
                                        select po).FirstOrDefault();



                //if (option.IsParentOption)
                //{
                //    if (option.IsParentOption != txtParent.Checked)
                //    {
                //        option.IsParentOption = txtParent.Checked;
                //        option.ProductOptions1.Clear();
                //    }
                //    else
                //    {
                //        GridDataItemCollection coll = grdOptions.Items;
                //        if (coll != null)
                //        {
                //            for (int i = 0; i < coll.Count; i++)
                //            {
                //                CheckBox chk = coll[i].FindControl("txtSelected") as CheckBox;

                //                if (chk != null)
                //                {
                //                    GridDataItem dataitem = coll[i];
                //                    GridEditableItem editItem = coll[i] as GridEditableItem;
                //                    int childid = (int)editItem.GetDataKeyValue("ProductOptionID");
                //                    ProductOption childOption = (from po in entities.ProductOptions
                //                                                 where po.ProductOptionID == childid
                //                                                 select po).FirstOrDefault();
                //                    if (chk.Checked)
                //                    {
                //                        if (!option.ProductOptions1.Contains(childOption))
                //                            option.ProductOptions1.Add(childOption);
                //                    }
                //                    else
                //                    {
                //                        if (option.ProductOptions1.Contains(childOption))
                //                        {
                //                            option.ProductOptions1.Remove(childOption);
                //                        }
                //                    }
                //                }
                //            }
                //            if (option.ProductOptions1.Count == 0)
                //            {
                //                txtError.Text = "You must select Sub Options if you have selected Is Parent.";
                //                Options.Style.Remove("display");
                //                flag = false;
                //            }
                //        }
                //    }
                //}
                //else if (txtParent.Checked)
                //{
                //    if (option.ProductOptions.Count > 0)
                //    {
                //        txtError.Text = "You can not make this option as Parent Option because this is child option of another Option.";
                //        flag = false;
                //    }
                //    else
                //    {
                //        GridDataItemCollection coll = grdOptions.Items;
                //        if (coll != null)
                //        {
                //            for (int i = 0; i < coll.Count; i++)
                //            {
                //                CheckBox chk = coll[i].FindControl("txtSelected") as CheckBox;

                //                if (chk != null)
                //                {
                //                    GridDataItem dataitem = coll[i];
                //                    GridEditableItem editItem = coll[i] as GridEditableItem;
                //                    int childid = (int)editItem.GetDataKeyValue("ProductOptionID");
                //                    ProductOption childOption = (from po in entities.ProductOptions
                //                                                 where po.ProductOptionID == childid
                //                                                 select po).FirstOrDefault();
                //                    if (chk.Checked)
                //                    {
                //                        if (!option.ProductOptions1.Contains(childOption))
                //                            option.ProductOptions1.Add(childOption);
                //                    }
                //                    else
                //                    {
                //                        if (option.ProductOptions1.Contains(childOption))
                //                        {
                //                            option.ProductOptions1.Remove(childOption);
                //                        }
                //                    }
                //                }
                //            }
                //            if (option.ProductOptions1.Count == 0)
                //            {
                //                txtError.Text = "You must select Sub Options if you have selected Is Parent.";
                //                Options.Style.Remove("display");
                //                flag = false;
                //            }
                //        }
                //    }
                //}

                //if (flag)
                //{
                //    using (TransactionScope transaction = new TransactionScope())
                //    {
                //        if (option.IsMultiSelect != txtIsMulti.Checked)
                //        {
                //            option.IsMultiSelect = txtIsMulti.Checked;
                //        }
                //        option.OptionName = txtName.Text;
                //        entities.SaveChanges();
                //        transaction.Complete();
                //        Response.Redirect("ProductOptions.aspx");
                //    }
                //}
            }
        }
        else
            txtError.Text = "Enter Product Option name.";
    }
}