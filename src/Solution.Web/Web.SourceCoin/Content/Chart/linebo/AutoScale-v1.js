
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
            let step = 60000;
            const usedata = ((extremes.userMax - extremes.userMin) / 1000);
            if (usedata<=120) {
                step = 60000;
            } else if (usedata > 120 && usedata<=300) {
                step = 120000;
            } else if (usedata > 300 && usedata <= 900) {
                step = 180000;
            }
            else if (usedata > 900 && usedata <= 1800) {
                step = 300000;
            } else {
                step = 60000;
            }
            //(extremes.dataMax - extremes.dataMin) / 100 * 20;
            let newMin = extremes.min;
            //let pointwidthCurrent = chart.series[0].points[0].pointWidth;
            //let steppointwidth = 0;
            //console.log("rang: " + (extremes.max - newMin) / 1000);
            
            if (e.deltaY < 0) {
                newMin += step;
                //if ((pointwidthCurrent + (pointwidthCurrent * 20 / 100)) <= 35) {
                //    steppointwidth = pointwidthCurrent + (pointwidthCurrent * 20 / 100);
                //}
            } else {
                newMin -= step;
                //if ((pointwidthCurrent - (pointwidthCurrent * 20 / 100))>=1.5) {
                //    steppointwidth = pointwidthCurrent - (pointwidthCurrent * 20 / 100);
                //}
            }
            const rangtimes = (extremes.max - newMin) / 1000;
            if (rangtimes >= 50 && rangtimes <= 3600) {
                xAxis.setExtremes(newMin, (extremes.max), true);
            } else if (rangtimes<50){
                let diff = 120000;
                chart.xAxis[0].setExtremes(extremes.dataMax - diff, extremes.dataMax);
            } else {
                return false;
            }
            //if (steppointwidth>0) {
            //    chart.update({
            //        plotOptions: {
            //            series: {
            //                pointPadding: 0,
            //                groupPadding: 0.2,
            //                pointWidth: steppointwidth,
            //                //maxPointWidth: 40
            //            },
            //            candlestick: {
            //                pointPadding: 0,
            //                groupPadding: 0.2,
            //                pointWidth: steppointwidth,
            //                //maxPointWidth: 40
            //            }
            //        }
            //    });
            //}
            
        });
    });
}(Highcharts));

