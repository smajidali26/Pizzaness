<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Orders</h2>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" />
                    <telerik:AjaxUpdatedControl ControlID="txtError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProductCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            function openRadWindow(OrderID) {


                var oWnd = radopen("/Admin/OrderDetail.aspx?OID=" + OrderID, "OrderDetail");
                oWnd.setSize(800, 500);
                oWnd.show();
            }
            function refresh() {
                window.location = "Default.aspx";
            }
            function OnClientshow(sender, eventArgs) {

            }
        </script>
    </telerik:RadCodeBlock>
    <br />
    <table width="980px" border="0" cellpadding="4" cellspacing="0" style="margin: 0px auto; padding: 0px auto;">
        <tbody>
            <tr>
                <td style="width: 150px;">Order Status: 
                    
                </td>
                <td style="width: 570px;">
                    <telerik:RadComboBox ID="ddlOrderStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="grdOrder" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NumericPages"
                        AllowPaging="true" PageSize="10" OnNeedDataSource="grdOrder_NeedDataSource" OnSelectedIndexChanged="grdOrder_SelectedIndexChanged">
                        <MasterTableView DataKeyNames="OrderID" ItemStyle-VerticalAlign="Top" AllowCustomPaging="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" HeaderStyle-Width="150px"
                                    ItemStyle-Width="150px" DataFormatString="{0:dd-MM-yyyy HH:mm}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name" HeaderStyle-Width="100px"
                                    ItemStyle-Width="100px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CustomerEmail" HeaderText="Customer Email" HeaderStyle-Width="150px"
                                    AllowSorting="false" ItemStyle-Width="150px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CustomerTelephone" HeaderText="Telephone" HeaderStyle-Width="80px"
                                    AllowSorting="false" ItemStyle-Width="80px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CustomerMobile" HeaderText="Mobile" HeaderStyle-Width="80px"
                                    AllowSorting="false" ItemStyle-Width="80px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OrderTotal" HeaderText="Order Total" HeaderStyle-Width="80px"
                                    AllowSorting="false" ItemStyle-Width="80px" DataType="System.Double" DataFormatString="{0:C}">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn ItemStyle-Width="80px" HeaderStyle-Width="80px" HeaderText="Delivery Method">
                                    <ItemTemplate>
                                        <%# ((BusinessEntities.OrderType)Eval("OrderTypeID")).ToString()%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="IsPaid" HeaderText="Paid" HeaderStyle-Width="50px"
                                    ItemStyle-Width="50px">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridTemplateColumn ItemStyle-Width="80px" HeaderStyle-Width="80px" HeaderText="Order Status">
                                    <ItemTemplate>
                                        <%# ((BusinessEntities.OrderStatus)Eval("OrderStatusID")).ToString()%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ItemStyle-Width="80px" HeaderStyle-Width="80px" HeaderText="Payment">
                                    <ItemTemplate>
                                        <%# ((BusinessEntities.PaymentType)Eval("PaymentMethod")).ToString()%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <a href="#" onclick="openRadWindow('<%# Eval("OrderID") %>'); return false;">View Detail</a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                        <Windows>
                            <telerik:RadWindow ID="OrderDetail" runat="server" Modal="true" Behaviors="Close,Move"
                                Width="800px" Height="500px" InitialBehaviors="Close,Move" OnClientShow="OnClientshow" OnClientClose="refresh">
                            </telerik:RadWindow>
                        </Windows>
                    </telerik:RadWindowManager>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="980px" border="0">
        <tbody>
            <tr>
                <td>
                    <fieldset>
                        <legend>Filter</legend>
                        <table width="800px">
                            <tbody>
                                <tr>
                                    <td style="width: 100px">From:</td>
                                    <td style="width: 250px">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepickerstart datepicker_normal" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">To:</td>
                                    <td style="width: 250px">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="datepickerend datepicker_normal" ClientIDMode="Static"></asp:TextBox>

                                    </td>
                                    <td>
                                        <input type="button" value=" Generate Graph " onclick="CustomChart();" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>

                </td>
            </tr>
            <tr>
                <td id="weekchart"></td>
            </tr>
        </tbody>
    </table>
</asp:Content>

