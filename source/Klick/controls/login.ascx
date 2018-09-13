<%@ Control Language="VB" AutoEventWireup="false" CodeFile="login.ascx.vb" Inherits="controls_login" %>

        <asp:Login ID="Login2" runat="server" align="center">
            <LayoutTemplate>
                <table border="0" cellpadding="0" width="100%" class="dc_form_contact_light table">
                    <tr>
                        <td align="center" colspan="2" width="40%" class="validator">
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                                                Email
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserName" runat="server" Width="130"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login2">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Password" runat="server" Width="130" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login2">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="LoginButton" runat="server" Text="Sign In" 
                                ValidationGroup="Login2" onclick="LoginButton_Click" SkinID="ButtonSubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <a href="/forget-password.aspx">Forgot your password?</a>
                            <br />
                            <a href="/register.aspx">Register as new member?</a>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
