<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="controls_Menu" %>

    <ul class="menu">
        <asp:Repeater ID="rptMenu" runat="server">
            <ItemTemplate>
                <li><a href="/ProductPage.aspx?id=<%#Eval("CategoryID") %>"><%#Eval("Name") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>

