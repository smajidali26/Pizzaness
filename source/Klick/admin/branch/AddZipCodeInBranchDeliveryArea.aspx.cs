using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using Telerik.Web.UI;

public partial class admin_branch_AddZipCodeInBranchDeliveryArea : BasePage
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
            BindBranchCombo();
            BindData();            
        }
    }

    private void BindBranchCombo()
    {
        txtError.Text = string.Empty;
        try
        {
            List<Branch> list = entities.Branches.ToList();
            txtCbBranch.DataSource = list;
            txtCbBranch.DataBind();
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindData()
    {
        txtError.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue))
            {
                int branchid = int.Parse(txtCbBranch.SelectedValue);
                List<BranchDeliveryArea> list = entities.BranchDeliveryAreas.Where(bd=>bd.BranchID == branchid).ToList();
                grdBranchDeliveryZipCode.DataSource = list;
            }
            else
                grdBranchDeliveryZipCode.DataSource = null;
            grdBranchDeliveryZipCode.DataBind();
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }
    protected void grdBranchDeliveryZipCode_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Branch branch = e.Item.DataItem as Branch;
            GridDataItem item = e.Item as GridDataItem;
            List<BranchDeliveryArea> list = entities.BranchDeliveryAreas.Where(bd => bd.BranchID == branch.BranchID).ToList();
            if (list != null)
            {
                string deliverarea = string.Empty;
                for (int i = 0; i < list.Count;i++ )
                {
                    if (i + 1 != list.Count)
                    {
                        deliverarea += list[i].ZipCode + ", ";
                    }
                    else
                    {
                        deliverarea += list[i].ZipCode;
                    }
                    Label txtZipcodes = item.FindControl("txtZipcodes") as Label;
                    txtZipcodes.Text = deliverarea;
                }

            }
        }
    }
    protected void subBtn_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(txtZipCode.Text))
        {
            int code = int.Parse(txtZipCode.Text);
            int branchid = int.Parse(txtCbBranch.SelectedValue);
            if (entities.BranchDeliveryAreas.Where(bd => bd.BranchID == branchid && bd.ZipCode == code).FirstOrDefault() == null)
            {
                BranchDeliveryArea branchdeliveryarea = new BranchDeliveryArea();
                branchdeliveryarea.BranchID = branchid;
                branchdeliveryarea.ZipCode = code;

                entities.BranchDeliveryAreas.Add(branchdeliveryarea);
                entities.SaveChanges();
                BindData();
            }
            else
                txtError.Text = "Zip Code already exist for selected branch.";
        }
        else
            txtError.Text = "Enter Zip code.";
    }
    protected void cancelBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Branches.aspx");
    }
}