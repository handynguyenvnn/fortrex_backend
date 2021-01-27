var hovertrend = "";
var sym = window.sym;
var secondnow = 0;
var lastCandles = [];
var spush = true;
var value_profit = 0;
var pairconfig = {
    pairname: "",
    profit: 0,
    decimal: 4
};
let statushidecontroll = true;
var isMobile = false;
var arrorderline = [];

const charttemplate = {
    cr: {
        "BTC_USD": {
            "minRange": 5,
            "maxRange": 1000,
            "range": 60000,
            "profit": 65
        },
        "ETH_USD": {
            "minRange": 0.5,
            "maxRange": 100,
            "range": 60000,
            "profit": 70
        },
        "ADA_USD": {
            "minRange": 0.001,
            "maxRange": 0.025,
            "range": 60000,
            "profit": 85
        },
        "DASH_USD": {
            "minRange": 1.5,
            "maxRange": 60,
            "range": 60000,
            "profit": 75
        },
        "BCH_USD": {
            "minRange": 0.5,
            "maxRange": 80,
            "range": 60000,
            "profit": 75
        },
        "BNB_USD": {
            "minRange": 0,
            "maxRange": 18,
            "range": 60000,
            "profit": 75
        },
        "XLM_USD": {
            "minRange": 0.0012,
            "maxRange": 0.025,
            "range": 60000,
            "profit": 75
        },
        "ETH_BTC": {
            "minRange": 0,
            "maxRange": 0.0125,
            "range": 60000,
            "profit": 75
        },
        "BNB_BTC": {
            "minRange": 0.00004,
            "maxRange": 0.00125,
            "range": 60000,
            "profit": 75
        },
        "AUD_CAD": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 85
        },
        "CAD_JPY": {
            "minRange": 0,
            "maxRange": 60,
            "range": 60000,
            "profit": 90
        },
        "EUR_AUD": {
            "minRange": 0,
            "maxRange": 0.4,
            "range": 60000,
            "profit": 85
        },
        "EUR_CHF": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 85
        },
        "EUR_GBP": {
            "minRange": 0,
            "maxRange": 0.25,
            "range": 60000,
            "profit": 80
        },
        "EUR_JPY": {
            "minRange": 0.5,
            "maxRange": 80,
            "range": 60000,
            "profit": 80
        },
        "EUR_USD": {
            "minRange": 0,
            "maxRange": 0.25,
            "range": 60000,
            "profit": 80
        },
        "GBP_AUD": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 85
        },
        "GBP_CHF": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 85
        },
        "GBP_JPY": {
            "minRange": 0.1,
            "maxRange": 80,
            "range": 60000,
            "profit": 85
        },
        "GBP_USD": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 85
        },
        "GBP_CAD": {
            "minRange": 0,
            "maxRange": 0.25,
            "range": 60000,
            "profit": 80
        },
        "USD_AUD": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 85
        },
        "USD_CAD": {
            "minRange": 0,
            "maxRange": 0.3,
            "range": 60000,
            "profit": 80
        },
        "USD_JPY": {
            "minRange": 0.2,
            "maxRange": 50,
            "range": 60000,
            "profit": 85
        }
    }
};
Math.easeOutBounce = function (pos) {
    if ((pos) < (1 / 2.75)) {
        return (7.5625 * pos * pos);
    }
    if (pos < (2 / 2.75)) {
        return (7.5625 * (pos -= (1.5 / 2.75)) * pos + 0.75);
    }
    if (pos < (2.5 / 2.75)) {
        return (7.5625 * (pos -= (2.25 / 2.75)) * pos + 0.9375);
    }
    return (7.5625 * (pos -= (2.625 / 2.75)) * pos + 0.984375);
};
// Round numbers to 2 digits after comma (compare round and floor (1.0002))
function roundToTwo(num) {
    return +(Math.floor(num + "e+3") + "e-3");
}
function chartloadInit() {
    let i = 0;
    setInterval(async () => {
        let series = chart.series[0];
        //let data = [...series.data];
        const resonse = await callRealtimeData();
        if (resonse !== null && resonse !== undefined) {
            const {
                OPEN,
                HIGH,
                LOW,
                CLOSE,
                TIMES,
                VolumeTo,
                LASTTIME
            } = resonse;
            //if (i <= data.length - 1) {
            if (i > 0) {
                if (CLOSE > 0 && TIMES > 0) {
                    //const xAxis = chart.xAxis[0];
                    //buildfunction._generateChart(OPEN, HIGH, LOW, CLOSE);
                    const typecurrentchart = chart.series[0].type;
                    if ("areaspline" === typecurrentchart || "spline" === typecurrentchart) {

                        //series.addPoint([TIMES+1000, CLOSE, HIGH, LOW, CLOSE], true, true, true);
                        //edit to
                        setDataAreaspline(series, LASTTIME, CLOSE, 1);
                        //chart.series[0].drawPoints();
                        //series.chart.redraw(true);
                        chart.series[2].setData([]);
                        chart.series[2].addPoint({
                            x: LASTTIME,
                            y: CLOSE,
                            title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                            useHTML: true
                        }, true, false);

                    } else {
                        //this.series[0].addPoint([TIMES, OPEN, HIGH, LOW, CLOSE], true, true, true);
                        setData(series, TIMES, OPEN, HIGH, LOW, CLOSE, VolumeTo, 5, 'candlestick');
                        chart.series[2].setData([]);
                        chart.series[2].addPoint({
                            x: TIMES,
                            y: CLOSE,
                            title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                            useHTML: true
                        }, true, false);
                    }

                    //this.series[0].addPoint([TIMES, OPEN, HIGH, LOW, CLOSE], true, true,true);

                    //this.series[0].drawPoints();
                    //this.series[0].redraw();

                    //const arr = chart.xAxis[0].plotLinesAndBands;
                    //console.log('plotLine:', arr[0]);
                    //console.log('plotLine value RED:', arr[0].options.value);
                    //console.log('plotLine value GREEN:', arr[1].options.value);
                    //console.log('new TIMES:', TIMES);
                    //if (parseInt(TIMES) == parseInt(arr[0].options.value)) {
                    //    console.log(' 1:', TIMES + " / " + arr[0].options.value);
                    //}
                    //if (parseInt(TIMES) == parseInt(arr[1].options.value)) {
                    //    console.log('2:', TIMES + " / " + arr[1].options.value);
                    //}

                    //chart.xAxis[0].update({ overscroll: 30 * 1000 });

                    //focus trend range
                    //if (hovertrend !== "") {
                    //    if (hovertrend === "buy") {
                    //        buildfunction._hoverRangeBuy();
                    //    } else {
                    //        buildfunction._hoverRangeSell();
                    //    }
                    //}
                }
                i += 1;
            } else {
                i += 1;
                //i = 0;
                ChartInit("");
                //buildfunction._zoomTime('2min', 1);
            }
        }
        //series.setData(data);
    }, 600);
}
var buildfunction = {
     //set minimum value-temp-win with percent profit
    _valueTempWin: function (value_amount) {

        var total_temp = 0;
       // value_amount_fl = (isNaN(value_amount) || value_amount < 0 || value_amount === "") ? 0 : parseFloat(value_amount);
       //value_profit_fl = value_profit / 100;
        //total_temp = (value_amount_fl + (value_amount_fl * value_profit_fl));

        value_profit_fl = value_profit / 100;
        total_temp = (value_amount + (value_amount * value_profit_fl));
        total_temp=roundToTwo(total_temp);
        $("#value-temp").empty().text("+$" + total_temp);
    },
    loading: function () {
        $("#overloading").addClass("over-loading");
        $("#overloading").empty().append("<p class='loader loader-icon'><svg width='30px' height='30px' fill='#4FB95C' viewBox='0 0 30 30'> <circle cx='4' cy='4' r='4'></circle>  <circle cx='4' cy='26' r='4'></circle> <circle cx='26' cy='4' r='4'></circle>  <circle cx='26' cy='26' r='4'></circle> </svg><span>Loading</span></p>");
    },
    loadingError: function () {
        //$("#overloading").addClass("over-loading");
        $("#overloading .loader svg").remove();
        $("#overloading .loader span").empty().append("<span>Error in loading</span><img onclick='buildfunction.changechartbysymbol();' class='i-refresh' src='/images/icon/refresh.svg'/>");
    },
    stopLoading: function () {
        $("#overloading").removeClass("over-loading");
        $("#overloading").empty();
    },
    loadingSlideLeft: function () {
        $("#overloadingslideleft").addClass("over-loading");
        $("#overloadingslideleft").empty().append("<p class='loader loader-icon'><svg width='30px' height='30px' fill='#4FB95C' viewBox='0 0 30 30'> <circle cx='4' cy='4' r='4'></circle>  <circle cx='4' cy='26' r='4'></circle> <circle cx='26' cy='4' r='4'></circle>  <circle cx='26' cy='26' r='4'></circle> </svg><span>Loading</span></p>");
    },
    stopLoadingSlideLeft: function () {
        $("#overloadingslideleft").removeClass("over-loading");
        $("#overloadingslideleft").empty();
    },
    clickaudio: function () {
        let sClick = document.getElementById("clickaudio");
        let playPromise = sClick.play();
        if (playPromise !== undefined) {
            playPromise.then(_ => {

            })
                .catch(error => { });
        }
    },
    soundbookorder: function () {
        let sOrder = document.getElementById("soundbookorder");
        let playPromise = sOrder.play();
        if (playPromise !== undefined) {
            playPromise.then(_ => {

            })
                .catch(error => { });
        }

    },
    soundoverorder: function () {
        let soverOrder = document.getElementById("soundoverorder");
        let playPromise = soverOrder.play();

        if (playPromise !== undefined) {
            playPromise.then(_ => {

            })
                .catch(error => { });
        }

    },
    chartRefresh: function () {
        ChartInit();
    },
    flagRefresh: function () {
        ChartInit();
        chart.series[1].setData([]);
        for (var i = 0; i < arrorderline.length; i++) {
            chart.yAxis[0].removePlotBand("order-line-" + arrorderline[i]);
        }

    },
    chartreflowNow: function (width, height) {
        chart.setSize(width, height);
        //chart.reflowNowManual(width);
        //chart.reflowNowManual();
    },
    changechartbysymbol: function (symbol) {
        //chart.resetZoomButton;
        //chart.xAxis[0].setExtremes(null, null);
        //try {
        //    chart.toolbar.remove('zoom');
        //}
        //catch (e) { }

        //buildfunction.clickaudio();
        $("#tools-tradepair").empty().text(symbol.replace('_', '/'));
        $("#tool-tradingcurent-icon").attr("src", "/images/symbol/flags/" + symbol + ".png");
        chart.series[0].setData([]);
        window.sym = symbol !== null ? symbol : "BTC_USD";
        ChartInit();
        $("#toplist-pair div.active").removeClass('active');
        $("#toplist-pair #toppair-" + symbol).addClass('active');


    },
    updateRange: function (symbol) {
        const conf = _configGet(symbol);
        if (conf !== undefined) {
            chart.yAxis[0].update({
                minRange: conf.minRange,
                maxRange: conf.maxRange
            });
        }
    },
    flagPrice: function (type, curprice = 0) {
        let yLastPrice = 0;
        if (curprice > 0) {
            yLastPrice = curprice;
        } else {
            if (chart.series[0].type === "candlestick") {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1][3];
            } else {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1];
            }
        }

        let xLastTime = chart.series[0].xData[chart.series[0].data.length - 1];
        //let _amounttrade = parseInt($("#order_amount").val(), 0);
        let _amounttrade = parseFloat($("#order_amount").val(), 0);
        let i_order = type === "buy" ? "up" : "down";
        if (_amounttrade > 0) {
            chart.series[1].addPoint({
                x: xLastTime,
                y: yLastPrice,
                title: "<div class='flags-line'><span class='flag-" + type + "'>$" + _amounttrade + "</span><img src='/images/symbol/flags/" + i_order + ".png' style='height: 20px;'></div>",
                useHTML: true,
                //text: "Your order"
            }, true, false);

            // chart.series[1].drawPoints();
            //add horizontal line 
            const yAxis = chart.yAxis[0];
            orderbooksbypricetime(yAxis, type, yLastPrice);

            //const keyLineGreen = "plot-line-2";
            //const keyLineRed = "plot-line-1";
            //xAxis.removePlotLine(keyLineGreen);
            //xAxis.removePlotLine(keyLineRed);
        } else {
            //console.log("error: " + _amounttrade);
        }
        return yLastPrice + "-" + xLastTime;
    },
    _removeOrderbooks: function (valuebook) {
        chart.series[1].data[0].remove(valuebook.split('-')[1], valuebook.split('-')[0]);
        chart.yAxis[0].removePlotBand("order-line-" + valuebook.split('-')[0]);
    },
    _toogleLeftSide: function () {
        buildfunction.clickaudio();
        if (statushidecontroll) {
            $("#rightside").addClass('fullrightside');
            $("#leftside").addClass('hideleftside');
            $(".fbt-leftside-hide-controller").css('margin-left', '0px');
            $(".leftside-close-controller").css('display', 'none');
            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(180deg)');
            setTimeout(function () {
                buildfunction.chartreflowNow(null, null);
            }, 250);
            statushidecontroll = false;
        } else {
            $("#leftside").removeClass('hideleftside');
            $("#rightside").removeClass('fullrightside');


            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(0deg)');
            $(".leftside-close-controller").css('display', 'inherit');
            setTimeout(function () {
                buildfunction.chartreflowNow(null, null);
            }, 250);
            const screenWidth = parseInt(window.innerWidth);
            if (screenWidth <= 375) {
                $(".fbt-leftside-hide-controller").css('margin-left', 'calc(100% - 13px)');
            } else {
                $(".fbt-leftside-hide-controller").css('margin-left', '275px');
            }
            statushidecontroll = true;
        }
        //chart.setSize(null);
    },
    _setHideByResize: function (hideStatus) {
        if (!hideStatus) {
            $("#leftside").addClass('hideleftside');
            $("#rightside").addClass('fullrightside');
            $(".fbt-leftside-hide-controller").css('margin-left', '0px');
            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(180deg)');
            $(".leftside-close-controller").css('display', 'none');
            setTimeout(function () {
                buildfunction.chartreflowNow(null, null);
            }, 250);
            statushidecontroll = false;
        } else {
            $("#leftside").removeClass('hideleftside');
            $("#rightside").removeClass('fullrightside');

            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(0deg)');
            $(".leftside-close-controller").css('display', 'inherit');
            setTimeout(function () {
                buildfunction.chartreflowNow(null, null);
            }, 250);
            const screenWidth = parseInt(window.innerWidth);
            if (screenWidth <= 375) {
                $(".fbt-leftside-hide-controller").css('margin-left', 'calc(100% - 13px)');
            } else {
                $(".fbt-leftside-hide-controller").css('margin-left', '275px');
            }
            statushidecontroll = true;
        }
        //chart.setSize(null);
    },
    _zoomTime: function (valuename, time) {
        //chart.rangeSelector.buttons[time].element.onclick();

        $("#rangeSelectorFocus").text(valuename);
        const xExtremes = chart.xAxis[0].getExtremes();

        let diff = 60000;
        switch (time) {
            case 0:
                diff = 2 * diff;
                break;
            case 1:
                diff = 5 * diff;
                break;
            case 2:
                diff = 15 * diff;
                break;
            case 3:
                diff = 30 * diff;
                break;
            case 4:
                diff = 3 * 60 * diff;
                break;
            case 5:
                diff = 24 * 60 * diff;
                break;
            default:
        }
        chart.xAxis[0].setExtremes(xExtremes.dataMax - diff, xExtremes.dataMax, true);

        //chart.xAxis[0].update(
        //    {
        //        //max: (xExtremes.dataMax + 60000),
        //        overscroll: 60000,
        //    }
        //);
        //buildfunction._zoomRange();
        if (time === 0) {
            if (isMobile) {
                chart.xAxis[0].update({ overscroll: 12 * 1000 });
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0.1,
                            groupPadding: 0.1,
                            //pointWidth: 10,
                            //maxPointWidth: 5
                        },
                        candlestick: {
                            pointPadding: 0.1,
                            groupPadding: 0.1,
                            //pointWidth: 10,
                            //maxPointWidth: 5
                        }
                    }
                });
            } else {
                chart.xAxis[0].update({ overscroll: 10 * 1000 });
            }

        } else if (time === 1) {
            chart.xAxis[0].update({ overscroll: 20 * 1000 });
        }
        else if (time === 2) {
            chart.xAxis[0].update({ overscroll: 120 * 1000 });
        }
        else if (time === 3) {
            chart.xAxis[0].update({ overscroll: 240 * 1000 });
        }
        else if (time === 4) {
            chart.xAxis[0].update({ overscroll: 300 * 1000 });
        }

    },
    _zoomRange: function () {
        let xAxis = chart.xAxis[0];
        let extremes = xAxis.getExtremes();
        const rangtimes = (extremes.userMax - extremes.userMin) / 1000;
        if (isMobile) {
            chart.update({
                plotOptions: {
                    series: {
                        pointPadding: 0.1,
                        groupPadding: 0,
                        pointWidth: 6,
                        maxPointWidth: 8
                    },
                    candlestick: {
                        pointPadding: 0.1,
                        groupPadding: 0,
                        pointWidth: 6,
                        maxPointWidth: 8
                    }
                }
            });
            //const xExtremes = chart.xAxis[0].getExtremes();
            // chart.xAxis[0].update(
            //        {
            //         //max: (xExtremes.dataMax + 60000),
            //         overscroll: 40 * 1000,
            //        }
            //    );
        } else {
            if (rangtimes > 50 && rangtimes <= 120) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0.25,
                            pointWidth: 30,
                            maxPointWidth: 30
                        },
                        candlestick: {
                            pointPadding: 0.25,
                            pointWidth: 30,
                            maxPointWidth: 30
                        }
                    }
                });

            } else if (rangtimes > 120 && rangtimes <= 180) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0.25,
                            //pointWidth: 20,
                            //maxPointWidth: 20
                        },
                        candlestick: {
                            pointPadding: 0.25,
                            //pointWidth: 20,
                            //maxPointWidth: 20
                        }
                    }
                });

            } else if (rangtimes > 180 && rangtimes <= 240) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0.25,
                            pointWidth: 15,
                            maxPointWidth: 15
                        },
                        candlestick: {
                            pointPadding: 0.25,
                            pointWidth: 15,
                            maxPointWidth: 15
                        }
                    }
                });
                //const xExtremes = chart.xAxis[0].getExtremes();
                //chart.xAxis[0].update(
                //    {
                //        //max: (xExtremes.max + 60000),
                //        overscroll: 60 * 1000,
                //    }
                //);
            } else if (rangtimes > 240 && rangtimes <= 300) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0.25,
                            groupPadding: 0,
                            //pointWidth: 10,
                            //maxPointWidth: 10
                        },
                        candlestick: {
                            pointPadding: 0.25,
                            groupPadding: 0,
                            //pointWidth: 10,
                            //maxPointWidth: 10
                        }
                    }
                });

            } else if (rangtimes > 300) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0.1,
                            groupPadding: 0,
                            pointWidth: 5,
                            maxPointWidth: 5
                        },
                        candlestick: {
                            pointPadding: 0.1,
                            groupPadding: 0,
                            pointWidth: 5,
                            maxPointWidth: 5
                        }
                    }
                });
                //const xExtremes = chart.xAxis[0].getExtremes();
                //chart.xAxis[0].update(
                //    {
                //        //max: (xExtremes.max + 60000),
                //        overscroll: 60 * 1000,
                //    }
                //);
            }
        }

    },
    _zoomFocus: function () {
        const xExtremes = chart.xAxis[0].getExtremes();
        let diff = 120000;
        chart.xAxis[0].setExtremes(xExtremes.dataMax - diff, xExtremes.dataMax);

        //buildfunction._zoomRange();
        // chart.xAxis[0].update(
        //    {
        //        //max: (xExtremes.max + 30000),
        //        overscroll: 30 * 1000,
        //    }
        //);
    },
    _serverTime: function () {
        //if (time !== 'undefined') {
        //    _timeRemain = time !== null ? time : 59;
        //} else {
        //    _timeRemain = 59;
        //}
        return $.ajax({
            url: "/ServerTime",
            data: JSON.stringify(),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {

                return result;
            }

        });

    },
    _toppairsadd: function () {
        buildfunction.clickaudio();
        $('#marketsModal').modal('show');
        tradePairAddfavorite();
    },
    _addFavorite: function (pairname) {
        buildfunction.clickaudio();
        let data = {
            'pairname': pairname
        };
        return $.ajax({
            url: "/office/PairName_Favorite_Ins",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('#marketsModal').modal('hide');
                buildfunction.changechartbysymbol(pairname);
                loadPairbyUserInit();

            }
        });
    },
    _deleteFavorite: function (pairname) {
        buildfunction.clickaudio();
        $("#toppair-" + pairname).remove();
        let data = {
            'pairname': pairname
        };
        return $.ajax({
            url: "/office/PairName_Favorite_Del",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {

                loadPairbyUserInit().done(function () {
                    let stickeyPainame = localStorage.getItem("stickeyPainame");
                    if (stickeyPainame.indexOf(",") >= 0) {
                        if (localStorage.getItem("stickeyPainame").substring(0, stickeyPainame.indexOf(",")) !== window.sym) {
                            buildfunction.changechartbysymbol(localStorage.getItem("stickeyPainame").substring(0, stickeyPainame.indexOf(",")));
                        }

                    } else {
                        let getstickeyPainamefirst = localStorage.getItem("stickeyPainame").substring(0, 8);
                        if (getstickeyPainamefirst !== window.sym) {

                            buildfunction.changechartbysymbol(getstickeyPainamefirst);
                        }

                    }
                });

            }
        });
    },
    _showtradePairInit: function () {
        this._setHideByResize(true);
        $("#slide-tradepairs").removeClass('d-none');
        $("#slide-tradinghistory").addClass('d-none');
        $("#tabtradingpair a").addClass('active');
        $("#tabtradinghistory a").removeClass('active');
        $("#slide1").removeClass("fade").addClass('active');
        $("#slide2").removeClass('active').addClass("fade");
        tradePairInit();
    },
    _hideCharttools: function () {
        chart.stockTools.showhideBtn.click();
    },
    _hoverRangeBuy: function () {
        hovertrend = "buy";
        let yAxis = chart.yAxis[0];
        const keyrangeUp = "plot-rangebuysell";
        yAxis.removePlotLine(keyrangeUp);
        let yLastPrice = 0;
        const yitem = chart.series[0].yData[chart.series[0].data.length - 1];
        if (yitem !== undefined) {
            if (chart.series[0].type === "candlestick") {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1][3];
            } else {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1];
            }
            const yAxisExtremes = chart.yAxis[0].getExtremes(); //yAxisExtremes.max

            yAxis.update({
                plotBands: [{ // mark the weekend
                    id: keyrangeUp,
                    //color: '#FCFFC5',
                    className: "plotrangeUp",
                    color: {
                        linearGradient: {
                            x1: 0,
                            x2: 0,
                            y1: 1,
                            y2: 0
                        },
                        stops: [
                            [0.1, 'rgba(0, 128, 0, 1) 100%'],
                            [0.2, 'rgba(0, 128, 0, 0.6) 80%'],
                            [0.5, 'rgba(0, 128, 0, 0.4) 60%'],
                            [0.7, 'rgba(0, 128, 0, 0.3) 40%'],
                            [0.8, 'rgba(0, 128, 0, 0.2) 20%'],
                            [0.9, 'rgba(0, 128, 0, 0) 10%']
                        ]
                    },
                    zindex: 11,
                    from: yLastPrice,
                    to: yAxisExtremes.max
                }]
            });
            // add arrow trend
            let xLastTime = chart.series[0].xData[chart.series[0].data.length - 1];
            chart.series[3].setData([]);
            chart.series[3].addPoint({
                x: xLastTime,
                y: yLastPrice,
                title: "<div class='flags-line'><span class='flag-up '></span><img class='trend' src='/images/symbol/flags/up.png' style='height: 20px;'></div>",
                useHTML: true
            }, true, false);
            hovershowrangetrend(yAxis, "buy", yLastPrice);
            $(".highcharts-crosshair-label .highcharts-label-box").addClass("rangeup");
        }

    },
    _hoverRangeSell: function () {
        hovertrend = "sell";
        let yAxis = chart.yAxis[0];
        const keyrangeUp = "plot-rangebuysell";
        yAxis.removePlotLine(keyrangeUp);
        let yLastPrice = 0;
        const yitem = chart.series[0].yData[chart.series[0].data.length - 1];
        if (yitem !== undefined) {
            if (chart.series[0].type === "candlestick") {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1][3];
            } else {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1];
            }
            const yAxisExtremes = chart.yAxis[0].getExtremes(); //yAxisExtremes.max

            yAxis.update({
                plotBands: [{ // mark the weekend
                    id: keyrangeUp,
                    color: {
                        linearGradient: {
                            x1: 0,
                            x2: 0,
                            y1: 0,
                            y2: 1
                        },
                        stops: [
                            [0.1, 'rgba(255, 0, 0, 1) 50%'],
                            [0.2, 'rgba(255, 0, 0, 0.8) 20%'],
                            [0.5, 'rgba(255, 0, 0, 0.6) 40%'],
                            [0.7, 'rgba(255, 0, 0, 0.4) 60%'],
                            [0.8, 'rgba(255, 0, 0, 0.2) 80%'],
                            [0.9, 'rgba(255, 0, 0, 0) 100%']
                        ]
                    },
                    zindex: 11,
                    from: yLastPrice,
                    to: yAxisExtremes.min
                }]
            });
            // add arrow trend
            let xLastTime = chart.series[0].xData[chart.series[0].data.length - 1];
            chart.series[3].setData([]);
            chart.series[3].addPoint({
                x: xLastTime,
                y: yLastPrice,
                title: "<div class='flags-line'><span class='flag-down'></span><img class='trend' src='/images/symbol/flags/down.png' style='height: 20px;'></div>",
                useHTML: true
            }, true, false);
            hovershowrangetrend(yAxis, "sell", yLastPrice);
            $(".highcharts-crosshair-label .highcharts-label-box").addClass("rangedown");
        }


    },
    _hoverRangeRemove: function () {
        hovertrend = "";
        let yAxis = chart.yAxis[0];
        const keyrangeUp = "plot-rangebuysell";
        yAxis.removePlotLine(keyrangeUp);
        $(".highcharts-crosshair-label .highcharts-label-box.rangedown").removeClass("rangedown");
        $(".highcharts-crosshair-label .highcharts-label-box.rangeup").removeClass("rangeup");

        $(".plotrangeUp").removeClass("plotrangeUp");

        //remove flag trend
        chart.series[3].setData([]);
        //remove line trend
        const keyLinetrend = "line-trend-select";
        yAxis.removePlotLine(keyLinetrend);
    },
    _processing: function (b, s) {
        $(".slider-volume .barr.volume-lower").css("height", s + "%");
        $(".slider-volume .barr.volume-higher").css("height", b + "%");
    },
    _generateChart: function (y, high, low, close) {
        const typecurrentchart = chart.series[0].type;
        if (secondnow % 5 === 0 || secondnow === 59) {
            let lastCandlestick = chart.series[0].data.length > 0 ? chart.series[0].data.slice(-1)[0] : undefined;
            if (lastCandlestick !== undefined) {

                let lasttime = secondnow === 59 ? lastCandlestick.x + 6000 : lastCandlestick.x + 5000;
                chart.series[0].addPoint({
                    x: lasttime,
                    y: close,
                    high: high, // lastCandlestick.high,
                    low: y, //lastCandlestick.y,
                    close: close //lastCandlestick.y
                }, true, true, true);
                if ("candlestick" === typecurrentchart) {
                    chart.series[4].addPoint([lasttime, 0]);
                    chart.series[4].redraw();
                }
                // chart.series[0].redraw(true);

                chart.series[2].setData([]);
                chart.series[2].addPoint({
                    x: lasttime,
                    y: lastCandlestick.y,
                    title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                    useHTML: true
                }, true, false);
            }
        }
    },
    _chartHeightGet: function () {
        const h = chart.options.chart.height;
        return h;
    },
    _setDataRealtime: function (TimeOpen, TimeClose, OPEN, HIGH, LOW, CLOSE, VolumeTo, period) {
        let series = chart.series[0];
        const typecurrentchart = chart.series[0].type;
        if ("areaspline" === typecurrentchart || "spline" === typecurrentchart) {
            setDataAreaspline(series, TimeClose, CLOSE, 1);
            chart.series[2].setData([]);
            chart.series[2].addPoint({
                x: TimeClose,
                y: CLOSE,
                title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                useHTML: true
            }, true, false);

        } else {
            //this.series[0].addPoint([TIMES, OPEN, HIGH, LOW, CLOSE], true, true, true);
            setData(series, TimeOpen, OPEN, HIGH, LOW, CLOSE, VolumeTo, 5, typecurrentchart);
            chart.series[2].setData([]);
            chart.series[2].addPoint({
                x: TimeOpen,
                y: CLOSE,
                title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                useHTML: true
            }, true, false);
        }

    }
};

function _configGet(keyname) {
    return charttemplate.cr[keyname];
    //const jsonpath = "/Content/Chart/linebo/configRange.json?v=5";
    //return $.ajax({
    //    url: jsonpath,
    //    data: JSON.stringify(),
    //    type: "GET",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        result = data[keyname];
    //        return result;
    //    }
    //});
}
let stickeyPainame = localStorage.getItem("stickeyPainame");
if (stickeyPainame !== "" && stickeyPainame !== null) {
    if (stickeyPainame.indexOf(",") >= 0) {
        window.sym = localStorage.getItem("stickeyPainame").substring(0, stickeyPainame.indexOf(","));
    } else {
        const getstickeyPainamefirst = localStorage.getItem("stickeyPainame").substring(0, 8);
        window.sym = getstickeyPainamefirst === "" ? "BTC_USD" : getstickeyPainamefirst;
    }
}

window.addEventListener("load", function () {
    window.buildfunction = buildfunction;
    chartloadInit();
    setInterval(() => {
        tradePairInit();
        // run init value_profit with value_default.
        //buildData._setValueAmountInit();
        var value_amount = document.getElementById("order_amount").value;
        value_amount = (isNaN(value_amount) || value_amount < 0 || value_amount === "" || value_amount === null) ? 0 : parseFloat(value_amount);
       
        buildfunction._valueTempWin(value_amount);
    }, 1000);
    buildfunction._setHideByResize(false);
    buildfunction._hideCharttools();
    loadPairbyUserInit();
    $("#tools-tradepair").empty().text(window.sym.replace('_', '/'));
});

function _serverTime() {
    return $.ajax({
        url: "/serverTime",
        data: JSON.stringify(),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            let settiem = result;
            console.log(settiem);
            return settiem;
        }
    });
}

function orderbooksbypricetime(yAxis, type, valueline) {
    yAxis.addPlotLine({
        value: valueline,
        color: type === "buy" ? "green" : "red",
        dashStyle: 'dash',
        width: 1,
        id: "order-line-" + valueline,
        label: {
            useHTML: true,
            text: "<div class='order-line-price " + type + "'>" + valueline + "</div>",
            verticalAlign: "right",
            textAlign: "center",
            align: "right",
            x: -30
            //x: -16
        }
    });
    arrorderline.push(valueline);
    yAxis.renderLine();
}

function hovershowrangetrend(yAxis, type, valueline) {
    const keyLinetrend = "line-trend-select";
    yAxis.removePlotLine(keyLinetrend);
    yAxis.addPlotLine({
        value: valueline,
        color: type === "buy" ? "green" : "red",
        dashStyle: 'LongDash',
        width: 1,
        id: keyLinetrend,

    });
    arrorderline.push(valueline);
    yAxis.renderLine();
}

function initialXAxisPlotLine(xAxis, valueGreenLine, valueRedLine) {
    const keyLineGreen = "plot-line-2";
    const keyLineRed = "plot-line-1";
    xAxis.removePlotLine(keyLineGreen);
    xAxis.removePlotLine(keyLineRed);
    //   console.log("add line");
    xAxis.addPlotLine({
        value: valueRedLine,
        color: "#DB4931",
        dashStyle: 'solid',
        width: 1.5,
        id: "plot-line-1",
        zIndex: 2,
        label: {
            id: "hsdkjfhdkjsfhdjk",
            useHTML: true,
            text: "<img src='/images/icon/Flag.png'/>",
            verticalAlign: "bottom",
            textAlign: "center",
            y: -10,
            //x: -16
        }
    });
    xAxis.addPlotLine({
        value: valueGreenLine,
        color: "#e4e4e4",
        dashStyle: 'ShortDash',
        width: 1.5,
        id: "plot-line-2",
        zIndex: 2,
        label: {
            useHTML: true,
            text: "<img src='/images/icon/clock.png'/>",
            verticalAlign: "bottom",
            textAlign: "center",
            y: -10
            //x: -16
        }
    });

    xAxis.renderLine();

}

function InitData() {
    var data = {
        pair: window.sym,
        interval: '5s'
    };
    //$("#select-trade").val(window.sym);

    return $.ajax({
        url: "/Trade/Candlesticks",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var ohlc = [],
                volume = [];
            if (result.Result.length > 0) {
                var lasttime = 0;
                for (var i = 0; i < result.Result.length; i++) {
                    var item = result.Result[i];
                    lasttime = item.Times;
                    ohlc.push([
                        item.Times, // the date
                        item.Open, // open
                        item.High, // high
                        item.Low, // low
                        item.Close // close

                    ]);
                    //volume.push([
                    //    item.Times, // the date
                    //    //item.VolumeFrom, // VolumeFrom
                    //    item.VolumeTo //VolumeTo
                    //]);
                }
                //for (var i2 = 0; i2 < 30; i2++) {
                //    ohlc.push([
                //        (lasttime+5000), // the date
                //        null, // open
                //        null, // high
                //        null, // low
                //        null,null // close

                //    ]);
                //    lasttime = lasttime + 5000;
                //}

            }
            return ohlc;
        }

    });

}
//load pair by user name
function loadPairbyUserInit() {
    let data = {};
    return $.ajax({
        url: "/office/Get_PairName_by_UserId",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            let rowsarray = "",
                rows = "";
            if (result.Result.length > 0) {
                $("#toplist-pair").empty();
                let stickeyPainame = [];
                localStorage.setItem("stickeyPainame", []);
                let countitem = result.Result.length;
                for (var i = 0; i < result.Result.length; i++) {
                    const item = result.Result[i];
                    rows = "";
                    if (item.PairName === window.sym) {
                        rows += "<div id='toppair-" + item.PairName + "' class='top-pairs active' >";
                    } else {
                        rows += "<div id='toppair-" + item.PairName + "' class='top-pairs' >";
                    }

                    if (countitem > 1) {
                        rows += "<a onclick=buildfunction._deleteFavorite('" + item.PairName + "')><img src='/Images/symbol/close.svg'/></a>";

                    }
                    rows += "<div class='pair-logo'>";
                    rows += "<img src='/images/symbol/flags/" + item.PairName + ".png'>";
                    rows += "</div>";
                    rows += "<div class='pair-title' onclick=buildfunction.changechartbysymbol('" + item.PairName + "');>";
                    rows += "<div class='pair-name'><span>" + item.PairName.replace('_', '/') + "</span></div>";
                    rows += "<div class='pair-type'><span>Binary</span></div>";
                    rows += "</div>";
                    rows += "</div>";
                    rows += "</div>";

                    $("#toplist-pair").append(rows);
                    stickeyPainame.push(item.PairName);
                }
                let pairadd = "";
                localStorage.setItem("stickeyPainame", stickeyPainame);
                if (countitem >= 1 && countitem < 5) {
                    pairadd = "<div id='top-pairs-add' class='top-pairs-add' data-toggle='modal' data-target='#marketsModal' data-placement='bottom' onclick='buildfunction._toppairsadd()'><svg version='1.1' id='Capa_1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='0 0 31.444 31.444' style='enable-background:new 0 0 31.444 31.444;' xml:space='preserve'><path d='M1.119,16.841c-0.619,0-1.111-0.508-1.111-1.127c0-0.619,0.492-1.111,1.111-1.111h13.475V1.127 C14.595,0.508,15.103,0,15.722,0c0.619,0,1.111,0.508,1.111,1.127v13.476h13.475c0.619,0,1.127,0.492,1.127,1.111 c0,0.619-0.508,1.127-1.127,1.127H16.833v13.476c0,0.619-0.492,1.127-1.111,1.127c-0.619,0-1.127-0.508-1.127-1.127V16.841H1.119z'></path></svg>";
                    pairadd += "</div > ";
                    $("#toplist-pair").append(pairadd);
                } else if (countitem > 5) {
                    pairadd = "<div id='top-pairs-add' class='top-pairs-add ' data-toggle='modal' data-target='#marketsModal' data-placement='bottom' onclick='buildfunction._toppairsadd()'><svg version='1.1' id='Capa_1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='0 0 31.444 31.444' style='enable-background:new 0 0 31.444 31.444;' xml:space='preserve'><path d='M1.119,16.841c-0.619,0-1.111-0.508-1.111-1.127c0-0.619,0.492-1.111,1.111-1.111h13.475V1.127 C14.595,0.508,15.103,0,15.722,0c0.619,0,1.111,0.508,1.111,1.127v13.476h13.475c0.619,0,1.127,0.492,1.127,1.111 c0,0.619-0.508,1.127-1.127,1.127H16.833v13.476c0,0.619-0.492,1.127-1.111,1.127c-0.619,0-1.127-0.508-1.127-1.127V16.841H1.119z'></path></svg>";
                    pairadd += "</div > ";
                    $("#toplist-pair").append(pairadd);
                }

                //pairadd += "<div id='panel-markets' class='dropdown-menu panel-market'>";
                //pairadd += "</div > ";



            }
            //else {
            //    $("#toplist-pair").empty();
            //    let pairadd = "<div id='top-pairs-add' class='top-pairs-add' data-toggle='modal' data-target='#marketsModal' data-placement='bottom' onclick='buildfunction._toppairsadd();'><svg version='1.1' id='Capa_1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='0 0 31.444 31.444' style='enable-background:new 0 0 31.444 31.444;' xml:space='preserve'><path d='M1.119,16.841c-0.619,0-1.111-0.508-1.111-1.127c0-0.619,0.492-1.111,1.111-1.111h13.475V1.127 C14.595,0.508,15.103,0,15.722,0c0.619,0,1.111,0.508,1.111,1.127v13.476h13.475c0.619,0,1.127,0.492,1.127,1.111 c0,0.619-0.508,1.127-1.127,1.127H16.833v13.476c0,0.619-0.492,1.127-1.111,1.127c-0.619,0-1.127-0.508-1.127-1.127V16.841H1.119z'></path></svg>";
            //    //pairadd += "<div id='panel-markets' class='dropdown-menu panel-market'>";
            //    //pairadd += "</div > ";
            //    pairadd += "</div > ";
            //    $("#toplist-pair").append(pairadd);
            //}

        }
    });
}

function tradePairAddfavorite() {
    let data = {

    };
    return $.ajax({
        url: "/Trade/Pairs",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            let rowsarray = "",
                rows = "";
            $("#tbForexmarkets").empty();
            $("#tbmarkets").empty();
            if (result.Result.length > 0) {
                let countitemforex = 0,
                    countitemcrypto = 0;
                let stickeyPainame = localStorage.getItem("stickeyPainame");
                for (var i = 0; i < result.Result.length; i++) {
                    rows = "";
                    const item = result.Result[i];
                    let _pairname = item.PairName.replace('/', '_');
                    if (item.PriceChangePercent < 0) {
                        rows += "<tr onclick=buildfunction._addFavorite('" + _pairname + "') class='assets-down'>";


                    } else if (item.PriceChangePercent > 0) {
                        rows += "<tr onclick=buildfunction._addFavorite('" + _pairname + "') class='assets-up'>";

                    }
                    rows += " <td> <img src='/images/symbol/flags/" + _pairname + ".png'><span>" + item.PairName + "</span></td>";
                    rows += "<td><span class='price'>" + item.OpenPrice + "</span></td>";
                    rows += "<td>" + item.BidPrice + "</td>";
                    rows += "<td>" + item.AskPrice + "</td>";
                    rows += "<td><span class='profit'>" + item.TradeWinPercent + "%</span></td>";
                    if (stickeyPainame.indexOf(_pairname) >= 0) {
                        rows += "<td><i class='fa fa-star star-yellow' aria-hidden='true'></i></td>";
                    } else {
                        rows += "<td><i class='fa fa-star' aria-hidden='true'></i></td>";
                    }
                    rows += "</tr>";
                    //rowsarray.concat(rows);
                    if ('FOR' === item.MarketType) {
                        $("#tbForexmarkets").append(rows);
                        countitemforex += 1;
                    } else {
                        $("#tbmarkets").append(rows);
                        countitemcrypto += 1;
                    }

                }
                $("#circlesnumitemcryto").text(countitemcrypto);
                $("#circlesnumitemforex").text(countitemforex);
            }

        }

    });
}

function tradePairInit() {
    let checkvisiable = $("#slide-tradepairs").hasClass("d-none");
    if (!checkvisiable) {
        let data = {

        };
        return $.ajax({
            url: "/api/trade_pairs",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                let rowsarray = "",
                    rows = "";
                $("#tradepairs-main").text("");
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        rows = "";
                        const item = result[i];
                        const _pairname = item.PairName.replace('/', '_');
                        if (item.PairSymbol === sym && pairconfig.pairname !== sym) {
                            pairconfig.pairname = sym;
                            pairconfig.profit = item.TradeWinPercent;
                            pairconfig.decimal = item.FormatDecimal;
                            chart.series[0].update({
                                lastVisiblePrice: {
                                    label: {
                                        format: '{value:.' + pairconfig.decimal + 'f}'
                                    }
                                }
                            });

                        }
                        if (item.PriceChangePercent < 0) {
                            if (item.PairSymbol === sym) {

                                $("#value-profit").text("+" + item.TradeWinPercent + "%");
                                value_profit = item.TradeWinPercent;
                                rows += " <li class='media assets-item assets-down active' onclick=buildfunction._addFavorite('" + _pairname + "');>";
                            } else {
                                rows += " <li class='media assets-item assets-down' onclick=buildfunction._addFavorite('" + _pairname + "');>";
                            }

                            rows += "<img class='i-symbol' src='/Images/Icon/down-arrow.png' />";
                        } else if (item.PriceChangePercent > 0) {
                            if (item.PairSymbol === sym) {

                                $("#value-profit").text("+" + item.TradeWinPercent + "%");
                                value_profit = item.TradeWinPercent;
                                rows += " <li class='media assets-item assets-up active' onclick=buildfunction._addFavorite('" + _pairname + "');>";
                            } else {
                                rows += " <li class='media assets-item assets-up' onclick=buildfunction._addFavorite('" + _pairname + "');>";
                            }
                            rows += "<img class='i-symbol' src='/Images/Icon/up-arrow.png' />";
                        }
                        rows += "<header class='assets-item-header'>";
                        rows += "<div class='assets-item-title'><i class='icon-chevron-up-black'></i><span >" + item.PairName + "</span></div>";
                        rows += "<div class='assets-item-payout'>" + item.TradeWinPercent + "%</div>";
                        rows += "</header>";
                        rows += "<div class='assets-info'>";
                        rows += "<div class='assets-info-item'>";
                        rows += "<span class='info-item-title'>Rate</span>";
                        rows += "<span class='positive-text'>" + item.OpenPrice + "</span>";
                        rows += "</div>";
                        rows += "<div class='assets-info-item'>";
                        rows += "<span class='info-item-title'>Bid</span>";
                        rows += "<span class='positive-text'>" + item.BidPrice + "</span>";
                        rows += "</div>";
                        rows += "<div class='assets-info-item'>";
                        rows += "<span class='info-item-title'>Ask</span>";
                        rows += "<span class='positive-text'>" + item.AskPrice + "</span>";
                        rows += "</div>";
                        rows += "</div>";
                        rows += "</li>";
                        //rowsarray.concat(rows);
                        $("#tradepairs-main").append(rows);
                    }
                    buildData._assetsfilter();
                }

            }

        });
    }

}
const LIMIT_NODE_TIME = 60;
const initialState = {
    dealType: null,
    dealValue: 0,
    limitArray: LIMIT_NODE_TIME,
    maxValue: 2000,
    minValue: 1000,
    indexCurrentNode: 0,
    indexLineRed: 0,
    indexLineGreen: 0
};
const state = {
    ...initialState
};

function callRealtimeData() {
    //buildfunction._serverTime();
    let data = {
        pair: window.sym,
        interval: '5s'
    };

    return $.ajax({
        url: "/api/market_price",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (resultData) {
            return resultData;
        },
        error: function () {
            return [0, 0, 0, 0, 0];
        },
        timeout: 1200000
    });
}

function move(event) {
    let x = event.pageX,
        y = event.pageY,
        path = ['M', chart.plotLeft, y,
            'L', chart.plotLeft + chart.plotWidth, y,
            'M', x, chart.plotTop,
            'L', x, chart.plotTop + chart.plotHeight
        ];

    if (chart.crossLines) {
        // update lines
        chart.crossLines.attr({
            d: path
        });
    } else {
        // draw lines
        chart.crossLines = chart.renderer.path(path).attr({
            'stroke-width': 2,
            stroke: 'green',
            zIndex: 10
        }).add();
    }

    if (chart.crossLabel) {
        // update label
        chart.crossLabel.attr({
            y: y + 6,
            text: chart.yAxis[0].toValue(y).toFixed(2)
        });
    } else {
        // draw label
        chart.crossLabel = chart.renderer.text(chart.yAxis[0].toValue(y).toFixed(2), chart.plotLeft - 40, y + 6).add();
    }
}

function RealtimGeneralData() {
    //maxValue = CallRealtimeData();
    if (lastOpen > 0) {
        let series = chart.series[0];
        let volumn = chart.series[1];
        //const { maxValue, indexLineRed, indexLineGreen } = state;
        let valuedecimal = Math.floor((Math.random() * 100) + 1);
        let ran = valuedecimal;
        if (valuedecimal <= 100) {
            valuedecimal = parseFloat(valuedecimal / 1000);
        } else {
            valuedecimal = parseFloat(valuedecimal / 9999);
        }

        let time = moment();
        let open, high, low, close;

        if (ran % 2 === 0) {
            var timenew = time;
            open = lastOpen + valuedecimal;
            high = lastHigh + valuedecimal;
            low = lastLow + valuedecimal;
            close = lastClose + valuedecimal;
            var newPoint = [timenew, open, high, low, close];
            var newVolumn = [timenew, lastVolumn];
            series.addPoint(newPoint, true, false);
            volumn.addPoint(newVolumn, true, false);
        } else {
            timenew = time;
            open = lastOpen - valuedecimal;
            high = lastHigh - valuedecimal;
            low = lastLow - valuedecimal;
            close = lastClose - valuedecimal;
            newPoint = [timenew, open, high, low, close];
            newVolumn = [timenew, lastVolumn];
            series.addPoint(newPoint, true, false);
            volumn.addPoint(newVolumn, true, false);
        }
        //renderCurrentPriceIndicator(chart);


        //series.setData(newPoint, true, true, false);
        //series.drawPoints();
        // volumn.addPoint(newVolumn, true, false);

        //let data = Array.from(Array(limitArray));
        //let time = moment();
        //let timeUTC = moment.utc();
        //data = data.map((d, index) => {
        //    time = moment(time).add(10, "seconds");
        //    timeUTC = moment.utc(timeUTC).add(10, "seconds");
        //    return index % 2 === 0
        //        ? [
        //            time.valueOf(),
        //            lastOpen + valuedecimal, // open
        //            lastHigh + valuedecimal, // high
        //            lastLow + valuedecimal, // low
        //            lastClose + valuedecimal // close
        //        ]
        //        : [time.valueOf(), null, null, null, null, null];
        //});
        //return data;
    }
    //return [null, null, null, null, null, null];
}

function handleGeneralData_bak(limitArray = LIMIT_NODE_TIME) {
    //maxValue = CallRealtimeData();

    const {
        maxValue,
        indexLineRed,
        indexLineGreen
    } = state;

    let data = Array.from(Array(limitArray));
    let time = moment();
    let timeUTC = moment.utc();
    data = data.map((d, index) => {
        time = moment(time).add(1, "seconds");
        timeUTC = moment.utc(timeUTC).add(1, "seconds");
        return index % 2 === 0 ?
            [
                time.valueOf(),
                null, // open
                null, // high
                null, // low
                null // close
            ] :
            [time.valueOf(), null, null, null, null, null];
    });
    return data;
}

function handleGeneralData(limitArray = LIMIT_NODE_TIME) {
    let data = Array.from(Array(limitArray));
    let time = moment();
    let timeUTC = moment.utc();
    data = data.map((d, index) => {
        time = moment(time).add(1, "seconds");
        timeUTC = moment.utc(timeUTC).add(1, "seconds");
        return index % 2 === 0 ?
            [
                time.valueOf(),
                null, // open
                null, // high
                null, // low
                null // close
            ] :
            [time.valueOf(), null, null, null, null, null];
    });
    return data;
}
// theme chart
Highcharts.theme = {
    colors: ['#A2A6AB', '#90ee7e', '#f45b5b', '#7798BF', '#aaeeee', '#ff0066',
        '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee'
    ],
    chart: {
        backgroundColor: {
            linearGradient: {
                x1: 0,
                y1: 0,
                x2: 1,
                y2: 1
            },
            stops: [
                [0, '#2a2a2b'],
                [1, '#3e3e40']
            ]
        },
        plotBorderColor: '#656c7d'
    },
    xAxis: {
        ordinal: true,
        //max: 15,
        //range: 4 * 60 * 1000, //5 minutes
        minRange: 1 * 60 * 1000,
        //maxRange: 5 * 60 * 1000,
        //tickInterval: 2,
        //tickInterval: 15 * 1000, // width grid bettwen 2 line
        pointInterval: 5000, //5000,
        //tickPixelInterval: 1000, // test now
        tickPixelInterval: 170,
        //overscroll: 60 * 1000, // 10 seconds *
        //pointRange:   2,
        //events: {
        //    afterSetExtremes: function () {
        //        var xAxis = this,
        //            numberOfPoints = xAxis.series[0].points.length - 1,
        //            minRangeValue = xAxis.getExtremes().min,
        //            maxRangeValue = xAxis.getExtremes().max;
        //        console.log("numberOfPoints: " + numberOfPoints);
        //        //if (minRangeValue < 0) {
        //        //    xAxis.setExtremes(null, xAxis.options.max);
        //        //} else if (maxRangeValue > numberOfPoints) {
        //        //    xAxis.setExtremes(numberOfPoints - xAxis.options.max, numberOfPoints);
        //        //}

        //    }
        //},
        //tickInterval: 5000,//2 *  60 * 1000,
        visibility: "visible",
        type: 'datetime',
        offset: 0,
        opacity: 1,
        width: "100%",
        crosshair: {
            enabled: false,
            label: {
                enabled: true,
                padding: 8,
                style: {
                    color: '#fff',
                    backgroundColor: "#a9abae"
                },
                backgroundColor: "#a9abae",
                format: '{value:%H:%M:%S}'
            }
        },
        //gridLineWidth: 0,
        //gridLineColor: '#656c7d',
        labels: {
            style: {
                color: '#656c7d'
            }
        }
        //lineColor: '#656c7d',
        //minorGridLineColor: '#656c7d',
        //tickColor: '#656c7d',
        //title: {
        //    style: {
        //        color: '#656c7d'
        //    }
        //}
    },
    yAxis: {
        //tickInterval: 20, //***
        //minRange: 50,
        //maxRange: 1000,
        tickAmount: 4,
        endOnTick: false,
        startOnTick: false,
        opposite: true,
        gridLineWidth: 0,
        gridLineColor: '#656c7d',
        labels: {
            style: {
                color: '#656c7d',
                fontSize: "14px"
            }
        },
        lineColor: '#656c7d',
        minorGridLineColor: '#656c7d',
        tickColor: '#656c7d',
        title: {
            style: {
                color: '#656c7d'
            }
        }
    },
    tooltip: {
        enabled: false,
        crosshairs: false,
        shape: 'square',
        headerShape: 'callout',
        borderWidth: 0,
        backgroundColor: "#181F2C",
        shadow: false,
        useHTML: true,
        style: {
            color: "#888"
        },
        positioner: function (width, height, point) {
            var chart = this.chart,
                position;

            if (point.isHeader) {
                position = {
                    x: Math.max(
                        // Left side limit
                        chart.plotLeft,
                        Math.min(
                            point.plotX + chart.plotLeft - width / 2,
                            // Right side limit
                            chart.chartWidth - width - chart.marginRight
                        )
                    ),
                    y: point.plotY
                };
            } else {
                position = {
                    x: point.series.chart.plotLeft,
                    y: point.series.yAxis.top - chart.plotTop
                };
            }

            return position;
        }
    },
    plotOptions: {
        series: {

            //minTickInterval: 30000,
            //tickPixelInterval: 200,
            //pointPadding: 0.1,
            //groupPadding: 0.5,
            //grouping: false,
            //maxPointWidth: 36,
            //animation: {
            //    duration: 1000,
            //    easing: 'easeOutBounce'
            //},
            //pointWidth: 30,
            lineWidth: 1,
            dataLabels: {
                enabled: false,
                color: '#656c7d',
                style: {
                    fontSize: '14px',
                    width: "0.5px"
                },
                borderRadius: 5,

                //backgroundColor: 'rgba(252, 255, 197, 0.7)',
                borderWidth: 1,
                //borderColor: '#AAA',
                //format: '{point.series.name}'
            },
            marker: {
                enabled: false,
                // lineColor: '#656c7d',
                //width: "0.5px"
            },
            //pointIntervalUnit: 2 *1000
        },
        boxplot: {
            //fillColor: '#656c7d'
        },
        candlestick: {

            animation: {
                duration: 1500,
                easing: 'easeOutBounce'
            },
            //grouping: true,
            lineColor: '#656c7d',
            //pointWidth: 30,
            pointPadding: 0,
            groupPadding: 0.2,
            //maxPointWidth: 36,
            shadow: false,
            //pointPlacement: "between",
            //tickInterval: 1 *1000,
            minTickInterval: 100,
            tickPixelInterval: 200,
            pointInterval: 5 * 1000,
            pointIntervalUnit: 5 * 1000,
            dataLabels: {
                borderRadius: 5
            },
            borderRadius: 5,
            states: {
                hover: {
                    enabled: false
                }
            }
        },
        areaspline: {
            styles: {
                strokeWidth: 1
            },
            animation: {
                duration: 1000,
                easing: 'easeOutBounce'
            },
            lineWidth: 1,
            //lineColor: '#7b808c',
            lineColor: '#f5f5f5',
            pointInterval: 1000
        },
        rsi: {
            // shared options for all rsi series
        },
        errorbar: {
            color: 'white'
        }
    },
    legend: {
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
        itemStyle: {
            color: '#E0E0E3'
        },
        itemHoverStyle: {
            color: '#FFF'
        },
        itemHiddenStyle: {
            color: '#606063'
        },
        title: {
            style: {
                color: '#C0C0C0'
            }
        }
    },
    // scroll charts
    rangeSelector: {

        buttonTheme: {
            fill: '#505053',
            stroke: '#000000',
            style: {
                color: '#CCC'
            },
            states: {
                hover: {
                    fill: '#707073',
                    stroke: '#000000',
                    style: {
                        color: 'white'
                    }
                },
                select: {
                    fill: '#000003',
                    stroke: '#000000',
                    style: {
                        color: 'white'
                    }
                }
            }
        }

    },
    scrollbar: {
        barBackgroundColor: '#888',
        barBorderRadius: 0,
        barBorderWidth: 0,
        buttonBackgroundColor: '#888',
        buttonBorderWidth: 0,
        buttonBorderRadius: 0,
        trackBackgroundColor: 'none',
        trackBorderWidth: 0.5,
        trackBorderRadius: 4,
        trackBorderColor: '#888'
    }
};
Highcharts.setOptions(Highcharts.theme);
Highcharts.setOptions({
    global: {
        useUTC: false
    }
});
// create the chart
//window.chart = new Highcharts.stockChart('containerchart', {

window.chart = new Highcharts.stockChart('containerchart', {
    chart: {
        renderTo: 'containerchart',
        //animation: Highcharts.svg,
        animation: false,
        panning: true,
        //animation: {
        //    duration: 1000,
        //    easing: 'easeOutBounce'
        //},
        //animation: {
        //    enabled: true,
        //    duration: 1000,
        //    easing: 'linear'
        //},
        "spacingLeft": 15,
        //"padding": 0,

        lang: {
            noData: "No Data"
        },
        noData: {
            style: {
                fontWeight: 'bold',
                fontSize: '15px',
                color: '#888'
            }
        },
        //indicators: [{
        //    id: 'dataseries',
        //    type: 'rsi',
        //    params: {
        //        period: 14,
        //        overbought: 70,
        //        oversold: 30
        //    },
        //    styles: {
        //        strokeWidth: 1,
        //        stroke: 'black',
        //        dashstyle: 'solid',
        //        background: 'red'
        //    },
        //    yAxis: {
        //        lineWidth: 1,
        //        title: {
        //            text: 'RSI'
        //        },
        //        plotLines: [{
        //            value: 70,
        //            color: 'blue',
        //            width: 1
        //        }, {
        //            value: 30,
        //            color: 'blue',
        //            width: 1
        //        }]
        //    }
        //}],
        events: {
            //load: function () {
            //    chartloadInit();
            //    //ChartInit("");
            //    setInterval(() => {
            //        tradePairInit();
            //    }, 1000);

            //}
        }
    },
    resize: {
        enabled: true
    },
    xAxis: {
        visibility: "visible",
        //crosshair: {
        //    enabled: true
        //},
        labels: {
            align: 'right',
            x: -3,
            visibility: "visible",
            //format: '{value:.7f}',
        },

        type: 'datetime',
        offset: 0,
        opacity: 1,
        width: "100%",
    },
    yAxis: [{
        visibility: "visible",
        opposite: true,
        labels: {
            align: 'right',
            x: -3,
            visibility: "visible",
            //format: '{value:.3f}',
        },
        height: '80%',
        offset: 0, //100
        lineWidth: 1,
        resize: {
            enabled: true
        }
    }, {
        labels: {
            align: 'right',
            x: -3,
            visibility: "visible",
            enabled: true
        },
        title: {
            text: 'Volume',
            enabled: false
        },
        top: '80%',
        height: '20%',
        offset: 0,
        lineWidth: 1
    }],
    navigationBindings: {
        events: {
            selectButton: function (event) {
                var newClassName = event.button.className + ' highcharts-active',
                    topButton = event.button.parentNode.parentNode;

                if (topButton.classList.contains('right')) {
                    newClassName += ' right';
                }

                // If this is a button with sub buttons,
                // change main icon to the current one:
                if (!topButton.classList.contains('highcharts-menu-wrapper')) {
                    topButton.className = newClassName;
                }

                // Store info about active button:
                this.chart.activeButton = event.button;
            },
            deselectButton: function (event) {
                event.button.parentNode.parentNode.classList.remove('highcharts-active');

                // Remove info about active button:
                this.chart.activeButton = null;
            },
            showPopup: function (event) {
                if (!this.indicatorsPopupContainer) {
                    this.indicatorsPopupContainer = document
                        .getElementsByClassName('highcharts-popup-indicators')[0];
                }
                if (!this.annotationsPopupContainer) {
                    this.annotationsPopupContainer = document
                        .getElementsByClassName('highcharts-popup-annotations')[0];
                }

                if (event.formType === 'indicators') {
                    this.indicatorsPopupContainer.style.display = 'block';
                } else if (event.formType === 'annotation-toolbar') {
                    // If user is still adding an annotation, don't show popup:
                    if (!this.chart.activeButton) {
                        this.chart.currentAnnotation = event.annotation;
                        this.annotationsPopupContainer.style.display = 'block';
                    }
                }

            },
            closePopup: function () {
                this.indicatorsPopupContainer.style.display = 'block';
                this.annotationsPopupContainer.style.display = 'block';
            }
        }
    },

    //mapNavigation: {
    //    enabled: false,
    //    //enableButtons: true,
    //    enableMouseWheelZoom: true
    //},
    mapNavigation: {
        enabled: true,
        enableButtons: false,
        enableMouseWheelZoom: true,
        //zoomType: 'y'
    },

    stockTools: {
        gui: {
            buttons: [
                'thresholds',
                'separator',
                'indicators',
                'separator',
                'simpleShapes',
                'lines',
                'crookedLines',
                //'measure',
                //'advanced',
                'toggleAnnotations',
                'separator',
                'verticalLabels',
                'flags',
                'separator',
                //'zoomChange',
                'fullScreen',
                'separator',
                'saveChart'
            ],
            definitions: {
                thresholds: {
                    className: 'highcharts-threshold-annotation',
                    symbol: 'horizontal-line.svg'
                }
            }
        }
    },
    navigation: {
        bindings: {
            thresholds: {
                className: 'highcharts-threshold-annotation',
                start: function (event) {
                    var chart = this.chart,
                        x = chart.xAxis[0].toValue(event.chartX),
                        y = chart.yAxis[0].toValue(event.chartY),
                        colors = chart.options.colors,
                        series = chart.series[0],
                        zones = series.userOptions.zones || [];
                    chart.customColorIndex = chart.customColorIndex || 1;
                    chart.customColorIndex++;

                    if (
                        chart.customColorIndex === colors.length
                    ) {
                        chart.customColorIndex = 1;
                    }

                    zones.push({
                        color: colors[chart.customColorIndex],
                        value: y
                    });

                    chart.addAnnotation({
                        langKey: 'thresholds',
                        zoneIndex: zones.length - 1,
                        type: 'infinityLine',
                        draggable: 'y',
                        events: {
                            drag: function (e) {
                                var newZones = series.userOptions.zones;

                                newZones[this.userOptions.zoneIndex].value = chart.yAxis[0].toValue(e.chartY);

                                chart.series[0].update({
                                    zones: newZones
                                });
                            }
                        },
                        typeOptions: {
                            type: 'horizontalLine',
                            points: [{
                                x: x,
                                y: y
                            }]
                        }
                    });
                    chart.series[0].update({
                        zones: zones
                    });
                }
            }
        }
    },
    rangeSelector: {
        //verticalAlign: 'bottom',
        align: 'left',
        buttons: [{
            count: 2,
            type: 'minute',
            text: '2M',
            dataGrouping: {
                units: [
                    [
                        'second',
                        [1, 2, 5, 10, 15, 30],
                        'minute',
                        [1, 2, 5, 10, 15, 30]
                    ]
                ]
            }
        }, {
            count: 5,
            type: 'minute',
            text: '5M',
            dataGrouping: {
                units: [
                    [
                        'second',
                        [1, 2, 5, 10, 15, 30]
                    ],
                    [
                        'minute',
                        [1, 2, 5, 10, 15, 30]
                    ],
                    [
                        'hour',
                        [1, 2, 3, 4, 6, 8, 12]
                    ],
                    [
                        'day',
                        [1]
                    ],
                    [
                        'week',
                        [1]
                    ],
                    [
                        'month',
                        [1, 3, 6]
                    ],
                    [
                        'year',
                        null
                    ]
                ]
            }

        }, {
            count: 15,
            type: 'minute',
            text: '15M',
            dataGrouping: {
                units: [
                    [
                        'second',
                        [1, 2, 5, 10, 15, 30]
                    ],
                    [
                        'minute',
                        [1, 2, 5, 10, 15, 30]
                    ],
                    [
                        'hour',
                        [1, 2, 3, 4, 6, 8, 12]
                    ],
                    [
                        'day',
                        [1]
                    ],
                    [
                        'week',
                        [1]
                    ],
                    [
                        'month',
                        [1, 3, 6]
                    ],
                    [
                        'year',
                        null
                    ]
                ]
            }
        }, {
            count: 30,
            type: 'minute',
            text: '30M',
            dataGrouping: {
                units: [
                    [
                        'second',
                        [1, 2, 5, 10, 15, 30]
                    ],
                    [
                        'minute',
                        [1, 2, 5, 10, 15, 30]
                    ],
                    [
                        'hour',
                        [1, 2, 3, 4, 6, 8, 12]
                    ],
                    [
                        'day',
                        [1]
                    ],
                    [
                        'week',
                        [1]
                    ],
                    [
                        'month',
                        [1, 3, 6]
                    ],
                    [
                        'year',
                        null
                    ]
                ]
            }
        }, {
            count: 3,
            type: 'hour',
            text: '3h',
            dataGrouping: {
                units: [
                    [
                        'second',
                        [1, 2, 5, 10, 15, 30],
                    ]
                ]
            }
        }, {
            count: 1,
            type: 'day',
            text: '1 day',
            dataGrouping: {
                units: [
                    [
                        'second',
                        [1, 2, 5]
                    ]
                ]
            }
        }],
        inputEnabled: false,
        selected: 1
    },

    navigator: {
        enabled: true,
        //xAxis: {
        //    overscroll: 30 * 1000 //*
        //}
    },
    scrollbar: {
        showFull: true
        //overscroll: 60 * 1000 // 10 seconds
    },

    series: [{
        type: 'candlestick', //candlestick
        id: 'dataseries',
        name: 'dataseries',
        data: [], //ohlc,
        threshold: null,
        //showInLegend: false,
        //allowPointSelect: false,
        lastVisiblePrice: {
            enabled: true,
            label: {
                enabled: true,
                format: '{value:.3f}'
            },
            color: "#2a2a2b"

        },
        marker: {
            enabled: false,
            radius: 0
        },
        shadow: false,
        //lastPrice: {
        //    enabled: true,
        //    color: 'red'
        //},
        x: 0,
        y: 0,
        offset: 0,
        //pointInterval: 1000, // 1s
        //pointInterval: 5000, // a candlestick is 5s
        dataGrouping: {
            units: [
                [
                    'second',
                    [1, 2, 5, 10, 15, 30]
                ],
                [
                    'minute',
                    [1, 2, 5, 10, 15, 30]
                ],
                [
                    'hour',
                    [1, 2, 3, 4, 6, 8, 12]
                ],
                [
                    'day',
                    [1]
                ],
                [
                    'week',
                    [1]
                ],
                [
                    'month',
                    [1, 3, 6]
                ],
                [
                    'year',
                    null
                ]
            ]
        }
    }, {
        type: 'flags',
        id: 'aapl-flags',
        data: [], //ohlc,
        onSeries: 'dataseries',
        fillColor: 'red',
        lineColor: 'none',
        width: 10,
        height: 10,
        y: 0,
        useHTML: true,
        //shape: 'triangle-down'
    },
    {
        type: 'flags',
        id: 'point-current-price',
        data: [], //ohlc,
        onSeries: 'dataseries',
        fillColor: 'red',
        lineColor: 'none',
        width: 10,
        height: 10,
        y: 0,
        useHTML: true
    },
    {
        type: 'flags',
        id: 'point-trend-price',
        data: [], //ohlc,
        onSeries: 'dataseries',
        fillColor: 'red',
        lineColor: 'none',
        width: 10,
        height: 10,
        y: 0,
        useHTML: true
    }, {
        type: 'column',
        name: 'Volume',
        data: [],
        yAxis: 1,
        x: 0,
        //dataGrouping: {
        //    units: groupingUnits
        //}
    }
    ],
    responsive: {
        resize: {
            enabled: true
        },
        rules: [{
            condition: {
                maxWidth: 900
            },
            chartOptions: {
                rangeSelector: {
                    allButtonsEnabled: false

                },
                subtitle: {
                    text: null
                },
                navigator: {
                    enabled: false
                }
            }
        }]
    }
});
//candlestick

function setData(series, x, y, high, low, close, VolumeTo, period, typecurrentchart) {
    let temp = series.data.length > 0 ? series.data.slice(-1)[0] : undefined;
    if (temp !== undefined) {
        let x2 = temp.x;
        if ((period * 1000) <= x - x2) {
            //x = (new Date()).getTime();
            lastCandles = [];
            lastCandles.push([close, close, close, close]);
            series.addPoint({
                x: x,
                y: y,
                high: high,
                low: low,
                close: close
            }, true, true, true);
            if ("candlestick" === typecurrentchart) {
                chart.series[4].addPoint([x, VolumeTo]);
            }

        } else {
            if (lastCandles[0] !== undefined) {
                if (lastCandles[0][1] < close) {
                    lastCandles[0][1] = close;
                }
                if (lastCandles[0][2] > close) {
                    lastCandles[0][2] = close;
                }
                newValue = [x2,
                    lastCandles[0][0],
                    lastCandles[0][1],
                    lastCandles[0][2], //low
                    close //close
                ];

                //var newdate = new Date().getTime();
                temp.update(newValue, true, true);
                //series.chart.redraw(true);
                //var enddate = new Date().getTime();
                //console.log("calculator: " + (parseInt(enddate) -parseInt(newdate)));
                // series.chart.redraw(true);

                if ("candlestick" === typecurrentchart) {
                    let vol = chart.series[4].data.length > 0 ? chart.series[4].data.slice(-1)[0] : undefined;
                    if (vol !== undefined) {
                        vol.update([vol.x, VolumeTo]);
                    }
                }
            }

            //newValue = [x2,
            //    y,
            //    high,
            //    low, //low
            //    close //close
            //];

            //series.chart.redraw(true);
        }
    }

}
//areaspline
function setDataAreaspline(series, x, y, period) {
    let temp = series.data.length > 0 ? series.data.slice(-1)[0] : undefined;
    if (temp !== undefined && temp !== null) {
        let x2 = temp.x;
        if ((period * 1000) <= x - x2) {
            //x = (new Date()).getTime();
            series.addPoint({
                x: x,
                y: y
            }, true, true, true);
        } else {
            newValue = [x2,
                y
            ];
            temp.update(newValue, false, false);
            series.chart.redraw(true);

        }
    }

}

function setData_Bak(series, x, y, high, low, close, VolumeFrom, VolumeTo, period, type) {
    const typecurrentchart = chart.series[0].type;
    let last = series.data.length > 0 ? series.data.slice(-1)[0] : undefined;
    if (last !== undefined) {
        newValue = [last.x,
            y,
            high,
            low, //low
            close //close
        ];
        last.update(newValue, false, false);
        series.chart.redraw(true);
        if ("candlestick" === typecurrentchart) {
            let vol = chart.series[4].data.length > 0 ? chart.series[4].data.slice(-1)[0] : undefined;
            vol.update([vol.x, VolumeTo]);
        }


        chart.series[2].setData([]);
        chart.series[2].addPoint({
            x: last.x,
            y: close,
            title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
            useHTML: true
        }, true, false);
    }

}

function ChartInit(_interval = "5s") {
    lastCandles = [];
    let data = {
        pair: window.sym,
        interval: _interval
    };
    $("#select-trade").val(window.sym);
    let typecurrentchart = chart.series[0].type;
    let ohlc = [],
        lasttime = 0,
        lastprice = 0,
        volume = [];
    let ajaxCall = $.ajax({
        url: "/api/candlestick",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            buildfunction.loading();
        },
        timeout: 1200000,
        error: function () {
            buildfunction.loadingError();
        }
    });
    ajaxCall.done(function (result) {
        if (result !== null) {
            if (result.length > 0) {
                for (var i = 0; i < result.length; i++) {
                    let item = result[i];
                    let _lasttime = parseInt(item.Times);
                    lasttime = _lasttime >= parseInt(lasttime) ? _lasttime : lasttime;
                    if ("areaspline" === typecurrentchart || "spline" === typecurrentchart) {
                        ohlc.push([
                            item.Times, // the date
                            item.Close // close
                        ]);
                    } else {
                        ohlc.push([
                            item.Times, // the date
                            item.Open, // open
                            item.High, // high
                            item.Low, // low
                            item.Close // close
                        ]);
                        volume.push([
                            item.Times, // the date
                            //item.VolumeFrom, // VolumeFrom
                            item.VolumeTo //VolumeTo
                        ]);
                    }
                    if (i === result.length - 1) {
                        lastprice = item.Close;
                        lastCandles.push([item.Open, item.High, item.Low, item.Close]);
                    }
                }
                //highcharts-series-group
                // reset 
                chart.series[0].setData([]);

                if ("areaspline" === typecurrentchart || "spline" === typecurrentchart) {
                    //chart.series[0].update({
                    //    type: "candlestick"
                    //});
                    chart.series[0].setData(ohlc);

                    //chart.series[0].redraw();
                    chart.xAxis[0].update({
                        pointInterval: 1000
                    });
                    //chart.series[0].update({
                    //    type: "areaspline"
                    //});
                } else {
                    chart.series[0].setData(ohlc);
                    if ("candlestick" === typecurrentchart) {
                        chart.series[4].setData(volume);
                    }
                }


                chart.series[2].setData([]);
                chart.series[2].addPoint({
                    x: lasttime,
                    y: lastprice,
                    title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                    useHTML: true
                }, true, false);

                //chart.xAxis[0].dataMax = lasttime;
                //this.yAxis[0].setExtremes(chart.dataMin, candlestick.dataMax, true, false);
                //chart.series[1].setData(ohlc2);
                //chart.xAxis[0].categories.push(lasttime);

                //chart.series[0].drawPoints();
                //chart.series[0].data[chart.series[0].data.length - 900];
                //chart.series[0].pointStart =Date.UTC(2020, 4, 15, 7, 10);
                //chart.series[0].pointInterval = 30 * 3600 * 1000 * 24;
                //chart.series[1].setData(ohlc);
                //chart.series[2].setData(ohlc);
                //chart.series[2].setData(ohlc);
                //chart.series.type ="area";

                //chart.xAxis[0].x -1;
                //chart.series[0][chart.series[0].data.length - 25];
                //chart.series[1].setData(volume);

                // set line
                //let xAxis = chart.xAxis[0];

                //i = 0;
                //const indexRedLine = chart.series[0].data.length - 45;
                //const indexGreenLine = chart.series[0].data.length - 15;

                //let valueGreenLine = chart.series.data[indexGreenLine].x;
                //let valueRedLine = chart.series.data[indexRedLine].x;

                //buildfunction._serverTime().done(function (time) {
                //    //update max time
                //    //console.log("second: "+ (parseInt(time) * 1000));
                //    //xAxis.update({ max: lasttime + (parseInt(time) * 1000) });
                //    initialXAxisPlotLine(xAxis, (lasttime + (parseInt(time) * 1000)) - 30000, lasttime + (parseInt(time) * 1000));
                //});

                //let newdata = chart.series[0].data;
                //newdata = newdata.slice(-20);
                //chart.series[0].setData(newdata, true, false);
                //chart.series[0].drawPoints();
                // Reset Data
                //if (i === data.length - 1) {
                //    if (data.length >= 20) {
                //        i -= 10;
                //        data = data.slice(-10);
                //    }
                //    data = [...data, ...handleGeneralData()];
                //    valueGreenLine = data[data.length - 5].x;
                //    valueRedLine = data[data.length - 2].x;
                //    initialXAxisPlotLine(xAxis, valueGreenLine, valueRedLine);
                //} else {
                //    i += 1;
                //}
                //const xExtremes = chart.xAxis[0].getExtremes();
                ////const yExtremes = chart.yAxis[0].getExtremes();
                ////console.log("xExtremes.dataMin: " + xExtremes.dataMin + '-' + xExtremes.min);
                ////let xExtremes = chart.xAxis[0].getExtremes();
                //let diff = 20000;
                ////chart.xAxis[0].setExtremes(xExtremes.min - diff, xExtremes.max - diff);
                //chart.xAxis[0].setExtremes(xExtremes.dataMin - diff, xExtremes.dataMax - diff);
                //chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);

                buildfunction.updateRange(window.sym);
                const screenWidth = parseInt(window.innerWidth);
                if (screenWidth < 415) {
                    isMobile = true;
                    buildfunction._zoomTime("2m", 0);
                    //buildfunction._zoomRange();

                } else if (screenWidth >= 415 && screenWidth < 1024) {
                    isMobile = true;
                    buildfunction._zoomTime("2m", 0);
                    //buildfunction._zoomRange();
                }
                else if (screenWidth >= 1024) {
                    isMobile = false;
                    buildfunction._zoomTime("5m", 1);
                    //buildfunction._zoomRange();
                }
                //chart.xAxis[0].update({ overscroll: 60 * 1000 });
                //if ("candlestick" === typecurrentchart) {
                //    buildfunction._zoomFocus();
                //}
                //buildfunction._zoomFocus();
                buildfunction.stopLoading();
                return true;
            }
            //const xAxisExtremes = chart.xAxis[0].getExtremes();
            //var diff = (xAxisExtremes.dataMax - xAxisExtremes.dataMin) / 20 ;
            //let diff = 20000;
            //chart.xAxis[0].setExtremes(xAxisExtremes.min + diff, xAxisExtremes.max + diff);
            buildfunction.stopLoading();
            return true;
        }
    });
}
chart.reflowNow = (function () {
    this.containerHeight = this.options.chart.height || window.window.HighchartsAdapter.adapterRun(this.renderTo, 'height');
    this.containerWidth = this.options.chart.width || window.window.HighchartsAdapter.adapterRun(this.renderTo, 'width');
    this.setSize(this.containerWidth, this.containerHeight, true);
    this.hasUserSize = null;
    //renderCurrentPriceIndicator_Resize(chart);
    //renderCurrentPriceIndicator(this);
});
chart.reflowNowManual = (function (width) {
    //this.containerHeight = this.options.chart.height;
    //this.containerWidth = this.options.chart.width ;
    this.setSize(width, this.options.chart.height, true);
    this.hasUserSize = null;
    //renderCurrentPriceIndicator_Resize(chart);
});
$('#chart-type-Candlestick').click(function () {
    chart.xAxis[0].update({
        pointInterval: 5000
    });
    chart.yAxis[0].update({
        height: '80%',
    });
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/candlestick.png");
    chart.series[0].update({
        type: "candlestick",
        dataGrouping: {
            approximation: 'ohlc',
            units: [
                [
                    'second',
                    [1, 2, 3, 5, 10, 15, 30]
                ]
            ]
        },
        keys: ['x', 'open', 'high', 'low', 'close']
    });
    chart.series[4].update({
        visible: true
    });
    ChartInit();
});
$('#chart-type-ohlc').click(function () {
    chart.xAxis[0].update({
        pointInterval: 5000
    });
    chart.yAxis[0].update({
        height: '80%',
    });
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/ohlc.png");
    //  ChartInit();
    chart.series[0].update({
        type: "ohlc",
        dataGrouping: {
            approximation: 'ohlc'
        },
        keys: ['x', 'open', 'high', 'low', 'close']
    });
    ChartInit();
});
$('#chart-type-Line').click(function () {
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/line.png");
    chart.series[0].update({
        type: "spline",
        dataGrouping: {
            approximation: 'average',
            units: [
                [
                    'second',
                    [1, 2, 3, 5, 10, 15, 30]
                ]
            ]
        },
        keys: ['x', 'y'],
        threshold: null
    });
    chart.series[4].update({
        visible: false
    });
    chart.yAxis[0].update({
        height: '100%',
    });
    ChartInit("5m");
    chart.xAxis[0].update({
        pointInterval: 1000
    });
    //buildfunction._zoomTime("5m", 2);
});
$('#chart-type-Areaspline').click(function () {
    spush = false;
    chart.series[0].setData([]);
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/linearea.png");
    chart.series[0].update({
        type: "areaspline",
        dataGrouping: {
            approximation: 'average',
            units: [
                [
                    'second',
                    [1, 2, 3, 5, 10, 15, 30]
                ]
            ]
        },
        keys: ['x', 'y'],
        threshold: null,
        fillColor: {
            linearGradient: {
                x1: 0,
                x2: 0,
                y1: 0.1,
                y2: 1
            },
            stops: [

                //[0.1, 'rgba(45, 56, 71,0.52)'],
                //[0.2, 'rgba(45, 56, 71, 0.42)'],
                //[0.35, 'rgba(45, 56, 71, 0.3)'],
                //[0.6, 'rgba(45, 56, 71, 0.2)'],
                //[0.8, 'rgba(45, 56, 71, 0.15)'],
                //[0.9, 'rgba(45, 56, 71,0.1)']

                [0.1, 'rgba(224, 224, 227,0.50)'],
                [0.2, 'rgba(224, 224, 227, 0.40)'],
                [0.5, 'rgba(224, 224, 227, 0.25)'],
                [0.7, 'rgba(224, 224, 227, 0.2)'],
                [0.8, 'rgba(224, 224, 227, 0.15)'],
                [0.9, 'rgba(224, 224, 227,0.1)']
            ]

        }

    });
    chart.series[4].update({
        visible: false
    });
    chart.yAxis[0].update({
        height: '100%',
    });
    ChartInit("5s");
    //const xExtremes = chart.xAxis[0].getExtremes();
    //let diff = 1000;
    //chart.xAxis[0].setExtremes(xExtremes.dataMin - diff, xExtremes.dataMax - diff);
});

$('#testcache5').click(function () {
    console.log(1);
    chart.series[0].update({
        dataGrouping: {
            forced: true,
            units: [
                ['minute', [15]]
            ]
        }
    });
});
$('#time15m').click(function () {

    chart.series[0].update({
        dataGrouping: {
            forced: true,
            units: [
                ['minute', [15]]
            ]
        }
    });
});
$('#time30m').click(function () {
    chart.series[0].update({
        dataGrouping: {
            forced: true,
            units: [
                ['minute', [30]]
            ]
        }
    });
});


$(document).on('click', '.highcharts-arrow-left', function (event) {
    const statustools = chart.stockTools.visible;
    if (statustools === false) {
        $("#slider-volume").css("margin-left", "0px");

    } else {
        $("#slider-volume").css("margin-left", "48px");
    }
});
$(document).on('click', '#zoomin', function (event) {
    const xAxisExtremes = chart.xAxis[0].getExtremes();
    //var diff = (xAxisExtremes.dataMax - xAxisExtremes.dataMin) / 20 ;
    let diff = 20000;
    chart.xAxis[0].setExtremes(xAxisExtremes.min + diff, xAxisExtremes.max + diff);
});
$(document).on('click', '#zoomfocus', function (event) {
    buildfunction._zoomTime("2m", 0);
    buildfunction._zoomFocus();
    //const xExtremes = chart.xAxis[0].getExtremes();
    //let diff = 5000;
    //chart.xAxis[0].setExtremes(xExtremes.dataMin + diff, xExtremes.dataMax + diff);
    //chart.xAxis[0].update(
    //    {
    //        overscroll: 50 * 1000,
    //        scrollbar: {
    //            overscroll: 50 * 1000 // 10 seconds
    //        }
    //    }
    //);

});
$(document).on('click', '#zoomout', function (event) {
    const xAxisExtremes = chart.xAxis[0].getExtremes();
    //var diff = (xAxisExtremes.dataMax - xAxisExtremes.dataMin) / 20 ;
    let diff = 20000;
    chart.xAxis[0].setExtremes(xAxisExtremes.min - diff, xAxisExtremes.max - diff);
});

$('#resetZoom2').click(function () {
    const xExtremes = chart.xAxis[0].getExtremes();
    let diff = 20000;
    chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
    //chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);
    chart.xAxis[0].update({
        overscroll: 60 * 1000,
        scrollbar: {
            overscroll: 60 * 1000 // 10 seconds
        },
    });
    // buildfunction.updateRange(window.sym);

});
$('#resetZoom2').click(function () {

    const xExtremes = chart.xAxis[0].getExtremes;
    const yExtremes = chart.yAxis[0].getExtremes;
    chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
    chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);

    zoomRatio = 1;

});


/// mouse wheel
//$(function () {
//    Highcharts.setOptions({
//        lang: {
//            resetZoom: ''
//        }
//    });

//    var zoomRatio = 1;
//    var lastX;
//    var lastY;
//    var mouseDown;
//    function createData() {
//        var arr = [];
//        for (var i = 0; i < 200; i++) {
//            arr.push(Math.random() * 100);
//        }
//        return arr;
//    }
//    var setZoom = function () {
//        var xMin = chart.xAxis[0].getExtremes().dataMin;
//        var xMax = chart.xAxis[0].getExtremes().dataMax;
//        var yMin = chart.yAxis[0].getExtremes().dataMin;
//        var yMax = chart.yAxis[0].getExtremes().dataMax;

//        chart.xAxis[0].setExtremes(xMin + (1 - zoomRatio) * xMax, xMax * zoomRatio);
//        chart.yAxis[0].setExtremes(yMin + (1 - zoomRatio) * yMax, yMax * zoomRatio);
//    };



//    $('#containerchart').bind('mousewheel', function (event) {
//        alert(111);
//        event.preventDefault();

//        if (event.deltaY > 0) {
//            if (zoomRatio > 0.7) {
//                zoomRatio = zoomRatio - 0.1;
//                setZoom();
//            }

//        }
//        else if (event.deltaY < 0) {
//            zoomRatio = zoomRatio + 0.1;
//            setZoom();
//        }
//        console.log("111"+ event.deltaY, event.deltaFactor, event.originalEvent.deltaMode, event.originalEvent.wheelDelta);
//    });


//    $('#resetZoom').click(function () {
//        var xExtremes = chart.xAxis[0].getExtremes;
//        var yExtremes = chart.yAxis[0].getExtremes;
//        chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
//        chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);
//        zoomRatio = 1;
//    });

//    $('#containerchart').mousedown(function () {
//        mouseDown = 1;
//    });

//    $('#containerchart').mouseup(function () {
//        mouseDown = 0;
//    });

//    $('#containerchart').mousemove(function (e) {
//        if (mouseDown === 1) {
//            if (e.pageX > lastX) {
//                var diff = e.pageX - lastX;
//                diff = diff * 3;
//                var xExtremes = chart.xAxis[0].getExtremes();
//                chart.xAxis[0].setExtremes(xExtremes.min - diff, xExtremes.max - diff);
//            }
//            else if (e.pageX < lastX) {
//                let diff = lastX - e.pageX;
//                diff = diff * 3;
//                let xExtremes = chart.xAxis[0].getExtremes();
//                chart.xAxis[0].setExtremes(xExtremes.min + diff, xExtremes.max + diff);
//            }

//            if (e.pageY > lastY) {
//                var ydiff = 0.1 * (e.pageY - lastY);
//                var yExtremes = chart.yAxis[0].getExtremes();
//                chart.yAxis[0].setExtremes(yExtremes.min + ydiff, yExtremes.max + ydiff);
//            }
//            else if (e.pageY < lastY) {
//                let ydiff = 0.1 * (lastY - e.pageY);
//                let yExtremes = chart.yAxis[0].getExtremes();
//                chart.yAxis[0].setExtremes(yExtremes.min - ydiff, yExtremes.max - ydiff);
//            }
//        }
//        lastX = e.pageX;
//        lastY = e.pageY;
//        console.log("lastX: " + lastX);
//        console.log("lastY: " + lastY);
//    });


//});