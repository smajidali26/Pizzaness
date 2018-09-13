<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddAdonInBranchProduct.aspx.cs" Inherits="admin_branch_AddAdonInBranchProduct" ValidateRequest="false" EnableEventValidation="false" ViewStateEncryptionMode="Never" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    </telerik:RadScriptBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table width="700px" style="margin: 0px auto; padding: 0px auto;" border="0" cellpadding="4"
            cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Select Branch *
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbBranch" runat="server" DataTextField="Title" DataValueField="BranchID"
                        AutoPostBack="true" OnSelectedIndexChanged="txtCbBranch_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Select Category *
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbCategory" runat="server" DataTextField="Name"
                        DataValueField="CategoryID" AutoPostBack="True" MaxHeight="200px"
                        OnSelectedIndexChanged="txtCbCategory_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Select Product *
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbProduct" runat="server" DataTextField="Name"
                        DataValueField="ProductID" AutoPostBack="True"
                        OnSelectedIndexChanged="txtCbProduct_SelectedIndexChanged" MaxHeight="200px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr id="OptionTypeTr" runat="server">
                <td style="width: 200px;">
                    <asp:Label ID="Label1" runat="server" Text="Select Adon Type" EnableViewState="false"></asp:Label>
                    *               
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbAdonType" runat="server" DataTextField="AdOnTypeName"
                        DataValueField="AdOnTypeId" AutoPostBack="true"
                        OnSelectedIndexChanged="txtCbAdonType_SelectedIndexChanged" MaxHeight="100px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr id="Tr1" runat="server">
                <td style="width: 200px;">
                    <asp:Label ID="Label2" runat="server" Text="Select Adon Type" EnableViewState="false"></asp:Label>
                    *               
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbDisplayFormat" runat="server" MaxHeight="100px">
                        <Items>
                            <telerik:RadComboBoxItem Text="Radio Button List" Value="1" />
                            <telerik:RadComboBoxItem Text="Radio Button" Value="2" />
                            <telerik:RadComboBoxItem Text="Checkbox" Value="3" />
                            <telerik:RadComboBoxItem Text="None & Full Radio Button List" Value="4" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr id="AdonPricetr">
                <td style="width: 200px;">Default Adon Price 
                </td>
                <td style="width: 500px;">
                    <telerik:RadNumericTextBox ID="txtPrice" runat="server" Type="Currency" Width="40px">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 200px;" valign="top">Available Adons *
                </td>
                <td style="width: 500px;">
                    <telerik:RadGrid ID="grdAdons" runat="server" AutoGenerateColumns="false"
                        Width="500px" OnItemDataBound="grdAdons_ItemDataBound">
                        <MasterTableView DataKeyNames="AdOnID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="AdOnName" HeaderText="Adon Name" ItemStyle-Width="280px">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Check To Add" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtSelected" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Enable" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtEnabled" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Default Selected" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="txtCbDefault" runat="server" Width="100px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="None" Value="0" />
                                                <telerik:RadComboBoxItem Text="Full" Value="1" />
                                                <telerik:RadComboBoxItem Text="First Half" Value="2" />
                                                <telerik:RadComboBoxItem Text="Second Half" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="ButtonSave" runat="server" Text=" Save " SkinID="ButtonSave" OnClick="SaveBtn_Click" />
                    <asp:Button ID="ButtonCancel" runat="server" Text=" Cancel " SkinID="ButtonCancel" OnClick="CancelBtn_Click" />
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
