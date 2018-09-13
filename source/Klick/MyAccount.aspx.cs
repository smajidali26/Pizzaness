using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using BusinessService;
using BusinessEntities;
using System.Drawing;

public partial class MyAccount : BasePage
{
    #region Private Members
    private ContactInfoManager contactInfoManager = new ContactInfoManager();
    private KlickEntities entities = new KlickEntities();
    #endregion

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected override void OnInit(EventArgs e)
    {
        if (SessionUser == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionUser != null)
        {
            BusinessObjects.ContactInfo contactinfo = (from C in entities.ContactInfoes
                                      where C.ContactInfoId == SessionUser.ContactInfoId
                                      select C).FirstOrDefault();
            txtFirstName.Text = contactinfo.FirstName;
            txtLastName.Text = contactinfo.LastName;
            txtEmail.Text = contactinfo.Email;
            txtTelephone.Text = contactinfo.Telephone;
            txtMobile.Text = contactinfo.Mobile;
            ContactAddress address = contactinfo.ContactAddresses.Where( c=>c.AddressType.AddressTypeName.ToLower().Equals("billing")).FirstOrDefault();
            txtAddress.Text = address.Address;
            txtCity.Text = address.City;
            txtZipCode.Text = address.Zip;
            txtState.Text = address.State;
            ViewState["ContactInfoId"] = contactinfo.ContactInfoId;
        }
    }


    protected void SaveDetail_Click(object sender, EventArgs e)
    {
        ContactAddresses contactAddress = new ContactAddresses();
        contactAddress.Address = txtAddress.Text;
        contactAddress.City = txtCity.Text;
        contactAddress.State = txtState.Text;
        contactAddress.Zip = txtZipCode.Text;
        contactAddress.ContactInfoId = (Int64)ViewState["ContactInfoId"];

        BusinessEntities.ContactInfo contactInfo = new BusinessEntities.ContactInfo();
        contactInfo.FirstName = txtFirstName.Text;
        contactInfo.LastName = txtLastName.Text;
        contactInfo.Email = txtEmail.Text;
        contactInfo.Telephone = txtTelephone.Text;
        contactInfo.Mobile = txtMobile.Text;
        contactInfo.ContactInfoId = (Int64)ViewState["ContactInfoId"];
        contactInfo.ContactAddressList = new List<ContactAddresses>();
        contactInfo.ContactAddressList.Add(contactAddress);

        Int32 result = contactInfoManager.UpdateContactInfo(contactInfo);
        if (result > 0)
        {
            txtMessage.Text = "Your personal information has been updated successfully.";
            txtMessage.ForeColor = Color.Green;
        }
    }
}