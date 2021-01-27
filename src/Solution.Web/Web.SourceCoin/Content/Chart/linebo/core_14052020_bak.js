
let stickeyPainame = localStorage.getItem("stickeyPainame");
if (stickeyPainame !== "" && stickeyPainame !== null) {
    if (stickeyPainame.indexOf(",") >= 0) {
        window.sym = localStorage.getItem("stickeyPainame").substring(0, stickeyPainame.indexOf(","));
    } else {
        const getstickeyPainamefirst = localStorage.getItem("stickeyPainame").substring(0, 8);
        window.sym = getstickeyPainamefirst === "" ? "BTC_USD" : getstickeyPainamefirst;
    }
}
let hovertrend = "";
var sym = window.sym;
//import { EnumDateTimeFormat_Sperator_DDMMYYYY_HHMMSS } from "./enum.js";
window.addEventListener("load", function () {
    window.buildfunction = buildfunction;

    buildData._setHideByResize(false);
    loadPairbyUserInit();
    $("#tools-tradepair").empty().text(window.sym.replace('_', '/'));
    setInterval(processing, 2300);
});
function processing() {

    //$(".selllist .p-row .progress-bar").each(function (index) {
    //    var volumntrade = Math.floor((Math.random() * 100) + 1);
    //    var randompercent = Math.floor((Math.random() * 100) + 1);
    //    if (volumntrade > randompercent) {
    //        $(this).css("width", volumntrade + "%");
    //        $(this).parent().parent().css("background-color", "rgb(222, 226, 230)");
    //    } else {
    //        $(".selllist .p-row").removeAttr("style");
    //    }
    //});
    var volumntrade = Math.floor((Math.random() * 100) + 1);
    var randompercent = Math.floor((Math.random() * 100) + 1);
    if (volumntrade > randompercent) {
       // $(this).css("width", volumntrade + "%");
        //$(this).parent().parent().css("background-color", "rgb(222, 226, 230)");
        if ((100 - volumntrade)>25) {
            $(".slider-volume .volume-higher").css("height", volumntrade + "%");
            $(".slider-volume .volume-lower").css("height", (100 - volumntrade) + "%");
        }
      
    } else {
        if ((100 - volumntrade) > 25) {
            $(".slider-volume .volume-lower").css("height", volumntrade + "%");
            $(".slider-volume .volume-higher").css("height", (100 - volumntrade) + "%");
        }
       
    }
   
}
let statushidecontroll = true;
let statushietools = false;
var arrorderline = [];
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
        dashStyle: 'LongDash',
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
    let data = {
    };
    return $.ajax({
        url: "/office/Get_PairName_by_UserId",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            let rowsarray = "", rows = "";
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
                localStorage.setItem("stickeyPainame", stickeyPainame);
                let pairadd = "<div id='top-pairs-add' class='top-pairs-add' data-toggle='modal' data-target='#marketsModal' data-placement='bottom' onclick='buildfunction._toppairsadd()'><svg version='1.1' id='Capa_1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='0 0 31.444 31.444' style='enable-background:new 0 0 31.444 31.444;' xml:space='preserve'><path d='M1.119,16.841c-0.619,0-1.111-0.508-1.111-1.127c0-0.619,0.492-1.111,1.111-1.111h13.475V1.127 C14.595,0.508,15.103,0,15.722,0c0.619,0,1.111,0.508,1.111,1.127v13.476h13.475c0.619,0,1.127,0.492,1.127,1.111 c0,0.619-0.508,1.127-1.127,1.127H16.833v13.476c0,0.619-0.492,1.127-1.111,1.127c-0.619,0-1.127-0.508-1.127-1.127V16.841H1.119z'></path></svg>";
                //pairadd += "<div id='panel-markets' class='dropdown-menu panel-market'>";
                //pairadd += "</div > ";
                pairadd += "</div > ";
                $("#toplist-pair").append(pairadd);

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
            let rowsarray = "", rows = "";
            $("#tbForexmarkets").empty();
            $("#tbmarkets").empty();
            if (result.Result.length > 0) {
                let countitemforex = 0, countitemcrypto = 0;
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
            url: "/Trade/Pairs",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                let rowsarray = "", rows = "";
                $("#tradepairs-main").text("");
                if (result.Result.length > 0) {

                    for (var i = 0; i < result.Result.length; i++) {
                        rows = "";
                        const item = result.Result[i];
                        const _pairname = item.PairName.replace('/', '_');
                        if (item.PriceChangePercent < 0) {
                            if (item.PairSymbol === sym) {
                                rows += " <li class='media assets-item assets-down active' onclick=buildfunction._addFavorite('" + _pairname + "');>";
                            } else {
                                rows += " <li class='media assets-item assets-down' onclick=buildfunction._addFavorite('" + _pairname + "');>";
                            }

                            rows += "<img class='i-symbol' src='/Images/Icon/down-arrow.png' />";
                        } else if (item.PriceChangePercent > 0) {
                            if (item.PairSymbol === sym) {
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
    indexLineGreen: 0,
};

const state = {
    ...initialState
};
//let lastTime = null,
//    lastOpen = 0,
//    lastHigh = 0,
//    lastLow = 0,
//    lastClose = 0, lastVolumn = 0;
//function callRealtimeData() {
//    // this is where you paste your api key
//    var apiKey = "272b0ff3ac5f1592453d95545232dd6b68f8889fbf974f2385a58b6c388e9653";
//    var ccStreamer = new WebSocket('wss://streamer.cryptocompare.com/v2?api_key=' + apiKey);
//    ccStreamer.onopen = function onStreamOpen() {
//        var subRequest = {
//            "action": "SubAdd",
//            "subs": ["24~CCCAGG~BTC~USD~m"]
//        };
//        ccStreamer.send(JSON.stringify(subRequest));
//    }

//    ccStreamer.onmessage = function onStreamMessage(message) {
//        // console.log(event.data);
//        var message = JSON.parse(event.data);
//        console.log(JSON.stringify(message));
//        if (message.TYPE.toString() === "24") {
//            //console.log("24:" + JSON.stringify(message));
//            //let chart = this.$refs.highcharts.chart;
//            var valuedecimal = Math.floor((Math.random() * 100) + 1);
//            if (valuedecimal <= 100) {
//                valuedecimal = parseFloat(valuedecimal / 1000);
//            } else {
//                valuedecimal = parseFloat(valuedecimal / 9999);
//            }
//            var series = chart.series[0];
//            var volumn = chart.series[1];
//            //var time = message.TIME ;
//            //var open = message.OPEN + valuedecimal;
//            //var high = message.HIGH + valuedecimal;
//            //var low = message.LOW + valuedecimal;
//            //var close = message.CLOSE + valuedecimal;
//            lastTime = message.TIME;
//            lastOpen = message.OPEN;
//            lastHigh = message.HIGH;
//            lastLow = message.LOW;
//            lastClose = message.CLOSE;
//            lastVolumn = message.VOLUMETO;
//            var newPoint = [lastTime, lastOpen, lastHigh, lastLow, lastClose];
//            var newVolumn = [lastTime, message.VOLUMEFROM, message.VOLUMETO];
//            series.addPoint(newPoint, true, false);
//            volumn.addPoint(newVolumn, true, false);
//            //series.setData(data, true, true, true);
//            //series.drawPoints();
//            //renderCurrentPriceIndicator(chart);

//        }


//        //const resonse =  callRealtimeData("BTC_USDT");
//        //const { OPEN, HIGH, LOW, CLOSE } = resonse;
//    }

//}
function callRealtimeData() {
    //buildfunction._serverTime();
    let data = {
        pair: window.sym,
        interval: '5s'
    };

    return $.ajax({
        url: "/office/marketPrice",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (resultData) {

            return resultData;
        },
        timeout: 120000
    });
}
function move(event) {
    let x = event.pageX,
        y = event.pageY,
        path = ['M', chart.plotLeft, y,
            'L', chart.plotLeft + chart.plotWidth, y,
            'M', x, chart.plotTop,
            'L', x, chart.plotTop + chart.plotHeight];

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
        var series = chart.series[0];
        var volumn = chart.series[1];
        //const { maxValue, indexLineRed, indexLineGreen } = state;
        let valuedecimal = Math.floor((Math.random() * 100) + 1);
        let ran = valuedecimal;
        if (valuedecimal <= 100) {
            valuedecimal = parseFloat(valuedecimal / 1000);
        } else {
            valuedecimal = parseFloat(valuedecimal / 9999);
        }

        let time = moment();
        var open, high, low, close;

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

    const { maxValue, indexLineRed, indexLineGreen } = state;

    let data = Array.from(Array(limitArray));
    let time = moment();
    let timeUTC = moment.utc();
    data = data.map((d, index) => {
        time = moment(time).add(1, "seconds");
        timeUTC = moment.utc(timeUTC).add(1, "seconds");
        return index % 2 === 0
            ? [
                time.valueOf(),
                null, // open
                null, // high
                null, // low
                null // close
            ]
            : [time.valueOf(), null, null, null, null, null];
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
        return index % 2 === 0
            ? [
                time.valueOf(),
                null, // open
                null, // high
                null, // low
                null // close
            ]
            : [time.valueOf(), null, null, null, null, null];
    });
    return data;
}

// theme chart
Highcharts.theme = {
    colors: ['#A2A6AB', '#90ee7e', '#f45b5b', '#7798BF', '#aaeeee', '#ff0066',
        '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee'],
    chart: {
        //backgroundColor: {
        //    linearGradient: { x1: 0, y1: 0, x2: 1, y2: 1 },
        //    stops: [
        //        [0, '#2a2a2b'],
        //        [1, '#3e3e40']
        //    ]
        //},

        plotBorderColor: '#656c7d'
    },

    xAxis: {
        range: 2 * 60 * 1000, //5 minutes
        minRange: 2 * 60 * 1000,
        //maxRange: 24 * 60 * 60 * 1000, //24h
        //tickInterval: 2,
        tickInterval: 0.01,
        pointInterval: 5000,
        overscroll: 60 * 1000, // 10 seconds
        pointRange: 60 * 1000,
        //visibility: "visible",
        type: 'datetime',
        offset: 0,
        //tickPixelInterval: 100,
        //crosshair: {
        //    label: {

        //        enabled: true,
        //        padding: 8,
        //        visibility: "visible"
        //    }
        //},
        opacity: 1,
        width: "100%",
        crosshair: {
            enabled: false,
            label: {
                enabled: false,
                padding: 8,
                style: {
                    color: '#656c7d'
                }
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
        //minRange: 0,
        //maxRange: 10,
        //endOnTick: false,
        //startOnTick: false,
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
            dataLabels: {
                color: '#656c7d',
                style: {
                    fontSize: '14px',
                    width: "0.5px"
                }
            },
            marker: {
                lineColor: '#656c7d',
                width: "0.5px"
            }
        },
        boxplot: {
            fillColor: '#656c7d'
        },
        candlestick: {
            lineColor: '#656c7d'
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
//window.chart = new Highcharts.stockChart('container-chart', {

window.chart = new Highcharts.stockChart('container-chart', {
    chart: {
        renderTo: 'container-chart',
        animation: false,
        panning: true,
        //animation: {
        //    duration: 1000,
        //    easing: function (t) { return t; }
        //},
        "spacingRight": 1,
        "padding": 0,
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

        events: {
            load: function () {

                let i = 0;
                setInterval(async () => {

                    const series = this.series[0];
                    //let data = [...series.data];

                    const resonse = await callRealtimeData();
                    const { OPEN, HIGH, LOW, CLOSE, TIMES } = resonse;
                    //if (i <= data.length - 1) {
                    if (i > 0) {
                        if (CLOSE > 0 && TIMES > 0) {
                            //const xAxis = chart.xAxis[0];
                            const typecurrentchart = chart.series[0].type;
                            if ("areaspline" === typecurrentchart) {

                                this.series[0].addPoint([TIMES, OPEN, HIGH, LOW, CLOSE], true, true, false);
                            } else {
                                //this.series[0].addPoint([TIMES, OPEN, HIGH, LOW, CLOSE], true, true, true);
                                setData(series, TIMES, OPEN, HIGH, LOW, CLOSE, 5, 'candlestick');
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

                            chart.series[2].setData([]);
                            chart.series[2].addPoint({
                                x: TIMES,
                                y: CLOSE,
                                title: "<svg class='pulse-svg' width='60px' height='6px' viewBox='0 0 50 50' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='65'></circle><circle class='circle first-circle' fill='#53BB63' cx='25' cy='25' r='45'></circle><circle class='circle second-circle' fill='#53BB63' cx='25' cy='25' r='40'></circle><circle class='circle third-circle' fill='#53BB63' cx='25' cy='25' r='35'></circle><circle class='circle' fill='#53BB63' cx='25' cy='25' r='25'></circle></svg>",
                                useHTML: true
                            }, true, false);

                            //chart.xAxis[0].update({ overscroll: 30 * 1000 });

                            //focus trend range
                            if (hovertrend !== "") {
                                if (hovertrend === "buy") {
                                    buildfunction._hoverRangeBuy();
                                } else {
                                    buildfunction._hoverRangeSell();
                                }
                            }
                        }
                        i += 1;
                    } else {
                        i += 1;
                        //i = 0;
                        ChartInit("");
                        buildfunction._zoomTime('2min', 0);
                    }
                    //series.setData(data);
                }, 1000);
                //ChartInit("");
                setInterval(() => {
                    tradePairInit();
                }, 1000);
                //setInterval(() => {
                //    ChartInit("");
                //}, 2000);
            }
        }
    },
    resize: {
        enabled: true
    },

    xAxis: {
        crosshair: {
            enabled: false
        },
        //startOnTick: true,
        overscroll: 60 * 1000, // 10 seconds
        //groupPixelWidth: 15,
        //pointInterval: 5000,
        //pointRange: 60 * 1000,
        
        //visibility: "visible",
        type: 'datetime',
        offset: 0,
        //minPadding: 0.15,
        opacity: 1,
        width: "100%",

        //tickPixelInterval: 120,
        //maxPadding: 1.5,
        range: 2 * 60 * 1000,
        minRange: 2*60*1000,
        maxRange: 5 * 60 * 1000,
        //max: end,
    },
    yAxis: [{
        visibility: "visible",
        //tickPixelInterval: 100,
        opposite: true,

        labels: {
            align: 'right',
            x: -3,
            visibility: "visible",
            //format: '{value:.7f}',
        },
        height: '100%',
        offset: 0,//100
        lineWidth: 2,
        resize: {
            enabled: true
        },
        //crosshair: {
        //    offset: 0,
        //    visibility: "visible",
        //    snap: false,
        //    color: '#888',
        //    label: {
        //        offset: -50,
        //        backgroundColor: {
        //            linearGradient: { x1: 0, y1: 0, x2: 1, y2: 1 },
        //            stops: [
        //                [0, '#2a2a2b'],
        //                [1, '#3e3e40']
        //            ]
        //        },
        //        color: '#000',
        //        enabled: true,
        //        format: '{value:.4f}',
        //        visibility: "visible"
        //    }
        //}
    }
    ],
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
        enabled: false,
        enableButtons: false,
        enableMouseWheelZoom: false,
        //zoomType: 'x'
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
        buttons: [{
            count: 2,
            type: 'minute',
            text: '2M',
            //dataGrouping: {
            //    //smoothed: true,
            //    //groupAll: true,
            //    //approximation: "candlestick",
            //    // forced: true,
            //    units: [
            //        [
            //            'second',
            //            [1, 2, 5, 10, 15, 30]
            //        ],
            //        [
            //            'minute',
            //            [1, 2, 5, 10, 15, 30]
            //        ]
            //    ]
            //}
        }, {
            count: 5,
            type: 'minute',
            text: '5M',
            //dataGrouping: {
            //    groupAll: true,
            //    //approximation: "candlestick",
            //    // forced: true,
            //    smoothed: true,
            //    units: [
            //        [
            //            'second',
            //            [1, 2, 5, 10, 15, 30]
            //        ],
            //        [
            //            'minute',
            //            [1, 2, 5, 10, 15, 30]
            //        ]
            //    ]
            //}

        }, {
            count: 15,
            type: 'minute',
            text: '15M',
            //dataGrouping: {
            //    //groupAll: true,
            //    //approximation: "candlestick",
            //    // forced: true,
            //    smoothed: true,
            //    units: [
            //        [
            //            'second',
            //            [1, 2, 5, 10, 15, 30]
            //        ],
            //        [
            //            'minute',
            //            [1, 2, 5, 10, 15, 30]
            //        ]
            //    ]
            //}
        }, {
            count: 30,
            type: 'minute',
            text: '30M'
        }, {
            count: 3,
            type: 'hour',
            text: '3h'
        }, {
            count: 1,
            type: 'day',
            text: '1 day'
        }],
        inputEnabled: false,
        selected: 1
    },

    plotOptions: {
        series: {

            //cropThreshold: 500,
            //shadow: false,
            //groupPadding: 0.2,
            //connectNulls: true,
            //connectEnds: true,
            pointInterval: 5000,//2 *3600 * 1000,
            //pointIntervalUnit: "seconds",
            enableMouseTracking: false,
            //marker: {
            //    radius: 2
            //}
            //dataGrouping: {
            //    enabled: true,
            //    force: true,
            //    units: [['seconds', [1, 2, 5]]]
            //},
            //pointRange: 5 * 1000,
            //pointPadding: 0,
            //groupPadding: 0
        }
    },
    navigator: {
        enabled: false,
        xAxis: {
            overscroll: 60 * 1000 // 10 seconds
        }
    },
    scrollbar: {
        showFull: false,
        overscroll: 60 * 1000 // 10 seconds
    },

    series: [{
        type: 'candlestick', //candlestick
        id: 'dataseries',
        name: 'dataseries',
        data: [],//ohlc,
        //pointWidth: 10,
        showInLegend: true,
        allowPointSelect: true,
        lastVisiblePrice: {
            enabled: true,
            label: {
                enabled: true
            },
            color: "#D3D5D9"
        },
        marker: {
            enabled: false,
            radius: 0
        },
        shadow: true,
        //lastPrice: {
        //    enabled: true,
        //    color: 'red'
        //},
        x: 0,
        y: 0,
        offset: 0,
        pointInterval: 5000, // a candlestick is 5s
        dataGrouping: {
            //groupAll: true,
            //groupPixelWidth: 300,
            //approximation: "candlestick",
            //forced: true,
            //smoothed: true,
            units: [[
                'minute',
                [1, 2, 5, 10, 15, 30]
            ], [
                'hour',
                [1, 2, 3, 4, 6, 8, 12]
            ], [
                'day',
                [1]
            ], [
                'week',
                [1]
            ], [
                'month',
                [1, 3, 6]
            ], [
                'year',
                null
            ]]
        },

    }, {
        type: 'flags',
        id: 'aapl-flags',
        data: [],//ohlc,
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
        data: [],//ohlc,
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
        data: [],//ohlc,
        onSeries: 'dataseries',
        fillColor: 'red',
        lineColor: 'none',
        width: 10,
        height: 10,
        y: 0,
        useHTML: true
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
function setData(series, x, y, high, low, close, period, type) {
    let temp = series.data.length > 0 ? series.data.slice(-1)[0] : undefined;
    if (temp !== undefined && temp !== null) {
        let x2 = temp.x;
        if ((period * 1000) <= x - x2) {
            //x = (new Date()).getTime();
            series.addPoint({
                x: x,
                y: y,
                high: high,
                low: low,
                close: close
            }, true, true, true);
            //$('#mes').append('<br/>new: ' + x + 'close: ' + close);
        } else {
            newValue = [x,
                temp.y,
                high,
                temp.low, //low
                close //close
            ];
            temp.update(newValue, false, false);
            series.chart.redraw(true);
            // series.chart.redraw(true);

            //$('#mes').append('<br/>update: ' + x + 'close: ' + close);
        }
    }

}

function ChartInit() {
    let data = {
        pair: window.sym
    };
    $("#select-trade").val(window.sym);
    let ohlc = [], lasttime = 0, lastprice = 0,
        volume = [];
    $.ajax({
        url: "/Trade/Candlesticks",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            buildfunction.loading();
        },
        success: function (result) {
            if (result.Result.length > 0) {
                for (var i = 0; i < result.Result.length; i++) {
                    let item = result.Result[i];
                    let _lasttime = parseInt(item.Times);
                    lasttime = _lasttime >= parseInt(lasttime) ? _lasttime : lasttime;
                    ohlc.push([
                        item.Times, // the date
                        item.Open, // open
                        item.High, // high
                        item.Low, // low
                        item.Close // close
                    ]);
                    if (i === result.Result.length - 1) {
                        lastprice = item.Close;
                    }

                    //volume.push([
                    //    item.Times, // the date
                    //    //item.VolumeFrom, // VolumeFrom
                    //    item.VolumeTo //VolumeTo
                    //]);
                }
                //highcharts-series-group
                let typecurrentchart = chart.series[0].type;
                if ("areaspline" === typecurrentchart) {
                    chart.series[0].update({
                        type: "candlestick"
                    });
                    chart.series[0].setData(ohlc);
                    chart.series[0].update({
                        type: "areaspline"
                    });
                } else {
                    chart.series[0].setData(ohlc);
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
                let xAxis = chart.xAxis[0];

                i = 0;
                //const indexRedLine = chart.series[0].data.length - 45;
                //const indexGreenLine = chart.series[0].data.length - 15;

                //let valueGreenLine = chart.series.data[indexGreenLine].x;
                //let valueRedLine = chart.series.data[indexRedLine].x;

                buildfunction._serverTime().done(function (time) {
                    //update max time
                    //console.log("second: "+ (parseInt(time) * 1000));
                    //xAxis.update({ max: lasttime + (parseInt(time) * 1000) });
                    initialXAxisPlotLine(xAxis, (lasttime + (parseInt(time) * 1000)) - 30000, lasttime + (parseInt(time) * 1000));
                });

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

                const xExtremes = chart.xAxis[0].getExtremes();
                const yExtremes = chart.yAxis[0].getExtremes();
                //console.log("xExtremes.dataMin: " + xExtremes.dataMin + '-' + xExtremes.min);
                //let xExtremes = chart.xAxis[0].getExtremes();
                let diff = 5000;
                chart.xAxis[0].setExtremes(xExtremes.min - diff, xExtremes.max - diff);

                //chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
                //chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);
                //chart.xAxis[0].update({ overscroll: 60 * 1000 });
            }
            buildfunction.stopLoading();
        },
        error: function () {
            buildfunction.loadingError();
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

var buildfunction = {
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
        sClick.play();
    },
    soundbookorder: function () {
        let sOrder = document.getElementById("soundbookorder");
        sOrder.play();
    },
    soundoverorder: function () {
        let soverOrder = document.getElementById("soundoverorder");
        soverOrder.play();
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
        chart.setSize(chart.options.chart.width, height);
        //chart.reflowNowManual(width);
        //chart.reflowNowManual();
    },
    changechartbysymbol: function (symbol) {
        buildfunction.clickaudio();
        $("#tools-tradepair").empty().text(symbol.replace('_', '/'));
        $("#tool-tradingcurent-icon").attr("src", "/images/symbol/flags/" + symbol + ".png");
        chart.series[0].setData([]);
        window.sym = symbol !== null ? symbol : "BTC_USD";
        ChartInit(window.sym);
        if (symbol === "BTC_USD") {
            chart.yAxis[0].update({
                minRange: 500,
                maxRange: 1000
            });
            chart.xAxis[0].update({
                range: 60000
            });
        } else {
            chart.yAxis[0].update({
                minRange: 0,
                maxRange: 200
            });
        }
        $("#toplist-pair div.active").removeClass('active');
        $("#toplist-pair #toppair-" + symbol).addClass('active');
    },
    flagPrice: function (type,price=0) {

        let yLastPrice = 0;
        if (price<=0) {
            if (chart.series[0].type === "candlestick") {

                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1][3];
            } else {
                yLastPrice = chart.series[0].yData[chart.series[0].data.length - 1];
            }
        } else {
            yLastPrice = price;
        }
        
        let xLastTime = chart.series[0].xData[chart.series[0].data.length - 1];

        let _amounttrade = parseInt($("#order_amount").val(), 0);
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
            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(180deg)');
            setTimeout(function () {
                const w = parseFloat($("#rightside").css("width").replace('px', ''));
                buildfunction.chartreflowNow(w);
            }, 350);

            statushidecontroll = false;
        } else {
            $("#leftside").removeClass('hideleftside');
            $("#rightside").removeClass('fullrightside');
            $(".fbt-leftside-hide-controller").css('margin-left', '275px');
            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(0deg)');
            setTimeout(function () {
                const w = parseFloat($("#rightside").css("width").replace('px', ''));
                buildfunction.chartreflowNow(w - 140);
            }, 350);

            statushidecontroll = true;
        }
        //chart.setSize(null);
    },
    _setHideByResize: function (hideStatus) {
        if (hideStatus) {
            $("#leftside").addClass('hideleftside');
            $("#rightside").addClass('fullrightside');
            $(".fbt-leftside-hide-controller").css('margin-left', '0px');
            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(180deg)');
            setTimeout(function () {
                const w = parseFloat($("#rightside").css("width").replace('px', ''));
                buildfunction.chartreflowNow(w - 140);
            }, 350);
            statushidecontroll = false;
        } else {
            $("#leftside").removeClass('hideleftside');
            $("#rightside").removeClass('fullrightside');
            $(".fbt-leftside-hide-controller").css('margin-left', '275px');
            $(".fbt-leftside-hide-controller img").css('transform', 'rotate(0deg)');
            setTimeout(function () {
                const w = parseFloat($("#rightside").css("width").replace('px', ''));
                buildfunction.chartreflowNow(w - 140);
            }, 350);
            statushidecontroll = true;
        }
        //chart.setSize(null);
    },
    _zoomTime: function (valuename, time) {
        var xExt = chart.xAxis[0].getExtremes();
        chart.xAxis[0].setExtremes(xExt.dataMin, xExt.dataMax);
        chart.rangeSelector.buttons[time].setState(time);
        chart.rangeSelector.clickButton(time, time, false);
        $("#rangeSelectorFocus").text(valuename);
        //const xExtremes = chart.xAxis[0].getExtremes;
        //const yExtremes = chart.yAxis[0].getExtremes;
        //chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
        //chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);
        //if (time === 0) {
        //    chart.xAxis[0].update({ overscroll: 30 * 1000 });
        //} else if (time === 1) {
        //    chart.xAxis[0].update({ overscroll: 60 * 1000 });
        //}
        //else if (time === 2) {
        //    chart.xAxis[0].update({ overscroll: 90 * 1000 });
        //}
        //else if (time === 3) {
        //    chart.xAxis[0].update({ overscroll: 120 * 1000 });
        //}
        //else if (time === 4) {
        //    chart.xAxis[0].update({ overscroll: 120 * 1000 });
        //}


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
        let data = { 'pairname': pairname };
        return $.ajax({
            url: "/office/PairName_Favorite_Ins",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('#marketsModal').modal('hide');
                loadPairbyUserInit();
                buildfunction.changechartbysymbol(pairname);
            }
        });
    },
    _deleteFavorite: function (pairname) {
        buildfunction.clickaudio();
        $("#toppair-" + pairname).remove();
        let data = { 'pairname': pairname };
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
        this._setHideByResize(false);
        //$("#slide-tradepairs").removeClass('d-none');
        //$("#slide-tradinghistory").addClass('d-none');
        tradePairInit();
    },
    _hidetools: function () {
        chart.stockTools.showhideBtn.click();
    },
    _hoverRangeBuy: function () {
        hovertrend = "buy";
        let yAxis = chart.yAxis[0];
        const keyrangeUp = "plot-rangebuysell";
        yAxis.removePlotLine(keyrangeUp);
        let yLastPrice = 0;
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
                    linearGradient: { x1: 0, x2: 0, y1: 1, y2: 0 },
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
    },
    _hoverRangeSell: function () {
        hovertrend = "sell";
        let yAxis = chart.yAxis[0];
        const keyrangeUp = "plot-rangebuysell";
        yAxis.removePlotLine(keyrangeUp);
        let yLastPrice = 0;
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
                    linearGradient: { x1: 0, x2: 0, y1: 0, y2: 1 },
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
    }
};
$('#chart-type-Candlestick').click(function () {
    chart.xAxis[0].update({ pointInterval: 5000 });
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/candlestick.png");
    chart.series[0].update({
        type: "candlestick"
        //dataGrouping: {
        //    forced: true,
        //    units: [[
        //        'minute',
        //        [2]
        //    ]]
        //}
    });
});
$('#chart-type-ohlc').click(function () {
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/ohlc.png");
    //  ChartInit();
    chart.series[0].update({
        type: "ohlc"
    });

});
$('#chart-type-Line').click(function () {
    chart.xAxis[0].update({ pointInterval: 1000 });
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/line.png");
    chart.series[0].update({
        type: "spline"
    });
    //buildfunction._zoomTime("5m", 2);
});
$('#chart-type-Areaspline').click(function () {
    chart.xAxis[0].update({ pointInterval: 1000 });
    $("#tool-selecttype-icon").attr("src", "/Images/Icon/tool-chart/linearea.png");
    chart.series[0].update({
        type: "areaspline",
        threshold: null,
        fillColor: {
            linearGradient: {
                x1: 0,
                y1: 0,
                x2: 0,
                y2: 1
            },
            stops: [
                [0, Highcharts.getOptions().colors[0]],
                [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
            ]
        },

    });
});


$('#time15m').click(function () {

    chart.series[0].update({
        dataGrouping: {
            forced: true,
            units: [['minute', [15]]]
        }
    });
});
$('#time30m').click(function () {
    chart.series[0].update({
        dataGrouping: {
            forced: true,
            units: [['minute', [30]]]
        }
    });
});

$('#auto').click(function () {
    //buildfunction.flagPrice("buy");

});
$('#auto2').click(function () {
    //buildfunction.flagPrice("sell");

    //chart.xAxis[0].update({ overscroll: 180 * 1000});
    //chart.series[0].xAxis[0].update({
    //    overscroll: 180 * 1000
    //});
});

$('.highcharts-arrow-left').click(function () {
    if (statushietools) {
        $('#slider-volume').removeClass("slider-volume").addClass("slider-volume-left");
        statushietools = true;
    } else {
        $('#slider-volume').removeClass("slider-volume-left").addClass("slider-volume");
        statushietools = false;
    }
});
$('.highcharts-arrow-right').click(function () {
    if (statushietools) {
        $('#slider-volume').removeClass("slider-volume").addClass("slider-volume-left");
        statushietools = true;
    } else {
        $('#slider-volume').removeClass("slider-volume-left").addClass("slider-volume");
        statushietools = false;
    }
});
$('#resetZoom2').click(function () {
    var tesst = document.querySelector('#main-action-order');
   // console.log(tesst.innerHTML);
});
$('#resetZoom').click(function () {

    const xExtremes = chart.xAxis[0].getExtremes;
    const yExtremes = chart.yAxis[0].getExtremes;
    chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
    chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);

    zoomRatio = 1;

});
var lastX;
var mouseDown;

$('#container-chart').mousedown(function () {
    mouseDown = 1;
});

$('#container-chart').mouseup(function () {
    mouseDown = 0;
});

$('#container-chart').mousemove(function (e) {
    if (chart !== undefined) {
        if (mouseDown === 1) {
            if (e.pageX > lastX) {
                let diff = (e.pageX - lastX) * 200;
                let xExtremes = chart.xAxis[0].getExtremes();

                chart.xAxis[0].setExtremes(xExtremes.min - diff, xExtremes.max - diff);

            }
            else if (e.pageX < lastX) {
                let diff = (lastX - e.pageX) * 200;
                let xExtremes = chart.xAxis[0].getExtremes();

                chart.xAxis[0].setExtremes(xExtremes.min + diff, xExtremes.max + diff);
            }

        }
        // chart.xAxis[0].update({ overscroll: 50 * 1000 });
        lastX = e.pageX;
    }
});
$('#container-chart').on('mousewheel', function (event) {
    const xAxisExtremes = chart.xAxis[0].getExtremes();
    //var diff = (xAxisExtremes.dataMax - xAxisExtremes.dataMin) / 20 ;
    let diff = 20000;



    if (event.originalEvent.wheelDelta >= 0) {
        //mouse up
        //move to left

        chart.xAxis[0].setExtremes(xAxisExtremes.min - diff, xAxisExtremes.max - diff);

    }
    else {
        //mouse down
        //move to right

        chart.xAxis[0].setExtremes(xAxisExtremes.min + diff, xAxisExtremes.max + diff);

    }
    lastX = event.pageX;
});
