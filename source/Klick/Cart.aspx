<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Blog Page Section Start-->
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Cart</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
    </section>
    <!--Blog Page Section End-->
    <section class="cart-page">
        <div class="row-fluid">
            <div class="page_content">
                <div class="span12" id="block_content_first">
                    <article class="span12 mbtm  first">
                        <div class="woocommerce">
                            <div class="cart_table_holder">

                                <div class="clear"></div>
                                <table width="100%" cellpadding="10" border="0">
                                    <thead>
                                        <tr>

                                            <!--<th class="product-thumbnail">&nbsp;</th>-->

                                            <th width="60%" colspan="2" class="product-name">Product</th>
                                            <th width="10%" class="product-price">Price</th>
                                            <th width="15%" class="product-quantity">Quantity</th>
                                            <th width="10%" class="product-subtotal">Total</th>
                                            <th width="5%" class="product-remove"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptCart" runat="server" OnItemDataBound="rptCart_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="cart_table_item" id='<%#Eval("OrderDetailID") %>' unitprice="<%#Eval("Price") %>" totalprice="<%#Eval("ItemTotal") %>">
                                                    <!-- Remove from cart link -->

                                                    <!-- The thumbnail -->
                                                    <td class="img">
                                                        <img src="<%# "/Products/S_"+Eval("ProductImage") %>" width="60px" alt="<%#Eval("ProductName") %>" /></td>

                                                    <!-- Product Name -->
                                                    <td width="40%;" class="product-name"><%#Eval("ProductName") %>
                                                        <ul style="font-size: 11px  !important;">
                                                            <asp:Repeater ID="Repeater1" runat="server">

                                                                <ItemTemplate>

                                                                    <li>
                                                                        <%#Eval("ProductName") %>
                                                                    </li>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </td>

                                                    <!-- Product price -->
                                                    <td class="product-price price unitprice"><span class="amount">$<%#Eval("Price") %></span></td>

                                                    <!-- Quantity inputs -->
                                                    <td class="product-quantity">
                                                        <div class="quantity buttons_added">
                                                            <input type="button" class="minus" value="-" style="height: 38px; margin-right: 2px;" onclick="IncreaseOrDescreaseQuantity(<%#Eval("OrderDetailID") %>,false)">
                                                            <input type="number" maxlength="12" class="input-text qty text" title="Qty" size="4" value="<%#Eval("Quantity") %>" max="" min="1" step="1" id="quantity<%#Eval("OrderDetailID") %>" name="quantity<%#Eval("OrderDetailID") %>" onchange="return CalculatePrice(this);">
                                                            <input type="button" class="plus" value="+" style="height: 38px; margin-right: 0px;" onclick="IncreaseOrDescreaseQuantity(<%#Eval("OrderDetailID") %>,true)">
                                                        </div>
                                                    </td>

                                                    <!-- Product subtotal -->
                                                    <td class="product-subtotal price totalprice"><span class="amount">$<%#Eval("ItemTotal") %></span></td>
                                                    <td class="product-remove"><a title="Remove this item" class="cbtn" href="javascript:void(0);" onclick="SelectItemsToDelete(<%#Eval("OrderDetailID") %>)">Remove</a></td>
                                                </tr>
                                            </ItemTemplate>
                                            
                                        </asp:Repeater>

                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td width="15%" class="total_price" colspan="6">
                                                <div class="cart_totals ">
                                                    <table cellspacing="0" class="total_cart">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <h2>Cart Totals</h2>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                        <tbody>
                                                            <tr class="cart-subtotal">
                                                                <td style="width: 70%;"><strong>Cart Subtotal</strong></td>
                                                                <td style="width: 30%;"><strong><span class="amount subtotal">$<asp:Label ID="txtSubTotal" runat="server"></asp:Label></span></strong></td>
                                                            </tr>
                                                            <tr class="deduction" id="deduction" runat="server" Visible="False">
                                                                <td><strong>Discount</strong></td>
                                                                <td><strong><span class="amount subtotal">$<asp:Label ID="txtDiscount" runat="server"></asp:Label></span></strong></td>
                                                            </tr>
                                                            <tr class="shipping">
                                                                <td>TAX
                                                                    <asp:HiddenField ID="hdVAT" runat="server" ClientIDMode="Static" />
                                                                </td>
                                                                <td class="amount vat">$<asp:Label ID="txtVATAmount" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Order Total</strong></td>
                                                                <td><strong><span class="amount total">$<asp:Label ID="txtTotal" runat="server"></asp:Label></span></strong></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div class="cart_btn_wrapper">
                                    
                                    <input type="button" value="Proceed to Checkout →" name="proceed" onclick="javascript:window.location.href='Proceed.aspx'; " class="pull-right checkout_btn checkout-button button alt">
                                    
                                </div>
                            </div>
                        </div>
                    </article>
                </div>
            </div>
        </div>
    </section>
    <!--Cart End-->
    
    <asp:HiddenField ID="hdSelectedDeletedItems" runat="server" ClientIDMode="Static" />
    <asp:Button ID="buttonDelete" runat="server" Text="Delete Items" CssClass="hide" ClientIDMode="Static" OnClick="buttonDelete_Click" />
           
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

