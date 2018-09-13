using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payment.API_Models.Check;
using Payment.Services;
public partial class PaymentTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TestPayeezy();
    }

    private void TestPayeezy()
    {
        Token token = new Token();
        SendToCheckAPI sendToCheckAPI = new SendToCheckAPI();
        token = sendToCheckAPI.GetToken();

        var builder = new StringBuilder("{ " +
        "\"merchant_ref\": \"Acme Sock\", " +
        "\"transaction_type\": \"authorize\", " +
        "\"method\": \"credit_card\", " +
        "\"amount\": \"1299\", " +
        "\"currency_code\": \"USD\", " +
        "\"credit_card\": { " +
        " \"type\": \"visa\", " +
        " \"cardholder_name\": \"John Smith\", " +
        " \"card_number\": \"4788250000028291\", " +
        " \"exp_date\": \"1020\", " +
        " \"cvv\": \"123\" " +
        "} " +
        "}");
        var payload = builder.ToString();
        byte[] byteArray = Encoding.UTF8.GetBytes(payload);
        var apiKey = token.api_key;
        var timeStamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture);
        var nonce = (10000000000000000000 * new Random(DateTime.Now.Millisecond).NextDouble()).ToString("0000000000000000000");
        var merchantToken = token.merchant_token;
        var secretKey = token.api_secret;

        var post = (HttpWebRequest)HttpWebRequest.Create("https://api-cert.payeezy.com/v1/transactions");
        post.Method = "POST";
        post.KeepAlive = true;
        post.Accept = "*/*";
        post.Headers.Add("Accept-Encoding", "gzip");
        post.Headers.Add("Accept-Language", "en-US");
        post.Headers.Add("apikey", apiKey);
        post.Headers.Add("nonce", nonce);
        post.Headers.Add("timestamp", timeStamp);
        var authorize = CreateHMAC(apiKey, secretKey, merchantToken, payload, nonce, timeStamp);
        post.Headers.Add("Authorization", authorize);
        post.ContentType = "application/json";
        post.Headers.Add("token", merchantToken);
        post.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7";

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        post.GetRequestStream().Write(byteArray, 0, byteArray.Length);
        var response = post.GetResponse();
        var reader = new StreamReader(response.GetResponseStream());
        var responseFromServer = reader.ReadToEnd();
    }

    public  string CreateHMAC(string apiKey, string apiSecret, string token,
        string payload, string nonce, string timeStamp)
    {
        var hmacData = apiKey + nonce + timeStamp + token + payload;
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
        var encBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(hmacData));
        var encStr = ByteArrayToHexString(encBytes);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes((encStr)));
    }

    public string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}