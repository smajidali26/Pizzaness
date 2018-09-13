using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signout : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Login.aspx");
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}