<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyUserControl.ascx.cs" Inherits="MyUserControl" ClassName="MyUserControl" %>
<div class="multiproductmain">
    <div class="productheader floatleft">
        <asp:Label ID="txtProductName" runat="server" Text=""></asp:Label>
    </div>

    <div class="productdetail floatleft">
        <div id="imgdiv" class="floatleft">
            <asp:Image ID="txtImage" runat="server" />
        </div>
        <div id="detaildiv" class="floatleft">
            <div class="productdescription floatleft">
                <div id="left" class="floatleft">
                    Description
                </div>
                <div id="right" class="floatleft">
                    <asp:Label ID="txtDescription" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="optionDiv" ID="IsCustomizablePanel" Visible="False" runat="server">
                <div class="left">
                    Option
                </div>
                <div class="right">
                    <asp:DropDownList ID="ddlDealOptions" AutoPostBack="True" OnSelectedIndexChanged="ddlDealOptions_OnSelectedIndexChanged"
                         runat="server"/>
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
        <asp:Label ID="OptionHeading" runat="server" Text='Options' CssClass="multiproduct_optionheading" Visible="false"></asp:Label>
        <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
            <ItemTemplate>
                <table class="optionstable" border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td style="font-weight: bold;">
                                <asp:Label ID="txtOptionName" runat="server" Text='<%# Eval("OptionTypeName") %>'></asp:Label>
                                <asp:HiddenField ID="hdOptionTypeId" runat="server" Value='<%#Eval("OptionTypeID") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxList" runat="server" DataTextField="OptionName" DataValueField="OptionID"
                                    OnSelectedIndexChanged="chklist_SelectedIndexChanged" AutoPostBack="true">
                                </asp:CheckBoxList>
                                <asp:RadioButtonList ID="RadioButtonList" runat="server" DataTextField="OptionName" DataValueField="OptionID"
                                    OnSelectedIndexChanged="radiolist_SelectedIndexChanged" AutoPostBack="true">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvOptionSelect" runat="server" ErrorMessage="*" ControlToValidate="RadioButtonList" SetFocusOnError="true" Visible="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="productadonsmaindiv floatleft">

        <asp:Repeater ID="rptAdonsType" runat="server" OnItemDataBound="rptAdonsType_ItemDataBound">
            <ItemTemplate>
                <fieldset style="width: 320px; border: 0px solid; float: left; margin-left: 0px; margin-right: 0px; padding-left: 0px; padding-right: 0px;">
                    <table border="0" cellpadding="0" cellspacing="0" width="320px" class="floatright">
                        <tbody>
                            <tr>
                                <td colspan="5" style="font-weight: bold; padding-left: 10px;">
                                    <asp:Label ID="txtAdonTypeName" runat="server" Text='<%# Eval("AdOnTypeName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdAdonTypeId" runat="server" Value='<%#Eval("AdonTypeID") %>' />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="AdonHeading" runat="server" Visible="false">
                        
                    </asp:Panel>
                    <asp:Panel ID="NoneFullHeading" runat="server" Visible="false">
                        <table border="0" cellpadding="2" cellspacing="0" width="320px" class="floatright">
                            <tbody>
                                <tr>
                                    <td style="width: 130px;"></td>
                                    <td style="width: 40px;">None
                                    </td>
                                    <td style="width: 40px;">Full
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                    <asp:Repeater ID="rptAdons" runat="server" OnItemDataBound="rptAdons_ItemDataBound">
                        <ItemTemplate>
                            <div class="ProductAdonMain floatleft">
                                <div class="productAdonName floatleft">
                                    <asp:Label ID="txtAdonName" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdAdonId" runat="server" Value='<%#Eval("AdOnID") %>' />
                                </div>
                                <div class="floatleft productAdonOptions">
                                    <span style="display:none;"><%# Eval("AdOnID") %></span>
                                                <asp:ImageButton ID="None" runat="server" ImageUrl="~/image/None-Selected.png" CssClass="floatleft sizeselection" CommandArgument="0" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="None" CausesValidation="false" OnClientClick='SingleProductToppingSelection(0 , this);' />
                                                <asp:ImageButton ID="First" runat="server" ImageUrl="~/image/FirstHalf_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="2" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="First half" CausesValidation="false" OnClientClick="SingleProductToppingSelection(2,this);"  />
                                                <asp:ImageButton ID="Full" runat="server" ImageUrl="~/image/Full_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="1" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="Full" CausesValidation="false" OnClientClick="SingleProductToppingSelection(1,this);"  />
                                                <asp:ImageButton ID="Second" runat="server" ImageUrl="~/image/2ndHalf_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="3" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="Second half" CausesValidation="false" OnClientClick="SingleProductToppingSelection(3,this);"  />
                                                <asp:ImageButton ID="Double" runat="server" ImageUrl="~/image/Double_NotSelected.png" CssClass="floatleft sizeselection" CommandArgument="4" CommandName="1" OnClick="PizzaHalfSelection_Click" ToolTip="Repeat" CausesValidation="false" OnClientClick="SingleProductToppingSelection(4,this);"  />
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

                                    <asp:CheckBox ID="txtDouble" runat="server" CssClass="floatright" OnCheckedChanged="txtDouble_CheckedChanged" AutoPostBack="true" Visible="false" />
                                    <asp:CheckBox ID="AdonCheckBox" runat="server" CssClass="floatleft" Visible="false" />
                                    <asp:RadioButton ID="AdonRadioButton" runat="server" CssClass="floatleft" Visible="false" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </fieldset>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
