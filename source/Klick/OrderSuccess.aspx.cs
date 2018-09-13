using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessService;
using interfax;

public partial class OrderSuccess : BasePage
{
    private OrderManager orderManager = new OrderManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LogManager log = new LogManager();
            log.SaveLogData(HttpContext.Current.Session.SessionID, LogLevel.INFO.ToString(),
                            Logger.ReturnFromPayment.ToString(), Common.ReturnFromOnlinePayment(1), null);
            if (!String.IsNullOrEmpty(Request["x_invoice_num"]))
            {
                log.SaveLogData(HttpContext.Current.Session.SessionID, LogLevel.INFO.ToString(),
                                Logger.ReturnFromPayment.ToString(),
                                Common.ReturnFromOnlinePayment(2, Request["x_invoice_num"]), null);
                Int64 orderId = Convert.ToInt64(Request["x_invoice_num"]);
                String PaymentStatus = Request["Bank_Message"];
                String TransactionApproved = Request["Transaction_Approved"];
                
                if ((!String.IsNullOrEmpty(PaymentStatus) && PaymentStatus.ToLower().Equals("approved")) 
                    || (!String.IsNullOrEmpty(TransactionApproved) && TransactionApproved.ToLower().Equals("yes")))
                {
                    log.SaveLogData(HttpContext.Current.Session.SessionID, LogLevel.INFO.ToString(),
                                Logger.ReturnFromPayment.ToString(), Common.ReturnFromOnlinePayment(3), null);
                    int result = orderManager.OrderPaid(orderId);
                    if (result > 0)
                    {
                        log.SaveLogData(HttpContext.Current.Session.SessionID, LogLevel.INFO.ToString(),
                                Logger.ReturnFromPayment.ToString(), Common.ReturnFromOnlinePayment(4), null);
                        
                        //Core.Utility.SendEmail(ConfigurationManager.AppSettings["DonotReplyEmail"], SessionUser.Username,
                        //                   String.Empty, ConfigurationManager.AppSettings["OrderReceiveEmail"], "New Order",
                        //                   SessionEmail, true);
                        InterFax client = new InterFax();
                        if (ConfigurationManager.AppSettings["AllowFax"].Equals("true"))
                            client.Sendfax(ConfigurationManager.AppSettings["FaxUsername"],
                                           ConfigurationManager.AppSettings["FaxPassword"],
                                           ConfigurationManager.AppSettings["FaxNumber"],
                                           System.Text.Encoding.UTF8.GetBytes(SessionFax), "HTML");
                        log.SaveLogData(HttpContext.Current.Session.SessionID, LogLevel.INFO.ToString(),
                                Logger.ReturnFromPayment.ToString(), Common.ReturnFromOnlinePayment(5), null);
                        
                        SessionMessage = "Your order payment has been completed successfully. Order detail has been sent to your registered email and fax to back office for your order process.";
                        SessionMessageType = MessageType.Success;
                        Response.Redirect("~/Default.aspx");
                        SessionFax = SessionEmail = String.Empty;
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request["x_response_reason_text"]))
                    {
                        txtMessage.Text = "Error:" + Request["x_response_reason_text"];
                    }
                }
            }
        }
        catch (Exception exception)
        {

            txtMessage.Text = (exception.InnerException != null)
                                  ? exception.InnerException.Message
                                  : exception.Message;
        }
        
    }
}