using System;
using System.Configuration;
using BusinessEntities;
using BusinessService;
using Core;
using System.Security.Cryptography;
using FirstDataCreditTransaction;

public partial class GiftCards : BasePage
{
    #region Private Members

    private String name = string.Empty;
    private String creditCardNumber = string.Empty;
    private String cvvNumber;
    private string recepientEmail = string.Empty;
    private byte expiryMonth;
    private byte expiryYear;
    private string ccAddress; //
    private const string bccAddress = "zee.a@hotmail.com";
    private Double amount = 0.0;
    private string billingAddress = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearFields();
            PublicKey.Value = ConfigurationManager.AppSettings["RSAPublicKey"];
        }
    }

    protected void SubmitButton_OnClick(object sender, EventArgs e)
    {
        ////name = Utility.RsaDecrypt(EncrName.Value);
        ////expiry = Convert.ToDateTime(Utility.RsaDecrypt(EncrCardExpiry.Value));
        ////creditCardNumber = Utility.RsaDecrypt(EncrCreditCard.Value);
        ////cvvNumber = Utility.RsaDecrypt(EncrCvv.Value);
        ////recepientEmail = Utility.RsaDecrypt(ReceiverEmail.Value);
        ////ccAddress = Utility.RsaDecrypt(UserEmail.Value);
        ////amount = Convert.ToDouble(Utility.RsaDecrypt(Amount.Value));

        try
        {
            string cipheredText = EncryptedFormData.Value;

            string decipheredString = Utility.RsaDecrypt(cipheredText);

            string[] decipheredAllFields = decipheredString.Split('|');

            name = decipheredAllFields[0];
            creditCardNumber = decipheredAllFields[1];
            expiryMonth = Convert.ToByte(decipheredAllFields[2]);
            expiryYear = Convert.ToByte(decipheredAllFields[3]);
            cvvNumber = decipheredAllFields[4];
            ccAddress = decipheredAllFields[5];
            recepientEmail = decipheredAllFields[6];
            amount = Convert.ToDouble(decipheredAllFields[7]);

            billingAddress = Utility.RsaDecrypt(EncryptedBillingAddress.Value);
        }
        catch (Exception)
        {
            //MessageBar1.ShowMessage(Resources.ErrorMessages.InputFieldsInvalid, MessageType.Error);
            return;
        }



        Result transactionResut = Result.None;
        if (amount > 0)
            transactionResut = ProcessTransaction(creditCardNumber, name, amount, expiryMonth.ToString() + expiryYear.ToString(), cvvNumber, billingAddress);
        else
        {
            ShowMessage("Amount is too less, can't make transaction", MessageType.Error);
            return;
        }

        if (transactionResut == Result.Success)
        {
            // Add Promotion code to DB with expiry
            PromotionCodes promoCode = new PromotionCodes();
            promoCode.CodeUsageCounter = 0;
            promoCode.EndDate = null;
            promoCode.StartDate = null;
            promoCode.PromotionCode = Utility.RandomString(5);
            promoCode.PromotionName = "E-Gift by " + name;
            promoCode.PromotionValue = amount;
            promoCode.TypeOfPromotion = PromotionType.EGiftCard;

            PromotionCodeManager promoCodeManager = new PromotionCodeManager();
            int result = promoCodeManager.AddPromotionCode(promoCode);
            if (result > 0)
            {
                string subject = Resources.EmailSubjects.EGift;
                string body = string.Format(Resources.EmailMessages.EGiftIntimation, amount, name, promoCode);
                string senderEmail = ConfigurationManager.AppSettings["DonotReplyEmail"];
                Utility.SendEmail(senderEmail, recepientEmail, ccAddress, ConfigurationManager.AppSettings["OrderReceiveEmail"], subject, body, false);
                ShowMessage(Resources.ErrorMessages.EGiftSuccess, MessageType.Success);
                //ClearFields();
            }
            else
            {
                ShowMessage(Resources.ErrorMessages.EGiftFailure, MessageType.Warning);
            }
        }
        else
        {
            ShowMessage(Resources.ErrorMessages.CreditCardTransactionError, MessageType.Error);
        }
    }

    public Result ProcessTransaction(String cardNumber, String cardHolderName, Double amount, String expireDate,
                                     String CAVV, String billingAddress)
    {
        Result resultObj = Result.None;
        ServiceSoapClient client = new ServiceSoapClient();
        Transaction txn = new Transaction();
        System.IdentityModel.Selectors.SecurityTokenManager token =
            client.ClientCredentials.CreateSecurityTokenManager();
        //client.ClientCredentials.HttpDigest.ClientCredential = new System.Net.NetworkCredential("oriental501", "Welcome123-");
        // set correct credential values
        txn.ExactID = ConfigurationManager.AppSettings["ExactID"]; // "AE2449-01";
        txn.Password = ConfigurationManager.AppSettings["ExactPassword"]; //"Welcome123-";
        // keyid = 113045

        txn.Transaction_Type = "00";
        txn.Card_Number = cardNumber; //"4111111111111111";
        txn.CardHoldersName = cardHolderName; // "CSharp NET Sample";
        txn.DollarAmount = amount.ToString(); // "101.00";
        txn.Expiry_Date = expireDate; // "1115";
        txn.CAVV = CAVV;

        TransactionResult result = client.SendAndCommit(txn);
        ReturnMessage = result.Bank_Message;
        switch (result.Bank_Resp_Code)
        {
            case "100":
                resultObj = Result.Success;
                break;
            default:
                resultObj = Result.Failed;
                break;
        }
        return resultObj;
    }

    private void ClearFields()
    {
        txtName.Text = "";
        txtCreditCardNumber.Text = "";
        txtCVV.Text = "";
        txtRecepientEmail.Text = "";
        txtUserEmail.Text = "";
        txtAmount.Text = "";
        txtAddress.Text = "";
    }

    #region

    public String ReturnMessage { get; set; }

    #endregion


    public enum Result
    {
        None = 0,
        Success = 1,
        Failed = 2
    }
}