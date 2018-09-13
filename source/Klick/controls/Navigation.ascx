<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Navigation.ascx.cs" Inherits="controls_Navigation" %>
<!-- RIGHT COLUMN -->

    <div class="box-right-navigation float-left">
        <h3 class="heading-title"><span>My Account</span></h3>
        <div class="box-content box-account red-color">
            <ul>
                <li id="login" runat="server"><a href="/Login.aspx" class="red-color">Login</a></li>
                <li id="register" runat="server"><a href="/Register.aspx" class="red-color">Register</a></li>
                <li><a href="/ForgotPassword.aspx" class="red-color">Forgotten Password</a></li>
                <li><a href="/MyAccount.aspx" class="red-color">My Account</a></li>
                <li id="orderhistory" runat="server" visible="false"><a href="/OrderHistory.aspx"  class="red-color">Order History</a></li>
            </ul>
        </div>
    </div>
<!-- END OF RIGHT COLUMN -->
