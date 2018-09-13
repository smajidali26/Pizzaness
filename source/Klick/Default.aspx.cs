using System;
using System.Collections.Generic;
using System.Configuration;
using BusinessEntities;
using BusinessService;
using Valutec;

public partial class _Default : BasePage
{
    #region Private Members

    private ProductManager _productManager = new ProductManager();

    private SliderImageManager _sliderImageManager = new SliderImageManager();

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetImageSlides();
            BindSpecailProducts();
        }
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CurrentStoreTime",
                                                         "<script>var StoreTime='" + GetStoreTime().ToString() +
                                                         "';</script>");
    }

    #endregion

    #region Private  Methods

    private void GetImageSlides()
    {
        ICollection<SliderImage> sliders = _sliderImageManager.GetHomePageSliders();
        if (sliders.Count > 0)
        {
            rptSlider.DataSource = sliders;
            rptSlider.DataBind();
        }
        else
        {

        }
    }

    private void BindSpecailProducts()
    {
        rptSpecial.DataSource = _productManager.GetHomePageProducts();
        rptSpecial.DataBind();
    }

    #endregion

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        //Get stored host settings (These are stored by the default.aspx page)
        string clientKey = ConfigurationManager.AppSettings["CLIENT_KEY"];
        string terminalId = ConfigurationManager.AppSettings["TERMINAL_ID"];
        string serverId = ConfigurationManager.AppSettings["ServerID"];
        string identifier = ConfigurationManager.AppSettings["Identifier"];

        //Get CardNumber & Amount
        string CardNumber = txtCardNumber.Text;

        //Create Instance of Webservice to run tran.
        ValutecWS Service = new ValutecWS();

        //Run the Get Balance Transaction Type, assigning the result to the [TR] Object.
        TransactionResponse response = Service.Transaction_CardBalance(clientKey, terminalId, ProgramType.Gift,
                                                                       CardNumber, serverId, identifier);

        //If the Transaction was Authorized, then show the balance..
        if (response.Authorized)
        {
            ltInfoMessage.Text = "The Transaction Was Completed.<br/>";
            ltInfoMessage.Text += "The remaining balance is: " + response.Balance;
        }
        else
        {
            //Transaction was not authorized - show the error which occured.
            ltInfoMessage.Text = response.ErrorMsg;
        }
    }

    protected void btnPromoCode_OnClick(object sender, EventArgs e)
    {
        PromotionCodeManager promoManager = new PromotionCodeManager();

        // Add a check at confirm order screen over this promo value field
        //      check would be to not allow user add same code again at check-out screen
        // On check-out screen, show deducted amount in order total.

        string promoCode = txtPromo.Text;
        double promoCodeValue = 0.0d;

        BusinessEntities.PreOrderPromo sessionPreOrderPromo = SessionPreOrderPromo;
        if (sessionPreOrderPromo == null)
        {
            sessionPreOrderPromo = new BusinessEntities.PreOrderPromo();
        }

        DateTime currentDate = DateTime.Now;
        PromotionCodes code = promoManager.GetPromotionCodeByCode(sessionPreOrderPromo.PreOrderPromoCode = promoCode);

        if (code != null)
        {
            if (currentDate > code.EndDate || currentDate < code.StartDate)
            {
                ShowMessage(Resources.ErrorMessages.PromoOutOfDate, MessageType.Error);
            }
            else if (code.CodeUsageCounter != null && code.CodeUsageCounter == 0)    //  in db it's incrementing 
            {
                ShowMessage(Resources.ErrorMessages.PromoCounterZero, MessageType.Error);
            }
            else
            {
                sessionPreOrderPromo.PreOrderPromoValue = code.PromotionValue - code.PromoValueUsed;
                sessionPreOrderPromo.PreOrderPromoCode = code.PromotionCode;
                txtPromo.Text = "";
                SessionPreOrderPromo = sessionPreOrderPromo;
            }
        }
        
        

        if (sessionPreOrderPromo.PreOrderPromoValue > 0)
            ShowMessage(string.Format(Resources.InfoMessages.PromoCodeAdded, promoCode), MessageType.Success);
        else
            ShowMessage(Resources.ErrorMessages.PromoNotAdded, MessageType.Error);
    }
}