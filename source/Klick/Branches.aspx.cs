using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using System.Transactions;
using System.Data;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
public partial class Branches : BasePage
{
    private KlickEntities entities = new KlickEntities();

    public override void Dispose()
    {
        entities.Dispose(); // Comment Added
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            grdBranches.DataBind();
        }
    }

    private void BindData()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["zip"]))
        {
            string zip = Request.QueryString["zip"];
            List<Branch> branchlist = (from b in entities.Branches
                             where b.Zip.Equals(zip)
                             select b).ToList();
            grdBranches.DataSource = branchlist;            
        }
    }

    protected void grdBranches_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }
    protected void grdBranches_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.EditCommandName)
        {
            GridDataItem item =  e.Item as GridDataItem;
            int branchid = (int)item.GetDataKeyValue("BranchID");
            Branch branch = (from b in entities.Branches
                             where b.BranchID == branchid
                             select b).FirstOrDefault();
            Session["BranchID"] = branch.BranchID;
            Session["BranchDetail"] = branch.Title + "<br />" + branch.Address;
            Response.Redirect("Menu.aspx");
        }
    }
    protected void grdBranches_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = e.Item as GridDataItem;
            Label txtDistance = item.FindControl("txtDistance") as Label;
            if (Session["User"] != null)
            {
                Branch branch = item.DataItem as Branch;
                UserLogin login = Session["User"] as UserLogin;
                string url = ConfigurationManager.AppSettings["GoogleDirection"];
                login = entities.UserLogins.Where(u => u.UserLoginId == login.UserLoginId).FirstOrDefault();
                ContactAddress address = login.ContactInfo.ContactAddresses.Where(a => a.AddressType.AddressTypeName.ToLower().Equals("billing")).FirstOrDefault();
                string origin = "origin=" + address.Address + " " + address.City + "," + address.Zip + "," + address.State;
                string destination = "destination=" + branch.Address + " " + branch.City + "," + branch.Zip + "," + branch.State;
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + origin + "&" + destination + "&sensor=false");
                    request.Timeout = 10000;
                    request.Method = "Get";
                    request.ContentType = "application/x-www-form-urlencoded";

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    string responseXml = reader.ReadToEnd();
                    response.Close();
                    stream.Close();
                    reader.Close();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(responseXml);

                    XmlNodeList nodelist = doc.GetElementsByTagName("status");
                    if (nodelist != null)
                    {
                        if (nodelist.Count > 0)
                        {
                            if (nodelist[0].InnerText.ToLower().Equals("ok"))
                            {
                                nodelist = doc.GetElementsByTagName("distance");
                                XmlNode node = nodelist[nodelist.Count - 1].LastChild;
                                txtDistance.Text = node.InnerText;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                txtDistance.Text = "N/A";
            }
        }
    }
}