using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core;

public partial class ContactUs : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void buttonSendEmail_Click(object sender, EventArgs e)
    {
        #region Mail Body

        StringBuilder sb = new StringBuilder();
        sb = sb.Append(Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "ContactUs.html"));
        sb = sb.Replace("{URL}", BaseSiteUrl);
        sb = sb.Replace("{NAME}", txtName.Text);
        sb = sb.Replace("{EMAIL}", txtEmail.Text);
        sb = sb.Replace("{TELEPHONE}", txtTelephone.Text);
        sb = sb.Replace("{COMMENTS}", txtEnquiry.Text);

        #endregion

        Core.Utility.SendEmail(ConfigurationManager.AppSettings["DonotReplyEmail"], ConfigurationManager.AppSettings["OrderReceiveEmail"], "Contact Us", sb.ToString(), true);
        txtEmail.Text = txtEnquiry.Text = txtName.Text = txtTelephone.Text = String.Empty;
        txtMessage.Text = "Thank you for contacting us.";
        txtMessage.ForeColor = Color.Green;
    }
}