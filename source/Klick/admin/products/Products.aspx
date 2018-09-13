<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="admin_products_Products" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProducts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProducts" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtError" />
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
    <br />

        <table width="760px" border="0" cellpadding="4" cellspacing="0" style="margin: 0px auto; padding: 0px auto;">
            <tbody>
                <tr>
                    <td style="width: 150px;">Select Category: 
                    
                    </td>
                    <td style="width: 570px;">
                        <telerik:RadComboBox ID="txtProductCategory" runat="server"
                            DataValueField="CategoryID" DataTextField="Name" AutoPostBack="true"
                            OnSelectedIndexChanged="txtProductCategory_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="grdProducts" runat="server" AutoGenerateColumns="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                            OnItemCommand="grdProducts_ItemCommand" OnItemCreated="grdProducts_ItemCreated" AllowPaging="true" PageSize="10"
                            Width="680px" OnItemDataBound="grdProducts_ItemDataBound" OnDetailTableDataBind="grdProducts_DetailTableDataBind">
                            <MasterTableView DataKeyNames="ProductID" CommandItemDisplay="Top" EditMode="PopUp"
                                CommandItemSettings-AddNewRecordText="Add New Product" ItemStyle-VerticalAlign="Top">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Name" HeaderText="Product Name" HeaderStyle-Width="200px"
                                        ItemStyle-Width="200px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="Enabled" HeaderStyle-Width="50px"
                                        AllowSorting="false" ItemStyle-Width="50px">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsSpecial" HeaderText="Is Special" HeaderStyle-Width="50px"
                                        AllowSorting="false" ItemStyle-Width="50px">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridBoundColumn DataField="Description" HeaderText="Description" HeaderStyle-Width="200px"
                                        AllowSorting="false" ItemStyle-Width="200px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DisplayOrder" HeaderText="Display Order" HeaderStyle-Width="100px"
                                        AllowSorting="false" ItemStyle-Width="100px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="130px" HeaderStyle-Width="130px" HeaderText="Product Image">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImagePath") +"S_"+ Eval("Image") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridEditCommandColumn ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>

</asp:Content>
