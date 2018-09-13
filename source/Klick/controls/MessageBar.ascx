<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBar.ascx.cs" Inherits="controls_MessageBar" %>
<div id="noty" runat="server" visible="false" enableviewstate="false" clientidmode="Static">
    <div class="noty_message">
        <asp:Label ID="lblMessage" EnableViewState="false" ClientIDMode="Static" runat="server" Text="" CssClass="noty_text"></asp:Label>
    </div>
</div>