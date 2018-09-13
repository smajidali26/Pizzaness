using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;

public partial class admin_Settings_PromotionCodeAdd : BasePage
{
    #region Members
    private PromotionCodeManager _promoCodeManage = new PromotionCodeManager();

    #endregion

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTypeOfPromotion();
            GetPromotionCodeById();
        }
    }

    protected void buttonSave_Click(object sender, EventArgs e)
    {
        PromotionCodes obj = new PromotionCodes();
        if (ViewStateID != null)
        {
            obj.PromotionCodeId = (Int32)ViewStateID;
        }
        obj.PromotionName = txtPromotionName.Text;
        obj.PromotionCode = txtPromotionCode.Text;
        obj.TypeOfPromotion = (PromotionType)Convert.ToInt16(rblPromotionType.SelectedValue);
        obj.StartDate = StartDate.SelectedDate;
        obj.EndDate = EndDate.SelectedDate;
        obj.PromotionValue = Convert.ToDouble(txtPromotionValue.Text);

        int result = 0;
        if (ViewStateID == null)
        {
            result = _promoCodeManage.AddPromotionCode(obj);
        }
        else
        {
            result = _promoCodeManage.UpdatePromotionCode(obj);
        }

        if (result > 0)
        {
            if (ViewStateID == null)
            {
                SessionMessage = "Promotion code has been added successfully.";
            }
            else
            {
                SessionMessage = "Promotion code has been updated successfully.";
            }
            Response.Redirect("PromotionCodeList.aspx");
        }
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PromotionCodeList.aspx");
    }

    #endregion

    #region Methods

    private void BindTypeOfPromotion()
    {
        rblPromotionType.Items.Add(new ListItem(PromotionType.Money.ToString(), Convert.ToInt16(PromotionType.Money).ToString()));
        rblPromotionType.Items.Add(new ListItem(PromotionType.Percentage.ToString(), Convert.ToInt16(PromotionType.Percentage).ToString()));
        rblPromotionType.Items.Add(new ListItem(PromotionType.Percentage.ToString(), Convert.ToInt16(PromotionType.EGiftCard).ToString()));
    }

    private void GetPromotionCodeById()
    {
        PromotionCodes obj = _promoCodeManage.GetPromotionCodesById(Convert.ToInt32(QueryStringParamID));
        if (obj != null)
        {
            txtPromotionName.Text = obj.PromotionName;
            txtPromotionCode.Text = obj.PromotionCode;
            rblPromotionType.SelectedValue = Convert.ToInt16(obj.TypeOfPromotion).ToString();
            StartDate.SelectedDate = obj.StartDate;
            EndDate.SelectedDate = obj.EndDate;
            txtPromotionValue.Text = obj.PromotionValue.ToString();
            ViewStateID = obj.PromotionCodeId;
        }
    }
    #endregion
}