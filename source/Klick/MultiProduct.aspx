<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiProduct.aspx.cs" Inherits="MultiProduct" %>

<%@ Register Src="~/MyUserControl.ascx" TagName="MyControl" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/App_Themes/MultiProduct.css?v=0.6" rel="stylesheet" type="text/css" />
    <link href="~/StyleSheet/dc_text_buttons.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="/js/script.js?v=0.6" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div style="margin: 0px auto; padding: 0px auto;">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            </telerik:RadScriptManager>
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript" language="javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                    function BeginRequestHandler(sender, args) {
                        $j('input[type="checkbox"]').each(function () {
                            $j(this).attr("disabled", true);
                        });
                        $j('input[type="radio"]').each(function () {
                            $j(this).attr("disabled", true);
                        });

                    }
                    function EndRequestHandler(sender, args) {
                        $j('input[type="checkbox"]').each(function () {
                            $j(this).removeAttr("disabled");
                        });
                        $j('input[type="radio"]').each(function () {
                            $j(this).removeAttr("disabled");
                        });
                    }
                </script>
            </telerik:RadCodeBlock>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadMultiPage1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
                            <telerik:AjaxUpdatedControl ControlID="txtNetPrice" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="NextButton">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>            
            <telerik:RadTabStrip ID="RadTabStrip1" SelectedIndex="0" runat="server" Align="Justify" MultiPageID="RadMultiPage1"
                Skin="Silk" CssClass="tabStrip" CausesValidation="true">
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" RenderMode="Lightweight"
                CssClass="multiPage">
            </telerik:RadMultiPage>
            <div class="productpricediv floatleft">
                <div id="price-left" class="floatleft">
                    <span class="floatleft">Price: </span>                  
                        <asp:Label ID="txtNetPrice" runat="server" Text="" CssClass="floatleft"></asp:Label>
                </div>
                <div id="price-center" class="floatleft">
                </div>
                <div id="price-right" class="floatleft">
                    <asp:Button ID="NextButton" runat="server" Text=" Next " CssClass="dc_button3 ico-arrow_r floatright" OnClick="NextButton_Click"/>
                    <asp:Button ID="AddToCartButton" runat="server" Text=" Add to Order " CssClass="dc_button3 ico-cart_add floatright" OnClick="AddToCartButton_Click"  Visible="false" />
                    <asp:Label ID="txtStoreClosed" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="TabIndex" Value="-1" ClientIDMode="Static"/>
    </form>
</body>
</html>
