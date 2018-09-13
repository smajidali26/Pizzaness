<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true" CodeFile="ProductPage.aspx.cs" Inherits="ProductPage" %>

<%@ Register Src="~/controls/Menu.ascx" TagName="Menu" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            function refresh() {
                window.location = "Menu.aspx";
            }
            function OnClientshow(sender, eventArgs) {

            }
        </script>
    </telerik:RadCodeBlock>
    <section class="blog-page-section">
        <div class="span12">
            <div class="heading">
                <h1>
                    <asp:Literal ID="ltCategoryName" runat="server"></asp:Literal></h1>
            </div>
        </div>
        <div class="span12">
            <div class="caption span3">
                <div class="row-fluid">
                    <div class=" banner-left">

                        <div class="caption-left">
                            <div class="deal-tag"><strong class="title">Menu</strong> </div>
                            <uc:Menu ID="Menu" runat="server"/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span7 offset1">
                <asp:Repeater ID="rptMenu" runat="server">
                    <ItemTemplate>
                        <%#StartDiv(Container.ItemIndex) %>
                        <div class="span4">
                            <div class="span2_2">

                                <img src="<%# "/Products/L_"+Eval("Image") %>" width="180px" height="180px" alt="<%# Eval("Name")%>" />

                            </div>
                            <div class="span2">
                                <h5><%# Eval("Name")%></h5>
                                <div class="span2_2" style="display: table; min-height: 100px;font-size:12px !important;"><%# Eval("Description")%> </div>
                                <div class="span2_2" style="display: table;"><strong class="price float-left"><%#Eval("Price","{0:C}") %>&nbsp;</strong> <a href="javascript:void();" onclick="openRadWindow('<%# Eval("ProductID") %>','<%# Eval("BranchProductID") %>','<%# Eval("IsSpecial") %>'); return false;" class="btn-submit-2  float-left">Add to Cart</a> </div>

                            </div>
                        </div>
                        <%#EndDiv(Container.ItemIndex) %>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>



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
</asp:Content>

