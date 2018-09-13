<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="admin_Order_OrderDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/App_Themes/product.css" rel="stylesheet" type="text/css" />
    <link href="~/App_Themes/template/Content/form.css" rel="stylesheet" />
    <script src="/js/jquery-1.4.js" type="text/javascript"></script>
    <script src="/js/script.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>

        <div>
            <h2>Order Detail</h2>
            <table style="width: 600px;">
                <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="txtMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdOrderDetail" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdOrderDetail_NeedDataSource"
                                OnItemCommand="grdOrderDetail_ItemCommand" OnItemDataBound="grdOrderDetail_ItemDataBound"
                                OnDetailTableDataBind="grdOrderDetail_DetailTableDataBind" Skin="Default">
                                <MasterTableView DataKeyNames="OrderDetailID" ItemStyle-VerticalAlign="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product Name" HeaderStyle-Width="200px"
                                            ItemStyle-Width="200px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Quantity" HeaderText="Quantity" HeaderStyle-Width="80px"
                                            ItemStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Price" HeaderText="Price" HeaderStyle-Width="80px"
                                            ItemStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RecipientName" HeaderText="Recipient Name" HeaderStyle-Width="100px"
                                            ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Comments" HeaderText="Comments" HeaderStyle-Width="150px"
                                            ItemStyle-Width="150px">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="OrderDetailOptionId" Name="OrderDetailOption" Caption="Order Option Detail">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="OderDetailId" MasterKeyField="OrderDetailID" />
                                            </ParentTableRelation>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ProductOptionName" HeaderText="Option Name" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="80px">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Price" HeaderText="Price" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="80px" DataFormatString="{0:C}">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <DetailTables>
                                            </DetailTables>
                                        </telerik:GridTableView>
                                        <telerik:GridTableView DataKeyNames="OrderDetailAdonId" Name="OrderDetailAdon" Caption="Order Topping Detail">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="OrderDetailId" MasterKeyField="OrderDetailID" />
                                            </ParentTableRelation>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="AdonName" HeaderText="Topping Name" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="80px">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn ItemStyle-Width="80px" HeaderStyle-Width="80px" HeaderText="Selected Option">
                                                    <ItemTemplate>
                                                        <%# ((BusinessEntities.SelectedOption)Eval("SelectedAdonOption")).ToString()%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridCheckBoxColumn DataField="IsDoubleSelected" HeaderText="Double" HeaderStyle-Width="50px"
                                                    ItemStyle-Width="50px">
                                                </telerik:GridCheckBoxColumn>
                                            </Columns>
                                            <DetailTables>
                                            </DetailTables>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                </MasterTableView>
                            </telerik:RadGrid>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Paid" runat="server" Text=" Paid " OnClick="Paid_Click" />
                            <asp:Button ID="Delivered" runat="server" Text=" Delivered " OnClick="Delivered_Click" />
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="Delete_Click"/>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
