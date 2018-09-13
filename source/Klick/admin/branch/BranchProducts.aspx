<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="BranchProducts.aspx.cs" Inherits="admin_branch_BranchProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProducts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProducts" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProductCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProducts" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            function openRadWindow( BranchProductID) {
                
                var oWnd = radopen("BranchProductDetail.aspx?id=" +BranchProductID, "ProductDetail");
                oWnd.setSize(650, 500);
                
                oWnd.show();
            }
            function refresh() {
                window.location = "Menu.aspx";
            }
            function OnClientshow(sender, eventArgs) {

            }
        </script>
    </telerik:RadCodeBlock>
    <br />
    <table width="900px" border="0" cellpadding="4" cellspacing="0" style="margin: 0px auto;
        padding: 0px auto; text-align:left !important;">
        <tbody>
            <tr>
                <td colspan="2"><h3>Products in Branch</h3></td>
            </tr>
             <tr>
                    <td style="width: 150px;">Select Category: 
                    
                    </td>
                    <td style="width: 750px;">
                        <telerik:RadComboBox ID="txtProductCategory" runat="server"
                            DataValueField="CategoryID" DataTextField="Name" AutoPostBack="true"
                            OnSelectedIndexChanged="txtProductCategory_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="grdProducts" runat="server" AutoGenerateColumns="false" 
                        Width="680px" onitemcreated="grdProducts_ItemCreated" 
                        onitemdatabound="grdProducts_ItemDataBound" OnItemCommand="grdProducts_ItemCommand"
                        onneeddatasource="grdProducts_NeedDataSource">
                        <MasterTableView DataKeyNames="BranchProductID" ItemStyle-VerticalAlign="Top" InsertItemDisplay="Top" CommandItemDisplay="Top">
                            <Columns>
                               
                                <telerik:GridTemplateColumn DataField="ProductID" HeaderText="Product Name" HeaderStyle-Width="200px"
                                    ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn DataField="BranchID" HeaderText="Branch Name" HeaderStyle-Width="200px"
                                    ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                </telerik:GridTemplateColumn>                                
                                <telerik:GridBoundColumn DataField="Price" HeaderText="Product Price" HeaderStyle-Width="100px"
                                    ItemStyle-Width="100px">
                                </telerik:GridBoundColumn>                                
                                <telerik:GridCheckBoxColumn DataField="Enable" HeaderText="Enabled" HeaderStyle-Width="50px"
                                    AllowSorting="false" ItemStyle-Width="50px">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="150px"
                                    ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <a href="/Admin/branch/AddProductInBranch.aspx?Id=<%# Eval("BranchProductID") %>">Edit</a>
                                    <a href="#" onclick="openRadWindow(<%# Eval("BranchProductID") %>);">View Detail</a>
                                </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName= "DeleteColumn" ConfirmText="Are you sure you want to delete?" ConfirmTitle="Delete product from branch." />
                            </Columns>                            
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </tbody>
    </table>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow ID="ProductDetail" runat="server" Modal="true" Behaviors="Close,Move"
                    Width="650px" Height="500px" InitialBehaviors="Close,Move" OnClientShow="OnClientshow">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
</asp:Content>

