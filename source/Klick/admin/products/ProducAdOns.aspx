<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="ProducAdOns.aspx.cs" Inherits="admin_products_ProducAdOns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <table width="700px" style="margin: 0px auto; padding: 0px auto; text-align: left;">
        <tr>
            <td>

                <h3>Adons</h3>
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="txtError" runat="server" Text="" ForeColor="Red"></asp:Label>
                <fieldset>
                    <legend>
                        <asp:Literal ID="ltSearch" runat="server" Text="Search"></asp:Literal></legend>
                    <asp:Panel ID="SearchPanel" runat="server" DefaultButton="buttonSearch">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltName" runat="server" Text="Adon Name"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchAdonName" runat="server" MaxLength="100" SkinID="TextBoxSearch"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="buttonSearch" runat="server" Text=" Search " SkinID="Search" OnClick="buttonSearch_Click"
                                            meta:resourcekey="buttonSearchResource1" />
                                        <asp:Button ID="buttonClear" runat="server" Text=" Clear " SkinID="Clear" OnClick="buttonClear_Click"
                                            meta:resourcekey="buttonClearResource1" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdProductAdon" runat="server" AutoGenerateColumns="false" Width="700px"
                    AllowPaging="true" PageSize="10" OnNeedDataSource="grdProductAdon_NeedDataSource"
                    OnItemCreated="grdProductAdon_ItemCreated" OnItemCommand="grdProductAdon_ItemCommand" AllowCustomPaging="true">
                    <MasterTableView DataKeyNames="AdOnID" CommandItemDisplay="Top" EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="AdOnName" HeaderText="AdOn Name" HeaderStyle-Width="300px"
                                ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AdOnTypeName" HeaderText="AdOn Type"
                                HeaderStyle-Width="200px" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="80px" HeaderStyle-Width="80px" HeaderText="Image" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Products/AdonImage/"+ Eval("ImageName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" ConfirmText="Are you sure you want to delete?" ConfirmTitle="Delete Topping." />
                        </Columns>
                        <EditFormSettings EditFormType="Template" CaptionDataField="AdOnID" CaptionFormatString="Edit Product AdOn"
                            InsertCaption="Insert New Product AdOn">
                            <FormTemplate>
                                <table border="0" cellpadding="4" cellspacing="0" width="500px">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="error" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">Adon Name *
                                            </td>
                                            <td style="width: 250px;">
                                                <telerik:RadTextBox ID="txtAdOnName" runat="server" Text='<%#Eval("AdOnName") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>AdOn Type *
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="txtAdonType" runat="server" DataTextField="AdOnTypeName"
                                                    SelectedValue='<%# (Container is GridEditFormInsertItem) ? null : DataBinder.Eval(Container.DataItem,"AdOnType")  %>'
                                                    DataValueField="AdOnTypeId">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Image
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="txtImage" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdAdonId" runat="server" Value='<%#Eval("AdOnId") %>' />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" Text=' Save '
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' OnClick="Button1_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Width="500px"></PopUpSettings>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
