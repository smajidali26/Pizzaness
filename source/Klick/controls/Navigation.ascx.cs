using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_Navigation : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionUserId != 0)
        {
            login.Visible = false;
            register.Visible = false;
            orderhistory.Visible = true;
        }
    }
}