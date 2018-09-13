using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Data.Common;
using System.Data.Objects;

public partial class forgotpassword : System.Web.UI.Page
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bError = false;
        string query = @"Select CI.Email,UL.Username,UL.Password From CotactInfoes AS CI Inner Join UserLogins AS UL On CI.ContactInfoId = UL.ContactInfoId ";
        if (!string.IsNullOrEmpty(txtEmail.Text))
        {
            if (ValidationUtil.IsEmail(txtEmail.Text))
            {
                query += " Where CI.Email= N'" + Server.HtmlDecode(txtEmail.Text) + "'";
            }
            else
            {
                bError = true;
                txtMessage.Text = "Enter Valid Email.";
            }
        }
        else
        {
            bError = true;
            txtMessage.Text = "Enter Username or Email to get Password.";
        }

        if (!bError)
        {
           /*DbDataRecord result = new ObjectQuery<DbDataRecord>(query, entities).FirstOrDefault();
            
            if (result != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<table border='0' cellpadding='3'cellspacing='0' width='500px' style='font-size:12px; font-family:Arial;'>");
                sb.Append("<tr><td colspan='2'>Below is detail of your Email and Password detail.</td></tr>");
                sb.Append("<tr><td style='150px'>Email:</td><td style='width:350px;'>" + result.GetValue(result.GetOrdinal("Username")) + "</td></tr>");
                sb.Append("<tr><td style='150px'>Password:</td><td style='width:350px;'>" + result.GetValue(result.GetOrdinal("Password")) + "</td></tr>");
                sb.Append("</table>");
                sb.Append("</html>");
                MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["NoReplyEmail"], (string)result.GetValue(result.GetOrdinal("Email")), "Password Recovery Mail", sb.ToString());
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtphost"], int.Parse(ConfigurationManager.AppSettings["smtpport"]));
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpuser"], ConfigurationManager.AppSettings["smtppassword"]);
                
                client.Send(mail);
                txtEmail.Text =  string.Empty;
                txtMessage.Text = "Password Recovery email has been sent to your registered email address";
            }
            else
            {
                txtMessage.Text = "No information found. Please enter valid values.";
            }*/
        }
    }
}