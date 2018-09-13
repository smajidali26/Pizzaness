namespace Payment.API_Models
{
    public class CheckResponse
    {
        public string method { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string transaction_status { get; set; }
        public string validation_status { get; set; }
        public string transaction_type { get; set; }
        public string transaction_id { get; set; }
        public string transaction_tag { get; set; }
        public string bank_resp_code { get; set; }
        public string bank_message { get; set; }
        public string gateway_resp_code { get; set; }
        public string gateway_message { get; set; }
        public string correlation_id { get; set; }
        public string vip  { get; set; }
        public string micr { get; set; }
        public string accountholder_name { get; set; }
        public string check_number { get; set; }
        public string check_type { get; set; }
        public string account_number { get; set; }
        public string routing_number { get; set; }
        public string customer_id_type { get; set; }
        public string customer_id_number { get; set; }
        public string client_email { get; set; }
        public string release_type { get; set; }
        public string gift_card_type { get; set; }
        public string date_of_birth { get; set; }
        public string registration_number { get; set; }
        public string registration_date { get; set; }
        public string clerk_id { get; set; }
        public string device_id { get; set; }
    }
}
