using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Enum
/// </summary>
public enum MessageType
{
    None = 0,
    Error = 1,
    Notice = 2,
    Warning = 3,
    Success = 4,
    Confirmation = 5
}

public enum LogLevel
{
    INFO = 10,
    Exception = 20
}

public enum Logger
{
    AddToCart = 10,
    RemoveFromCart = 20,
    UserLogin = 30,
    PaymentMethod =40,
    CheckOut = 50,
    ConfirmOrder = 60,
    ReturnFromPayment=70,
    OrderStatusPaid=80
}