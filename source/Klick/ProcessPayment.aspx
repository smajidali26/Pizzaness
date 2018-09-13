<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessPayment.aspx.cs" Inherits="ProcessPayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/jquery-1.4.js"></script>
    <script src="/js/script.js"></script>
</head>
<body>
    <form id="form1">
    <div>
            <input type='hidden' name='x_login' id='x_login' value="<%= XLogin %>" />
        <input type='hidden' name='x_fp_sequence' id='x_fp_sequence' value="<%=XFpSequence %>" />
        <input type='hidden' name='x_fp_timestamp' id='x_fp_timestamp' value="<%=XFpTimeStamp %>" />
        <input type='hidden' name='x_fp_hash' id='x_fp_hash' value="<%=XFpHash %>"/>
        <input type='hidden' name='x_test_request' id='x_test_request' value="<%=XTestRequest %>"/>
        <input type='hidden' name='x_show_form' id='x_show_form' value="<%=XShowForm %>" />
        <input type='hidden' name='x_invoice_num' id='x_invoice_num' value="<%=XInvoiceNum %>" />
        <input type='hidden' name='x_amount' id='x_amount' value="<%=XAmount %>" />
    </div>
    </form>
</body>
</html>
