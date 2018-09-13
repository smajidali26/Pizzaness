using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_MessageBox : BaseUserControl, IMessageBar
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public String Message
    {
        set
        {
            txtMessage.Text = value;
            notification.Attributes.Remove("class");
            notification.Attributes.Add("class", "alert alert-success fade in");
        }
    }

    #region "IMessageBar Implementation"
    /// <summary>
    /// ShowMessage
    /// </summary>
    /// <param name="message"></param>
    /// <param name="type"></param>
    public override void ShowMessage(string message, MessageType type)
    {
        txtMessage.Text = message;
        txtMessage.Visible = true;

        if (!String.IsNullOrEmpty(message))
        {
            txtMessage.Text = message;
            notification.Attributes.Clear();
            notification.Attributes.Add("class", "alert alert-" + Convert.ToString(type).ToLowerInvariant() + " fade in");
            notification.Visible = true;
        }
        else
        {
            notification.Visible = false;
        }
    }
    #endregion
}