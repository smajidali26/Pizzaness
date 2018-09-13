<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="PromotionCodeAdd.aspx.cs" Inherits="admin_Settings_PromotionCodeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <h3>
        <asp:Literal ID="ltHeading" runat="server" Text="Add Promotion Code"></asp:Literal></h3>
    <table width="800px" style="text-align:left;">
        <tbody>
            <tr>
                <td style="width: 200px;">Promotion Name *</td>
                <td style="width: 600px;">
                    <asp:TextBox ID="txtPromotionName" runat="server" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPromotionName" runat="server" ControlToValidate="txtPromotionName" ClientIDMode="Static" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Promotion Code *</td>
                <td>
                    <asp:TextBox ID="txtPromotionCode" runat="server" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPromotionCode" runat="server" ControlToValidate="txtPromotionCode" ClientIDMode="Static" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Promotion Type *</td>
                <td>
                    <asp:RadioButtonList ID="rblPromotionType" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"></asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfvPromotionType" runat="server" ControlToValidate="rblPromotionType" ClientIDMode="Static" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Promotion Start Date</td>
                <td>
                    <telerik:RadDateTimePicker ID="StartDate" runat="server"></telerik:RadDateTimePicker>
                </td>
            </tr>
            <tr>
                <td>Promotion End Date</td>
                <td>
                    <telerik:RadDateTimePicker ID="EndDate" runat="server"></telerik:RadDateTimePicker>
                </td>
            </tr>
            <tr>
                <td>Promotion Value *</td>
                <td>
                    <telerik:RadNumericTextBox ID="txtPromotionValue" runat="server" Type="Number" Width="40px">
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPromotionValue" ClientIDMode="Static" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="buttonSave" runat="server" Text=" Save " OnClick="buttonSave_Click" />
                    <asp:Button ID="buttonCancel" runat="server" Text=" Cancel " CausesValidation="false" OnClick="buttonCancel_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

