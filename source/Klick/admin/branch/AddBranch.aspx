<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddBranch.aspx.cs" Inherits="admin_branch_AddBranch" Debug="true" %>

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProductOption">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProductOption" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
              
                $("#ContentPlaceHolder1_txtYes").click(function () {
                    $("#ContentPlaceHolder1_DeliveryTaxtr").show();
                });
                $("#ContentPlaceHolder1_txtNo").click(function () {
                    $("#ContentPlaceHolder1_DeliveryTaxtr").hide();
                });
            });

        </script>
    </telerik:RadScriptBlock>
    <br />
    <div style="margin: 0px auto; padding: 0px auto; width: 510px;text-align:left; ">
        <table border="0" cellpadding="4" cellspacing="0" width="500px">
            <tbody>
                <tr>
                    <td style="width: 200px;">
                        Brach Name :
                    </td>
                    <td style="width: 300px;">
                        <telerik:RadTextBox ID="txtBranchName" runat="server" Width="230px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">
                        Address
                    </td>
                    <td style="width: 250px;">
                        <telerik:RadTextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="4"
                            Columns="30">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip Code
                    </td>
                    <td>
                        <telerik:RadMaskedTextBox ID="txtZipCode" runat="server" Mask="#####" Width="40px">
                        </telerik:RadMaskedTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        City
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtCity" runat="server">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        State
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtState" runat="server" MaxLength="2" Width="20px">
                        </telerik:RadTextBox>
                    </td>
                </tr>               
                <tr>
                    <td>
                        Phone
                    </td>
                    <td>
                        <telerik:RadMaskedTextBox ID="txtPhone" runat="server" Mask="(###)- ### -####" Width="90px">
                        </telerik:RadMaskedTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fax
                    </td>
                    <td>
                        <telerik:RadMaskedTextBox ID="txtFax" runat="server" Mask="(###)- ### -####" Width="90px">
                        </telerik:RadMaskedTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Branch Open Time
                    </td>
                    <td>
                        <telerik:RadTimePicker ID="txtOpenTime" runat="server" TimeView-Interval="00:15:00"
                            TimeView-Height="150px" TimeView-Width="400px" TimeView-Columns="4">
                        </telerik:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        Branch Close Time
                    </td>
                    <td>
                        <telerik:RadTimePicker ID="txtCloseTime" runat="server" TimeView-Interval="00:15:00"
                            TimeView-Height="150px" TimeView-Width="400px" TimeView-Columns="4">
                        </telerik:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tax Percentage
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPercentage" runat="server" Type="Percent" Width="40px">
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Branch Provide Delivery?
                    </td>
                    <td>
                        <asp:RadioButton ID="txtYes" Text="Yes" runat="server" GroupName="Delivery" /><asp:RadioButton
                            ID="txtNo" Text="No" GroupName="Delivery" runat="server" />
                    </td>
                </tr>
                <tr id="DeliveryTaxtr" style="display:none;" runat="server">
                    <td>
                        Delivery Tax
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtDeliveryTax" runat="server" Type="Currency" Width="40px">
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Is Enabled?
                    </td>
                    <td>
                        <asp:CheckBox ID="txtEnabled" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Is Close?
                    </td>
                    <td>
                        <asp:CheckBox ID="txtIsClose" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:ImageButton ID="SaveProductBtn" runat="server" ImageUrl="~/images/submit-button.jpg"
                            AlternateText="Save Button" OnClick="SaveProductBtn_Click" />                        
                        <asp:ImageButton ID="UpdateProductBtn" runat="server" ImageUrl="~/images/update-button.jpg"
                            AlternateText="Update Button" onclick="UpdateProductBtn_Click" />
                        <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="~/images/cancel-button.png"
                            AlternateText="Cancel Button" OnClick="CancelBtn_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="txtError" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
