using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using System.Transactions;

public partial class Register : BasePage
{
    private KlickEntities entities = new KlickEntities();

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected override void OnInit(EventArgs e)
    {
        if (SessionUserId != 0)
        {
            Response.Redirect("Default.aspx");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        txtError.Text = string.Empty;
        try
        {
            if (validateValues())
            {
                ContactInfo contactinfo = new ContactInfo();
                contactinfo.Title = txtCbTitle.SelectedValue;
                contactinfo.FirstName = txtFirstName.Text;
                contactinfo.LastName = txtLastName.Text;
                contactinfo.Email = txtEmail.Text;
                contactinfo.Telephone = txtTelephone.Text;
                contactinfo.Mobile = txtMobile.Text;
                contactinfo.CreatedOn = DateTime.Now;
                UserLogin userlogin = new UserLogin();
                userlogin.Username = txtEmail.Text;
                userlogin.Password = txtPassword.Text;
                userlogin.CreatedOn = DateTime.Now;
                userlogin.Enable = true;
                userlogin.UserLoginType = entities.UserLoginTypes.Where(ul => ul.LoginTypeName.ToLower().Equals("user")).FirstOrDefault();
                ContactAddress contactaddress = new ContactAddress();
                contactaddress.Address = txtAddress.Text;
                contactaddress.City = txtCity.Text;
                contactaddress.Zip = txtZipCode.Text;
                contactaddress.State = txtState.Text;
                contactaddress.Country = txtCbCountry.SelectedValue;
                contactaddress.CreatedOn = DateTime.Now;
                contactaddress.AddressType = entities.AddressTypes.Where(at => at.AddressTypeName.ToLower().Equals("billing")).FirstOrDefault();
                contactinfo.ContactAddresses.Add(contactaddress);
                contactinfo.UserLogins.Add(userlogin);

                using (TransactionScope transaction = new TransactionScope())
                {
                    entities.ContactInfoes.Add(contactinfo);
                    entities.SaveChanges();
                    transaction.Complete();
                    SessionMessage = "Your account has been created successfully. You can login now to order.";
                    Response.Redirect("~/Confirmation.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    private bool validateValues()
    {        
        if (string.IsNullOrEmpty(txtFirstName.Text))
        {
            txtError.Text = "Enter First Name.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtLastName.Text))
        {
            txtError.Text = "Enter Last Name.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtEmail.Text))
        {
            txtError.Text = "Enter Email";
            return false;
        }
        else if (!ValidationUtil.IsEmail(txtEmail.Text))
        {
            txtError.Text = "Invalid Email.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtTelephone.Text))
        {
            txtError.Text = "Enter Telephone Number.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtPassword.Text))
        {
            txtError.Text = "Enter Password.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtAddress.Text))
        {
            txtError.Text = "Enter Address.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtCity.Text))
        {
            txtError.Text = "Enter City.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtState.Text))
        {
            txtError.Text = "Enter State.";
            return false;
        }
        else if (string.IsNullOrEmpty(txtZipCode.Text))
        {
            txtError.Text = "Enter Zip Code.";
            return false;
        }
        else if (txtCbCountry.SelectedValue.Equals("0"))
        {
            txtError.Text = "Select Country.";
            return false;
        }
        return true;
    }
}