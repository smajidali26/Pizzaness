using System;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Common
{
    #region Private Methods

    private static string AnonymousUserCheck(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            return "Anonymous User";
        return userName;
    }

    private static string AnonymousRecepientCheck(string recepientName)
    {
        if (string.IsNullOrEmpty(recepientName))
            return "Anonymous Recepient";
        return recepientName;
    }

    private static string UnspecifiedSizeCheck(string size)
    {
        if (string.IsNullOrEmpty(size))
            return "Unspecified Size";
        return size;
    }

    private static string UnspecifiedAdonsCheck(string adons)
    {
        if (string.IsNullOrEmpty(adons))
            return "None";
        return adons;
    }

    #endregion


    #region Public Static Methods

    public static string ConfirmOrderLogMessage(string deal, double price, string adons,
                                             string size = "unknown", string recepientName = "unknown", string userName = "Anonymous User")
    {
        userName = AnonymousUserCheck(userName);
        recepientName = AnonymousRecepientCheck(recepientName);
        size = UnspecifiedSizeCheck(size);

        return string.Format(Resources.LogMessages.ConfirmOrder, userName, deal, size, adons, price, recepientName);
    }

    public static string PaymentMethodLogMessage(string userName, string paymentMethod)
    {
        return string.Format(Resources.LogMessages.PaymentMethod, userName, paymentMethod);
    }


    public static string CheckoutLogMessage(string deal, double price, string adons, string size = "unknown", string userName = "Anonymous User")
    {
        userName = AnonymousUserCheck(userName);
        return string.Format(Resources.LogMessages.Checkout, userName, deal, size, adons, price);
    }


    public static string AddToCartLogMessage(string deal, double price, string adons, string size = "unknown", string userName = "Anonymous User")
    {
        userName = AnonymousUserCheck(userName);
        return string.Format(Resources.LogMessages.AddToCart, userName, deal, size, adons, price);
    }


    public static string RemoveFromCartLogMessage(string itemIDs, string userName = "Anonymous User")
    {
        userName = AnonymousUserCheck(userName);
        return string.Format(Resources.LogMessages.RemoveFromCart, userName, itemIDs);
    }
    

    public static string UserLoginLogMessage(string userName)
    {
        return string.Format(Resources.LogMessages.LoginMessage, userName);
    }

    public static string LineTipLogMessage(string lineTip,String orderId)
    {
        return string.Format(Resources.LogMessages.LineTip, lineTip,orderId);
    }
    public static string ReturnFromOnlinePayment(int number,string orderId="")
    {
        switch (number)
        {
            case 1:
                return string.Format(Resources.LogMessages.ReturnFromOnlinePayment);
                break;
            case 2:
                return string.Format(Resources.LogMessages.GetInvoiceNumberFromOnlinePayment,orderId);
                break;
            case 3:
                return string.Format(Resources.LogMessages.PaymentAcceptedSuccessfully);
                break;
            case 4:
                return string.Format(Resources.LogMessages.OrderInformationUpdatedToPaid);
                break;
            case 5:
                return string.Format(Resources.LogMessages.EmailAndFaxSent);
                break;
        }
        return string.Empty;
    }
    #endregion
}