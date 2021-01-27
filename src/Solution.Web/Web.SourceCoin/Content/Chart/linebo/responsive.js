// new vesion(er)
var template = {
    m: "<div class='frm-order' style='padding-right: 5px;'><div class='buy-sell-widget'><form method='post' name='myform' class='currency_validate'><div class='form-group fill-amount notranslate'><label class='mr-sm-2'>Time</label><div class='form-control download-timer'><i class='fa fa-clock-o mr-1' aria-hidden='true' style='font-size:16px;color:#888'></i><span id='id-download-timer'></span></div></div><div class='form-group fill-amount gr-flex'><label class='tex-left'>Amount</label><div class='mouse_scroll' style='left:75px;'><span class='m_scroll_arrows unu'></span><span class='m_scroll_arrows doi'></span><span class='m_scroll_arrows trei'></span></div><a class='fill-amount-max'>Max</a><div class='form-control download-money'><i class='fa fa-dollar icon-price-m' aria-hidden='true' style='font-size:16px;color:#888'></i><input type='text' class='form-control order-amount' name='currency_amount' id='order_amount' autocomplete='off' placeholder='10 USD' value='10'></div><ul id='list_ calc'><li id='fillamountplus' trade-act='plus' onmousedown='buildData._mousedown(2)' onmouseup='buildData._mouseup()'>+</li><li id='fillamountminus' trade-act='minus' onmousedown='buildData._mousedown(-1)' onmouseup='buildData._mouseup()'>-</li></ul></div></form></div><div class='d-grid justify-content-between align-items-center text-center frm-order-profit'><div class='profit-form-w1 d-grid justify-content-between align-items-center text-center frm-order-profit pl-2'><h6 class='mb-1 pl-4'>Profit</h6><h1 class='value-profit mb-1 pr-4'><span id='value-profit'></span></h1></div><div class='profit-form-w3'></div><div class='profit-form-w1 d-grid justify-content-between align-items-center text-center frm-order-profit pl-2'><img class='img-fluid mb-1 pl-3' src='/Images/Icon/crown24.png' alt='info-image'><h4 class='value-temp mb-1 pr-5'><span id='value-temp'></span></h4></div></div><div id='frm-order-button' class='d-grid justify-content-between align-items-center text-center mouseevent'><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeBuy()' onmouseout='buildfunction._hoverRangeRemove()'><button id='btn-user-call-event' class='btn btn-buy text-center' onclick=buildData._pushOrder('BUY')><img src='/Images/Icon/call.png' />HIGHER</button></div><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeSell()' onmouseout='buildfunction._hoverRangeRemove()'><button id='btn-user-put-event' class='btn btn-sell text-center' onclick=buildData._pushOrder('SELL')> <img src='/Images/Icon/put.png' />LOWER</button></div></div></div>",
    t: "<div class='frm-order d-flex frm-landscape' style='padding-right: 5px;'><div class='profit-form-t-w1 mt-2 text-center'><h5 class='mb-0'>Profit</h5><h4 class='value-profit mb-0 pb-1 pt-2'><span id='value-profit'></span></h4></div><div class='profit-form-t-w2 mt-1 text-center'><img class='img-fluid' src='/Images/Icon/crown24.png' alt='info-image'><h4 class='value-temp mb-0 pb-2 pt-1'><span id='value-temp'></span></h4></div><div class='buy-sell-widget buy-sell-widge-wt'><form method='post' name='myform' class='currency_validate'><div class='form-group fill-amount notranslate'><label class='mr-sm-2'>Time</label><div class='form-control download-timer'><i class='fa fa-clock-o' aria-hidden='true' style='font-size:16px;color:#888'></i><span class='ml-1' id='id-download-timer'></span></div></div><div class='form-group fill-amount gr-flex'><label class='tex-left'>Amount</label><div class='mouse_scroll' style='left:67px;'><span class='m_scroll_arrows unu'></span><span class='m_scroll_arrows doi'></span><span class='m_scroll_arrows trei'></span></div><a class='fill-amount-max'>Max</a><div class='form-control download-money'><i class='fa fa-dollar icon-price-t' aria-hidden='true' style='font-size:16px;color:#888'></i><input type='text' class='form-control order-amount' name='currency_amount' id='order_amount' autocomplete='off' placeholder='10 USD' value='10'></div><ul><li id='fillamountplus' trade-act='plus' onclick='buildData._fillOrder(2)'>+</li><li id='fillamountminus' trade-act='minus' onclick='buildData._fillOrder(-1)'>-</li></ul></div></form></div><div id='frm-order-button' class='d-flex justify-content-between align-items-center text-center g-order'><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeBuy()'><button id='btn-user-call-event' class='btn btn-buy text-center' onclick=buildData._pushOrder('BUY')><img src='/Images/Icon/call.png' />HIGHER</button></div><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeSell()'><button id='btn-user-put-event' class='btn btn-sell text-center' onclick=buildData._pushOrder('SELL')><img src='/Images/Icon/put.png' />LOWER</button></div></div></div>",
    d: "<div class='frm-order' style='padding-right: 5px;'><div class='buy-sell-widget'><form method='post' name='myform' class='currency_validate'><div class='form-group fill-amount notranslate'><label class='mr-sm-2'>Time</label><div class='form-control download-timer'><i class='fa fa-clock-o' aria-hidden='true' style='font-size:16px;color:#888'></i><span class='ml-1' id='id-download-timer'></span></div></div><div class='form-group fill-amount gr-flex'><label class='tex-left'>Amount</label><div class='mouse_scroll'><span class='m_scroll_arrows unu'></span><span class='m_scroll_arrows doi'></span><span class='m_scroll_arrows trei'></span></div><a class='fill-amount-max'>Max</a><div class='form-control download-money'> <i class='fa fa-dollar icon-price-d' aria-hidden='true' style='font-size:16px;color:#888'></i><input type='text' class='form-control order-amount' name='currency_amount' id='order_amount' autocomplete='off' placeholder='10 USD' value='10'></div><ul id='list_calc'><li id='fillamountminus' trade-act='minus' onmousedown='buildData._mousedown(-1)' onmouseup='buildData._mouseup()'>-</li><li id='fillamountplus' trade-act='plus' onmousedown='buildData._mousedown(2)' onmouseup='buildData._mouseup()'>+</li></ul></div></form></div><div class='d-grid justify-content-between align-items-center text-center frm-order-profit'><h6>Profit</h6><h1 class='value-profit'><span id='value-profit'></span></h1><h2 class='value-temp'><img class='img-fluid mb-1 pr-2' src='/Images/Icon/crown24.png' alt='info-image'><span id='value-temp'></span></h2></div><div id='frm-order-button' class='d-grid justify-content-between align-items-center text-center mouseevent'><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeBuy()' onmouseout='buildfunction._hoverRangeRemove()'> <button id='btn-user-call-event' class='btn btn-buy text-center' onclick=buildData._pushOrder('BUY')> <img src='/Images/Icon/call.png' />HIGHER</button></div><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeSell()' onmouseout='buildfunction._hoverRangeRemove()'><button id='btn-user-put-event' class='btn btn-sell text-center' onclick=buildData._pushOrder('SELL')><img src='/Images/Icon/put.png' />LOWER</button></div></div></div>"
};

// Old vesion(1)
//var template = {
//    m: "<div class='frm-order' style='padding-right: 5px;'><div class='buy-sell-widget'><form method='post' name='myform' class='currency_validate'><div class='form-group fill-amount notranslate'> <label class='mr-sm-2'>Time</label><div class='form-control download-timer'> <i class='fa fa-clock-o' aria-hidden='true' style='font-size:16px;color:#888'></i> <span id='id-download-timer'>00 : 00</span></div></div><div class='form-group fill-amount gr-flex'><label class='mr-sm-2'> Amount</label><input type='number' class='form-control' name='currency_amount' value='10' id='order_amount' placeholder='100 USD'><a class='fill-amount-max'>Max</a><ul id='list_ calc'><li id='fillamountplus' trade-act='plus' onmousedown='buildData._mousedown(2)' onmouseup='buildData._mouseup()'>+</li><li id='fillamountminus' trade-act='minus' onmousedown='buildData._mousedown(-1)' onmouseup='buildData._mouseup()'>-</li></ul></div></form></div><div class='d-grid justify-content-between align-items-center text-center frm-order-profit'><h6>Profit</h6><h1 class='value-profit'><span id='value-profit'>+95</span><span class='profit-per'>%</span></h1></div><div id='frm-order-button' class='d-grid justify-content-between align-items-center text-center mouseevent'><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeBuy()' onmouseout='buildfunction._hoverRangeRemove()'> <button id='btn-user-call-event' class='btn btn-buy text-center' onclick=buildData._pushOrder('BUY')> <img src='/Images/Icon/call.png' /> HIGHER </button></div><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeSell()' onmouseout='buildfunction._hoverRangeRemove()'> <button id='btn-user-put-event' class='btn btn-sell text-center' onclick=buildData._pushOrder('SELL')> <img src='/Images/Icon/put.png' /> LOWER </button></div></div></div>",
//    t: "<div class='frm-order d-flex frm-landscape' style='padding-right: 5px;'><div class='d-grid justify-content-between align-items-center text-center frm-order-profit'><h6>Profit</h6><h1 class='value-profit'><span id='value-profit'>+95</span><span class='profit-per'>%</span></h1></div><div class='buy-sell-widget'><form method='post' name='myform' class='currency_validate'><div class='form-group fill-amount'> <label class='mr-sm-2'>Time</label><div id='id-download-timer' class='form-control download-timer'>00 : 00</div></div><div class='form-group fill-amount gr-flex'> <label class='mr-sm-2'>Amount</label> <input type='number' name='currency_amount' value='10' id='order_amount' class='form-control' placeholder='100 USD'> <a class='fill-amount-max'>Max</a><ul><li id='fillamountplus' trade-act='plus' onclick='buildData._fillOrder(2)'>+</li><li id='fillamountminus' trade-act='minus' onclick='buildData._fillOrder(-1)'>-</li></ul></div></form></div><div id='frm-order-button' class='d-flex justify-content-between align-items-center text-center g-order'><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeBuy()'> <button id='btn-user-call-event' class='btn btn-buy text-center' onclick=buildData._pushOrder('BUY')> <img src='/Images/Icon/call.png' /> HIGHER </button></div><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeSell()'> <button id='btn-user-put-event' class='btn btn-sell text-center' onclick=buildData._pushOrder('SELL')> <img src='/Images/Icon/put.png' /> LOWER </button></div></div></div>",
//    d: "<div class='frm-order' style='padding-right: 5px;'><div class='buy-sell-widget'><form method='post' name='myform' class='currency_validate'><div class='form-group fill-amount notranslate'> <label class='mr-sm-2'>Time</label><div class='form-control download-timer'> <i class='fa fa-clock-o' aria-hidden='true' style='font-size:16px;color:#888'></i> <span id='id-download-timer'>00 : 00</span></div></div><div class='form-group fill-amount gr-flex'><label class='mr-sm-2'> Amount</label><input type='number' class='form-control' name='currency_amount' value='10' id='order_amount' placeholder='100 USD'><a class='fill-amount-max'>Max</a><ul id='list_ calc'><li id='fillamountminus' trade-act='minus' onmousedown='buildData._mousedown(-1)' onmouseup='buildData._mouseup()'>-</li><li id='fillamountplus' trade-act='plus' onmousedown='buildData._mousedown(2)' onmouseup='buildData._mouseup()'>+</li></ul></div></form></div><div class='d-grid justify-content-between align-items-center text-center frm-order-profit'><h6>Profit</h6><h1 class='value-profit'><span id='value-profit'>+95</span><span class='profit-per'>%</span></h1></div><div id='frm-order-button' class='d-grid justify-content-between align-items-center text-center mouseevent'><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeBuy()' onmouseout='buildfunction._hoverRangeRemove()'> <button id='btn-user-call-event' class='btn btn-buy text-center' onclick=buildData._pushOrder('BUY')> <img src='/Images/Icon/call.png' /> HIGHER </button></div><div class='gr-btn-trade' onmouseover='buildfunction._hoverRangeSell()' onmouseout='buildfunction._hoverRangeRemove()'> <button id='btn-user-put-event' class='btn btn-sell text-center' onclick=buildData._pushOrder('SELL')> <img src='/Images/Icon/put.png' /> LOWER </button></div></div></div>"
//};


window.addEventListener("load", function () {
    window.dispatchEvent(new Event('resize'));
    setResize();
   // setHeightVolume();
   //setHeightVolume();
    //setWidthVolume();
  setHeightTradingInfoMobile();
   //setResponsiveNavMenu()
});
window.onresize = function () {
    setResize();
    //setHeightVolume();
    //setHeightVolume();
    //setWidthVolume();
    setHeightTradingInfoMobile();
   //setResponsiveNavMenu()
};

//function setHeightVolume() {
    
//    setTimeout(function () {
//      let h = buildfunction._chartHeightGet();
//        const botheight = $("#bottomside").height();
//        $(".slider-volume").css('height', h + "px"); 
//    }, 550);
//}
function setHeightTradingInfoMobile() {
    setTimeout(function () {

    const botheight = $("#bottomside").height();
   
        $(".leftside").css('height', "calc(100% - " + botheight +"px)"); 
    }, 400);
}
function setResponsiveNavMenu(){
    setTimeout(function () {
        // get width lolo
        const logoWidth = $("#fblogo").width();
        // get width div
        const divWidthPair = $("#toplist-pair").width();
        $("#toplist-pair").css('margin-left', (divWidthPair-logoWidth) +"px"); 
    }, 500);

}
function setResize() {
    const screenWidth = parseInt(window.innerWidth);
    //heightOutput.textContent = window.innerHeight;
    //widthOutput.textContent = window.innerWidth;
    //$('#id-download-timer2').html(screenWidth);
    if (screenWidth < 415) {
        isMobile = true;
        renderFormOrderMobile();
        buildfunction._setHideByResize(false);
        chart.yAxis[0].update({ labels: { style: { fontSize: "10px" } } });
      
    } else if (screenWidth >= 415 && screenWidth < 1024) {
        //console.log("renderFormOrderLandscape: " + screenWidth);
        isMobile = true;
        renderFormOrderLandscape();
        buildfunction._setHideByResize(false);
        chart.yAxis[0].update({ labels: { style: { fontSize: "10px" } } });
        //buildfunction._hideCharttools();
    }
    else if (screenWidth >= 1024) {
        isMobile = false;
        renderFormOrderDesktop();
        buildfunction._setHideByResize(false);
        chart.yAxis[0].update({ labels: { style: { fontSize: "13px" } } });
    }

}
function renderFormOrderMobile_bak() {
    let mainform = template.d; //document.querySelector('#mainslidetemplate');
    if (mainform.innerHTML.trim() !== "") {
        //document.querySelector('#bottomside').innerHTML = mainform.innerHTML;
        $('#bottomside').empty().append(mainform.innerHTML);
        document.querySelector('#main-action-order').innerHTML = "";
        $('#frm-order-button').removeClass('d-grid').addClass('d-flex');
        //$("#frm-order-button").addClass("d-flex");
        //$('#main-action-order').addClass('d-none');
        $('.fill-amount-max').css('right', '25px');

       // $('#list_ calc ul li:nth-child(2)').css('order', '-1');
    }
    setTimeout(function () {
        let botheight = parseFloat($("#bottomside").css("height").replace('px', '')) + 35;
        buildfunction.chartreflowNow(parseInt(window.innerWidth), parseInt(window.innerHeight - botheight));
    }, 500);
}
function renderFormOrderMobile() {
    let mainform = template.m; //document.querySelector('#mainslidetemplate');
    if (mainform !== "") {
        //document.querySelector('#bottomside').innerHTML = mainform.innerHTML;
        $('#bottomside').empty().append(mainform);
        document.querySelector('#main-action-order').innerHTML = "";
        $('#frm-order-button').removeClass('d-grid').addClass('d-flex');
        //$("#frm-order-button").addClass("d-flex");
        //$('#main-action-order').addClass('d-none');
        $('.fill-amount-max').css('right', '27px');

        //$('.fill-amount ul li:nth-child(2)').css('order', '-1');
        //window.onresize = function () { location.reload(); }
        buildData._callCheckValidNumber();
    }
    setTimeout(function () {
        let botheight = parseFloat($("#bottomside").css("height").replace('px', '')) + 35;
        $("#slider-volume").css("height", "calc(100% - 245px)");
        $("#slider-volume").css("width", "10px");

        buildfunction.chartreflowNow(parseInt(window.innerWidth), parseInt(window.innerHeight - botheight));

    }, 500);

}
function renderFormOrderLandscape() {
    buildfunction.chartreflowNow(parseInt(window.innerWidth), parseInt(window.innerHeight - 100));
    let mainform = template.t;//document.querySelector('#bottomsideipad');
    if (mainform !== "") {
        $('#bottomside').empty().append(mainform);
        document.querySelector('#main-action-order').innerHTML = "";
        // document.querySelector('#bottomsideipad').innerHTML = "";
        $('.fill-amount-max').css('right', '27px');
        $(".mouse_scroll3").addClass('d-none');
        $(".mouse_scroll2").addClass('d-none');
        buildData._callCheckValidNumber();
    }
    setTimeout(function () {
        let botheight = parseFloat($("#bottomside").css("height").replace('px', '')) + 35;
        buildfunction.chartreflowNow(null, parseInt(window.innerHeight - botheight));
        $("#slider-volume").css("height", "calc(100% - 116px)");
        $("#slider-volume").css("width", "10px");

    }, 500);
}
function renderFormOrderDesktop() {
    buildfunction.chartreflowNow(null, parseInt(window.innerHeight - 60));
    let mainform = template.d;//document.querySelector('#mainslidetemplate');
    document.querySelector('#main-action-order').innerHTML = mainform;
    // check validate and add value_temp profit when input number
   
    buildData._callCheckValidNumber();
}

    //function setInputFilter(textbox, inputFilter) {
    //    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
    //        textbox.addEventListener(event, function () {
    //            if (inputFilter(this.value)) {
    //                this.oldValue = this.value;
    //                this.oldSelectionStart = this.selectionStart;
    //                this.oldSelectionEnd = this.selectionEnd;
    //                //show value
    //            } else if (this.hasOwnProperty("oldValue")) {
    //                this.value = this.oldValue;
    //                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
    //            } else {
    //                this.value = "";
    //            }

    //        });
    //    });
    //}

    ////check validate input (limited )
    //setInputFilter(document.getElementById("order_amount"), function (value) {
    //    return /^\d?\d*[.,]?\d{0,4}$/.test(value);
    //});








