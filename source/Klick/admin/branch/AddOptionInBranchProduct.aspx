<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddOptionInBranchProduct.aspx.cs" Inherits="admin_branch_AddOptionInBranchProduct" EnableEventValidation="true" %>

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

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {

                $("#ContentPlaceHolder1_txtYes").click(function () {
                    $("#OptionPricetr").show();

                });
                $("#ContentPlaceHolder1_txtNo").click(function () {
                    $("#OptionPricetr").hide();
                });

                if ($("#ContentPlaceHolder1_txtYes").is(":checked")) {
                    $("#OptionPricetr").show();
                }
            });

        </script>
    </telerik:RadScriptBlock>

    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table width="700px" style="margin: 0px auto; padding: 0px auto;" border="0" cellpadding="4"
            cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Select Branch *
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbBranch" runat="server" DataTextField="Title" DataValueField="BranchID"
                        AutoPostBack="true" OnSelectedIndexChanged="txtCbBranch_SelectedIndexChanged" MaxHeight="200px">
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
                        DataValueField="ProductID" AutoPostBack="True" MaxHeight="200px"
                        OnSelectedIndexChanged="txtCbProduct_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr id="OptionTypeTr" runat="server">
                <td style="width: 200px;">
                    <asp:Label ID="Label1" runat="server" Text="Select Option Type" EnableViewState="false"></asp:Label>
                    *
                </td>
                <td style="width: 500px;">
                    <telerik:RadComboBox ID="txtCbOptionType" runat="server" DataTextField="OptionTypeName" MaxHeight="200px"
                        DataValueField="OptionTypeID" AutoPostBack="true" OnSelectedIndexChanged="txtCbOptionType_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Will Price remain same for below options? *
                </td>
                <td style="width: 500px;">
                    <asp:RadioButton ID="txtYes" runat="server" Text="Yes" GroupName="SamePrice" AutoPostBack="True"
                        OnCheckedChanged="txtYes_CheckedChanged" />
                    <asp:RadioButton ID="txtNo" runat="server" Text="No" GroupName="SamePrice" AutoPostBack="True"
                        OnCheckedChanged="txtNo_CheckedChanged" />
                </td>
            </tr>
            <tr id="OptionPricetr">
                <td colspan="2">
                    <asp:Panel ID="OptionPrice" runat="server" Visible="false">
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 200px;">Option Price *</td>
                                <td style="width: 500px;">
                                    <telerik:RadNumericTextBox ID="txtPrice" runat="server" Type="Currency" Width="40px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Adon Price will vary for below options? *
                </td>
                <td style="width: 500px;">
                    <asp:RadioButton ID="txtAdonYes" runat="server" Text="Yes" GroupName="AdonPrice"
                        AutoPostBack="True" OnCheckedChanged="txtAdonYes_CheckedChanged" />
                    <asp:RadioButton ID="txtAdonNo" runat="server" Text="No" GroupName="AdonPrice" AutoPostBack="True"
                        OnCheckedChanged="txtAdonNo_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Are Below options displayed as Multiselect options to user? *
                </td>
                <td style="width: 500px;">
                    <asp:RadioButton ID="txtMultiYes" runat="server" Text="Yes" GroupName="Multi" />
                    <asp:RadioButton ID="txtMultiNo" runat="server" Text="No" GroupName="Multi" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px;">Will this Option Type effect on product price on selection of any below option? *
                </td>
                <td style="width: 500px;">
                    <asp:RadioButton ID="txtPriceChangeYes" runat="server" Text="Yes"
                        GroupName="ProductPrice" OnCheckedChanged="txtPriceChangeYes_CheckedChanged" AutoPostBack="true" />
                    <asp:RadioButton ID="txtPriceChangeNo" runat="server" Text="No" GroupName="ProductPrice" AutoPostBack="true" />
                </td>
            </tr>

            <tr>
                <td style="width: 200px;" valign="top">Available Options *
                </td>
                <td style="width: 500px;">
                    <telerik:RadGrid ID="grdOptions" runat="server" AutoGenerateColumns="false" Width="500px"
                        OnItemDataBound="grdOptions_ItemDataBound">
                        <MasterTableView DataKeyNames="OptionID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" ItemStyle-Width="230px">
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
                                <telerik:GridTemplateColumn HeaderText="Display Order" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtDisplayOrder" runat="server" Width="40px" MaxLength="2" Type="Number" DataType="System.Int16" NumberFormat-DecimalDigits="0">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Option Price" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtOptionPrice" runat="server" Width="40px" MaxLength="5" Type="Currency" DataType="System.Double" NumberFormat-DecimalDigits="2"
                                            Enabled="false">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Adon Price" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtAdonPrice" runat="server" Width="40px" MaxLength="5" Type="Currency" DataType="System.Double" NumberFormat-DecimalDigits="2"
                                            Enabled="false">
                                        </telerik:RadNumericTextBox>
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
