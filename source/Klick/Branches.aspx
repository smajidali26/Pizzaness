<%@ Page Title="" Language="C#" MasterPageFile="~/templates/main.master" AutoEventWireup="true"
    CodeFile="Branches.aspx.cs" Inherits="Branches" enableEventValidation="false" viewStateEncryptionMode="Never" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div class="brachlist">
        <telerik:RadGrid ID="grdBranches" runat="server" Width="500px" 
            AutoGenerateColumns="false" AllowPaging="true" PageSize="3" 
            onitemcommand="grdBranches_ItemCommand" 
            onneeddatasource="grdBranches_NeedDataSource" 
            onitemdatabound="grdBranches_ItemDataBound">
            <MasterTableView DataKeyNames="BranchID">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="Title" HeaderText="Branch Name" ItemStyle-Width="150px" 
                    HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">                    
<HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>

<ItemStyle Width="150px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Distance" ItemStyle-Width="100px" 
                    HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txtDistance" runat="server" Text=""></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn HeaderText="Address" ItemStyle-Width="200px" 
                    HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("Address") + " " + Eval("City")%>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>

<ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridEditCommandColumn EditText="View" ItemStyle-Width="50px" 
                        CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif" 
                        UpdateImageUrl="Update.gif">
<ItemStyle Width="50px"></ItemStyle>
                    </telerik:GridEditCommandColumn>
                </Columns>

<EditFormSettings>
<EditColumn UniqueName="EditCommandColumn1" CancelImageUrl="Cancel.gif" 
        EditImageUrl="Edit.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif"></EditColumn>
</EditFormSettings>
            </MasterTableView>

<HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
        </telerik:RadGrid>
    </div>
</asp:Content>
