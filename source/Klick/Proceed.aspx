<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true"
    CodeFile="Proceed.aspx.cs" Inherits="Proceed" %>

<%@ MasterType VirtualPath="~/templates/main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $j(function () {
            $j("#accordion").accordion({
                autoHeight: false,
                navigation: true,
                event: false
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="blog-page-section">
        <div class="row-fluid">
            <div class="span12">
                <div class="heading">
                    <h1>Check Out</h1>
                </div>
            </div>
            <!--Blog Post End-->

        </div>
        <section class="menu-page full-menu-list">
            <div class="container">
                <div class="row-fluid">
                    <div id="accordion2" class="accordion accordion-area">
                        <div class="accordion-group">
                            <div class="accordion-heading active"><a href="#collapseOne" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle collapsed">Delivery Method</a> </div>
                            <div class="accordion-body collapse in" id="collapseOne">
                                <div class="accordion-inner">
                                    <div class="row-fluid">
                                        <div class="span12">
                                            <p>Please select the preferred delivery method to use on this order.</p>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:RadioButtonList ID="txtDeliveryMethod" runat="server" RepeatDirection="Vertical" RepeatLayout="UnorderedList" ClientIDMode="Static" OnSelectedIndexChanged="txtDeliveryMethod_SelectedIndexChanged" CssClass="proceed_delivery" AutoPostBack="true">
                                                        <asp:ListItem Text="Delivery" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Self Pickup" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList><br/><br/>
                                                    <asp:RequiredFieldValidator ID="rfvDeliveryMethod" runat="server" ErrorMessage="Select delivery option." ControlToValidate="txtDeliveryMethod" ClientIDMode="Static"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtDeliveryMethod" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="social-form">
                                                <div class="right"><a href="javascript:void(0);" id="button-shipping" onclick="ShowPanel('collapseTwo','collapseOne');" class="btn-submit2">Continue</a></div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-group">
                            <div class="accordion-heading"><a href="#collapseTwo" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">Payment Method </a></div>
                            <div class="accordion-body collapse" id="collapseTwo">
                                <div class="accordion-inner">
                                    <div class="row-fluid">
                                        <div class="span12">

                                            <p>Please select the preferred payment method to use on this order.</p>

                                            <asp:RadioButtonList ID="txtPaymentMethod" runat="server" RepeatDirection="Vertical" CssClass="proceed_delivery" RepeatLayout="UnorderedList" ClientIDMode="Static">
                                                <asp:ListItem Text="Online Payment" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Cash On Delivery" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvPaymentMethod" runat="server" ErrorMessage="Select payment option." ControlToValidate="txtPaymentMethod" ClientIDMode="Static"></asp:RequiredFieldValidator>

                                            <div class="social-form">
                                                <div class="right">
                                                    <a id="button-back" class="btn-submit2" href="javascript:void(0);" onclick="ShowPanel('collapseOne','collapseTwo');">Back</a> 
                                                    <a id="button-payment" class="btn-submit2 offset0" href="javascript:void(0);" onclick="ShowPanel('collapseThree','collapseTwo');">Continue</a>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-group" id="Billing">
                            <div class="accordion-heading"><a href="#collapseThree" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">Billing & Delivery Details </a></div>
                            <div class="accordion-body collapse" id="collapseThree">
                                <div class="accordion-inner">
                                    <div class="row-fluid">
                                        <article class="span8 mbtm  first">
                                            <div class="woocommerce">
                                                <div class="checkout">
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
                                                            Telephone
                                                            <abbr title="required" class="required">*</abbr></label>
                                                        <asp:TextBox ID="txtTelephone" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="20"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ErrorMessage="Required" ClientIDMode="Static" Display="Dynamic" ControlToValidate="txtTelephone"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p class="form-row form-row-wide">
                                                        <label>
                                                            Mobile </label>
                                                        <asp:TextBox ID="txtMobile" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="20"></asp:TextBox>
                                                    </p>
                                                    <p class="form-row form-row-wide">
                                                        <label>
                                                            Address
                                                            <abbr title="required" class="required">*</abbr></label>
                                                        <asp:TextBox ID="txtAddress" runat="server" ClientIDMode="Static" SkinID="TextBoxSimple" MaxLength="100"></asp:TextBox>
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
                                                    
                                                </div>
                                            </div>
                                        </article>
                                        
                                            <div class="social-form">
                                                            <div class="right">
                                                                <a class="btn-submit2" id="billing-back" href="avascript:void(0);" onclick="ShowPanel('collapseTwo','collapseThree');">Back</a>
                                                                <a class="btn-submit2 offset0" id="billing-continue" href="javascript:void(0);" onclick="ShowPanel('collapseFour','collapseThree');">Continue</a>
                                                            </div>
                                                        </div>

                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-group">
                            <div class="accordion-heading"><a href="#collapseFour" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">Confirm Order </a></div>
                            <div class="accordion-body collapse" id="collapseFour">
                                <div class="accordion-inner">
                                    <div class="row-fluid">
                                        <div class="span12">
                                                        <div class="cart_table_holder">
                                                            <table width="100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="image">Image</th>
                                                                        <th class="name">Product Name</th>
                                                                        <th class="model">Category</th>
                                                                        <th class="model">Recipient</th>
                                                                        <th class="model" style="width: 200px !important;">Note</th>
                                                                        <th class="quantity" style="width: 80px !important;">Quantity</th>
                                                                        <th class="price">Price</th>
                                                                        <th class="total">Total</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptCart" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>

                                                                                <td class="image">
                                                                                    <img src="<%# "/Products/S_"+Eval("ProductImage") %>" alt="<%#Eval("ProductName") %>" width="60px" /></td>
                                                                                <td class="name"><%#Eval("ProductName") %>
                                                                                    <div></div>
                                                                                </td>
                                                                                <td class="model"><%#Eval("CategoryName") %></td>
                                                                                <td class="recipient"><%#Eval("RecipientName") %></td>
                                                                                <td class="comments"><%#Eval("Comments") %>&nbsp;</td>
                                                                                <td class="quantity">
                                                                                    <%#Eval("Quantity") %></td>
                                                                                <td class="price">$<%#Eval("Price") %></td>
                                                                                <td class="total">$<%#Eval("ItemTotal") %></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <AlternatingItemTemplate>
                                                                            <tr class="odd">

                                                                                <td class="image">
                                                                                    <img src="<%# "/Products/S_"+Eval("ProductImage") %>" alt="<%#Eval("ProductName") %>" width="60px" /></td>
                                                                                <td class="name"><%#Eval("ProductName") %>
                                                                                    <div></div>
                                                                                </td>
                                                                                <td class="model"><%#Eval("CategoryName") %></td>
                                                                                <td class="recipient"><%#Eval("RecipientName") %></td>
                                                                                <td class="comments"><%#Eval("Comments") %></td>
                                                                                <td class="quantity">
                                                                                    <%#Eval("Quantity") %></td>
                                                                                <td class="price">$<%#Eval("Price") %></td>
                                                                                <td class="total">$<%#Eval("ItemTotal") %></td>
                                                                            </tr>
                                                                        </AlternatingItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                                <tfoot>
                                                                    <tr>
                                                                        <td colspan="5" style="text-align: right;">
                                                                            
                                                                               <b> Enter Promo Code:</b>
                                                                        </td>
                                                                        <td><asp:TextBox ID="txtPromotionCode" runat="server" Width="80px" MaxLength="50" CssClass="coupon" ClientIDMode="Static"></asp:TextBox></td>
                                                                        <td colspan="2"><input type="button" value="Apply Promo" onclick="return PromotionCode('txtPromotionCode');" name="apply_coupon" class="cbtn"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="price" colspan="7" style="text-align: right;"><b>Sub-Total:</b></td>
                                                                        <td class="total">$<asp:Label ID="txtPrice" runat="server" ClientIDMode="Static" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="price" colspan="7" style="text-align: right;"><b>Delivery Charges:</b></td>
                                                                        <td class="total">$<asp:Label ID="txtDeliveryCharges" runat="server" Text="" ClientIDMode="Static"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="price" colspan="7" style="text-align: right;"><b>Tax
                                <asp:Literal ID="txtTax" runat="server"></asp:Literal>%:</b></td>
                                                                        <td class="total">$<asp:Label ID="txtTaxAmount" runat="server" Text="" ClientIDMode="Static"></asp:Label></td>
                                                                    </tr>
                                                                    <tr id="linetip">
                                                                        <td class="price" colspan="7" style="text-align: right;"><b>Tip</b></td>
                                                                        <td class="total">$<asp:TextBox ID="txtTip" runat="server" Width="50px" MaxLength="4" ClientIDMode="Static" onchange='return LineTip(this);'></asp:TextBox>
                                                                            <asp:RangeValidator ID="rvTip" runat="server" ControlToValidate="txtTip" ErrorMessage="Invalid Value." Type="Double" ClientIDMode="Static" Display="Dynamic"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4"></td>
                                                                        <td colspan="4">
                                                                            <div class="messageBox"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="price" colspan="7" style="text-align: right;"><b>Total:</b></td>
                                                                        <td class="total">$<asp:Label ID="txtTotal" runat="server" Text="" ClientIDMode="Static"></asp:Label></td>
                                                                    </tr>
                                                                </tfoot>
                                                            </table>
                                                            <div class="buttons">
                                                                <div class="right">
                                                                    <a id="button-confirm-back" href="javascript:void(0);" onclick="ShowPanel('collapseThree','collapseFour');" class="btn-submit2">Back</a>

                                                                    <a class="btn-submit2 offset0" id="button-confirm" href="javascript:void(0);" onclick="buttonClick('OrderNow');"><span>Confirm Order</span></a>
                                                                </div>
                                                                <asp:Button ID="OrderNow" runat="server" CssClass="button hide" Text="Order Now" OnClick="OrderNow_Click" ClientIDMode="Static" />
                                                            </div>

                                                        </div>
                                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--Menu Page End-->
    </section>
    <!--Blog Page Section End-->

    <asp:HiddenField ID="hdCurrentAccordion" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdTotalChargesBeforeTax" runat="server" ClientIDMode="Static" />
</asp:Content>
