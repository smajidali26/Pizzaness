<%@ Page Title="Pizzaness Menu" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true"
    CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<%@ MasterType VirtualPath="~/templates/main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="blog-page-section">
        <div class="container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="heading">
                        <h1>Our Menu</h1>
                    </div>
                </div>
            </div>
        </div>
        <!--Blog Post End-->

        <section class="menu-page">
            
                
                    <asp:Repeater ID="rptMenu1" runat="server">
                        <ItemTemplate>
                            <div class="span3">
                                <div class="menu-box">
                                    <div class="thumb">
                                        <img src="/Products/Category/L_<%#Eval("ImagePath") %>" class="img-polaroid" alt="img" style="height: 180px; width: 280px;">
                                    </div>
                                    <div class="text">
                                        <strong class="title"><%#Eval("Name") %> </strong>
                                        
                                    </div>
                                    <div class="menu-box-bottom">
                                        <ul>
                                            <li><a href="/ProductPage.aspx?id=<%#Eval("CategoryID") %>" title="<%#Eval("Name") %>" class="add">View Menu Detail</a> </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                
        </section>
    </section>


</asp:Content>
