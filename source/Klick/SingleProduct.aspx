<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SingleProduct.aspx.cs" Inherits="SingleProduct" EnableEventValidation="false" ViewStateEncryptionMode="Never" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/App_Themes/product.css?v=0.7" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="/js/script.js?v=0.7" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript" language="javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                function BeginRequestHandler(sender, args) {
                    $j('input[type="image"]').each(function () {
                        $j(this).attr("disabled", true);
                    });

                }
                function EndRequestHandler(sender, args) {
                    $j('input[type="image"]').each(function () {
                        $j(this).removeAttr("disabled");
                    });
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="txtOptions">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtOptions" />
                        <telerik:AjaxUpdatedControl ControlID="txtNetPrice" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="txtAdonsList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtAdonsList" />
                        <telerik:AjaxUpdatedControl ControlID="txtNetPrice" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="singleproductmain">

            <div class="productheader floatleft">
                <asp:Label ID="txtProductName" runat="server" Text=""></asp:Label>
            </div>

            <div class="productdetail floatleft">
                <div id="imgdiv" class="floatleft">
                    <asp:Image ID="txtImage" runat="server" Width="80px" />
                </div>
                <div id="detaildiv" class="floatleft">
                    <div class="productdescription floatleft">
                        <div id="left" class="floatleft">
                            Description
                        </div>
                        <div id="right" class="floatleft">
                            <asp:Label ID="txtDescription" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </div>
                    </div>

                    <div runat="server" id="IsThinCrustPanel" class="optionDiv" visible="False">
                        <div class="left">
                            Pizza Crust
                        </div>
                        <div class="right">
                            <asp:RadioButtonList runat="server" ID="rblThinCrust" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <Items>
                                    <asp:ListItem Text="Regular" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Thin Crust" Value="1"></asp:ListItem>
                                </Items>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvThinCrust" runat="server" ErrorMessage="*" ControlToValidate="rblThinCrust" SetFocusOnError="true" Visible="false"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="floatleft productrecipient">
                        <div id="r-left" class="floatleft">
                            Recipient Name
                        </div>
                        <div id="r-right" class="floatleft">
                            <telerik:RadTextBox ID="txtRecipientName" runat="server" Width="150px" MaxLength="100">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="floatleft productinstruction">
                        <div id="i-left" class="floatleft">
                            Special Instruction (Option)
                        </div>
                        <div id="i-right" class="floatleft">
                            <telerik:RadTextBox ID="txtInstruction" runat="server" Width="200px" TextMode="MultiLine"
                                Rows="2" Columns="50" MaxLength="200">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="floatleft productoptionmaindiv">
                <telerik:RadListView ID="txtOptions" runat="server" OnItemDataBound="txtOptions_ItemDataBound"
                    OnNeedDataSource="txtOptions_NeedDataSource" ItemPlaceholderID="OptionsContrainer" DataKeyNames="ProductsOptionTypeId">
                    <LayoutTemplate>
                        <%# ((txtOptions.DataSource != null) ? "<span id=\"optionid\">Options</span>" : "")
                        %>
                        <asp:PlaceHolder ID="OptionsContrainer" runat="server"></asp:PlaceHolder>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <table class="optionstable" border="0" cellpadding="3" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="txtOptionName" runat="server" Text='<%# Eval("OptionType.OptionTypeName") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="CheckBoxList" runat="server" DataTextField="OptionName" DataValueField="ProductOptionsInProductID"
                                            OnSelectedIndexChanged="chklist_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:CheckBoxList>

                                        <asp:RadioButtonList ID="RadioButtonList" runat="server" DataTextField="OptionName"
                                            DataValueField="ProductOptionsInProductID" OnSelectedIndexChanged="radiolist_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvOptionSelect" runat="server" ErrorMessage="Please select" ControlToValidate="RadioButtonList" SetFocusOnError="true" Visible="false"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>

            <div class="productadonsmaindiv floatleft">
                <telerik:RadListView ID="txtAdonsList" runat="server" ItemPlaceholderID="AdonsMainContrainer"
                    Width="600px" OnItemDataBound="txtAdonsList_ItemDataBound" OnNeedDataSource="txtAdonsList_NeedDataSource" DataKeyNames="ProductsAdOnTypeId">
                    <LayoutTemplate>

                        <fieldset style="width: 600px; border: 0px solid; float: left;">
                            <%# ((txtAdonsList.DataSource != null) ? "<div id=\"HeadingAdons\">Toppings</div>" : "") 
                            %>


                            <asp:PlaceHolder ID="AdonsMainContrainer" runat="server"></asp:PlaceHolder>
                        </fieldset>
                    </LayoutTemplate>
                    <EmptyItemTemplate>
                        No Topping Available.
                    </EmptyItemTemplate>
                    <ItemTemplate>
                        <fieldset style="width: 300px; border: 0px solid; float: left; margin-left: 0px; margin-right: 0px; padding-left: 0px; padding-right: 0px;">
                            <table border="0" cellpadding="2" cellspacing="0" width="300px" class="floatright">
                                <tbody>
                                    <tr>
                                        <td colspan="5" style="font-weight: bold; padding-left: 5px">
                                            <asp:Label ID="txtAdonTypeName" runat="server" Text='<%# Eval("AdonType.AdOnTypeName") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Panel ID="AdonHeading" runat="server" Visible="false">
                            </asp:Panel>
                            <asp:Panel ID="NoneFullHeading" runat="server" Visible="false">
                                <table border="0" cellpadding="2" cellspacing="0" width="300px" class="floatright">
                                    <tbody>
                                        <tr>
                                            <td style="width: 120px;"></td>
                                            <td style="width: 40px;">None
                                            </td>
                                            <td style="width: 40px;">Full
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <telerik:RadListView ID="txtAdons" runat="server" ItemPlaceholderID="AdonsContrainer"
                                Width="300px" OnItemDataBound="Adons_ItemDataBound" DataKeyNames="ProductAdOnID">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="AdonsContrainer" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="ProductAdonMain floatleft">
                                        <div class="productAdonName floatleft">
                                            <asp:Label ID="txtAdonName" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="floatleft productAdonOptions">
                                            <span style="display: none;"><%# Eval("ProductAdOnID") %></span>
                                            <asp:ImageButton ID="None" runat="server" ImageUrl="~/image/None-Selected.png" CssClass="floatleft sizeselection" CommandArgument="0" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="None" CausesValidation="false" OnClientClick='SingleProductToppingSelection(0 , this);' />
                                            <asp:ImageButton ID="First" runat="server" ImageUrl="~/image/FirstHalf_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="2" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="First half" CausesValidation="false" OnClientClick="SingleProductToppingSelection(2,this);" />
                                            <asp:ImageButton ID="Full" runat="server" ImageUrl="~/image/Full_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="1" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="Full" CausesValidation="false" OnClientClick="SingleProductToppingSelection(1,this);" />
                                            <asp:ImageButton ID="Second" runat="server" ImageUrl="~/image/2ndHalf_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="3" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="Second half" CausesValidation="false" OnClientClick="SingleProductToppingSelection(3,this);" />
                                            <asp:ImageButton ID="Double" runat="server" ImageUrl="~/image/Double_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="4" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="Repeat" CausesValidation="false" OnClientClick="SingleProductToppingSelection(4,this);" />
                                            <asp:HiddenField ID="SelectedSize" runat="server" Value="0" ClientIDMode="Static" />
                                            <asp:HiddenField ID="IsDouble" runat="server" Value="0" ClientIDMode="Static" />
                                            <asp:RadioButtonList ID="NoneFullRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                                Width="80px" OnSelectedIndexChanged="NoneFullRadioButtonList_SelectedIndexChanged" AutoPostBack="true"
                                                CssClass="floatleft" CellPadding="0" CellSpacing="0" Visible="false">
                                                <asp:ListItem Text="" Value="0">                     
                                                </asp:ListItem>
                                                <asp:ListItem Text="" Value="1">                     
                                                </asp:ListItem>
                                            </asp:RadioButtonList>

                                            <asp:CheckBox ID="AdonCheckBox" runat="server" CssClass="floatleft" Visible="false" />
                                            <asp:RadioButton ID="AdonRadioButton" runat="server" CssClass="floatleft" Visible="false" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </fieldset>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>
        </div>
        <div class="productpricediv floatleft">
            <div id="price-left" class="floatleft">
                <span class="floatleft">Quantity : </span>
                <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Value="1" Width="40px"
                    Type="Number" DataType="System.Int16" NumberFormat-DecimalDigits="0"
                    MinValue="1" MaxValue="100" CssClass="floatleft">
                </telerik:RadNumericTextBox>
            </div>
            <div id="price-center" class="floatleft">
                <span class="floatleft">Price: </span>
                <asp:Label ID="txtNetPrice" runat="server" Text="" CssClass="floatleft"></asp:Label>
            </div>
            <div id="price-right" class="floatleft">
                <asp:Button ID="AddToCartButton" runat="server" Text=" Add to Order "
                    OnClick="AddToCartButton_Click" CssClass="button" />
                <asp:Label ID="txtStoreClosed" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
        <asp:HiddenField ID="Id" runat="server" Value="0" ClientIDMode="Static" />
    </form>
</body>
</html>
