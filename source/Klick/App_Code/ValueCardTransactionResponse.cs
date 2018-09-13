
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

[GeneratedCode("System.Xml", "2.0.50727.5476")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "https://ws.valutec.net/")]
[Serializable]
public class ValueCardTransactionResponse
{
    public string RawOutput { get; set; }
    public string TerminalID { get; set; }
    public string IPaddress { get; set; }
    public bool Authorized { get; set; }
    public string AuthorizationCode { get; set; }
    public string TransactionType { get; set; }
    public string Balance { get; set; }
    public string PointBalance { get; set; }
    public string RewardLevel { get; set; }
    public string Refund { get; set; }
    public string CardAmountUsed { get; set; }
    public string AmountDue { get; set; }
    public string TotalSales { get; set; }
    public string TotalSales_Amount { get; set; }
    public string TotalActivations { get; set; }
    public string TotalActivations_Amount { get; set; }
    public string TotalAddValues { get; set; }
    public string TotalAddValues_Amount { get; set; }
    public string TotalVoids { get; set; }
    public string TotalVoids_Amount { get; set; }
    public string TotalDeactivations { get; set; }
    public string TotalDeactivations_Amount { get; set; }
    public string Identifier { get; set; }
    public string ErrorMsg { get; set; }
    public string CardNumber { get; set; }
}
