<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="Branches.aspx.cs" Inherits="admin_branch_Branches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdBranches">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBranches" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <br />
    <table width="920px" border="0" cellpadding="4" cellspacing="0">
        <tbody>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="grdBranches" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdBranches_NeedDataSource"
                        OnItemCommand="grdBranches_ItemCommand" OnItemDataBound="grdBranches_ItemDataBound"
                        OnDetailTableDataBind="grdBranches_DetailTableDataBind" Skin="Default">
                        <MasterTableView DataKeyNames="BranchID" CommandItemDisplay="Top" ItemStyle-VerticalAlign="Top">
                            <Columns>
                                <telerik:GridBoundColumn DataField="Title" HeaderText="Branch Name" HeaderStyle-Width="200px"
                                    ItemStyle-Width="150px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" HeaderStyle-Width="200px"
                                    ItemStyle-Width="150px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="City" HeaderText="City" HeaderStyle-Width="80px"
                                    ItemStyle-Width="80px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="State" HeaderText="State" HeaderStyle-Width="80px"
                                    ItemStyle-Width="80px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Zip" HeaderText="Zip" HeaderStyle-Width="80px"
                                    ItemStyle-Width="80px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" HeaderStyle-Width="80px"
                                    ItemStyle-Width="80px">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsDeliveryEnabled" HeaderText="Delivery Available"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="100px">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="Enabled" HeaderStyle-Width="50px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridEditCommandColumn ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" />
                            </Columns>
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="BranchID" Name="BranchDetail" Caption="More Branch Detail">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="BranchID" MasterKeyField="BranchID" />
                                    </ParentTableRelation>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Fax" HeaderText="Fax" HeaderStyle-Width="80px"
                                            ItemStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delivery Area" ItemStyle-Width="150px" UniqueName="DeliveryAreas">
                                            <ItemTemplate>
                                                <asp:Label ID="txtZipcodes" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="DeliveryCharges" HeaderText="Delivery Tax" HeaderStyle-Width="80px"
                                            ItemStyle-Width="80px" EmptyDataText="N/A">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WorkingHourStart" HeaderText="Working Hour Start"
                                            HeaderStyle-Width="100px" DataFormatString="{0:hh:mm tt}" ItemStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WorkingHourEnd" HeaderText="Working Hour End"
                                            HeaderStyle-Width="100px" DataFormatString="{0:hh:mm tt}" ItemStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                                <telerik:GridTableView Name="BranchProducts" DataKeyNames="BranchProductID" Caption="<h4>Products in Branch</h4>"
                                    Width="600px" PageSize="5">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="BranchID" MasterKeyField="BranchID" />
                                    </ParentTableRelation>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Product Name" HeaderStyle-Width="200px"
                                            ItemStyle-Width="200px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridCheckBoxColumn DataField="Enable" HeaderText="Enabled" HeaderStyle-Width="50px"
                                            AllowSorting="false" ItemStyle-Width="50px">
                                        </telerik:GridCheckBoxColumn>
                                        <telerik:GridBoundColumn DataField="Price" HeaderText="Price" HeaderStyle-Width="60px"
                                            ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="BranchProducts" />
                                    <DetailTables>
                                        <telerik:GridTableView Name="ProductsOptions" DataKeyNames="OptionID" Width="550px">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="OptionTypeName" HeaderText="Group By" SortOrder="Ascending" />
                                                    </GroupByFields>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldName="OptionTypeName" HeaderText="Group By" HeaderValueSeparator=" : " />
                                                    </SelectFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" HeaderStyle-Width="200px"
                                                    ItemStyle-Width="200px">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="OptionTypeName" HeaderText="Option Type Name"
                                                    HeaderStyle-Width="100px" ItemStyle-Width="150px">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Price" HeaderText="Option Price" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="50px">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ToppingPrice" HeaderText="Adon Price" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="50px">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="BranchProductOptionHeader" />
                                        </telerik:GridTableView>
                                        <telerik:GridTableView Name="ProductsAdons" DataKeyNames="AdOnID" Width="380px">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="AdOnTypeName" HeaderText="Group By" SortOrder="Ascending" />
                                                    </GroupByFields>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldName="AdOnTypeName" HeaderText="Group By" HeaderValueSeparator=" : " />
                                                    </SelectFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="AdOnName" HeaderText="Adon Name" HeaderStyle-Width="200px"
                                                    ItemStyle-Width="200px">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="Price" HeaderText="Adon Price" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <%# ((bool)Eval("IsFreeAdonType")) ? "Free Adon" : Eval("Price")
                                                        %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="DefaultSelected" HeaderText="Default Seleted"
                                                    HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <%# ((short)Eval("DefaultSelected") == 0) ? "None" : ((short)Eval("DefaultSelected") == 1)? "Full" : ((short)Eval("DefaultSelected") ==2 ) ? "First Half" : "Second Half"
                                                        %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <HeaderStyle Height="28px" CssClass="BranchProductAdonHeader" />
                                        </telerik:GridTableView>
                                    </DetailTables>
                                </telerik:GridTableView>
                            </DetailTables>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="txtError" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
