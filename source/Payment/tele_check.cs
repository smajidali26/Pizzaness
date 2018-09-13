namespace Payment.API_Models.Check
{
    //REMEMBER: all money amounts lack decimals.  ie: 2.00 becomes 200
    public class tele_check
    {
        public int check_number { get; set; }
        public string check_type { get; set; }
        public string routing_number { get; set; }
        public int account_number { get; set; }
        public string accountholder_name { get; set; }
        public int customer_id_type { get; set; }
        public string customer_id_number { get; set; }
        public string client_email { get; set; }
        //these are optional - able to get approved demo transaction without
        //public int gift_card_amount { get; set; }
        //public char vip  { get; set; }
        //public string clerk_id { get; set; }
        //public string device_id { get; set; }
        //public string release_type { get; set; }
        //public int registration_number { get; set; }
        //public string registration_date { get; set; }
        //public string date_of_birth { get; set; }

    }
}
