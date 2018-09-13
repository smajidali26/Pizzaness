<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="ProductAdonTypes.aspx.cs" Inherits="admin_products_ProductAdonTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    
    <br />
    <table width="500px" style="margin: 0px auto; padding: 0px auto;text-align:left;">
        <tr>
            <td>

                <h3>Adon Types</h3>
            </td>

        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdProductAdonType" runat="server" AutoGenerateColumns="false"
                    Width="500px" AllowPaging="true" PageSize="10"
                    OnNeedDataSource="grdProductAdonType_NeedDataSource">
                    <MasterTableView DataKeyNames="AdOnTypeId" CommandItemDisplay="TopAndBottom" EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="AdOnTypeName" HeaderText="AdOn Type Name" HeaderStyle-Width="250px"
                                ItemStyle-Width="250px">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsFreeAdonType" HeaderText="Free AdOn Type"
                                HeaderStyle-Width="130px" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="80px" HeaderStyle-Width="80px" HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Products/AdonTypeImage/"+ Eval("ImageName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn>
                            </telerik:GridEditCommandColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template" CaptionDataField="AdOnTypeId" CaptionFormatString="Edit Product AdOn Type"
                            InsertCaption="Insert New Product AdOn Type">
                            <FormTemplate>
                                <table border="0" cellpadding="4" cellspacing="0" width="600px" style="text-align:left;">
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="error" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">
                                                Adon Name Type *
                                            </td>
                                            <td style="width: 400px;">
                                                <telerik:RadTextBox ID="txtAdOnTypeName" runat="server" Text='<%#Eval("AdOnTypeName") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">
                                                Adon On Type is Free?
                                            </td>
                                            
                                            <td style="width: 250px;">
                                                <asp:CheckBox ID="txtFree" runat="server" Checked='<%# (Container is GridEditFormInsertItem) ? false : DataBinder.Eval(Container.DataItem,"IsFreeAdonType") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Image
                                            </td>
                                            <td >
                                                <asp:FileUpload ID="txtImage" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdAdonTypeId" runat="server" Value='<%#Eval("AdOnTypeId") %>' />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" Text=' Save '
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' OnClick="Button1_Click">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Width="620px"></PopUpSettings>
                        </EditFormSettings>
                    </MasterTableView>
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
