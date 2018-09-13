<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="controls_MessageBox" %>
<div id="notification" runat="server" clientidmode="Static">
    <button class="close" type="button" data-dismiss="alert"></button>
    <asp:Literal ID="txtMessage" runat="server"></asp:Literal>
</div>
