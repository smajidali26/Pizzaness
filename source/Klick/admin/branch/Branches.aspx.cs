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

public partial class admin_branch_Branches : BasePage
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
            grdBranches.DataBind();            
        }
    }   

    private void BindData()
    {
        try
        {
            List<Branch> branches = (from b in entities.Branches
                                     select b).ToList();
            grdBranches.DataSource = branches;

        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }
    protected void grdBranches_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindData();
    }
    protected void grdBranches_ItemCommand(object source, GridCommandEventArgs e)
    {
        txtError.Text = string.Empty;
        if (e.CommandName == RadGrid.InitInsertCommandName)
        {
            Response.Redirect("AddBranch.aspx");
        }
        else if(e.CommandName == RadGrid.EditCommandName)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int id = (int)item.GetDataKeyValue("BranchID");
            Response.Redirect("AddBranch.aspx?id=" + id);
        }
        else if (e.CommandName == RadGrid.DeleteCommandName)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int id = (int)item.GetDataKeyValue("BranchID");
            Branch bb = (from b in entities.Branches
                        where b.BranchID == id
                        select b).FirstOrDefault();
            if (bb != null)
            {
                if (bb.ProductsInBranches.Count == 0)
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        entities.Branches.Remove(bb);
                        entities.SaveChanges();
                        transaction.Complete();
                    }
                }
                else
                {
                    txtError.Text = "You cannot delete Branch because this branch is linked with product(s).";
                }
            }
        }
    }

    protected void grdBranches_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridNestedViewItem)
        {
            GridNestedViewItem nestedItem = e.Item as GridNestedViewItem;
            Branch branch = nestedItem.DataItem as Branch;
            RadGrid grid = nestedItem.FindControl("grdProductsInBranches") as RadGrid;
            List<ProductsInBranch> list = (from pb in entities.ProductsInBranches
             where pb.BranchID == branch.BranchID
             select pb).ToList();
            grid.DataSource = list;
            grid.DataBind();
        }
    }
    protected void grdBranches_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        if (e.DetailTableView.Name.Equals("BranchProducts"))
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
            int branchid = (int)parentItem.GetDataKeyValue("BranchID");
            List<ProductsInBranch> list = (from pb in entities.ProductsInBranches
                                           where pb.BranchID == branchid
                                           select pb).ToList();
            var results = (from p in entities.Products 
                           join pb in entities.ProductsInBranches on p.ProductID equals pb.ProductID
                           where pb.BranchID == branchid
                           select new { p.Name, pb.Price, pb.Enable,pb.BranchID,pb.BranchProductID }).ToList();            
            e.DetailTableView.DataSource = results;
            
        }
        else if (e.DetailTableView.Name.Equals("ProductsOptions"))
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
            long branchproductid = (long)parentItem.GetDataKeyValue("BranchProductID");
            var results = (from po in entities.ProductOptions
                           join pop in entities.ProductOptionsInProducts on po.OptionID equals pop.ProductOptionID
                           join otp in entities.OptionTypesInProducts on pop.ProductsOptionTypeId equals otp.ProductsOptionTypeId
                           join bp in entities.ProductsInBranches on otp.BranchProductID equals bp.BranchProductID
                           where bp.BranchProductID == branchproductid
                           select new { po.OptionName, pop.Price, pop.ToppingPrice, po.OptionID,po.OptionType.OptionTypeName }).ToList();
            e.DetailTableView.DataSource = results;
        }
        else if (e.DetailTableView.Name.Equals("ProductsAdons"))
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
            long branchproductid = (long)parentItem.GetDataKeyValue("BranchProductID");
            var results = (from ao in entities.Adons
                           join pao in entities.ProductAdons on ao.AdOnID equals pao.AdonID
                           join otp in entities.AdOnTypeInProducts on pao.ProductAdonTypeID equals otp.ProductsAdOnTypeId
                           join bp in entities.ProductsInBranches on otp.BrachProductID equals bp.BranchProductID
                           where bp.BranchProductID == branchproductid
                           select new { ao.AdOnName, otp.Price, ao.AdOnID, ao.AdonType1.AdOnTypeName,ao.AdonType1.IsFreeAdonType,pao.DefaultSelected }).ToList();
            e.DetailTableView.DataSource = results;
        }
        else if (e.DetailTableView.Name.Equals("BranchDetail"))
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
            int branchid = (int)parentItem.GetDataKeyValue("BranchID");
            Branch branch = entities.Branches.Where(bp => bp.BranchID == branchid).FirstOrDefault();
            if (branch.BranchDeliveryAreas != null)
            {
                string deliverarea = string.Empty;
                for (int i = 0; i < branch.BranchDeliveryAreas.Count; i++)
                {
                    if (i + 1 != branch.BranchDeliveryAreas.Count)
                    {
                        deliverarea += branch.BranchDeliveryAreas.ToList()[i].ZipCode + ", ";
                    }
                    else
                    {
                        deliverarea += branch.BranchDeliveryAreas.ToList()[i].ZipCode;
                    }
                }
            }
        }
    }    
}