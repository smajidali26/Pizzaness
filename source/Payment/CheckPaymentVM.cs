using System.Collections.Generic;

namespace Payment.ViewModels
{
    public class CheckPaymentVM
    {
        public PrePaymentVM PrePaymentVM { get; set; }
        public string AccountHolderName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CheckNumber { get; set; }
        public int CustomerIdType { get; set; }
        public string CustomerIdNumber { get; set; }
        public string CheckType { get; set; }
    }
}
