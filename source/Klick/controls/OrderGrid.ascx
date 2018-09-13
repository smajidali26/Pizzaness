<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderGrid.ascx.cs" Inherits="controls_OrderGrid" %>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
</telerik:RadAjaxManager>
<telerik:RadGrid ID="txtOrderGrd" runat="server" AutoGenerateColumns="False" OnItemDataBound="txtOrderGrd_ItemDataBound"
    OnItemCommand="txtOrderGrd_ItemCommand" ClientIDMode="Static" Visible="false">
    <MasterTableView DataKeyNames="OrderDetailID">
        <NoRecordsTemplate>
            You currently have no items in your order.
        </NoRecordsTemplate>

        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
        <Columns>
            <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product Name" ItemStyle-Width="150px"
                HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                <ItemStyle Width="150px"></ItemStyle>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Quantity" HeaderText="Qty" ItemStyle-Width="30px"
                HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                <ItemStyle Width="30px"></ItemStyle>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Price" HeaderText="Price" ItemStyle-Width="30px"
                HeaderStyle-HorizontalAlign="Center" DataType="System.Double" DataFormatString="{0:G5}">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                <ItemStyle Width="40px"></ItemStyle>
            </telerik:GridBoundColumn>
            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName= "DeleteColumn" ConfirmTitle="Delete Item" ConfirmText="Are you sure you want to delete?" ImageUrl="~/images/delete.png"  />
           
        </Columns>
    </MasterTableView>
    <HeaderStyle Font-Size="11px" />

    <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default"
        Height="20px">
    </HeaderContextMenu>
</telerik:RadGrid>
