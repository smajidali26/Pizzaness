<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true" CodeFile="Sliders.aspx.cs" Inherits="admin_Settings_Sliders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProductAdonType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProductAdonType" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <br />
    <table width="500px" style="margin: 0px auto; padding: 0px auto;">
        <tr>
            <td>
                <telerik:RadGrid ID="grdSliderImage" runat="server" AutoGenerateColumns="false"
                    Width="430px" AllowPaging="true" PageSize="10" OnItemCommand="grdSliderImage_ItemCommand"
                    OnNeedDataSource="grdSliderImage_NeedDataSource">
                    <MasterTableView DataKeyNames="SliderImageId" CommandItemDisplay="TopAndBottom" EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ImageName" HeaderText="Image Name" HeaderStyle-Width="150px"
                                ItemStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Description" HeaderStyle-Width="250px"
                                ItemStyle-Width="250px">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsEnabled" HeaderText="Is Enabled"
                                HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridTemplateColumn DataField="ImagePath">
                                <ItemTemplate>
                                    <img src="/Sliders/<%#Eval("ImagePath") %>" width="80px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" ConfirmText="Are you sure you want to delete?" ConfirmTitle="Delete Slider Image." />
                        </Columns>
                        <EditFormSettings EditFormType="Template" CaptionDataField="SliderImageId" CaptionFormatString="Edit Slider"
                            InsertCaption="Add Slider">
                            <FormTemplate>
                                <table border="0" cellpadding="4" cellspacing="0" width="500px" style="text-align:left;">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="error" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">
                                                Image Name *
                                            </td>
                                            <td style="width: 350px;">
                                                <telerik:RadTextBox ID="txtImageName" runat="server" Text='<%#Eval("ImageName") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                Image File *
                                            </td>
                                            <td>
                                                <telerik:RadAsyncUpload ID="txtImage" runat="server" AllowedFileExtensions="jpg,jpeg,png,gif" MaxFileSize="4096000" Width="200px"></telerik:RadAsyncUpload>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                Description
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtDescription" runat="server" Text='<%#Eval("Description") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Is Enabled
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="txtIsEnabled" runat="server" Checked='<%# (Container is GridEditFormInsertItem) ? false : DataBinder.Eval(Container.DataItem,"IsEnabled") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="Button1" Text=' Save '
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Width="500px" Height="300px"></PopUpSettings>
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

