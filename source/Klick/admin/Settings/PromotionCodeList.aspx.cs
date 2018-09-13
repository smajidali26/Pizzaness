using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;
using Telerik.Web.UI;

public partial class admin_Settings_PromotionCodeList : BasePage
{
    #region Members
    private PromotionCodeManager _promoCodeManage = new PromotionCodeManager();

    #endregion

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            GetPromotionCodes();
            grdPromotionCode.DataBind();
        }
    }

    protected void grdPromotionCode_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.InitInsertCommandName)
        {
            Response.Redirect("PromotionCodeAdd.aspx");
        }
        else if(e.CommandName == RadGrid.EditCommandName)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int id = (int)item.GetDataKeyValue("PromotionCodeId");
            Response.Redirect("PromotionCodeAdd.aspx?id=" + id);
        }
        else if (e.CommandName == RadGrid.DeleteCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (editedItem != null)
            {
                
                try
                {
                    Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                    int id = (int)key["PromotionCodeId"];
                    _promoCodeManage.DeletePromotionCode(id);
                }
                catch (Exception ex)
                {
                    
                }
                GetPromotionCodes();
            }
        }
    }

    protected void grdPromotionCode_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        GetPromotionCodes();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdPromotionCode.CurrentPageIndex = 0;
        grdPromotionCode.VirtualItemCount = 0;
        GetPromotionCodes();
        grdPromotionCode.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearchPromotionCode.Text = txtSearchPromotionName.Text = String.Empty;
        grdPromotionCode.CurrentPageIndex = 0;
        grdPromotionCode.VirtualItemCount = 0;
        GetPromotionCodes();
        grdPromotionCode.DataBind();
    }
    #endregion

    #region Methods

    private void GetPromotionCodes()
    {
        ICollection<PromotionCodes> list = _promoCodeManage.GetPromotionCode(txtSearchPromotionName.Text, txtSearchPromotionCode.Text, grdPromotionCode.CurrentPageIndex+1, grdPromotionCode.PageSize);
        grdPromotionCode.DataSource = list;
        if (list != null && list.Count > 0)
            grdPromotionCode.VirtualItemCount = list.ElementAt(0).TotalRows;
        else
            grdPromotionCode.VirtualItemCount = 0;
    }
    #endregion

    
}