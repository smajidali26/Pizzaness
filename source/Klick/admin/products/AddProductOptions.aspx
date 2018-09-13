<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddProductOptions.aspx.cs" Inherits="admin_products_AddProductOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery-1.4.js" type="text/javascript"></script>
    <script>        

        function OnClick() {           
            
            if (document.getElementById('ContentPlaceHolder1_txtParent').checked == false) {
                $('#ContentPlaceHolder1_Options').hide("slow", function () { });
            }
            else {
                $('#ContentPlaceHolder1_Options').show("slow", function () { });
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <br />
    <table width="500px" border="0" cellpadding="4" cellspacing="0" style="margin: 0px auto;
        padding: 0px auto;">
        <tbody>
            <tr>
                <td style="width: 150px;">
                    Option Name *
                </td>
                <td style="width: 350px;">
                    <telerik:RadTextBox ID="txtName" runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">
                    Is Multi-Select *
                </td>
                <td style="width: 350px;">
                    <asp:CheckBox ID="txtIsMulti" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;" valign="top">
                    Is Parent?
                </td>
                <td style="width: 350px;">
                    <asp:CheckBox ID="txtParent" runat="server" />
                    <div id="Options" style="display: none;" runat="server">
                        <telerik:RadGrid ID="grdOptions" runat="server" Width="250px" 
                            AutoGenerateColumns="false" onitemdatabound="grdOptions_ItemDataBound">
                            <MasterTableView DataKeyNames="ProductOptionID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" HeaderStyle-Width="200px"
                                        ItemStyle-Width="200px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtSelected" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </td>
            </tr>
           
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="AddBtn" runat="server" Text="Add Option" OnClick="AddBtn_Click" />&nbsp;
                    <asp:Button ID="UpdateBtn" runat="server" Text="Update Option" Visible="false" 
                        onclick="UpdateBtn_Click" />
                    <asp:Button ID="CancelBtn" runat="server" Text="Cancel" OnClick="CancelBtn_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="txtError" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
