namespace Payment.API_Models.Check
{
    public class Transaction
    {
        public string method { get; set; }
        public string transaction_type { get; set; }
        public int amount { get; set; }
        public string currency_code { get; set; }
        public tele_check tele_check { get; set; }
        public billing_address billing_address { get; set; }
    }
}
