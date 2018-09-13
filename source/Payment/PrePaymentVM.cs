using System.Collections.Generic;

namespace Payment.ViewModels
{
    public class PrePaymentVM
    {
        //public List<InvoiceVM> SelectedInvoices { get; set; }
        public string SelectedInvoicesJSON { get; set; }
        public string Total { get; set; }
        public string CustomerPO { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerWeb { get; set; }
        public string Notes { get; set; }
    }
}
