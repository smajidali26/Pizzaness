<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="PromotionCodeList.aspx.cs" Inherits="admin_Settings_PromotionCodeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPromotionCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPromotionCode" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <table width="800px" style="text-align: left;">
        <tbody>
            <tr>
                <td>
                    <fieldset>
                        <legend>Search</legend>
                        <table width="800px">
                            <tr>
                                <td style="width: 150px">Promotion Name</td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txtSearchPromotionName" runat="server" Width="150px"></asp:TextBox></td>
                                <td style="width: 150px">Promotion Code</td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txtSearchPromotionCode" runat="server" Width="150px"></asp:TextBox></td>
                                <td style="width: 100px">
                                    <asp:Button ID="btnSearch" runat="server" Text=" Search " OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text=" Clear " OnClick="btnClear_Click" />
                                </td>
                            </tr>

                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="grdPromotionCode" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdPromotionCode_NeedDataSource"
                        OnItemCommand="grdPromotionCode_ItemCommand" AllowPaging="true" AllowCustomPaging="true" PageSize="10">
                        <MasterTableView DataKeyNames="PromotionCodeId" CommandItemDisplay="Top" ItemStyle-VerticalAlign="Top">
                            <Columns>
                                <telerik:GridBoundColumn DataField="PromotionName" HeaderText="Promotion Name" HeaderStyle-Width="200px"
                                    ItemStyle-Width="200px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PromotionCode" HeaderText="Promotion Code" HeaderStyle-Width="100px"
                                    ItemStyle-Width="100px">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn ItemStyle-Width="130px" HeaderStyle-Width="130px" HeaderText="Type of Promotion">
                                    <ItemTemplate>
                                        <%# Eval("TypeOfPromotion").ToString() %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" HeaderStyle-Width="100px"
                                    ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy HH:mm}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" HeaderStyle-Width="100px"
                                    ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy HH:mm}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PromotionValue" HeaderText="Promotion Value" HeaderStyle-Width="100px"
                                    ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" DataType="System.Double">
                                </telerik:GridBoundColumn>
                                <telerik:GridEditCommandColumn ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" ConfirmText="Are you sure you want to delete?" ConfirmTitle="Delete Promotion Code." />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

