<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="OptionTypeList.aspx.cs" Inherits="admin_products_OptionTypeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdOptionTypes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOptionTypes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        

    </telerik:RadScriptBlock>
    <br />
    <table width="500px" style="margin: 0px auto; padding: 0px auto; text-align:left;">
        <tr>
            <td>
                <telerik:RadGrid ID="grdOptionTypes" runat="server" AutoGenerateColumns="false"
                    OnItemCommand="grdOptionTypes_ItemCommand" OnNeedDataSource="grdOptionTypes_NeedDataSource"
                    Width="440px" AllowPaging="true" PageSize="10">
                    <MasterTableView DataKeyNames="OptionTypeID" CommandItemDisplay="TopAndBottom" EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="OptionTypeName" HeaderText="Option Type Name" HeaderStyle-Width="200px"
                                ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>                           
                            <telerik:GridBoundColumn DataField="OptionDisplayText" HeaderText="Option Type Display Text" HeaderStyle-Width="200px"
                                ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn ItemStyle-Width="40px">
                            </telerik:GridEditCommandColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template" CaptionDataField="OptionTypeID" CaptionFormatString="Edit Option Type"
                            InsertCaption="Insert New Option Type">
                            <FormTemplate>
                                <table border="0" cellpadding="4" cellspacing="0" width="400px">
                                    <tbody>
                                    <tr>
                                    <td>
                                        <asp:Label ID="error" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                                    </td>
                                    </tr>
                                        <tr>
                                            <td style="width: 150px;">
                                                Option Name *
                                            </td>
                                            <td style="width: 250px;">
                                                <telerik:RadTextBox ID="txtOptionTypeName" runat="server" Text='<%#Eval("OptionTypeName") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>                                       
                                        <tr>
                                            <td>
                                                Option Display Text *
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtOptionTypeDispllayText" runat="server" Text='<%#Eval("OptionDisplayText") %>' >
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Modal="false" Width="400px"></PopUpSettings>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
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

