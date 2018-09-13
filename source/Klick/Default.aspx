<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/controls/Menu.ascx" TagName="Menu" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            function openRadWindow(ProductID, BranchProductID, IsSpecial) {
                if (IsSpecial == "True") {
                    var oWnd = radopen("MultiProduct.aspx?PID=" + ProductID + "&BPID=" + BranchProductID, "MultiProduct");
                    oWnd.setSize(710, 550);
                }
                else {

                    var oWnd = radopen("SingleProduct.aspx?PID=" + ProductID + "&BPID=" + BranchProductID, "Product");
                    oWnd.setSize(650, 500);
                }
                oWnd.show();
            }
            function OnClientshow(sender, eventArgs) {

            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Banner Start-->
    <section id="banner" style="display: table;">
        <div class="caption span3">
            <div class="row-fluid">
                <div class=" banner-left">

                    <div class="caption-left">
                        <div class="deal-tag"><strong class="title">Menu</strong> </div>
                        <uc:Menu ID="Menu" runat="server" IsHomePageMenu="True" />
                    </div>
                </div>
            </div>
        </div>
        <div class="span9 offset1">
            <ul class="bxslider">
                <asp:Repeater ID="rptSlider" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src="/Sliders/<%#Eval("ImagePath") %>" title="<%#Eval("ImageName") %>" style="width: 1210px; height: 430px;" alt="<%#Eval("ImageName") %>" />
                        </li>

                    </ItemTemplate>
                </asp:Repeater>

            </ul>
        </div>
    </section>
    <!--Banner End-->




    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="Product" runat="server" Modal="true" Behaviors="Close,Move"
                Width="650px" Height="500px" InitialBehaviors="Close,Move" OnClientShow="OnClientshow">
            </telerik:RadWindow>
            <telerik:RadWindow ID="MultiProduct" runat="server" Modal="true" Behaviors="Close,Move"
                Width="710px" Height="550px" OnClientShow="OnClientshow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


    <section class="event-section">
        <div class="row-fluid">
            <div class="span5">
                <div class="row-fluid">
                    <div class="span6">
                        <div class="calendar-left">
                            <strong class="day">E-Gift Card</strong>
                            <a href="/GiftCards.aspx">
                                <img src="image/egift.png" />
                            </a>
                            <div>&nbsp;</div>
                        </div>
                    </div>
                    <div class="span6 margin-non">
                        <div class="food-critic-box">
                            <div class="food-critic-header">
                                <h4>Use Promo Code</h4>
                            </div>
                            <div class="empty-border"></div>
                            <div class="review-box">
                                
                                <blockquote class="review-blockquote">
                                    <p>
                                        Enter promo code to get discount
                                    </p>
                                </blockquote>
                                <asp:Label runat="server" ID="lblPromo" Text="Promo Code:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtPromo" Width="180px" MaxLength="50" ClientIDMode="Static" ValidationGroup="vgPromo"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="revPromoCode" ControlToValidate="txtPromo" Text="Please enter valid promo code"
                                                ValidationExpression="[^<]*" ToolTip="Please enter valid promo code" ErrorMessage="Please enter valid promo code" 
                                     ValidationGroup="vgPromo"/>
                                    
                                <asp:Button runat="server" ID="btnPromoCode" OnClick="btnPromoCode_OnClick" Text="Use Promo" CausesValidation="True" 
                                            CssClass="btn btn-review"  ValidationGroup="vgPromo" />
                                <asp:Label runat="server" ID="lblMessage" CssClass="messageBox"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span4">
                <div class="events-box">
                    <div class="events-box-head">
                        <div class="bg-opacity-3">
                            <h5>Specials</h5>
                        </div>
                    </div>
                    <div id="content_1" class="content scroll mCustomScrollbar _mCS_5">
                        <div class="mCustomScrollBox mCS-light" id="mCSB_5" style="position: relative; height: 100%; overflow: hidden; max-width: 100%;">
                            <div class="mCSB_container" style="position: relative; top: 0px;">
                                <ul>
                                    <asp:Repeater ID="rptSpecial" runat="server">
                                        <ItemTemplate>
                                            <li style="height: 107px;">
                                                <a style="height: 107px;" href="javascript:void(0);" onclick="openRadWindow('<%# Eval("ProductID") %>','<%# Eval("BranchProductID") %>','<%# Eval("IsSpecial") %>'); return false;">
                                                    <img src="<%# "/Products/L_"+Eval("Image") %>" alt="<%# Eval("Name")%>" width="140px" height="140px" />
                                                    <div class="caption">
                                                        <div class="text">
                                                            <strong class="date"><%#Eval("DefaultBranchProductPrice","{0:C}") %></strong>
                                                            <p><%# Eval("Name")%></p>
                                                        </div>
                                                    </div>
                                                </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <li></li>
                                </ul>
                            </div>
                            <div class="mCSB_scrollTools" style="position: absolute; display: block;">
                                <div class="mCSB_draggerContainer">
                                    <div class="mCSB_dragger" style="position: absolute; height: 120px; top: 0px;" oncontextmenu="return false;">
                                        <div class="mCSB_dragger_bar" style="position: relative; line-height: 120px;"></div>
                                    </div>
                                    <div class="mCSB_draggerRail"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span3">

                <div class="food-critic-box">
                    <div class="food-critic-header">
                        <h3>Rewards</h3>
                        <strong class="description">Card Inquiry</strong>
                    </div>
                    <div class="empty-border"></div>
                    <div class="review-box">

                        <blockquote class="review-blockquote">
                            <p>Enter value card number to check your balance.</p>
                        </blockquote>

                        <asp:Label runat="server" ID="lblCardNo" Text="Card Number: "></asp:Label>
                        <asp:TextBox ID="txtCardNumber" MaxLength="20" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="revCardNumber" ControlToValidate="txtCardNumber" ValidationGroup="vgValuetec"
                            ValidationExpression="[0-9]{15,20}" ErrorMessage="Invalid value card number" ToolTip="Invalid value card number"
                            Text="Invalid value card number"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCardNumber" ControlToValidate="txtCardNumber" ValidationGroup="vgValuetec"
                            ErrorMessage="Please enter card number" ToolTip="Please enter card number" Display="Dynamic" Text="Please enter card number">
                        </asp:RequiredFieldValidator>
                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_OnClick" Text="Check Balance" CssClass="btn btn-review" ValidationGroup="vgValuetec" runat="server" />
                        <asp:Literal runat="server" ID="ltInfoMessage" Text=""></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
