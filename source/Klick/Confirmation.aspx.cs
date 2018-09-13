using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Confirmation : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(SessionMessage))
        {
            txtMessage.Text = SessionMessage;
            SessionMessage = String.Empty;
        }
    }
}