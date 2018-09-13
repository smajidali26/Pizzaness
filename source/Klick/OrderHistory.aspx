<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="OrderHistory" %>

<%@ Register Src="~/controls/Navigation.ascx" TagName="navigation" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Order History</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
    </section>
    <section class="cart-page">
        <div class="row-fluid">
            <div class="page_content">
                <div class="span12" id="block_content_first">
                    <article class="span12 mbtm  first">
                        <div class="woocommerce">
                            <div class="cart_table_holder">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="remove">Order Date</th>
                                            <th class="name">Payment Type</th>
                                            <th class="model">Delivery Type</th>
                                            <th class="stock">Status</th>
                                            <th class="price">Payment Status</th>
                                            <th class="price">Total Price</th>
                                            <th class="price">Manage</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptOrderHistory" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="remove">
                                                        <%#Eval("OrderDate","{0:MM/dd/yyyy HH:mm}") %></td>
                                                    <td class="name"><%# ((BusinessEntities.PaymentType)Eval("PaymentMethod")).ToString() %></td>
                                                    <td class="model"><%# ((BusinessEntities.OrderType)Eval("OrderTypeID")).ToString() %></td>
                                                    <td class="stock"><%# ((BusinessEntities.OrderStatus)Eval("OrderStatusID")).ToString() %></td>
                                                    <td class="price">
                                                        <%# ReturnOrderPaymentStatus(Eval("IsPaid")) %>
                                    
                                                    </td>
                                                    <td class="price">
                                                        <div class="price"><%#Eval("OrderTotal","{0:C}") %> </div>
                                                    </td>
                                                    <td class="price">
                                                        <a class="button" style="display: <%# Display(Eval("IsPaid"),(BusinessEntities.PaymentType)Eval("PaymentMethod")) %>" onclick="RePayOrder(<%#Eval("OrderID") %>,<%#Eval("OrderTotal") %>);"><span>Pay Now</span></a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </article>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

