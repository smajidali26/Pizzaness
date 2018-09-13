var $j = jQuery.noConflict();
var StoreClosed = false;
var PreviousLineTip = 0;

$j(document).ready(function () {

    /************* Proceed Page ******************/
    if (document.getElementById('ContentPlaceHolder1_txtDelivery') != undefined) {
        if (document.getElementById('ContentPlaceHolder1_txtDelivery').checked) {
            $j('#NewAddressHeader').show();
        }
    }

    /************* Proceed Page End ******************/
    //InitChosenDropDown();
    initMessages();
    InitDatePicker();
    sendForm();
    //RotateSlider();
    OnDeliveryMethodSelection();
    OnShortCartHover();
    StoreCloseDialog();
    WeekChart();
    PorductAdd();

    if ($j("#sl").length > 0) {
        $j("#sl").wowSlider({ effect: "blinds", prev: "", next: "", duration: 20 * 100, delay: 20 * 100, width: 960, height: 360, autoPlay: true, playPause: true, stopOnHover: true, loop: false, bullets: true, caption: true, captionEffect: "slide", controls: true, onBeforeStep: 0, images: 0 });
    }
});

function CloseOnReload(args) {
    window.parent.location = window.parent.location;
    //GetRadWindow().BrowserWindow.refresh();
    GetRadWindow().close();
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

    return oWindow;
}

function CancelEdit() {
    GetRadWindow().close();
}


function Click() {
    $j('a#NewAddress').click(function () {
        $j('#txtCurrentAddress').removeAttr("checked");
        $j('#NewAddressTable').css('display', 'block');
        return false;
    });

    $j('#ContentPlaceHolder1_txtDelivery').click(function () {
        $j('#AddressRowHeader').css('display', 'block');
        $j('#AddressRow').css('display', 'block');
        $j('#NewAddressHeader').show();
    });
    $j('#ContentPlaceHolder1_txtPickUp').click(function () {
        $j('#AddressRowHeader').css('display', 'none');
        $j('#AddressRow').css('display', 'none');
        $j('#NewAddressHeader').hide();
    });
}


function InitChosenDropDown() {
    if ($j(".chzn-select").length > 0)
        $j(".chzn-select").chosen();
}

function ShowMessage(message, type) {
    $j.notifyBar({ cls: type, delay: 10000, html: message });

}

function initMessages() {
    var bMessageShown = false;

    $j(".noty_bar").each(function () {
        if (!bMessageShown && ($j(".noty_text").text() != "")) {
            bMessageShown = true;
            $j(this).hide();
            var msgType = $j(this).attr("rel");

            if (!msgType) {
                msgType = 'error'
            }


            var noty_id = noty({
                layout: 'top',
                text: $j(".noty_text").text(),
                type: msgType
            });
        }
    });
}
function sendForm() {
    var identifier = document.getElementById('x_fp_hash');
    var x_test_request = document.getElementById('x_test_request');
    if (identifier) {
        if (x_test_request.value == "true")
            document.forms[0].action = "https://demo.globalgatewaye4.firstdata.com/payment";
        else
            document.forms[0].action = "https://checkout.globalgatewaye4.firstdata.com/payment";
        document.forms[0].submit();
    }
}

function buttonClick(buttonClientId) {
    var clickButton = document.getElementById(buttonClientId);
    clickButton.click();
}

function AddjustMenu() {
    if ($j("#topnav").length > 0) {
        var width = $j("#topnav").width();
        var menuItems = new Array();
        var menuItemsWidth = new Array();
        $j("#topnav > li").each(function () {
            menuItems.push($j(this));
        });
        $j("#topnav > li").each(function () {
            menuItemsWidth.push($j(this).width());
        });
        $j("#topnav").empty();
        var more = $j(document.createElement('li'));
        var moreUL = $j(document.createElement('ul'));
        $j(more).html("<a href='#'>More</a>");
        $j(more).attr("class", "dropdown");
        $j(moreUL).attr("class", "children");
        $j(more).append($j(moreUL));
        var itemWidth = 0;
        for (var i = 0; i < menuItems.length; i++) {
            itemWidth = menuItemsWidth[i] + itemWidth;
            if (itemWidth + 64 < width) {
                $j("#topnav").append(menuItems[i]);
            }
            else {
                $j(moreUL).append(menuItems[i]);
            }
        }
        $j("#topnav").append($j(more));
    }
}

function RotateSlider() {
    var interval;
    if ($j('ul#myRoundabout').length > 0) {
        $j('ul#myRoundabout')
        .roundabout({
            'btnNext': '.next_round',
            'btnPrev': '.previous_round'
        }
          )
        .hover(
        function () {

            clearInterval(interval);
        },
        function () {

            interval = startAutoPlay();
        });

        interval = startAutoPlay();
    }
}

function startAutoPlay() {
    return setInterval(function () {
        $j('ul#myRoundabout').roundabout_animateToPreviousChild();
    }, 3000);
}

function CalculatePrice(obj) {
    var _orderDetailId = $j(obj).closest('tr').attr('id');
    var _quantity = $j("#quantity" + _orderDetailId).val();
    var _unitPrice = $j(obj).closest('tr').attr('unitprice');

    /*CHECK QUANTITY */
    if (_quantity == "") {
        $('input[id=quantity' + _orderDetailId + ']').focus();
        return;
    }
    else if (isNaN(parseInt(_quantity))) {
        $('input[id=quantity' + _orderDetailId + ']').focus();
        return;
    }
    else if (!validateInteger(parseInt(_quantity))) {
        $('input[id=quantity' + _orderDetailId + ']').focus();
        return;
    }

    var dto = { orderDetailId: _orderDetailId, quantity: _quantity };
    $j.ajax({
        type: "POST",
        url: "../Ajax/AjaxData.aspx/UpdateOrderDetailItem",
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg.d != '' && msg.d == true) {
                
                $j(obj).closest('tr').find('td[class="totalprice"]').html("$" + (_quantity * parseFloat(_unitPrice)).toFixed(2)); // Change Item Total Price 
                $j(obj).closest('tr').attr('totalprice', (_quantity * parseFloat(_unitPrice)).toFixed(2));  // Item Total Price 
                CalculateTotalOfItems($j(obj).closest('tr').closest('tbody'));
            }
        },
        error: function () {
            alert('error');
        }
    });
    return false;

}

function IncreaseOrDescreaseQuantity(OrderDetailID, IncreaseValue) {
    var quantityTextbox = $j('#quantity' + OrderDetailID);
    if (IncreaseValue) {
        $j(quantityTextbox).val(parseInt($j(quantityTextbox).val()) + 1);
        CalculatePrice(quantityTextbox);
    }
    else if(IncreaseValue == false) {
        if (parseInt($j(quantityTextbox).val()) > 1) {
            $j(quantityTextbox).val(parseInt($j(quantityTextbox).val()) - 1);
        }
        CalculatePrice(quantityTextbox);
    }
}
function validateInteger(value) {
    var RE = /^\d*$/;
    if (RE.test(value)) {
        return true;
    } else {
        return false;
    }
}

function validateDecimal(value) {
    var RE = /^\d*\.?\d*$/;
    if (RE.test(value)) {
        return true;
    } else {
        return false;
    }
}

function InitDatePicker() {
    if ($j(".datepicker").length > 0)
        $j(".datepicker").datepicker({ minDate: 0 });
    if ($j(".dateofbirth").length > 0) {
        $j(".dateofbirth").datepicker({
            changeMonth: true,
            changeYear: true,
            minDate: "01/01/1925",
            maxDate: (new Date()).getMonth() + "/" + (new Date()).getDate() + "/" + ((new Date()).getYear() - 16),
            yearRange: "-100:+0",
            showButtonPanel: true
        });
    }
    if ($j(".datepicker_normal").length > 0)
        $j(".datepicker_normal").datepicker();

    if ($j(".datepickerstart").length > 0) {
        $j('.datepickerstart').datepicker({
            maxDate: 0,
            onSelect: function (dateStr) {
                var date = $j(this).datepicker('getDate');
                if (date) {
                    date.setDate(date.getDate() + 1);
                }
                $j('.datepickerend').datepicker('option', 'minDate', date);
            }
        });

        $j('.datepickerend').datepicker({
            maxDate: 0,
            onSelect: function (selectedDate) {
                var date = $(this).datepicker('getDate');
                if (date) {
                    date.setDate(date.getDate() - 1);
                }
                $j('.datepickerstart').datepicker('option', 'maxDate', date || 0);
            }
        });
    }

}

function CalculateTotalOfItems(select) {
    var subTotal = 0.0;
    $j(select).find("tr").each(function () {
        subTotal = subTotal + parseFloat($j(this).attr('totalprice'));
    });
    $j('span[class="amount subtotal"]').text("$" + subTotal.toFixed(2));
    var VATValue = document.getElementById("hdVAT").value;
    var VAT = ((VATValue * subTotal.toFixed(2)) / 100).toFixed(2);
    $j('td[class="amount vat"]').text("$" + VAT);
    $j('span[class="amount total"]').text("$" + (parseFloat(VAT) + parseFloat(subTotal)).toFixed(2));
}

function SelectItemsToDelete(id) {
    var hdSelectedDeletedItems = document.getElementById("hdSelectedDeletedItems");
    hdSelectedDeletedItems.value = id;
    buttonClick('buttonDelete');
}

function DeleteOrderDetailItems() {
    var hdSelectedDeletedItems = document.getElementById("hdSelectedDeletedItems");
    if (hdSelectedDeletedItems.value != "" && confirm("Are you sure you want to delete item(s) from cart?")) {
        buttonClick("buttonDelete");
    }
    else {
        alert("Please select an item from cart to delete.");
    }
    return false;
}

function ShowPanel(panelNumber,previousPanel) {
    
    if (panelNumber == 'collapseTwo') {
        var rfvDeliveryMethod = document.getElementById("rfvDeliveryMethod");
        ValidatorValidate(rfvDeliveryMethod);
        if (!rfvDeliveryMethod.isvalid)
            return;
        if ($j("input[type='radio'][id='txtDeliveryMethod_0']").is(":checked") && document.getElementById("hdTotalChargesBeforeTax").value != "") {
            var ChargesBeforeTax = parseFloat(document.getElementById("hdTotalChargesBeforeTax").value);
            if (ChargesBeforeTax < 12) {
                alert("Minimum order $12.");
                return;
            }
        }
    }
    else if (panelNumber == 'collapseThree' || (panelNumber == 'collapseFour' && $j("input[type='radio'][id='txtDeliveryMethod_1']").is(":checked"))) {
        
        var rfvPaymentMethod = document.getElementById("rfvPaymentMethod");
        ValidatorValidate(rfvPaymentMethod);
        if (!rfvPaymentMethod.isvalid)
            return;
        if ($j("input[type='radio'][id='txtPaymentMethod_0']").is(":checked")) {
            $j("#linetip").show();
        }
        else {
            $j("#linetip").hide();
        }
    }
    else if (panelNumber == 'collapseFour') {
        var rfvFirstName = document.getElementById("rfvFirstName");
        var rfvLastName = document.getElementById("rfvLastName");
        var rfvTelephone = document.getElementById("rfvTelephone");
        var rfvAddress = document.getElementById("rfvAddress");
        var rfvCity = document.getElementById("rfvCity");
        var rfvZipCode = document.getElementById("rfvZipCode");
        var rfvState = document.getElementById("rfvState");
        ValidatorValidate(rfvFirstName);
        ValidatorValidate(rfvLastName);
        ValidatorValidate(rfvTelephone);
        ValidatorValidate(rfvAddress);
        ValidatorValidate(rfvCity);
        ValidatorValidate(rfvZipCode);
        ValidatorValidate(rfvState);
        if (!rfvFirstName.isvalid || !rfvLastName.isvalid || !rfvTelephone.isvalid || !rfvAddress.isvalid || !rfvCity.isvalid ||
            !rfvZipCode.isvalid || !rfvState.isvalid)
            return;
    }
    $j("#" + previousPanel).collapse('toggle');
    $j("#" + panelNumber).collapse('toggle');
    var hdSelectedDeletedItems = document.getElementById("hdCurrentAccordion");
    hdSelectedDeletedItems.value = panelNumber;
}

function ShowCurrentAccordion() {
    if ($j("#accordion").length > 0) {
        var hdSelectedDeletedItems = document.getElementById("hdCurrentAccordion");
        if (hdSelectedDeletedItems.value == "") {
            $j("#accordion").accordion("option", "active", 0);
        }
        else {
            $j("#accordion").accordion("option", "active", hdSelectedDeletedItems.value);
        }
    }
}

function OnDeliveryMethodSelection() {
    if ($j("#txtDeliveryMethod").length > 0) {
        if ($j("input[type='radio'][id='txtDeliveryMethod_0']").is(":checked")) {
            $j("#Billing").show();
            $j("a[id='button-payment']").removeAttr("onclick");
            $j("a[id='button-payment']").bind("click", function () { ShowPanel('collapseThree', 'collapseTwo'); });
            $j("a[id='button-confirm-back']").removeAttr("onclick");
            $j("a[id='button-confirm-back']").bind("click", function () { ShowPanel('collapseThree', 'collapseFour'); });
        }
        if ($j("input[type='radio'][id='txtDeliveryMethod_1']").is(":checked")) {
            $j("#Billing").hide();
            $j("a[id='button-payment']").removeAttr("onclick");
            $j("a[id='button-payment']").bind("click", function () { ShowPanel('collapseFour', 'collapseTwo'); });
            $j("a[id='button-confirm-back']").removeAttr("onclick");
            $j("a[id='button-confirm-back']").bind("click", function () { ShowPanel('collapseTwo', 'collapseFour'); });
        }
    }
}

function OnShortCartHover() {
    if ($j(".side_cart").length > 0) {
        $j(".side_cart").hover(function () {
            $j("#cart").html("<img src='/Images/fb-ajax-loader.gif' />");
            $j.ajax({
                type: "POST",
                url: "../Ajax/AjaxData.aspx/GetOrderDetails",
                //data: JSON.stringify(dto),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d != '') {
                        $j("#cart").html(msg.d);

                    }
                },
                error: function () {
                    alert('error');
                }
            });
        });
    }
}

function DeleteOptionType(id) {
    if (confirm("Are you sure you want to delete product option type?")) {
        document.getElementById("hiddenId").value = id;
        document.getElementById("hiddenDeleteType").value = "0";
        buttonClick("buttonDelete");
    }
}

function DeleteAdonType(id) {
    if (confirm("Are you sure you want to delete product adon type?")) {
        document.getElementById("hiddenId").value = id;
        document.getElementById("hiddenDeleteType").value = "1";
        buttonClick("buttonDelete");
    }
}

function StoreCloseDialog() {
    if (StoreClosed) {
        if ($j("#StoreClosed").length > 0)
            $j("#StoreClosed").modal();
    }
}

function LineTip(obj) {
    var lineTip = $j(obj).val();
    var txtTaxAmount = $j("#txtTaxAmount").text();
    txtTaxAmount = parseFloat(txtTaxAmount) + parseFloat($j("#txtDeliveryCharges").text());
    var rvTip = document.getElementById("rvTip");
    ValidatorValidate(rvTip);
    if (!rvTip.isvalid) {
        alert("Invalid value.");
        return;
    }
    if (lineTip == "")
        lineTip = "0";
    var dto = { currentValue: lineTip,  taxAmount : txtTaxAmount };
    $j.ajax({
        type: "POST",
        url: "../Ajax/AjaxData.aspx/AddLineTip",
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg.d != '') {
                $j("#txtTotal").html(parseFloat(msg.d).toFixed(2));

            }
        },
        error: function () {
            alert('error');
        }
    });
}

var timerID = 0;
function PromotionCode(obj) {
    var promoCode = $j('#'+obj).val();
    var _lineTip = $j('#txtTip').val();
    var _devliveryCharges = $j('#txtDeliveryCharges').text();
    var txtTaxAmount = $j("#txtTaxAmount").text();
    txtTaxAmount = parseFloat(txtTaxAmount) + parseFloat($j("#txtDeliveryCharges").text());

    var rvTip = document.getElementById("rvTip");
    if (rvTip)
        {
    ValidatorValidate(rvTip);
    if (!rvTip.isvalid) {
        alert("Invalid line tip value.");
        return;
    }
    }
    if (_devliveryCharges == "")
        _devliveryCharges = "0";
    if (_lineTip == "")
        _lineTip = "0";
    
    var dto = { promotionCode: promoCode, lineTip: _lineTip, deliveryCharges: _devliveryCharges };
    $j.ajax({
        type: "POST",
        url: "../Ajax/AjaxData.aspx/AddPromotionCode",
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg.d != '0') {
                clearTimeout(timerID);
                $j('.messageBox').css('display', 'none');
                if (msg.d.InvalidCode != '0') {
                    showErrorMessage(msg.d.InvalidCode);
                } else {
                    showErrorMessage("Promo code applied.");
                }
                //if (msg.d.InvalidCode == '1') {     // 1 for invalid promo code
                //    showErrorMessage('Invalid Promo code. Please try again.');
                //}
                //if (msg.d.InvalidCode == '2') { // 2 for Date not b/w limits
                //    showErrorMessage('Promo code is not valid for current date.');
                //}
                //if (msg.d.InvalidCode == '3') { // 3 for Promo code usage count expired
                //    showErrorMessage('Promo code has expired.');    // i.e. counter reached to zero
                //}
                //if (msg.d.InvalidCode == '4') { // 3 for Promo value used completely
                //    showErrorMessage('No more balance available...');
                //}
                
                $j("#txtPrice").html(parseFloat(msg.d.SubTotal).toFixed(2));
                $j("#txtTaxAmount").html(parseFloat(msg.d.Tax).toFixed(2));
                $j("#txtTotal").html(parseFloat(msg.d.OrderTotal).toFixed(2));
                
            }
        },
        error: function () {
            alert('Error..!!');
        }
    });
}

function PromotionCodeCart(obj) {
    var promoCode = $j('#' + obj).val();
    var _lineTip = 0;
    var _devliveryCharges = 0;
    
    var dto = { promotionCode: promoCode, lineTip: _lineTip, deliveryCharges: _devliveryCharges };
    $j.ajax({
        type: "POST",
        url: "../Ajax/AjaxData.aspx/AddPromotionCode",
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg.d != '0') {
                clearTimeout(timerID);
                $j('.messageBox').css('display', 'none');
                if (msg.d.InvalidCode != '0') {
                    alert(msg.d.InvalidCode);
                }
                //if (msg.d.InvalidCode == '1') {     // 1 for invalid promo code
                //    showErrorMessage('Invalid Promo code. Please try again.');
                //}
                //if (msg.d.InvalidCode == '2') { // 2 for Date not b/w limits
                //    showErrorMessage('Promo code is not valid for current date.');
                //}
                //if (msg.d.InvalidCode == '3') { // 3 for Promo code usage count expired
                //    showErrorMessage('Promo code has expired.');    // i.e. counter reached to zero
                //}
                //if (msg.d.InvalidCode == '4') { // 3 for Promo value used completely
                //    showErrorMessage('No more balance available...');
                //}

                $j("#txtSubTotal").html(parseFloat(msg.d.SubTotal).toFixed(2));
                $j("#txtVATAmount").html(parseFloat(msg.d.Tax).toFixed(2));
                $j("#txtTotal").html(parseFloat(msg.d.OrderTotal).toFixed(2));

            }
        },
        error: function () {
            alert('Error..!!');
        }
    });
}

// IT WAS NEVER CALLED SO CODE IS NOT TESTED
//function PreOrderPromo(obj) {
//    var promoCode = $j('#' + obj).val();
//    var dto = { promotionCode: promoCode };
//    $j.ajax({
//        type: "POST",
//        url: "../Ajax/AjaxData.aspx/AddPreOrderPromo",
//        data: JSON.stringify(dto),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function(msg) {
//            clearTimeout(timerID);
//            $j('.messageBox').css('display', 'none');

//            $j('.messageBox').html(parseFloat(msg.d.OrderTotal).toFixed(2)).css('display', 'block');
//        },
//        error: function() {
//            alert('Error..!!');
//        }
//    });
//}

function showErrorMessage(message) {
    $j('.messageBox').html('').append(message).slideDown('slow');
    timerID = setTimeout(function () {
        $j('.messageBox').slideUp('slow');
    }, 5000);
}

function RePayOrder(orderId, amount) {
    window.location = "ProcessPayment.aspx?orderid=" + orderId + "&total=" + amount;
}

function WeekChart() {
    if ($j('#weekchart').length > 0) {
        
        var data = null;
        var dto = { reportType: 1, fromDate: '', toDate: '' };
        $j.ajax({
            type: "POST",
            url: "/Ajax/AjaxData.aspx/SaleReport",
            data: JSON.stringify(dto),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                if (msg.d != '') {
                    data = msg.d;

                }
            },
            error: function (jqxhr, textStatus, error) {
                var error = '';
                alert('error');
            }
        });
        var seriesName = (new Date(parseInt(data[0][0].DateField.substr(6)))).toLocaleDateString() + " - " + (new Date(parseInt(data[0][6].DateField.substr(6)))).toLocaleDateString();
        
        var seriesData = new Array(7);
        var i = 0;
        for (i = 0; i < data[0].length; i++) {
            var j = 0,value = "";
            for (j = 0; j < data[1].length; j++) {
                if (parseInt(data[0][i].DateField.substr(6)) == parseInt(data[1][j].DateField.substr(6))) {
                    value = data[1][j].OrderTotal;
                }
            }
            if (value != "") {
                seriesData[i] = parseFloat(value);
            }
            else {
                seriesData[i] = 0;
            }
        }
        
        $j('#weekchart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'Week Sale'
            },
            subtitle: {
                text: 'Pizzaness'
            },
            xAxis: {
                categories: [
                    'Sun',
                    'Mon',
                    'Tus',
                    'Wed',
                    'Thu',
                    'Fri',
                    'Sat'
                ]
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Order Total'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">Order Total on {point.key}</span><table>',
                pointFormat: '<tr><td style="padding:0"><b>${point.y:.1f}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: [{
                name: seriesName,
                data: seriesData

            }]
        });
    }
    
}

function CustomChart() {
    if ($j('#weekchart').length > 0) {

        var data = null;
        var _fromDate = $j("#txtFromDate").val();
        var _toDate = $j("#txtToDate").val();
        var dto = { reportType: 1, fromDate: _fromDate, toDate: _toDate };
        $j.ajax({
            type: "POST",
            url: "/Ajax/AjaxData.aspx/SaleReport",
            data: JSON.stringify(dto),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                if (msg.d != '') {
                    data = msg.d;

                }
            },
            error: function (jqxhr, textStatus, error) {
                var error = '';
                alert('error');
            }
        });
        var seriesName = '';
        if (_fromDate != '' && _toDate != '') {
            seriesName = (new Date(_fromDate)).toLocaleDateString() + " - " + (new Date(_toDate)).toLocaleDateString();
        }
        else if (_fromDate != '' && _toDate == '') {
            seriesName = "From " + (new Date(_fromDate)).toLocaleDateString() +" till today ";
        }
        else if (_fromDate == '' && _toDate != '') {
            seriesName = "Before " + (new Date(_toDate)).toLocaleDateString();
        }

        var seriesData = new Array(data[1].length);
        var xAxis = new Array(data[1].length);
        var i = 0;
        var categories = '';
        
        for (j = 0; j < data[1].length; j++) {
            xAxis[j] = (new Date(parseInt(data[1][j].DateField.substr(6)))).toLocaleDateString();
            
            seriesData[j] = data[1][j].OrderTotal;
            
        }
        

        $j('#weekchart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'Sale Report'
            },
            subtitle: {
                text: 'Pizzaness'
            },
            xAxis: {
                categories: xAxis
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Order Total'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">Order Total on {point.key}</span><table>',
                pointFormat: '<tr><td style="padding:0"><b>${point.y:.1f}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: [{
                name: seriesName,
                data: seriesData

            }]
        });
    }

}

function PorductAdd() {
    
    if ($j(".addproduct").length > 0) {
        //var rfvListDays = document.getElementById("rfvListDays");
        var rfvTimeSpan = document.getElementById("rfvTimeSpan");
        var rfvStartTime = document.getElementById("rfvStartTime");
        var rfvEndTime = document.getElementById("rfvEndTime");

        if ($j("#txtYes").is(":checked")) {
            $j(".special").show();
            if ($j("#ctl00_ContentPlaceHolder1_rblTimeSpan_1").is(":checked")) {
                ValidatorEnable(rfvStartTime, true);
                ValidatorEnable(rfvEndTime, true);
                $j(".specialtimespan").show();
                rfvStartTime.style.display = 'none';
                rfvEndTime.style.display = 'none';
            }
            else {
                ValidatorEnable(rfvStartTime, false);
                ValidatorEnable(rfvEndTime, false);
                $j(".specialtimespan").hide();
            }
        }
        else {
            $j(".special").hide();
            $j(".specialtimespan").hide();
            //ValidatorEnable(rfvListDays, false);
            ValidatorEnable(rfvTimeSpan, false);
            ValidatorEnable(rfvStartTime, false);
            ValidatorEnable(rfvEndTime, false);
        }

        /**************** Is Special Product? Yes No Change ******************/
        $j("#txtYes").change(function () {
            if ($j(this).is(":checked")) {
                ValidatorEnable(rfvTimeSpan, true);
                ValidatorEnable(rfvStartTime, true);
                ValidatorEnable(rfvEndTime, true);
                $j(".special").show();
                $j(".specialtimespan").hide();
                rfvTimeSpan.style.display = 'none';
                rfvStartTime.style.display = 'none';
                rfvEndTime.style.display = 'none';
            }
        });
        $j("#txtNo").change(function () {
            if ($j(this).is(":checked")) {
                ValidatorEnable(rfvTimeSpan, false);
                ValidatorEnable(rfvStartTime, false);
                ValidatorEnable(rfvEndTime, false);
                $j(".special").hide();
                $j(".specialtimespan").hide();
                
            }
        });


        /******************** Display Time Span Change ************************/
        $j("#ctl00_ContentPlaceHolder1_rblTimeSpan_0").change(function () {
            if ($j(this).is(":checked")) {
                ValidatorEnable(rfvStartTime, false);
                ValidatorEnable(rfvEndTime, false);
                $j(".specialtimespan").hide();
            }
        });
        $j("#ctl00_ContentPlaceHolder1_rblTimeSpan_1").change(function () {
            if ($j(this).is(":checked")) {
                ValidatorEnable(rfvStartTime, true);
                ValidatorEnable(rfvEndTime, true);
                $j(".specialtimespan").show();
                rfvStartTime.style.display = 'none';
                rfvEndTime.style.display = 'none';
            }
        });
    }
    
}

function SingleProductToppingSelection(selectedSize, obj) {
    
    if (selectedSize != 4) {
        var id = $j(obj).parent().find('span')[0].innerText;
        var name = $j(obj).parent().find('input[type="hidden"]')[0].name;
        document.getElementsByName(name)[0].value = selectedSize;
        document.getElementById('Id').value = id;
    }
    else {
        var id = $j(obj).parent().find('span')[0].innerText;
        var name = $j(obj).parent().find('input[type="hidden"]')[1].name;
        if (document.getElementsByName(name)[0].value == "0")
            document.getElementsByName(name)[0].value = "1";
        else
            document.getElementsByName(name)[0].value = "0";
        if (document.getElementById('Id'))
            document.getElementById('Id').value = id;
    }
}

function PostEncryptedData() {
    if (Page_IsValid) {
        // Encrypt with the public key...
        
        var RSA = new JSEncrypt();
        RSA.setPublicKey($j('#PublicKey').val());

        var transactionData = $j('#txtName').val() + '|' + $j('#txtCreditCardNumber').val() + '|'
            + $j('#txtCreditExpiryMonth').val() + '|' + $j('#txtCreditExpiryYear').val()
            + '|' + $j('#txtCVV').val() + '|' + $j('#txtUserEmail').val() + '|'
            + $j('#txtRecepientEmail').val() + '|' + $j('#txtAmount').val();

        // NOTE: RSA 2048bit CAN ONLY ENCRYPT 256-11 BYTE BLOCK DATA.
        // IF YOU WANT TO ADD MORE DATA, CREATE SEPARATE BLOCK AND PROCESS THEM SEPARATELY.
        // http://www.daniweb.com/software-development/java/threads/231092/java-encryption-rsa-block-size
        var ciphered = RSA.encrypt(transactionData);

        $j('#EncryptedFormData').val(ciphered);

        transactionData = $j('#txtAddress').val();
        ciphered = RSA.encrypt(transactionData);
        
        $j('#EncryptedBillingAddress').val(ciphered);
        

        //var cipheredText = RSA.encrypt($j('#txtCreditCardNumber').val());
        //$j('#EncrCreditCard').val(cipheredText);
        //cipheredText = RSA.encrypt($j('#txtCreditExpiry').val());
        //$j('#EncrCardExpiry').val(cipheredText);
        //cipheredText = RSA.encrypt($j('#txtCVV').val());
        //$j('#EncrCvv').val(cipheredText);
        //cipheredText = RSA.encrypt($j('#txtName').val());
        //$j('#EncrName').val(cipheredText);
        //cipheredText = RSA.encrypt($j('#txtUserEmail').val());
        //$j('#UserEmail').val(cipheredText);
        //cipheredText = RSA.encrypt($j('#txtRecepientEmail').val());
        //$j('#ReceiverEmail').val(cipheredText);
        //cipheredText = RSA.encrypt($j('#txtAmount').val());
        //$j('#Amount').val(cipheredText);


        clearAllFields();
        
        buttonClick('SubmitButton');
    }
}

function clearAllFields() {
    $j('input[type="text"], textarea').val('');
}

function SetTabIndex(tabIndex) {
    document.getElementById('TabIndex').value = index;
}


// -----------------------------------------------------------------------------------
// http://wowslider.com/
// JavaScript Wow Slider is a free software that helps you easily generate delicious 
// slideshows with gorgeous transition effects, in a few clicks without writing a single line of code.
// Generated by WOW Slider 5.2
//
//***********************************************
// Obfuscated by Javascript Obfuscator
// http://javascript-source.com
//***********************************************
function ws_blinds(c, b, a) { var g = jQuery; var e = c.parts || 3; var f = g("<div>"); f.css({ position: "absolute", width: "100%", height: "100%", left: 0, top: 0, "z-index": 8 }).hide().appendTo(a); var h = []; for (var d = 0; d < e; d++) { h[d] = g("<div>").css({ position: "absolute", height: "100%", width: Math.ceil(100 / e) + 1 + "%", border: "none", margin: 0, overflow: "hidden", top: 0, left: Math.round(100 * d / e) + "%" }).appendTo(f) } this.go = function (m, p, j) { var l = p > m ? 1 : 0; if (j) { if (j <= -1) { m = (p + 1) % b.length; l = 0 } else { if (j >= 1) { m = (p - 1 + b.length) % b.length; l = 1 } else { return -1 } } } f.find("img").stop(true, true); f.show(); var o = g("ul", a); if (c.fadeOut) { o.fadeOut((1 - 1 / e) * c.duration) } for (var n = 0; n < h.length; n++) { var k = h[n]; g(b.get(m)).clone().css({ position: "absolute", top: 0, left: (!l ? (-f.width()) : (f.width() - k.position().left)) + "px", width: "auto", height: "100%", transform: "translate3d(0,0,0)" }).appendTo(k).animate({ left: -k.position().left + "px" }, (c.duration / (h.length + 1)) * (l ? (h.length - n + 1) : (n + 2)), ((!l && n == h.length - 1 || l && !n) ? function () { o.css({ left: -m + "00%" }).stop(true, true).show(); f.hide().find("img").remove() } : null)) } return m } };// -----------------------------------------------------------------------------------
// http://wowslider.com/
// JavaScript Wow Slider is a free software that helps you easily generate delicious 
// slideshows with gorgeous transition effects, in a few clicks without writing a single line of code.
// Generated by WOW Slider 5.2
//
//***********************************************
// Obfuscated by Javascript Obfuscator
// http://javascript-source.com
//***********************************************
