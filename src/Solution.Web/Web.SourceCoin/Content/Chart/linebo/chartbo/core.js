import { EnumDateTimeFormat_Sperator_DDMMYYYY_HHMMSS } from "./enum.js";

const LIMIT_NODE_TIME = 60;
const initialState = {
    data: [],
    dealType: null,
    dealValue: 0,
    limitArray: LIMIT_NODE_TIME,
    maxValue: 2000,
    minValue: 1000,
    indexCurrentNode: 0,
    indexLineRed: LIMIT_NODE_TIME - 30 - 5,
    indexLineGreen: LIMIT_NODE_TIME - 30 - 2 - 5
};

const state = {
    ...initialState
};
function CallRealtimeData() {
    var urldata = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD";
    var dataprice = 0;
    $.ajax({
        url: urldata,
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        success: function (resultData) {
            //here is your json.
            // process it
            //$("#terminal").append("<br/>"+resultData.USD);
           dataprice = resultData.USD;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert("error");
        },

        timeout: 120000,
    });
    return dataprice;
}
function handleGeneralData() {
    //maxValue = CallRealtimeData();
   
    const { limitArray, maxValue, indexLineRed, indexLineGreen } = state;
  
    let data = Array.from(Array(limitArray));
    let time = moment();
    data = data.map((d, index) => {
        if (index > 0) {
            time = moment(time).add(1, "seconds");
        }
        return {
            ...d,
            index,
            timeFormat: time.format(EnumDateTimeFormat_Sperator_DDMMYYYY_HHMMSS),
            time: time.valueOf(),
            x: time.valueOf(),
              // y: Math.random(),
            type: "RealtimeDatas",
            lineRed:
                index === indexLineRed - 1
                    ? 0
                    : index === indexLineRed
                        ? maxValue
                        : null,
            lineGreen:
                index === indexLineGreen - 1
                    ? 0
                    : index === indexLineGreen
                        ? maxValue
                        : null
        };
    });
     //console.log(data);
    return data;
}

function initialXAxisPlotLine(xAxis, valueGreenLine, valueRedLine) {
    const keyLineGreen = "plot-line-2";
    const keyLineRed = "plot-line-1";
    xAxis.removePlotLine(keyLineGreen);
    xAxis.removePlotLine(keyLineRed);
    //   console.log("add line");
    xAxis.addPlotLine({
        value: valueRedLine,
        color: "red",
        width: 2,
        id: "plot-line-1"
    });

    xAxis.addPlotLine({
        value: valueGreenLine,
        color: "#46D96C",
        dashStyle: 'Dash',
        width: 2,
        id: "plot-line-2"
    });
    xAxis.renderLine();
}
//Highcharts.getJSON('https://www.highcharts.com/samples/data/aapl-c.json', function (data) {
    Highcharts.chart("container", {
        chart: {
            type: "areaspline",
            //type: "candlestick",
            gridLineColor: "#2D4154",
            animation: Highcharts.svg, // don't animate in old IE
            marginRight: 10,
            backgroundColor: "#041122",
            plotBorderColor: '#2d4154',
            plotBorderWidth: 0.5,
            events: {
                //load: function () {

                //    // set up the updating of the chart each second
                //    var series = this.series[0];
                //    setInterval(function () {
                //        var x = (new Date()).getTime(), // current time
                //            y = Math.round(Math.random() * 100);
                //        series.addPoint([x, y], true, true);
                //    }, 1000);
                //}

                load: function () {
                    const series = this.series[0];
                    const xAxis = this.xAxis[0];
                    // set up the updating of the chart each second

                    let i = 0;
                    const indexRedLine = series.data.length - 2;
                    const indexGreenLine = series.data.length - 5;
                    let valueGreenLine = series.data[series.data.length - 35].x;
                    let valueRedLine = series.data[series.data.length - 5].x;
                    setInterval(function () {
                        let data = [...series.data];
                        //var datavalue_y = 0;//Math.floor(Math.random() * 500000) + 22000;
                        //var urldata = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD";
                      
                        data[i].y = Math.floor(Math.random() * 500000) + 22000;
                        
                        if (i === 0) {
                            initialXAxisPlotLine(xAxis, valueGreenLine, valueRedLine);
                        }

                        if (data[i].x === valueRedLine) {
                            //console.log("deal");
                            // CallRealtimeData();
                            // code handle here
                            $("#terminal").append(
                                "<span>Time: " +
                                moment(data[i].x).format(
                                    EnumDateTimeFormat_Sperator_DDMMYYYY_HHMMSS
                                ) +
                                " - Stock value: " +
                                data[i].y +
                                "</span><br />"
                            );
                           
                        }
                        // Reset Data
                        if (i === data.length - 4) {
                           
                            if (data.length >= 60) {
                            
                                var i_new = i - 30;
                                i -= i_new;
                               
                                data = data.slice(-(i_new));
                                //data = data.slice(-60);
                            }
                            data = [...data, ...handleGeneralData()];
                            valueGreenLine = data[data.length - 35].x;
                            valueRedLine = data[data.length - 5].x;
                            initialXAxisPlotLine(xAxis, valueGreenLine, valueRedLine);

                        } else {
                            i += 1;
                        }
                        series.setData(data, true, true, true);
                        series.drawPoints();
                    }, 1000);
                }
            }
        },
        //rangeSelector: {
        //    buttons: [{
        //        count: 1,
        //        type: 'minute',
        //        text: '1M'
        //    }, {
        //        count: 5,
        //        type: 'minute',
        //        text: '5M'
        //    }, {
        //        type: 'all',
        //        text: 'All'
        //    }],
        //    inputEnabled: true,
        //    selected: 0
        //},
        time: {
            useUTC: false
        },

        title: {
            text: ""
        },

        accessibility: {
            announceNewData: {
                enabled: true,
                minAnnounceInterval: 60000,
                announcementFormatter: function (allSeries, newSeries, newPoint) {
                    if (newPoint) {
                        //console.log("New point added. Value: " + newPoint.y);
                        return "New point added. Value: " + newPoint.y;
                    }
                    return false;
                }
            }
        },

        xAxis: {
            color:"#fff",
            type: "datetime",
            tickPixelInterval: 150,
            //minorTickInterval: 'auto',
            labels: {
                color: "#fff"
                //enabled: false
            }
        },

        yAxis: {
            title: {
                text: null
            },
            labels: {
                //enabled: false
            }
        },

        tooltip: {
            enabled: false,
            headerFormat: "<b>{series.name}</b><br/>",
            pointFormat: "{point.x:%Y-%m-%d %H:%M:%S}<br/>{point.y:.2f}"
        },

        legend: {
            enabled: false
        },

        exporting: {
            enabled: false
        },

        series: [
            {
                name: "Stock Data",
                data: (function () {
                    //let dataStock = CallRealtimeData();//handleGeneralData();
                    let dataStock = handleGeneralData();
                    return dataStock;
                })(),
                //data: CallRealtimeData(),
                threshold: null,
                fillColor: {
                    linearGradient: {
                        x1: 0,
                        y1: 0,
                        x2: 0,
                        y2: 1
                    },
                    stops: [
                        [0, 'rgba(4, 17, 34, 0.27)'],
                        [1, 'rgba(45, 65, 84, 0.19)']
                    ]
                    //stops: [
                    //    [0, Highcharts.getOptions().colors[0]],
                    //    [1, Highcharts.color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                    //]
                }
            }
        ]
    });
//});
