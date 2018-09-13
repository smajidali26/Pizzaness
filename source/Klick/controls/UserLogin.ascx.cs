using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using BusinessService;

public partial class controls_UserLogin : BaseUserControl
{
    private KlickEntities entities = new KlickEntities();

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        FailureText.Text = string.Empty;
        if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
        {
            UserLogin userlogin = (from u in entities.UserLogins
                                   where u.Username.Equals(txtUserName.Text) && u.Password.Equals(txtPassword.Text)
                                   select u).FirstOrDefault();
            if (userlogin != null)
            {
                SessionUserId = userlogin.UserLoginId;
                SessionUserRole = userlogin.UserLoginType;
                SessionUserContactInfoId = userlogin.ContactInfoId;
                SessionUser = userlogin;
                SessionUserFullName = userlogin.ContactInfo.FirstName + " " + userlogin.ContactInfo.LastName;

                // Log user login activity

                string sessionID = HttpContext.Current.Session.SessionID;
                string message = Common.UserLoginLogMessage(SessionUserFullName);

                LogManager log = new LogManager();
                log.SaveLogData(sessionID, LogLevel.INFO.ToString(), Logger.UserLogin.ToString(), message, null);

                
                if (userlogin.UserTypeId == 2 || userlogin.UserTypeId == 1)
                {
                    Response.Redirect("~/Admin/Default.aspx");
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["url"]))
                {
                    Response.Redirect(Request.QueryString["url"]);
                }
                else
                    Response.Redirect("~/Menu.aspx");
            }
            else
            {
                FailureText.Text = "Invalid Email or passsword.";
            }
        }
        else
        {
            FailureText.Text = (string.IsNullOrEmpty(txtUserName.Text)) ? "Enter Email." : "Enter Password";
        }
    }
}