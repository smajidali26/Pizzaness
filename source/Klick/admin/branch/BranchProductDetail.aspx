<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchProductDetail.aspx.cs" Inherits="admin_branch_BranchProductDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/StyleSheet/stylesheet.css" rel="stylesheet" />
     <link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" type="text/css" href="~/StyleSheet/jquery-ui-1.8.9.custom.css" />
    <!-- jQuery and Custom scripts -->
    <script src="/js/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="/js/script.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Options</h3>
            <table class="listing-table">
                <tbody>
                    <tr>
                        <th style="width:200px;">Option Type</th>
                        <th style="width:100px;">Is MultiSelect</th>
                        <th style="width:100px;">Is SamePrice</th>
                        <th style="width:100px;">Manage</th>
                    </tr>
                    <asp:Repeater ID="rptProductOptions" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("OptionTypeName") %>
                                </td>
                                <td>
                                    <%#Eval("IsMultiSelect") %>
                                </td>
                                <td>
                                    <%#Eval("IsSamePrice") %>
                                </td>
                                <td>
                                    <a href="#" onclick="DeleteOptionType(<%#Eval("ProductsOptionTypeId") %>)">Delete</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <h3>Adons</h3>
            <table class="listing-table">
                <tbody>
                    <tr>
                        <th style="width:200px;">Adon Type</th>
                        <th style="width:100px;">Price</th>
                        <th style="width:100px;">Manage</th>
                    </tr>
                    <asp:Repeater ID="rptProductAdon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("AdOnTypeName") %>
                                </td>
                                <td>
                                    <%#Eval("Price") %>
                                </td>
                                <td>
                                    <a href="#" onclick="DeleteAdonType(<%#Eval("ProductsAdOnTypeId") %>)">Delete</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <asp:HiddenField ID="hiddenId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hiddenDeleteType" runat="server" ClientIDMode="Static" />
        <asp:Button ID="buttonDelete" runat="server" Text="Delete" ClientIDMode="Static" CssClass="hide" OnClick="buttonDelete_Click" />
    </form>
</body>
</html>
