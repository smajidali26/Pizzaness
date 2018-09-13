<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddZipCodeInBranchDeliveryArea.aspx.cs" Inherits="admin_branch_AddZipCodeInBranchDeliveryArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<br />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 300px; margin: 0px auto;
        padding: 0px auto;">
        <tr>
            <td>
                Select Branch
            </td>
            <td>
                <telerik:RadComboBox ID="txtCbBranch" runat="server" DataTextField="Title" DataValueField="BranchID">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                New Zip Code
            </td>
            <td>
                <telerik:RadMaskedTextBox ID="txtZipCode" runat="server" Mask="#####" Width="40px">
                </telerik:RadMaskedTextBox>
            </td>
        </tr>
        <tr>
        <td colspan="2" align="center">
            <asp:ImageButton ID="subBtn" runat="server" 
                ImageUrl="~/images/submit-button.jpg" onclick="subBtn_Click" /> 
            <asp:ImageButton ID="cancelBtn" runat="server" 
                ImageUrl="~/images/cancel-button.png" onclick="cancelBtn_Click" />
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="2">
                <telerik:RadGrid ID="grdBranchDeliveryZipCode" runat="server" AutoGenerateColumns="false" Width="200px">
                    <MasterTableView DataKeyNames="BranchDeliveryID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ZipCode" HeaderText="Zip Code" ItemStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>        
    </table>
</asp:Content>
