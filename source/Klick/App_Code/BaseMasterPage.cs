using System;
using System.Web;
using System.Web.UI;
using System.Data;
using BusinessObjects;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for BaseMasterPage
/// </summary>
public class BaseMasterPage : MasterPage, IMessageBar
{
    #region Private Members
    private const string sessionMessage = "SessionMessage";
    private const string sessionUserId = "UserId";
    private const string sessionUserRole = "UserRole";
    private const string sessionUserContactInfoId = "UserContactInfoId";
    private const string sessionUserFullName = "UserFullName";
    private const string sessionUser = "User";
    private const string sessionLoginUserName = "LoginUserName";
    private const string sessionLoginUserPassword = "LoginUserPassword";
    private const string sessionUserOrder = "UserOrder";
    private const string sessionOrderDetailOptionList = "OrderDetailOptionList";
    private const string sessionOrderDetailAdonList = "OrderDetailAdonList";
    private const string sessionUserOrderTotal = "UserOrderTotal";
    private DataSet dataSet = null;

    #endregion

    public BaseMasterPage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    #region Properties

    #region Non Session Properties

    public string BaseSiteUrl
    {
        get
        {
            HttpContext context = HttpContext.Current;
            string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
            return baseUrl;
        }
    }

    public string BaseVirtualAppPath
    {
        get
        {
            HttpContext context = HttpContext.Current;
            string url = context.Request.ApplicationPath;
            if (url.EndsWith("/"))
                return url;
            else
                return url + "/";
        }
    }

    public string HtmlFolder
    {
        get { return "/Html/"; }
    }

    public object QueryStringParamID
    {
        get { return Request.QueryString["id"]; }
    }

    public object ViewStateID
    {
        get { return ViewState["id"]; }
        set { ViewState["id"] = value; }
    }

    public int BranchId
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["BranchId"]); }
    }

    public float OrderTotal { get; set; }
    #endregion


    #region Session Properties

    public Int64 SessionUserId
    {
        get
        {
            if (Session[sessionUserId] == null)
                return 0;
            return (Int64)Session[sessionUserId];
        }
        set
        {
            Session[sessionUserId] = value;
        }
    }

    public UserLoginType SessionUserRole
    {
        get
        {
            if (Session[sessionUserRole] == null)
                return null;
            return (UserLoginType)Session[sessionUserRole];
        }
        set
        {
            Session[sessionUserRole] = value;
        }
    }

    public Int64 SessionUserContactInfoId
    {
        get
        {
            if (Session[sessionUserContactInfoId] == null)
                return 0;
            return (Int64)Session[sessionUserContactInfoId];
        }
        set
        {
            Session[sessionUserContactInfoId] = value;
        }
    }

    public String SessionUserFullName
    {
        get
        {
            return (String)Session[sessionUserFullName];
        }
        set
        {
            Session[sessionUserFullName] = value;
        }
    }

    public UserLogin SessionUser
    {
        get
        {
            return (UserLogin)Session[sessionUser];
        }
        set
        {
            Session[sessionUser] = value;
        }
    }

    public String SessionLoginUserName
    {
        get
        {
            return (String)Session[sessionLoginUserName];
        }
        set
        {
            Session[sessionLoginUserName] = value;
        }
    }

    public String SessionLoginUserPassword
    {
        get
        {
            return (String)Session[sessionLoginUserPassword];
        }
        set
        {
            Session[sessionLoginUserPassword] = value;
        }
    }

    public static String SessionMessage
    {
        set
        {
            HttpContext.Current.Session[sessionMessage] = value;
        }
        get
        {
            String message = String.Empty;

            if (HttpContext.Current.Session[sessionMessage] != null && !String.IsNullOrEmpty(HttpContext.Current.Session[sessionMessage].ToString()))
            {
                message = Convert.ToString(HttpContext.Current.Session[sessionMessage]);
            }

            return message;
        }
    }

    public BusinessEntities.Orders SessionUserOrder
    {
        get
        {
            return (BusinessEntities.Orders)Session[sessionUserOrder];
        }
        set
        {
            Session[sessionUserOrder] = value;
        }
    }

    public ICollection<List<BusinessEntities.OrderDetailOptions>> SessionOrderDetailOptionList
    {
        get
        {
            return (ICollection<List<BusinessEntities.OrderDetailOptions>>)Session[sessionOrderDetailOptionList];
        }
        set
        {
            Session[sessionOrderDetailOptionList] = value;
        }
    }

    public ICollection<List<BusinessEntities.OrderDetailAdOns>> SessionOrderAdonList
    {
        get
        {
            return (ICollection<List<BusinessEntities.OrderDetailAdOns>>)Session[sessionOrderDetailAdonList];
        }
        set
        {
            Session[sessionOrderDetailAdonList] = value;
        }
    }

    public Double SessionUserOrderTotal
    {
        get
        {
            return (Double)Session[sessionUserOrderTotal];
        }
        set
        {
            Session[sessionUserOrderTotal] = value;
        }
    }

    #endregion

    #endregion

    #region Public Methods
    public void SetMessage(string messageText)
    {
        UserControl control = (UserControl)this.Master.FindControl("Message");

    }

    public bool IsStoreClosed()
    {
        bool isClosed;
        var Day = DateTime.Now.DayOfWeek.ToString().ToLower();

        DateTime storeTime = GetStoreTime();
        Int32 timeDiff = Convert.ToInt32(ConfigurationManager.AppSettings["TimeDifference"]);
        switch (Day)
        {
            case "friday":
            case "saturday":
                if (storeTime.Hour + timeDiff >= 11 && storeTime.Hour + timeDiff <= 23)
                    isClosed = false;
                else
                    isClosed = true;
                break;
            default:
                if (storeTime.Hour + timeDiff >= 11 && storeTime.Hour + timeDiff < 23)
                    isClosed = false;
                else
                    isClosed = true;
                break;
        }
        return isClosed;
    }

    public DateTime GetStoreTime()
    {
        DateTime storeTime;
        var london = TimeZoneInfo.FindSystemTimeZoneById
            (TimeZone.CurrentTimeZone.StandardName);
        var googleplex = TimeZoneInfo.FindSystemTimeZoneById
            ("Eastern Standard Time");
        var isClosed = false;
        var now = DateTime.Now;
        var Day = DateTime.Now.DayOfWeek.ToString().ToLower();

        TimeSpan londonOffset = london.GetUtcOffset(now);
        TimeSpan googleplexOffset = googleplex.GetUtcOffset(now);
        TimeSpan difference = new TimeSpan();

        difference = londonOffset - googleplexOffset;
        storeTime = DateTime.Now.Subtract(difference);
        return storeTime;
    }
    #endregion

    #region "IMessageBar Implementation"
    /// <summary>
    /// ShowMessage
    /// </summary>
    /// <param name="message"></param>
    /// <param name="type"></param>
    public void ShowMessage(string message, MessageType type)
    {
        Control control = this.FindControl("MessageBar1");

        if (control == null && this.Master != null)
        {
            control = this.Master.FindControl("MessageBar1");
        }

        if (control != null)
        {
            IMessageBar messageBar = control as IMessageBar;

            if (messageBar != null)
            {
                messageBar.ShowMessage(message, type);
            }
        }
    }
    #endregion
}