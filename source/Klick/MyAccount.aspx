<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true"
    CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<%@ Register Src="~/controls/Navigation.ascx" TagName="navigation" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>My Account</h1>
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
                            <asp:Panel ID="Panel1" runat="server" CssClass="col-1" DefaultButton="SaveDetail">
                                <h3>Personal Detail</h3>
                                <p class="form-row form-row-wide">
                                <label>
                                     Firstname
                                            <abbr title="required" class="required">*</abbr></label>
                                    <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Lastname
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Email
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:Label ID="txtEmail" runat="server" Text=""></asp:Label>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Telephone
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtTelephone" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtTelephone"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Mobile
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtMobile" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="20"></asp:TextBox>
                                    </p>
                                <h3>Billing Address</h3>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Address
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtAddress" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     City
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtCity" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     Zip code
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtZipCode" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtZipCode"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                <label>
                                     State
                                            <abbr title="required" class="required">*</abbr></label>
                                     <asp:TextBox ID="txtState" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtState"></asp:RequiredFieldValidator>
                                    </p>
                                 <p class="form-row form-row-wide">
                                    <asp:Button ID="SaveDetail" runat="server" CssClass="button alt" Text="Save Detail" OnClick="SaveDetail_Click" ClientIDMode="Static" />
                                     <asp:Button ID="CancelButton" runat="server" Text=" Cancel " CausesValidation="false"
                                                PostBackUrl="Menu.aspx" CssClass="button alt" />
                                    </p>
                                 <p class="form-row form-row-wide">
                                    <asp:Label ID="txtMessage" runat="server" Text=""></asp:Label>
                                    </p>
                                </asp:Panel>
                            </div>
                    </div>
                </article>
            </div>
        </div>
    </section>
</asp:Content>
