using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BusinessEntities
{
    [Serializable]
    public enum OrderStatus
    {
        [XmlEnum("0")]
        None = 0,
        [XmlEnum("1")]
        NewOrder = 1,
        [XmlEnum("2")]
        Delivered = 2
    }

    [Serializable]
    public enum OrderType
    {
        [XmlEnum("0")]
        None = 0,
        [XmlEnum("1")]
        Deliver = 1,
        [XmlEnum("2")]
        SelfPickup = 2
    }

    [Serializable]
    public enum SelectedOption
    {
        [XmlEnum("0")]
        None =0,
        [XmlEnum("1")]
        Full =1,
        [XmlEnum("2")]
        FirstHalf=2,
        [XmlEnum("3")]
        SecondHalf=3
    }

    [Serializable]
    public enum PaymentType
    {
        [XmlEnum("0")]
        None = 0,
        [XmlEnum("1")]
        OnlinePayment = 1,
        [XmlEnum("2")]
        CashPayment = 2
    }

    [Serializable]
    public enum PromotionType
    {
        [XmlEnum("0")]
        None = 0,
        [XmlEnum("10")]
        Money = 10,
        [XmlEnum("20")]
        Percentage = 20,
        [XmlEnum("30")]
        EGiftCard = 30
    }
}
