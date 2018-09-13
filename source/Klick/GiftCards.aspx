<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="GiftCards.aspx.cs" Inherits="GiftCards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>E-Gift</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
    </section>
    <section class="checkout-page">
        <div class="row-fluid">
            <div class="span2"></div>
            <div class="span8" id="block_content_first">
                <article class="span8 mbtm  first">
                    <div class="woocommerce">
                        <div class="checkout">
                            <asp:Panel ID="Panel1" runat="server">
                            <p class="form-row form-row-wide">
                                <label>
                                     Recepient Email
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox SkinID="TextBoxSimple" ID="txtRecepientEmail" runat="server" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Enter Recepient Email Address."
                                                ControlToValidate="txtRecepientEmail" ValidationGroup="vgEgift"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email format is not valid."
                                                ControlToValidate="txtRecepientEmail" ToolTip="Invalid email format." ValidationGroup="vgEgift"
                                                ValidationExpression="([a-zA-Z\d_\-\.]+)@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,3})">
                                            </asp:RegularExpressionValidator>
                            </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Your Email
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox SkinID="TextBoxSimple" ID="txtUserEmail" runat="server" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail2" runat="server" ErrorMessage="Enter Your Email Address" ValidationGroup="vgEgift"
                                                ControlToValidate="txtUserEmail"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revUserEmail" runat="server" ErrorMessage="Email format is not valid."
                                                ControlToValidate="txtUserEmail" ToolTip="Invalid Email format." ValidationGroup="vgEgift"
                                                ValidationExpression="([a-zA-Z\d_\-\.]+)@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,3})">
                                            </asp:RegularExpressionValidator>
                                 </p>
                                <p class="form-row form-row-wide">
                                <label>
                                     Credit Card Name
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox SkinID="TextBoxSimple" ID="txtName" runat="server" ClientIDMode="Static" MaxLength="60"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Enter Recepient Name"
                                                ControlToValidate="txtName" ValidationGroup="vgEgift"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
                                                ErrorMessage="Invalid characters in name." ToolTip="Invalid characters in name."
                                                ValidationExpression="[a-zA-Z\s\-']+" ValidationGroup="vgEgift"></asp:RegularExpressionValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                <label>
                                     Credit Card Number
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox SkinID="TextBoxSimple" ID="txtCreditCardNumber" ClientIDMode="Static" runat="server" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCreditCardNumber" runat="server" ErrorMessage="Enter Credit Card Number."
                                                ControlToValidate="txtCreditCardNumber" ValidationGroup="vgEgift"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revCreditCard" ErrorMessage="Credit Card Number is invalid."
                                                ControlToValidate="txtCreditCardNumber" ToolTip="Invalid Credit Card Number." ValidationGroup="vgEgift"
                                                ValidationExpression="\d{16}"></asp:RegularExpressionValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                <label>
                                     Expiry Date
                                            <abbr title="required" class="required">*</abbr></label>
                                            <table style="width: 100px;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox  ID="txtCreditExpiryMonth" ClientIDMode="Static" runat="server" Width="30" MaxLength="2"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox  ID="txtCreditExpiryYear" ClientIDMode="Static" runat="server" Width="30" MaxLength="2"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">MM
                                                    </td>
                                                    <td style="text-align: center">YY
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:RequiredFieldValidator ID="rfvExpiryMonth" runat="server" ErrorMessage="Enter Credit Expiry Date."
                                                ControlToValidate="txtCreditExpiryMonth" ValidationGroup="vgEgift"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revExpiryMonth" ErrorMessage="Credit Card Expiry Month is invalid."
                                                ControlToValidate="txtCreditExpiryMonth" ToolTip="Invalid Month." ValidationGroup="vgEgift"
                                                ValidationExpression="\d{2}"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvExpiryYear" runat="server" ErrorMessage="Enter Credit Expiry Date."
                                                ControlToValidate="txtCreditExpiryYear" ValidationGroup="vgEgift"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revExpiryYear" ErrorMessage="Credit Card Expiry Year is invalid."
                                                ControlToValidate="txtCreditExpiryYear" ToolTip="Invalid Year." ValidationGroup="vgEgift"
                                                ValidationExpression="\d{2}"></asp:RegularExpressionValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                <label>
                                     CVV
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox SkinID="TextBoxSimple" ID="txtCVV" ClientIDMode="Static" runat="server" MaxLength="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCvv" runat="server" ErrorMessage="Enter CVV Number" ValidationGroup="vgEgift"
                                                ControlToValidate="txtCvv"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revCVV" ErrorMessage="CVV Number is invalid."
                                                ControlToValidate="txtCVV" ToolTip="CVV Number is invalid." ValidationGroup="vgEgift"
                                                ValidationExpression="\d{3}"></asp:RegularExpressionValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                <label>
                                     Credit Card billing address
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox SkinID="TextBoxSimple" ID="txtAddress" ClientIDMode="Static" runat="server" TextMode="MultiLine" MaxLength="245"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Enter Billing Address" ValidationGroup="vgEgift"
                                                ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revAddress" ErrorMessage="Address contains invalid character(s)."
                                                ControlToValidate="txtAddress" ToolTip="Address contains invalid character(s)." ValidationGroup="vgEgift"
                                                ValidationExpression="[^<]*"></asp:RegularExpressionValidator>
                                </p>
                                <p class="form-row form-row-wide">
                                <label>
                                     Gift Amount
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox SkinID="TextBoxSimple" ID="txtAmount" ClientIDMode="Static" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Enter Amount you want to gift" ValidationGroup="vgEgift"
                                                ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revAmount" ErrorMessage="Only Numerics and a decimal point is allowed for amount"
                                                ControlToValidate="txtAmount" ToolTip="Invalid Character(s) entered for amount." ValidationGroup="vgEgift"
                                                ValidationExpression="\d+(\.\d+)?"></asp:RegularExpressionValidator>
                                </p>
                                <p class="form-row form-row-wide">
                               <asp:Button ID="SubmitButton" runat="server" Text=" Send E-Gift" OnClientClick="PostEncryptedData();" ClientIDMode="Static"
                                                OnClick="SubmitButton_OnClick" CssClass="button alt" /></p>
                                <p class="form-row form-row-wide">
                                <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                            </asp:Panel>
                        </div>
                    </div>
                </article>
            </div>
        </div>
    </section>
    <!--Blog Page Section End-->
    
    <asp:HiddenField runat="server" ID="PublicKey" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="EncrCreditCard" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="EncrCvv" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="EncrName" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="EncrCardExpiry" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="UserEmail" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="ReceiverEmail" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="EGiftAmount" ClientIDMode="Static" />

    <asp:HiddenField runat="server" ID="EncryptedFormData" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="EncryptedBillingAddress" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

