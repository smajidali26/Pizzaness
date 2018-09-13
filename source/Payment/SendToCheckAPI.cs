using Newtonsoft.Json;
using Payment.API_Models;
using Payment.API_Models.Check;
using Payment.ViewModels;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Payment.Services
{
    public class SendToCheckAPI
    {
        private static string api_key = "vRXyjspsaCQsvK6UyWZ06ys3lTAO8juE";
        private static string api_secret = "c1217094f3186a194761390ce35a9799800e2e854e1ee81e5c7166dd1f0af3d1";

        public CheckResponse Send(CheckPaymentVM checkPaymentVM)
        {
            CheckResponse checkResponse = new CheckResponse();
            Transaction transaction = new Transaction();
            Token token = new Token();

            transaction = CreateTransaction(checkPaymentVM);
            token = GetToken();

            var payload = JsonConvert.SerializeObject(transaction);

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

            post.GetRequestStream().Write(byteArray, 0, byteArray.Length);
            var response = post.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var responseFromServer = reader.ReadToEnd();

            checkResponse = JsonConvert.DeserializeObject<CheckResponse>(responseFromServer);

            return checkResponse;
        }

        public Transaction CreateTransaction(CheckPaymentVM checkPaymentVM)
        {
            Transaction transaction = new Transaction();
            billing_address billing_address = new billing_address();
            tele_check tele_check = new tele_check();

            transaction.method = "tele_check";
            transaction.transaction_type = "purchase";
            //REMEMBER CANNOT HAVE PERIODS OR SPACES
            transaction.amount = Convert.ToInt32(checkPaymentVM.PrePaymentVM.Total.Replace(".", "").Trim());
            transaction.currency_code = "USD";

            billing_address.city = checkPaymentVM.PrePaymentVM.CustomerCity;
            billing_address.country = "US";
            billing_address.state_province = checkPaymentVM.PrePaymentVM.CustomerState;
            billing_address.street = checkPaymentVM.PrePaymentVM.CustomerAddress;
            billing_address.zip_postal_code = checkPaymentVM.PrePaymentVM.CustomerZip;
            transaction.billing_address = billing_address;

            tele_check.account_number = Convert.ToInt32(checkPaymentVM.AccountNumber);
            tele_check.routing_number = checkPaymentVM.RoutingNumber;
            tele_check.check_number = checkPaymentVM.CheckNumber;
            tele_check.check_type = checkPaymentVM.CheckType;
            tele_check.accountholder_name = checkPaymentVM.AccountHolderName;
            tele_check.customer_id_type = checkPaymentVM.CustomerIdType;
            tele_check.customer_id_number = checkPaymentVM.CustomerIdNumber;
            tele_check.client_email = checkPaymentVM.PrePaymentVM.CustomerEmail;
            transaction.tele_check = tele_check;

            return transaction;
        }

        public Token GetToken()
        {
            Token token = new Token();
            //int branch = Convert.ToInt32(WebHelpers.GetEmployeeBranch());

            token.api_key = api_key;
            token.api_secret = api_secret;
            //demo information:
            //used switch here branch to merchant_token
            token.merchant_token = "fdoa-e0c7c1c26131976113f10b52e91a9710e0c7c1c261319761";

            return token;
        }

        public static string CreateHMAC(string apiKey, string apiSecret, string token,
        string payload, string nonce, string timeStamp)
        {
            var hmacData = apiKey + nonce + timeStamp + token + payload;
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            var encBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(hmacData));
            var encStr = ByteArrayToHexString(encBytes);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes((encStr)));
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
