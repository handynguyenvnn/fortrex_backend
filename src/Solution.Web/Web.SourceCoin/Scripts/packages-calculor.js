function getDetailPackages() {
    var inputValue = $('#txt-amount').val();
    if (inputValue == "") {
        inputValue = "0";
    }
    var type = $('#quote-request-hear').val();
    var number = parseFloat(inputValue);

    var minValue = parseFloat($('#hdhMinAmountBTC').val());
    if (type == "eth") {
        minValue = parseFloat($('#hdhMinAmountETH').val())
    }

    if (number < minValue) {
        $('#sm-lable-amount').addClass("text-red");
    } else {
        $("#sm-lable-amount").removeClass("text-red");
    }
    if (number == NaN) {
        $('#txt-amount').val("0");
    } else {
        var data = { "amount": number , "type" : type};
        $.ajax({
            url: '/home/packages',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result != null) {
                    var str = '';
                    if (type == "btc") {
                        str = '<div class="col-md-2 res-m-bttm-sm"><p>Amount invested</p><h4 class="text-orange">' + result.PriceFrom + '</h4><p>BTC</p></div><div class="col-md-2 res-m-bttm-sm"><p>You receive</p><h4 class="text-orange">' + result.PriceTo + '</h4><p>BTC</p></div>';
                        str = str + '<div class="col-md-2 res-m-bttm-sm"><p>Hash Power </p><h4 class="text-orange">' + result.Name + '</h4><p>GH/s</p></div><div class="col-md-2 res-m-bttm-sm"><p>Per/day</p><h4 class="text-orange">' + result.PercentOnDay + '%</h4><p>Percent</p></div>';
                        str = str + '<div class="col-md-2 res-m-bttm-sm"><p>Total Get</p><h4 class="text-orange">' + result.PercentTotal + '%</h4><p>Percent</p></div><div class="col-md-2 res-m-bttm-sm"><p>Pay back after</p><h4 class="text-orange">' + result.FinishDay + '</h4><p>Days</p></div>';
                    } else if (type == "eth") {
                        str = '<div class="col-md-2 res-m-bttm-sm"><p>Amount invested</p><h4 class="text-orange">' + result.PriceFrom + '</h4><p>ETH</p></div><div class="col-md-2 res-m-bttm-sm"><p>You receive</p><h4 class="text-orange">' + result.PriceTo + '</h4><p>ETH</p></div>';
                        str = str + '<div class="col-md-2 res-m-bttm-sm"><p>Hash Power </p><h4 class="text-orange">' + result.Name + '</h4><p>GH/s</p></div><div class="col-md-2 res-m-bttm-sm"><p>Per/day</p><h4 class="text-orange">' + result.PercentOnDay + '%</h4><p>Percent</p></div>';
                        str = str + '<div class="col-md-2 res-m-bttm-sm"><p>Total Get</p><h4 class="text-orange">' + result.PercentTotal + '%</h4><p>Percent</p></div><div class="col-md-2 res-m-bttm-sm"><p>Pay back after</p><h4 class="text-orange">' + result.FinishDay + '</h4><p>Days</p></div>';
                    }
                    $("#col-detail-packages").html(str);
                } else {
                    SetDefaultPackages(type);
                }
            },
            error: function () {
                SetDefaultPackages(type);

            }
        });
    }
}
function SetDefaultPackages(type) {
    var str2 = "";
    str2 = '<div class="col-md-2 res-m-bttm-sm"><p>Amount invested</p><h4 class="text-orange">0</h4><p>' + type.toUpperCase() + '</p></div><div class="col-md-2 res-m-bttm-sm"><p>You receive</p><h4 class="text-orange">0</h4><p>' + type.toUpperCase() + '</p></div>';
    str2 = str2 + '<div class="col-md-2 res-m-bttm-sm"><p>Hash Power </p><h4 class="text-orange">0</h4><p>GH/s</p></div><div class="col-md-2 res-m-bttm-sm"><p>Per/day</p><h4 class="text-orange">0%</h4><p>Percent</p></div>';
    str2 = str2 + '<div class="col-md-2 res-m-bttm-sm"><p>Total Get</p><h4 class="text-orange">0%</h4><p>Percent</p></div><div class="col-md-2 res-m-bttm-sm"><p>Pay back after</p><h4 class="text-orange">0</h4><p>Days</p></div>';
    $("#col-detail-packages").html(str2);
}
$(document).ready(function () {
    $("#txt-amount").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $("#txt-amount").keyup(function () {
        
        if ($('#txt-amount').val().indexOf(',') > 0) {
            $('#txt-amount').val($('#txt-amount').val().replace(',', '.'));
        }
        getDetailPackages();
    });
   
    $("#quote-request-hear").change(function () {
        var type = $('#quote-request-hear').val();
        if (type == "btc") {
            $('#min-value-mining').text($('#hdhMinAmountBTC').val() + " BTC");
        } else if (type == "eth") {
            $('#min-value-mining').text($('#hdhMinAmountETH').val() + " ETH")
        }

        getDetailPackages();
    });
    // promotion
    var min_value_btc = $('#min-value-btc').val();
    var min_value_eth = $('#min-value-eth').val();
    var promocode = $('#promocode').val();
    var total_days = $('#total-days').val();
    var received_btc = $('#received-btc').val();
    var received_eth = $('#received-eth').val();
    $("#quote-request-hear-promotion").change(function () {
        var type = $('#quote-request-hear-promotion').val();
        $('#lbl-condition-promotion').empty();
        if (type == "btc") {
            $('#lbl-condition-promotion').append("Investment conditions: Balance greater than or equal to " + min_value_btc + " BTC");
        } else if (type == "eth") {
            $('#lbl-condition-promotion').append("Investment conditions: Balance greater than or equal to " + min_value_eth + " ETH")
        }
        getDetailPromotion();
    });
    // promotion
    function getDetailPromotion() {
        var type = $('#quote-request-hear-promotion').val();
        var promotioncode = $('#txt-amount-promotion').val();
        if (promotioncode == promocode) {
            if (type == "eth") {
                GeneralMessage('MsgAlert', 'Congratulations, you are eligible to participate in this special investment promotion. <b>Investment ' + min_value_eth + ' ETH, ' + total_days + ' days after receiving ' + received_eth + ' ETH</b>', "success");
            } else {
                GeneralMessage('MsgAlert', 'Congratulations, you are eligible to participate in this special investment promotion. <b>Investment ' + min_value_btc + ' BTC, ' + total_days + ' days after receiving ' + received_btc + ' BTC</b>', "success");
            }
        } else {
            GeneralMessage('MsgAlert', '#Promotion code is incorrect', "warning");
        }
    }
    $("#txt-amount-promotion").keyup(function () {
        getDetailPromotion();
    });

    $('#btn-BuySell-confirm-promotion').click(function () {
        $('#btn-BuySell-confirm-promotion').text('Waiting...');
        var data = {
            'type': $('#quote-request-hear-promotion').val(),
            'code': $('#txt-amount-promotion').val()
        };
        if (data['code'].length > 0) {            
            $.ajax({
                url: '/office/promocode',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    CommonHelper.AjaxLoading();
                },
                success: function (result) {
                    CommonHelper.StopAjaxLoading();
                    $('#btn-BuySell-confirm-promotion').html('Invest Now');
                    GeneralMessage('MsgAlert', result.Message, result.ClassCss);
                },
                error: function () {
                    CommonHelper.StopAjaxLoading();
                    $('#btn-BuySell-confirm-promotion').html('Invest Now');
                }
            });
        } else {
            $('#btn-BuySell-confirm-promotion').html('Invest Now');
        }
    });
});
window.onload = getDetailPackages();