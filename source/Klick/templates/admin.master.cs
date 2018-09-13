using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class templates_admin : BaseMasterPage
{
    protected override void OnInit(EventArgs e)
    {
        if (SessionUser == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        else if (SessionUserRole.LoginTypeId == 3)
        {
            Response.Redirect("~/Login.aspx");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
