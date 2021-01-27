
window.onload = (function () {
    //var url = window.location.pathname,
    //    urlRegExp = new RegExp(url.replace(/\/$/, '') + "$"); // create regexp to match current url pathname and remove trailing slash if present as it could collide with the link in navigation in case trailing slash wasn't present there
    //// now grab every link from the navigation
    //$("ul li a").each(function () {
    //    // and test its normalized href against the url pathname regexp
    //    if (urlRegExp.test(this.href.replace(/\/$/, ''))) {
    //        $(this.parentNode).addClass('active');
    //        $(this.parentNode.parentNode.parentNode).addClass('active');
    //    } else {
    //        $(this.parentNode).removeClass('active');
    //    }
    //});
    //$.ajax({
    //    url: '/home/coinmarketcap',
    //    type: 'POST',
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    success: function (result) {
    //        $("#priceltc").append("$" + result.priceLTC);
    //        $("#priceeth").append("$" + result.priceETH);
    //        $("#pricebtc").append("$" + result.priceBTC);
    //        $("#pricexlm").append("$" + result.priceXLM);
    //        $("#pricexrp").append("$" + result.priceXRP);
    //    }
    //});
});

function selectwallettype() {
    var active = $('#wallet-active-code').val();
    if (active == 1) {
        $("#label-wallet-select").text("BTC");
    } else if (active == 2) {
        $("#label-wallet-select").text("ETH");
    }
    $('#drop-deposit').on('change', function () {
        var de = $('#drop-deposit').val();
        if (de == 1) {
            $("#show-image-code").attr("src", $('#wallet-btc-code').val());
            $("#urllinkintroduction").val($('#wallet-btc-address').val());
            $("#label-wallet-select").text("BTC");
            $("#btnurllinkintroduction").text("Copy Link");

        } else if (de == 2) {
            $("#show-image-code").attr("src", $('#wallet-eth-code').val());
            $("#urllinkintroduction").val($('#wallet-eth-address').val());
            $("#label-wallet-select").text("ETH");
            $("#btnurllinkintroduction").text("Copy Link");
        }
        $('#drop-deposit select').val(de);
    });
}

$(document).ready(function () {
    //$('#withdraw-wallet-name').on('change', function () {
    //    selectwallettype();
    //});
    //$('.progress .progress-bar').css("width",
    //    function () {
    //        return $(this).attr("aria-valuenow") + "%";
    //    }
    //);
    //$.ajax({
    //    url: '/office/coinmarketcap',
    //    type: 'POST',
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    success: function (result) {
    //        $("#currency-data").html(result);
    //    },
    //    error: function () {

    //    }
    //});
    $.ajax({
        url: '/office/UserAssets',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            var _totalBalance = 0;

            $("#total-balance").text("").append("$" + result.totalbalance);
            $("#total-trade").text("").append("$" + result.totalTrade);
        }
    });
});