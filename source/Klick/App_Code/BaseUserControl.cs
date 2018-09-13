using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using BusinessObjects;

/// <summary>
/// Summary description for BaseUserControl
/// </summary>
public class BaseUserControl : UserControl, IMessageBar
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
    private const string sessionUserOrder = "UserOrder";
    private const string sessionUserOrderTotal = "UserOrderTotal";
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

    public Int32 PizzaCategoryId
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["CategoryId"]); }
    }

    #endregion

    #endregion

    #region "IMessageBar Implementation"
    /// <summary>
    /// ShowMessage
    /// </summary>
    /// <param name="message"></param>
    /// <param name="type"></param>
    public virtual void ShowMessage(string message, MessageType type)
    {
        if (this.Page is IMessageBar)
        {
            IMessageBar messageBar = (IMessageBar)this.Page;
            messageBar.ShowMessage(message, type);
        }
    }
    #endregion
}