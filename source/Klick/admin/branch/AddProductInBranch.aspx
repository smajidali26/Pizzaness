<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddProductInBranch.aspx.cs" Inherits="admin_branch_AddProductInBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
      <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtBranch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtProduct" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <table border="0" cellpadding="4" cellspacing="0" width="920px">
        <tr>
            <td colspan="2">
            <br />
                <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:200px;">
                Select Branch :
            </td>
            <td style="width:720px;">
                <telerik:RadComboBox ID="txtBranch" runat="server" DataTextField="Title" 
                    DataValueField="BranchID" AutoPostBack="true" 
                    onselectedindexchanged="txtBranch_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Select Product :
            </td>
            <td>
                <telerik:RadComboBox ID="txtProduct" runat="server" DataTextField="Name" DataValueField="ProductID">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Product Price:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="50px" Type="Currency">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Is Active:
            </td>
            <td>
                <asp:CheckBox ID="txtActive" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
        <td>&nbsp;&nbsp;</td>
        <td >
          <asp:ImageButton ID="SaveBtn" runat="server" ImageUrl="~/images/submit-button.jpg"
                            AlternateText="Save Button" onclick="SaveBtn_Click" />
                       <asp:ImageButton ID="UpdateBtn" runat="server" ImageUrl="~/images/update-button.jpg"
                            AlternateText="Save Button" onclick="UpdateBtn_Click" Visible="false" />
                        <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="~/images/cancel-button.png"
                            AlternateText="Cancel Button" onclick="CancelBtn_Click"/>
        </td>
        </tr>
    </table>
</asp:Content>
