<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserLogin.ascx.cs" Inherits="controls_UserLogin" %>

<section class="checkout-page">
    <div class="row-fluid">
        <div class="span2"></div>
        <div class="span8" id="block_content_first">
            <article class="span8 mbtm  first">
                <div class="woocommerce">
                    <div class="checkout">
                        <asp:Panel ID="LoginPanel" runat="server" DefaultButton="LoginButton">
                            <p class="form-row form-row-wide"><asp:Label ID="FailureText" runat="server" EnableViewState="False"></asp:Label></p>
                            <p id="billing_username_field" class="form-row form-row-wide">
                                <label class="" for="billing_username">
                                     Username (Email)
                                            <abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox ID="txtUserName" runat="server" SkinID="TextBoxSimple"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName" Display="Dynamic"
                                    ErrorMessage="Username is required." ToolTip="Username is required." ValidationGroup="Login2"></asp:RequiredFieldValidator>
                                </p>
                        <p id="billing_password_field" class="form-row form-row-wide">
                                <label class="" for="billing_password">
                                     Password
                                            <abbr title="required" class="required">*</abbr></label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="TextBoxSimple"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" Display="Dynamic"
                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login2"></asp:RequiredFieldValidator>
                            </p>
                        <p class="form-row form-row-wide">
                            <asp:Button ID="LoginButton" runat="server" Text=" Sign In " ValidationGroup="Login2" ClientIDMode="Static"
            OnClick="LoginButton_Click" CssClass="button alt" />  <a href="/ForgotPassword.aspx" class="lost_password">Lost Password?</a>
                        </p>
                            
                        </asp:Panel>
                    </div>
                </div>
            </article>
        </div>
    </div>
</section>
