using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Telerik.Web.UI;
using System.Configuration;
using System.Transactions;
using System.Data;

public partial class admin_branch_AddBranch : BasePage
{
    private KlickEntities entities = new KlickEntities();

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                try
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    Branch branch = (from b in entities.Branches
                                     where b.BranchID == id
                                     select b).FirstOrDefault();
                    if (branch != null)
                    {
                        txtBranchName.Text = branch.Title;
                        txtAddress.Text = branch.Address;
                        txtCity.Text = branch.City;
                        txtCloseTime.SelectedDate = branch.WorkingHourEnd;
                        txtEnabled.Checked = branch.IsActive;
                        txtFax.Text = branch.Fax;
                        txtOpenTime.SelectedDate = branch.WorkingHourStart;
                        txtPercentage.Value = Convert.ToDouble(branch.TaxPercentage);
                        txtPhone.Text = branch.Phone;
                        txtState.Text = branch.State;
                        txtZipCode.Text = branch.Zip;
                        if (branch.IsDeliveryEnabled)
                        {
                            txtYes.Checked = true;
                            txtDeliveryTax.Value = Convert.ToDouble(branch.DeliveryCharges);
                            DeliveryTaxtr.Style.Clear();
                            DeliveryTaxtr.Style.Add("display", "block");
                        }
                        else
                            txtNo.Checked = true;
                        txtIsClose.Checked = branch.IsClose.Value;
                        ViewState["BID"] = id;
                        SaveProductBtn.Visible = false;
                        UpdateProductBtn.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                }
            }
            else
                UpdateProductBtn.Visible = false;
        }
    }
    protected void SaveProductBtn_Click(object sender, ImageClickEventArgs e)
    {
        txtError.Text = string.Empty;
        if (CheckValues())
        {
            try
            {
                Branch branch = new Branch();
                branch.Title = txtBranchName.Text;
                branch.Address = txtAddress.Text;
                branch.City = txtCity.Text;                
                branch.Fax = txtFax.Text;
                branch.IsActive = txtEnabled.Checked;
                if (txtYes.Checked)
                {
                    branch.IsDeliveryEnabled = true;
                    branch.DeliveryCharges = Convert.ToDecimal(txtDeliveryTax.Value);
                }
                else
                    branch.IsDeliveryEnabled = false;
                branch.Phone = txtPhone.Text;
                branch.State = txtState.Text;
                branch.TaxPercentage = Convert.ToDecimal(txtPercentage.Value);
                branch.WorkingHourEnd = (DateTime)txtCloseTime.DateInput.SelectedDate;
                branch.WorkingHourStart = (DateTime)txtOpenTime.DateInput.SelectedDate;
                branch.Zip = txtZipCode.Text;
                branch.IsClose = txtIsClose.Checked;
                entities.Branches.Add(branch);
                int val = entities.SaveChanges();
                if (val > 0)
                {
                    Response.Redirect("Branches.aspx");
                }
            }
            catch (Exception ex)
            {
                txtError.Text = (ex.InnerException!=null)?ex.InnerException.Message: ex.Message;
            }
        }
    }
    protected void CancelBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Branches.aspx");
    }    

    private bool CheckValues()
    {
        if (string.IsNullOrEmpty(txtBranchName.Text))
        {
            txtError.Text = "Enter Branch Name.";
            return false;
        }
        if (string.IsNullOrEmpty(txtAddress.Text))
        {
            txtError.Text = "Enter Branch Address.";
            return false;
        }
        if (string.IsNullOrEmpty(txtZipCode.Text))
        {
            txtError.Text = "Enter Zip Code of Branch.";
            return false;
        }
        else if (!ValidationUtil.IsPositiveInteger(txtZipCode.Text))
        {
            txtError.Text = "Zip Code must be positive integer values.";
            return false;
        }
        if (string.IsNullOrEmpty(txtState.Text))
        {
            txtError.Text = "Enter State.";
            return false;
        }
        else if (!ValidationUtil.IsName(txtState.Text))
        {
            txtError.Text = "State must be character values.";
            return false;
        }       
        if (string.IsNullOrEmpty(txtPhone.Text))
        {
            txtError.Text = "Enter Telephone Number.";
            return false;
        }
        if (string.IsNullOrEmpty(txtFax.Text))
        {
            txtError.Text = "Enter Fax Number.";
            return false;
        }
        if (string.IsNullOrEmpty(txtOpenTime.DateInput.Text))
        {
            txtError.Text = "Enter Branch open time.";
            return false;
        }
        if (string.IsNullOrEmpty(txtCloseTime.DateInput.Text))
        {
            txtError.Text = "Enter Branch close time.";
            return false;
        }
        if (txtPercentage.Value == null)
        {
            txtError.Text = "Enter Tax Percentage.";
            return false;
        }
        if (txtYes.Checked == txtNo.Checked)
        {
            txtError.Text = "Select Yes or No for branch delivery.";
            return false;
        }
        else if (txtYes.Checked)
        {
            if (txtDeliveryTax.Value == null)
            {
                txtError.Text = "You must enter delivery charges.";
                return false;
            }
        }

        return true;
    }

    protected void UpdateProductBtn_Click(object sender, ImageClickEventArgs e)
    {
        if (CheckValues() && ViewState["BID"] != null)
        {
            int id = (int)ViewState["BID"];
            Branch branch = (from b in entities.Branches
                             where b.BranchID == id
                             select b).FirstOrDefault();
            if (branch != null)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    branch.Title = txtBranchName.Text;
                    branch.Address = txtAddress.Text;
                    branch.City = txtCity.Text;
                    branch.Fax = txtFax.Text;
                    branch.IsActive = txtEnabled.Checked;
                    if (txtYes.Checked)
                    {
                        branch.IsDeliveryEnabled = true;
                        branch.DeliveryCharges = Convert.ToDecimal(txtDeliveryTax.Value);
                    }
                    else
                    {
                        branch.IsDeliveryEnabled = false;
                        if (branch.DeliveryCharges != null)
                            branch.DeliveryCharges = null;
                    }
                    branch.Phone = txtPhone.Text;
                    branch.State = txtState.Text;
                    branch.TaxPercentage = Convert.ToDecimal(txtPercentage.Value);
                    branch.WorkingHourEnd = (DateTime)txtCloseTime.DateInput.SelectedDate;
                    branch.WorkingHourStart = (DateTime)txtOpenTime.DateInput.SelectedDate;
                    branch.Zip = txtZipCode.Text;
                    branch.IsClose = txtIsClose.Checked;
                    entities.SaveChanges();
                    transaction.Complete();
                    Response.Redirect("Branches.aspx");
                }
            }
        }
    }
}