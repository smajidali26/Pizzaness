using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessEntities;
using BusinessService;
using Core;

namespace POS
{
    /// <summary>
    /// Interaction logic for Proceed.xaml
    /// </summary>
    public partial class Proceed : Elysium.Controls.Window
    {
        #region Members
        
        private ContactInfoManager _contactInfoManager = new ContactInfoManager();

        private OrderManager orderManager = new OrderManager();

        private BranchManager branchManager = new BranchManager();

        private Orders UserOrder = new Orders();

        private PromotionCodeManager promoManager = new PromotionCodeManager();

        private ContactInfo ContactInfoObject { get; set; }

        private Double deduction = 0;

        public ICollection<List<BusinessEntities.OrderDetailOptions>> OrderDetailOptionList
        {
            get;
            set;
        }

        public ICollection<List<BusinessEntities.OrderDetailAdOns>> OrderDetailAdonList
        {
            get;
            set;
        }

        #endregion

        public Proceed()
        {
            InitializeComponent();
        }

        #region Email Textbox Event

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchByEmail();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchByEmail();
            }
        }

        #endregion
        
        #region Phone Textbox event
                
        private void txtPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchByPhoneNumber();
        }
        
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchByPhoneNumber();
            }
        }

        #endregion

        private void txtPromoCode_LostFocus(object sender, RoutedEventArgs e)
        {
            /*PromotionCodes code = promoManager.GetPromotionCodeByCode(txtPromoCode.Text);
            if (code != null)
            {
                

                if (code.TypeOfPromotion == PromotionType.Money)
                {
                    deduction = code.PromotionValue;
                }
                else
                {
                    deduction = (UserOrder.OrderTotal * code.PromotionValue) / 100;
                }

            }
            else
            {
                deduction = 0;
                MessageBox.Show("Invalid promo code.");
            }*/
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ContactAddresses address = null;
            if (ContactInfoObject == null)
            {
                ContactInfoObject = new ContactInfo();
                ContactInfoObject.Title = (String)((ComboBoxItem)txtTitle.SelectedValue).Content;
                ContactInfoObject.FirstName = txtFirstName.Text;
                ContactInfoObject.LastName = txtLastName.Text;
                ContactInfoObject.Telephone = txtPhone.Text;
                ContactInfoObject.Mobile = String.Empty;
                ContactInfoObject.Email = txtEmail.Text;
                address = new ContactAddresses();
                address.Address = txtAddress.Text;
                address.AddressTypeId = 3;
                address.City = txtCity.Text;
                address.State = txtState.Text;
                address.Zip = txtZipCode.Text;
                address.Country = "United States";
                ContactInfoObject.ContactAddressList = new List<ContactAddresses>();
                ContactInfoObject.ContactAddressList.Add(address);
                Int64 result1 = _contactInfoManager.AddContactInfo(ContactInfoObject, 3);
                if (result1 > 0)
                {
                    ContactInfoObject.ContactInfoId = result1;
                }
            }
            if (txtOnlinePayment.IsChecked.Value)
            {
                UserOrder.PaymentMethod = PaymentType.OnlinePayment;
            }
            else
            {
                UserOrder.PaymentMethod = PaymentType.CashPayment;
            }

            UserOrder.CustomerEmail = ContactInfoObject.Email;
            UserOrder.CustomerTelephone = ContactInfoObject.Telephone;
            UserOrder.CustomerMobile = ContactInfoObject.Mobile;
            if (address == null)
                address = Utility.XmlToObject<ContactAddresses>(ContactInfoObject.ContactAddressXml); ;
            UserOrder.DeliveryAddress = address.Address + " " + address.City + " " + address.Zip + " " + address.State;

            ((MainWindow)this.Owner).ContactInfoObject = this.ContactInfoObject;
            this.Close();
        }
        
        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            ContactAddresses address = null;
            if (ContactInfoObject == null)
            {
                ContactInfoObject = new ContactInfo();
                ContactInfoObject.Title = (String)((ComboBoxItem)txtTitle.SelectedValue).Content;
                ContactInfoObject.FirstName = txtFirstName.Text;
                ContactInfoObject.LastName = txtLastName.Text;
                ContactInfoObject.Telephone = txtPhone.Text;
                ContactInfoObject.Mobile = String.Empty;
                ContactInfoObject.Email = txtEmail.Text;
                address = new ContactAddresses();
                address.Address = txtAddress.Text;
                address.AddressTypeId = 3;
                address.City = txtCity.Text;
                address.State = txtState.Text;
                address.Zip = txtZipCode.Text;
                address.Country = "United States";
                ContactInfoObject.ContactAddressList = new List<ContactAddresses>();
                ContactInfoObject.ContactAddressList.Add(address);
                Int64 result1 = _contactInfoManager.AddContactInfo(ContactInfoObject, 3);
                if (result1 > 0)
                {
                    ContactInfoObject.ContactInfoId = result1;
                }
            }
            
        }

        private void PrintLogo(ThermalPrinter printer)
        {
            printer.WriteLine("Test image:");
            Bitmap img = new Bitmap("/logo.png");
            printer.LineFeed();
            printer.PrintImage(img);
            printer.LineFeed();
            printer.WriteLine("Image OK");
        }

        private void SearchByPhoneNumber()
        {
            if (!String.IsNullOrEmpty(txtPhone.Text))
            {
                ContactInfoObject = _contactInfoManager.GetContactInfoByEmailOrPhone(false, txtPhone.Text);
                if (ContactInfoObject != null)
                {
                    txtFirstName.Text = ContactInfoObject.FirstName;
                    txtLastName.Text = ContactInfoObject.LastName;
                    txtEmail.Text = ContactInfoObject.Email;
                    txtTitle.SelectedValue = ContactInfoObject.Title;
                    ContactAddresses address = Utility.XmlToObject<ContactAddresses>(ContactInfoObject.ContactAddressXml);
                    if (address != null && !String.IsNullOrEmpty(address.City))
                    {
                        txtAddress.Text = address.Address;
                        txtCity.Text = address.City;
                        txtState.Text = address.State;
                        txtZipCode.Text = address.Zip;
                    }
                    ContactInfoObject.ContactAddressList = new List<ContactAddresses>();
                    ContactInfoObject.ContactAddressList.Add(address);
                }
                else
                {
                    txtFirstName.Text = txtLastName.Text = txtEmail.Text = txtAddress.Text = txtCity.Text = txtState.Text = txtZipCode.Text = String.Empty;
                }
            }
        }
        
        private void SearchByEmail()
        {
            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                ContactInfoObject = _contactInfoManager.GetContactInfoByEmailOrPhone(true, txtEmail.Text);
                if (ContactInfoObject != null)
                {
                    txtFirstName.Text = ContactInfoObject.FirstName;
                    txtLastName.Text = ContactInfoObject.LastName;
                    txtPhone.Text = ContactInfoObject.Telephone;
                    txtTitle.SelectedValue = ContactInfoObject.Title;
                    ContactAddresses address = Utility.XmlToObject<ContactAddresses>(ContactInfoObject.ContactAddressXml);
                    if (address != null && !String.IsNullOrEmpty(address.City))
                    {
                        txtAddress.Text = address.Address;
                        txtCity.Text = address.City;
                        txtState.Text = address.State;
                        txtZipCode.Text = address.Zip;
                        

                    }
                    ContactInfoObject.ContactAddressList = new List<ContactAddresses>();
                    ContactInfoObject.ContactAddressList.Add(address);
                }
                else
                {
                    txtFirstName.Text = txtLastName.Text = txtEmail.Text = txtAddress.Text = txtCity.Text = txtState.Text = txtZipCode.Text = String.Empty;
                }
            }
        }

    }
}
