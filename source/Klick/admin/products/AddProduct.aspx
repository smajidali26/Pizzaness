<%@ Page Title="" Language="C#" MasterPageFile="~/templates/admin.master" AutoEventWireup="true"
    CodeFile="AddProduct.aspx.cs" Inherits="admin_products_AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <br />
    <div style="margin: 0px auto; padding: 0px auto; width: 700px;">
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
            SelectedIndex="2">
            <Tabs>
                <telerik:RadTab Text="Product Information" PageViewID="RadPageView1" Selected="true" Visible="false">
                </telerik:RadTab>
                <telerik:RadTab Text="Combo Items" PageViewID="RadPageView2" Visible="false">
                </telerik:RadTab>
                <telerik:RadTab Text="Combo Options" PageViewID="RadPageView3" Visible="false">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true" BorderWidth="1px">
                <table border="0" cellpadding="4" cellspacing="0" width="700px" class="addproduct" style="text-align: left !important;">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <h3>Add Product</h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">Product Name * :
                            </td>
                            <td style="width: 550px;">
                                <telerik:RadTextBox ID="txtProductName" runat="server" Width="230px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ErrorMessage="Required" ControlToValidate="txtProductName"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Product Image :
                            </td>
                            <td>
                                <telerik:RadUpload ID="txtProductImage" runat="server" ControlObjectsVisibility="None"
                                    InitialFileInputsCount="1" Width="200px" AllowedFileExtensions=".jpg,.gif,.png">
                                </telerik:RadUpload>
                            </td>
                        </tr>
                        <tr>
                            <td>Is Special Product? :
                            </td>
                            <td>
                                <asp:RadioButton ID="txtYes" ClientIDMode="Static" runat="server" Text="Yes" GroupName="IsSpecial" />
                                <asp:RadioButton ID="txtNo" ClientIDMode="Static" runat="server" Text="No" GroupName="IsSpecial" />
                            </td>
                        </tr>
                        <tr class="special">
                            <td>Display Days :</td>
                            <td>
                                <asp:CheckBoxList ID="chkListDays" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                                    <asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Monday" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Friday" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
                                </asp:CheckBoxList></td>
                            <%--<asp:RequiredFieldValidator ID="rfvListDays" runat="server" ErrorMessage="Required" ControlToValidate="chkListDays"
                                    SetFocusOnError="true" ClientIDMode="Static"></asp:RequiredFieldValidator>--%>
                        </tr>
                        <tr class="special">
                            <td>Display Time Span :</td>
                            <td>
                                <asp:RadioButtonList ID="rblTimeSpan" runat="server" CssClass="float-left" RepeatColumns="2">
                                    <asp:ListItem Text="Whole Day" Value="1" ></asp:ListItem>
                                    <asp:ListItem Text="Specific Time" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvTimeSpan" runat="server" ErrorMessage="Required" ControlToValidate="rblTimeSpan"
                                    SetFocusOnError="true" ClientIDMode="Static"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="specialtimespan">
                            <td>Select Time Span</td>
                            <td><span class="float-left"> Start :</span>
                                <telerik:RadTimePicker ID="rtpStartTime" runat="server" CssClass="float-left" TimeView-StartTime="11:00:00" TimeView-EndTime="22:01:00">
                                     <TimeView Interval="00:15:00"> 
                                    </TimeView>
                                </telerik:RadTimePicker>
                                <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ErrorMessage="*" ControlToValidate="rtpStartTime"
                                    SetFocusOnError="true" ClientIDMode="Static"></asp:RequiredFieldValidator>
                                <br />
                                <br />
                                <span class="float-left"> End :</span>
                                <telerik:RadTimePicker ID="rtpEndTime" runat="server" CssClass="float-left" TimeView-StartTime="11:30:00" TimeView-EndTime="23:01:00">
                                    <TimeView Interval="00:15:00"> 
                                    </TimeView>
                                </telerik:RadTimePicker>
                                <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ErrorMessage="*" ControlToValidate="rtpEndTime"
                                    SetFocusOnError="true" ClientIDMode="Static"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Product Category * :
                            </td>
                            <td>
                                <telerik:RadComboBox ID="txtProductCategory" runat="server" DataTextField="Name" DataValueField="CategoryID">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvProductCategory" runat="server" ErrorMessage="Required" ControlToValidate="txtProductCategory"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Description * :
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5"
                                    Columns="35">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Required" ControlToValidate="txtDescription"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Display Order * :
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtDisplayOrder" runat="server" Type="Number" NumberFormat-DecimalDigits="0" DataType="System.Int16" MaxLength="3"></telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvDisplayOrder" runat="server" ErrorMessage="Required" ControlToValidate="txtDisplayOrder"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="SaveProductBtn" runat="server" Text=" Save " SkinID="ButtonSave" OnClick="SaveProductBtn_Click" />
                                <asp:Button ID="CancelBtn" runat="server" Text=" Cancel " SkinID="ButtonCancel" CausesValidation="false" OnClick="CancelBtn_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="txtError" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server" BorderWidth="1px">
                <p>Please select any items from below table to create new combo deal. To add item in combo you have to check each check-box infront of each item. Then you will proivide quantity and price of each checked item. No item will be part of deal if check-box is not checked.</p>
                <table width="700px" style="text-align: left !important;" border="0" cellpadding="4" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="txtMessageCombo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="grdProducts" runat="server" Width="700px" AutoGenerateColumns="false"
                                OnItemDataBound="grdProducts_ItemDataBound" OnNeedDataSource="grdProducts_NeedDataSource">
                                <MasterTableView DataKeyNames="ProductID">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Product Name" ItemStyle-Width="290px"
                                            HeaderStyle-Width="290px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="150px"
                                            HeaderStyle-Width="150px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderText="Add to Deal">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="OptionCheckBox" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="Quantity" runat="server" Type="Number" DataType="System.Int16" NumberFormat-DecimalDigits="0" MinValue="1" MaxLength="2" Width="40px"></telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderText="Unit Price">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="UnitPrice" runat="server" Type="Currency" DataType="System.Decimal" NumberFormat-DecimalDigits="2" Width="60px"></telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderText="Free Topping">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="FreeTopping" runat="server" Type="Number" DataType="System.Int16" NumberFormat-DecimalDigits="0" MinValue="0" MaxLength="2" Width="40px"></telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderText="Is Customizable">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="AllowCustomization" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="ButtonSave2" runat="server" Text=" Save " SkinID="ButtonSave" OnClick="SaveChildProductBtn_Click" />
                            <asp:Button ID="ButtonCanel2" runat="server" Text=" Cancel " SkinID="ButtonCancel" OnClick="CancelBtn_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="SelectedProducts" runat="server" ClientIDMode="Static" />
                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView3" runat="server" BorderWidth="1px">
                <table style="text-align: left;">
                    <tbody>
                        <tr>
                            <td>
                                <p>Below grid is showing options available for each checked item from previous screen. These option(s) are displayed on the basis of each selected product and these selected product(s) are included in branch. Here you have to select at-least one option if options are available. And any of selected option <b>must</b> have same price which is price of product in previous screen.  </p>
                                <asp:Label ID="txtMessageOptions" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="ComboOptions" runat="server" Width="650px" AutoGenerateColumns="false" OnNeedDataSource="ComboOptions_NeedDataSource"
                                    SkinID="Silk" OnItemDataBound="ComboOptions_ItemDataBound">
                                    <MasterTableView DataKeyNames="OptionID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="OptionName" HeaderText="Option Name" ItemStyle-Width="290px"
                                                HeaderStyle-Width="290px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OptionTypeName" HeaderText="Option Type" ItemStyle-Width="100px"
                                                HeaderStyle-Width="150px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderText="Add Option">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="OptionCheckBox" runat="server" />
                                                    <asp:HiddenField ID="ComboId" runat="server" Value='<%# Eval("ComboId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderText="Option Price">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="OptionPrice" runat="server" Type="Currency" DataType="System.Decimal" NumberFormat-DecimalDigits="2" Width="80px"></telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>

                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="ProductName" HeaderValueSeparator=" : " HeaderText="Product Name"></telerik:GridGroupByField>
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="ProductName"></telerik:GridGroupByField>
                                                </GroupByFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ButtonSave3" runat="server" Text=" Save " SkinID="ButtonSave" OnClick="ButtonSaveOptions_Click" />
                                <asp:Button ID="ButtonCancel3" runat="server" Text=" Cancel " SkinID="ButtonCancel" OnClick="CancelBtn_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
