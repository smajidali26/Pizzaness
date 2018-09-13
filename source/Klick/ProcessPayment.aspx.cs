using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessPayment :  BasePage
{
    public String XLogin { get { return ConfigurationManager.AppSettings["Payment_x_login"]; } }

    public String XTestRequest { get { return ConfigurationManager.AppSettings["Payment_x_test_request"]; } }

    public String XShowForm { get { return ConfigurationManager.AppSettings["Payment_x_show_form"]; } }

    public String XTransactionKey { get { return ConfigurationManager.AppSettings["Payment_x_transaction_key"]; } }

    public String XFpSequence { get; set; }

    public String XFpTimeStamp { get; set; }

    public String XFpHash { get; set; }

    public String XInvoiceNum { get; set; }

    public String XAmount { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb1 = new StringBuilder();
        // x_login^x_fp_sequence^x_fp_timestamp^x_amount^x_currency
        //x_login.Value = "HCO-NONE-471";//x_login
        if (!String.IsNullOrEmpty(Request.QueryString["orderid"]))
        {
            XInvoiceNum = Request.QueryString["orderid"];
        }
        else
            XInvoiceNum = SessionOrderId.ToString();
        Random random = new Random();
        XFpSequence = (random.Next(0, 1000)).ToString();
        //x_test_request.Value = "false";
        //x_show_form.Value = "PAYMENT_FORM";
        TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        int timestamp = (int)t.TotalSeconds;
        XFpTimeStamp = timestamp.ToString();
        if (!String.IsNullOrEmpty(Request.QueryString["total"]))
        {
            XAmount = Request.QueryString["total"];
        }
        else
            XAmount = SessionUserOrderTotal.ToString();
        //String x_currency = "USD"; // default empty


        sb1.Append(XLogin)
            .Append("^")
            .Append(XFpSequence)
            .Append("^")
            .Append(XFpTimeStamp)
            .Append("^")
            .Append(XAmount)
            .Append("^")
            .Append("");

        // Convert string to array of bytes. 
        byte[] data = Encoding.UTF8.GetBytes(sb1.ToString());

        // key
        byte[] key = Encoding.UTF8.GetBytes(XTransactionKey);//transaction_key

        // Create HMAC-SHA1 Algorithm;  
        HMACMD5 hmac = new HMACMD5(key);

        // Create HMAC-SHA1 Algorithm;  
        //HMACSHA1 hmac = new HMACSHA1(key);

        // Compute hash. 
        byte[] hashBytes = hmac.ComputeHash(data);

        // Convert to HEX string.  
        XFpHash = System.BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        if (String.IsNullOrEmpty(Request.QueryString["orderid"]))
        {
            SessionUserOrder = null;
            SessionOrderId = 0;
            SessionUserOrderTotal = 0;
        }
    }
}