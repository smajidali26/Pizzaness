using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_MessageBar : BaseUserControl, IMessageBar
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "IMessageBar Implementation"
    /// <summary>
    /// ShowMessage
    /// </summary>
    /// <param name="message"></param>
    /// <param name="type"></param>
    public override void ShowMessage(string message, MessageType type)
    {
        lblMessage.Text = message;
        noty.Attributes.Add("rel", type.ToString().ToLower(CultureInfo.InvariantCulture));
        noty.Attributes.Add("class", "noty_bar noty_theme_default noty_layout_top noty_" + type.ToString().ToLower(CultureInfo.InvariantCulture));
        noty.Visible = true;
    }
    #endregion
}