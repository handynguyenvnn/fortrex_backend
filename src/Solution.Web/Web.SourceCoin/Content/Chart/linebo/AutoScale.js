
/// mouse wheel
var cpw = 20;
var defaultscale = 3;
var scalemax = 50;
var scalemin = 5;
// JSLint options:
/*global Highcharts, document */

(function (H) {
    //var zoomRatio = 1;
    //var lastX;
    //var lastY;
    //var mouseDown=1;

    //var setZoom = function () {
    //    var xMin = chart.xAxis[0].getExtremes().min;//.dataMin;
    //    var xMax = chart.xAxis[0].getExtremes().max;//.dataMax;
    //    var yMin = chart.yAxis[0].getExtremes().min;//.dataMin;
    //    var yMax = chart.yAxis[0].getExtremes().max;//.dataMax;
    //    chart.xAxis[0].setExtremes(xMin + (1 - zoomRatio) * xMax, xMax * zoomRatio);
    //    chart.yAxis[0].setExtremes(yMin + (1 - zoomRatio) * yMax, yMax * zoomRatio);
    //    //chart.yAxis[0].setExtremes(chart.dataMin, yMax * zoomRatio, true, true);
    //};
    ////internal functions
    //function stopEvent(e) {
    //    if (e) {
    //        if (e.preventDefault) {
    //            e.preventDefault();
    //        }
    //        if (e.stopPropagation) {
    //            e.stopPropagation();
    //        }
    //        e.cancelBubble = true;
    //    }
    //}
    ////the wrap
    //H.wrap(H.Chart.prototype, 'render', function (proceed) {
    //    //debugger;
    //    var chart = this,
    //        mapNavigation = chart.options.mapNavigation;
    //    proceed.call(chart);
    //    // Add the mousewheel event
    //    //H.addEvent(chart.container, document.onmousewheel === undefined ? 'DOMMouseScroll' : 'mousewheel', function (event) {
    //    //    event.preventDefault();
    //    //    if (event.deltaY > 0) {
    //    //        if (zoomRatio > 0.7) {
    //    //            zoomRatio = zoomRatio - 0.1;
    //    //            //if ((cpw - defaultscale) >= scalemin) {
    //    //            //    cpw -= defaultscale;
    //    //            //    chart.update({
    //    //            //        plotOptions: {
    //    //            //            series: {
    //    //            //                pointPadding: 0,
    //    //            //                groupPadding: 0,
    //    //            //                pointWidth: cpw
    //    //            //            },
    //    //            //            candlestick: {
    //    //            //                pointPadding: 0,
    //    //            //                groupPadding: 0,
    //    //            //                pointWidth: cpw
    //    //            //            }
    //    //            //        }
    //    //            //    });
    //    //                //const ymin = chart.yAxis[0].minRange;
    //    //                ////console.log(ymin);
    //    //                //chart.yAxis[0].update({
    //    //                //    minRange: ymin + (ymin * 1 /100)
    //    //                //});
    //    //            //}
    //    //             setZoom();
    //    //            //buildfunction._zoomFocus();
    //    //        }
    //    //    }
    //    //    else if (event.deltaY < 0){
    //    //        zoomRatio = zoomRatio + 0.1;
    //    //        //if ((cpw + 1) <= scalemax){
    //    //        //    cpw += 1;
    //    //        //    chart.update({
    //    //        //        plotOptions: {
    //    //        //            series: {
    //    //        //                pointPadding: 0,
    //    //        //                groupPadding: 0,
    //    //        //                pointWidth: cpw
    //    //        //            },
    //    //        //            candlestick: {
    //    //        //                pointPadding: 0,
    //    //        //                groupPadding: 0,
    //    //        //                pointWidth: cpw
    //    //        //            }
    //    //        //        }
    //    //        //    });
    //    //        //    //const ymin = chart.yAxis[0].minRange;
    //    //        //    //console.log(ymin);
    //    //        //    //chart.yAxis[0].update({
    //    //        //    //    minRange: ymin - (ymin * 1 / 100)
    //    //        //    //});
    //    //        //    //buildfunction._zoomFocus();
    //    //        //}
    //    //       setZoom();
    //    //    }
    //    //    stopEvent(event); // Issue #5011, returning false from non-jQuery event does not prevent default
    //    //    return false;
    //    //});

    //    //$('#containerchart').mousemove(function (e) {
    //    //    if (mouseDown === 1) {
    //    //        if (e.pageX > lastX) {
    //    //            var diff = e.pageX - lastX;
    //    //            diff = diff * 3;
    //    //            var xExtremes = chart.xAxis[0].getExtremes();
    //    //            chart.xAxis[0].setExtremes(xExtremes.min - diff, xExtremes.max - diff);
    //    //        }
    //    //        else if (e.pageX < lastX) {
    //    //            let diff = lastX - e.pageX;
    //    //            diff = diff * 3;
    //    //            let xExtremes = chart.xAxis[0].getExtremes();
    //    //            chart.xAxis[0].setExtremes(xExtremes.min + diff, xExtremes.max + diff);
    //    //        }

    //    //        if (e.pageY > lastY) {
    //    //            var ydiff = 0.1 * (e.pageY - lastY);
    //    //            var yExtremes = chart.yAxis[0].getExtremes();
    //    //            chart.yAxis[0].setExtremes(yExtremes.min + ydiff, yExtremes.max + ydiff);
    //    //        }
    //    //        else if (e.pageY < lastY) {
    //    //            let ydiff = 0.1 * (lastY - e.pageY);
    //    //            let yExtremes = chart.yAxis[0].getExtremes();
    //    //            chart.yAxis[0].setExtremes(yExtremes.min - ydiff, yExtremes.max - ydiff);
    //    //        }
    //    //    }
    //    //    lastX = e.pageX;
    //    //    lastY = e.pageY;
    //    //    console.log("lastX: " + lastX);
    //    //    console.log("lastY: " + lastY);
    //    //});
    //});

    Highcharts.Chart.prototype.callbacks.push(function (chart) {
        H.addEvent(chart.container, 'mousewheel', function (e) {
            let xAxis = chart.xAxis[0];
            let extremes = xAxis.getExtremes();
            let step = (extremes.dataMax - extremes.dataMin) / 100 * 7;
            let newMin = extremes.min;
            //console.log("rang: " + (extremes.max - newMin) / 1000);
            
            if (e.deltaY < 0) {
                newMin += step;
                
            } else {
                newMin -= step;
            }
            const rangtimes = (extremes.max - newMin) / 1000;
            if (rangtimes >= 50 && rangtimes <= 3600) {
                xAxis.setExtremes(newMin, extremes.max, true);
            } else if (rangtimes<50) {
                let diff = 120000;
                chart.xAxis[0].setExtremes(extremes.dataMax - diff, extremes.dataMax);
            } else {
                return false;
            }
            if (rangtimes > 50 && rangtimes <= 120) {
                //chart.xAxis[0].minPixelPadding
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 40
                            maxPointWidth: 40
                        },
                        candlestick: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 40,
                            maxPointWidth: 40
                        }
                    }
                });
                const xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].update(
                    {
                        max: (xExtremes.dataMax + 60000),
                        overscroll: 60 * 1000,
                    }
                );
            }
            else if (rangtimes > 120 && rangtimes <= 180) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 20,
                            maxPointWidth: 20
                        },
                        candlestick: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 20,
                            maxPointWidth: 20
                        }
                    }
                });
                const xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].update(
                    {
                        max: (xExtremes.dataMax + 60000),
                        overscroll: 120 * 1000,
                    }
                );
            } else if (rangtimes > 180 && rangtimes <= 240) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 15,
                            maxPointWidth: 15
                        },
                        candlestick: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 15,
                            maxPointWidth: 15
                        }
                    }
                });
                const xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].update(
                    {
                        max: (xExtremes.dataMax + 60000),
                        overscroll: 120 * 1000,
                    }
                );
            } else if (rangtimes > 240 && rangtimes <= 300) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 10
                            maxPointWidth: 10
                        },
                        candlestick: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 10
                            maxPointWidth: 10
                        }
                    }
                });
                const xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].update(
                    {
                        max: (xExtremes.dataMax + 60000),
                        overscroll: 120 * 1000,
                    }
                );
            }
            else if (rangtimes > 300) {
                chart.update({
                    plotOptions: {
                        series: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 5
                            maxPointWidth: 5
                        },
                        candlestick: {
                            pointPadding: 0,
                            groupPadding: 0,
                            //pointWidth: 5
                            maxPointWidth: 5
                        }
                    }
                });
                const xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].update(
                    {
                        max: (xExtremes.dataMax + 60000),
                        overscroll: 120 * 1000,
                    }
                );
            }
        });
    });
}(Highcharts));

