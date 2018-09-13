using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using BusinessObjects;
using Telerik.Web.UI;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : RadAjaxPage, IMessageBar
{
    #region Private Members
    private const string sessionMessage = "SessionMessage";
    private const string sessionMessageType = "SessionMessageType";
    private const string sessionUserId = "UserId";
    private const string sessionUserRole = "UserRole";
    private const string sessionUserContactInfoId = "UserContactInfoId";
    private const string sessionUserFullName = "UserFullName";
    private const string sessionUser = "User";
    private const string sessionLoginUserName = "LoginUserName";
    private const string sessionLoginUserPassword = "LoginUserPassword";
    private static string sessionUserOrder = "UserOrder";
    private static string sessionOrderDetailOptionList = "OrderDetailOptionList";
    private static string sessionOrderDetailAdonList = "OrderDetailAdonList";
    private static string sessionUserOrderTotal = "UserOrderTotal";
    private const string sessionOrderId = "UserOrderId";
    private const string sessionPreOrderPromo = "sessionPreOrderPromo";
    private const string sessionEmail = "sessionEmail";
    private const string sessionFax = "sessionFax";
    #endregion

    #region Constructor

    public BasePage()
    {
        //Load += new EventHandler(Page_Load);
    }

    #endregion

    #region Events

    protected override void OnLoad(EventArgs e)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        DateTimeFormatInfo format = new DateTimeFormatInfo();
        format.ShortDatePattern = "MM/dd/yyyy";
        format.LongDatePattern = "MM/dd/yyyy HH:mm";
        cultureInfo.DateTimeFormat = format;

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        //Set message from session
        if (!String.IsNullOrEmpty(SessionMessage))
        {
            MessageType messageType = MessageType.Success;
            if (SessionMessageType != MessageType.None)
                messageType = SessionMessageType;
            ShowMessage(SessionMessage, messageType);
            SessionMessage = String.Empty;
            SessionMessageType = MessageType.None;
        }
        base.OnLoad(e);
    }

    #endregion

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

    public static string PhysicalApplicationPath
    {
        get
        {
            return HttpContext.Current.Server.MapPath("~");
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

    public static string HtmlFolder
    {
        get { return "/Html/"; }
    }

    public String QueryStringParamID
    {
        get { return Request.QueryString["id"]; }
    }

    public object ViewStateID
    {
        get { return ViewState["id"]; }
        set { ViewState["id"] = value; }
    }

    public static int BranchId
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["BranchId"]); }
    }

    public int GridPageSize { get { return Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]); } }

    //public bool IsStoreClosed { get { return false; } }

    public Int32 PizzaCategoryId
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["CategoryId"]); }
    }
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

    public String SessionMessage
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

    /// <summary>
    /// Message Type
    /// </summary>
    public MessageType SessionMessageType
    {
        get
        {
            if (Session[sessionMessageType] != null)
            {
                return (MessageType)Session[sessionMessageType];
            }
            return MessageType.None;
        }
        set
        {
            Session[sessionMessageType] = value;
        }
    }

    public static BusinessEntities.Orders SessionUserOrder
    {
        get
        {
            return (BusinessEntities.Orders)HttpContext.Current.Session[sessionUserOrder];
        }
        set
        {
            HttpContext.Current.Session[sessionUserOrder] = value;
        }
    }

    public static BusinessEntities.PreOrderPromo SessionPreOrderPromo
    {
        get
        {
            return (BusinessEntities.PreOrderPromo)HttpContext.Current.Session[sessionPreOrderPromo];
        }
        set
        {
            HttpContext.Current.Session[sessionPreOrderPromo] = value;
        }
    }

    public static ICollection<List<BusinessEntities.OrderDetailOptions>> SessionOrderDetailOptionList
    {
        get
        {
            return (ICollection<List<BusinessEntities.OrderDetailOptions>>)HttpContext.Current.Session[sessionOrderDetailOptionList];
        }
        set
        {
            HttpContext.Current.Session[sessionOrderDetailOptionList] = value;
        }
    }

    public static ICollection<List<BusinessEntities.OrderDetailAdOns>> SessionOrderAdonList
    {
        get
        {
            return (ICollection<List<BusinessEntities.OrderDetailAdOns>>)HttpContext.Current.Session[sessionOrderDetailAdonList];
        }
        set
        {
            HttpContext.Current.Session[sessionOrderDetailAdonList] = value;
        }
    }

    public static Double SessionUserOrderTotal
    {
        get
        {
            return (Double)HttpContext.Current.Session[sessionUserOrderTotal];
        }
        set
        {
            HttpContext.Current.Session[sessionUserOrderTotal] = value;
        }
    }

    public Int64 SessionOrderId
    {
        get
        {
            return (Int64)Session[sessionOrderId];
        }
        set
        {
            Session[sessionOrderId] = value;
        }
    }

    public String SessionEmail
    {
        get { return (String)Session[sessionEmail]; }
        set { Session[sessionEmail] = value; }
    }

    public String SessionFax
    {
        get { return (String)Session[sessionFax]; }
        set { Session[sessionFax] = value; }
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
        switch(Day)
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
            ("Eastern Standard Time");
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

    public bool ThumbImage(string originalFile, string newFile, int width, int height)
    {
        try
        {

            // Create an Image object from a file. 

            // PhotoTextBox.Text is the full path of your image

            using (System.Drawing.Image photoImg = System.Drawing.Image.FromFile(originalFile))
            {

                // Create a Thumbnail from image with size 50x40.

                // Change 50 and 40 with whatever size you want

                using (System.Drawing.Image thumbPhoto = photoImg.GetThumbnailImage(width, height, null, new System.IntPtr()))
                {

                    // The below code converts an Image object to a byte array

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        thumbPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] imgBytes = ms.ToArray();
                        thumbPhoto.Dispose();
                        photoImg.Dispose();
                        return ByteArrayToFile(newFile, imgBytes);

                    }
                }

            }

        }
        catch (Exception exp)
        {
            return false;

        }
    }

    public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
    {
        try
        {
            // Open file for reading
            System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

            // Writes a block of bytes to this stream using data from a byte array.
            _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

            // close file stream
            _FileStream.Close();

            return true;
        }
        catch (Exception _Exception)
        {

        }

        // error occured, return false
        return false;
    }

    public void SetDateTimeCulture(string culture, String shortDate, String longDate)
    {
        CultureInfo cultureInfo = new CultureInfo(culture);
        DateTimeFormatInfo format = new DateTimeFormatInfo();
        format.ShortDatePattern = shortDate;
        format.LongDatePattern = longDate;
        cultureInfo.DateTimeFormat = format;

        Thread.CurrentThread.CurrentCulture = cultureInfo;
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