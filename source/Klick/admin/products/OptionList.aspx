<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="OptionList.aspx.cs" Inherits="admin_products_OptionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <h2>Options</h2>
    <table width="800px" style="text-align:left;">
        <tr>
            <td>
                <telerik:RadGrid ID="grdProductOptions" runat="server" AutoGenerateColumns="false"
                    Width="800px" AllowPaging="true" PageSize="10" OnItemCommand="grdProductOptions_ItemCommand"
                    OnNeedDataSource="grdProductOptions_NeedDataSource" OnItemCreated="grdProductOptions_ItemCreated"
                    OnItemDataBound="grdProductOptions_ItemDataBound">
                    <MasterTableView DataKeyNames="OptionID" CommandItemDisplay="Top"
                        EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" HeaderStyle-Width="400px"
                                ItemStyle-Width="400px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OptionType.OptionTypeName" HeaderText="Option Type Name" HeaderStyle-Width="350px"
                                ItemStyle-Width="350px">
                            </telerik:GridBoundColumn>                            
                            <telerik:GridEditCommandColumn>
                            </telerik:GridEditCommandColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template" CaptionDataField="OptionID" CaptionFormatString="Edit Option"
                            InsertCaption="Insert New Option">
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
                                                <telerik:RadTextBox ID="txtOptionName" runat="server" Text='<%#Eval("OptionName") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>                                       
                                        <tr>
                                            <td>
                                                Option Type *
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="txtOptionType" runat="server" DataTextField="OptionTypeName"
                                                    SelectedValue='<%# (Container is GridEditFormInsertItem) ? null : DataBinder.Eval(Container.DataItem,"OptionType.OptionTypeID")  %>'
                                                    DataValueField="OptionTypeID">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="Button1" Text=' Save ' SkinID="ButtonSave"
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Width="400px"></PopUpSettings>
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

