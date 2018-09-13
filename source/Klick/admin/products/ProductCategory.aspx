<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="ProductCategory.aspx.cs" Inherits="admin_products_ProductCategory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    </telerik:RadScriptBlock>
    <br />
    <table width="700px" style="margin: 0px auto; padding: 0px auto;">
        <tr>
            <td>
                <h3>Categories</h3>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdProductCategory" runat="server" AutoGenerateColumns="false"
                    OnItemCommand="grdProductCategory_ItemCommand" OnNeedDataSource="grdProductCategory_NeedDataSource"
                    Width="850px" AllowPaging="true" PageSize="10">
                    <MasterTableView DataKeyNames="CategoryID" CommandItemDisplay="TopAndBottom" EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Name" HeaderText="Category Name" HeaderStyle-Width="200px"
                                ItemStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="Enabled" HeaderStyle-Width="60px"
                                ItemStyle-Width="60px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="DisplayOrder" HeaderText="Display Order" HeaderStyle-Width="100px"
                                ItemStyle-Width="100px">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="DisplayOnHomePage" HeaderText="Display On Homepage" HeaderStyle-Width="100px"
                                ItemStyle-Width="100px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="WebCategory" HeaderText="Display On Website" HeaderStyle-Width="100px"
                                ItemStyle-Width="100px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="DesktopCategory" HeaderText="Display On Desktop" HeaderStyle-Width="100px"
                                ItemStyle-Width="100px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Description" HeaderStyle-Width="200px"
                                ItemStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" ConfirmText="Are you sure you want to delete category?" />
                        </Columns>
                        <EditFormSettings EditFormType="Template" CaptionDataField="CategoryID" CaptionFormatString="Edit Category"
                            InsertCaption="New Category">
                            <FormTemplate>
                                <table border="0" cellpadding="4" cellspacing="0" width="500px" style="text-align: left;">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="error" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">Category Name *
                                            </td>
                                            <td style="width: 350px;">
                                                <telerik:RadTextBox ID="txtCategoryName" runat="server" Text='<%#Eval("Name") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ErrorMessage="Required" ControlToValidate="txtCategoryName"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Enabled *
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="txtEnabled" runat="server" Checked='<%#(Container is GridEditFormInsertItem)? true: Eval("IsActive")  %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">Display Order *
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtDisplayOrder" runat="server" Text='<%#Eval("DisplayOrder") %>'
                                                    TextMode="SingleLine">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="rfvDisplayOrder" runat="server" ErrorMessage="Required" ControlToValidate="txtDisplayOrder"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Description (Optional)
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtDescription" runat="server" Text='<%#Eval("Description") %>'
                                                    TextMode="MultiLine" Rows="4" Columns="30">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Image Path
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="txtImage" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Display on home page?
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="txtDisplayOnHomePage" runat="server" Checked='<%#(Container is GridEditFormInsertItem)? true: Eval("DisplayOnHomePage")  %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Display on website?
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="txtDisplayOnWebSite" runat="server" Checked='<%#(Container is GridEditFormInsertItem)? true: Eval("WebCategory")  %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Display on desktop application?
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="txtDisplayOnDesktop" runat="server" Checked='<%#(Container is GridEditFormInsertItem)? true: Eval("DesktopCategory")  %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Width="510px"></PopUpSettings>
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
