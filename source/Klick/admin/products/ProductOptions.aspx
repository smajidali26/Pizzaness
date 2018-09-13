<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="ProductOptions.aspx.cs" Inherits="admin_products_ProductOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProductOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProductOptions" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var popUp;
            function PopUpShowing(sender, eventArgs) {
                popUp = eventArgs.get_popUp();
                var gridWidth = sender.get_element().offsetWidth;
                var gridHeight = sender.get_element().offsetHeight;
                var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                popUp.style.top = ((gridHeight - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
            }
        </script>
    </telerik:RadCodeBlock>
    <br />
    <table width="500px" style="margin: 0px auto; padding: 0px auto;">
        <tr>
            <td>
                <telerik:RadGrid ID="grdProductOptions" runat="server" AutoGenerateColumns="false"
                    Width="550px" AllowPaging="true" PageSize="10" OnItemCommand="grdProductOptions_ItemCommand"
                    OnNeedDataSource="grdProductOptions_NeedDataSource" OnItemCreated="grdProductOptions_ItemCreated"
                    OnItemDataBound="grdProductOptions_ItemDataBound">
                    <MasterTableView DataKeyNames="OptionID" CommandItemDisplay="TopAndBottom"
                        EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" HeaderStyle-Width="200px"
                                ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsMultiSelect" HeaderText="Is Multi-Select"
                                HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="IsParentOption" HeaderText="Is Parent" HeaderStyle-Width="100px"
                                ItemStyle-Width="100px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridEditCommandColumn>
                            </telerik:GridEditCommandColumn>
                        </Columns>
                        <NestedViewSettings>
                            <ParentTableRelation>
                                <telerik:GridRelationFields DetailKeyField="OptionID" MasterKeyField="OptionID" />
                            </ParentTableRelation>
                        </NestedViewSettings>
                        <NestedViewTemplate>
                            <fieldset>
                                <legend>Child Product Options</legend>
                                <telerik:RadGrid ID="grdChildProductOptions" runat="server" Width="200px" AutoGenerateColumns="false">
                                    <MasterTableView DataKeyNames="ProductOptionID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" HeaderStyle-Width="200px"
                                                ItemStyle-Width="200px">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid></fieldset>
                        </NestedViewTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnPopUpShowing="PopUpShowing" />
                        <Selecting AllowRowSelect="true" />                        
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
