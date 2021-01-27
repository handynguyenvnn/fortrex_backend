
/// mouse wheel
var cpw = 20;
$(function () {
    Highcharts.setOptions({
        lang: {
            resetZoom: ''
        }
    });

    var zoomRatio = 1;
    var lastX;
    var lastY;
    var mouseDown;
    function createData() {
        var arr = [];
        for (var i = 0; i < 200; i++) {
            arr.push(Math.random() * 100);
        }
        return arr;
    }
    var setZoom = function () {
        var xMin = chart.xAxis[0].getExtremes().dataMin;
        var xMax = chart.xAxis[0].getExtremes().dataMax;
        var yMin = chart.yAxis[0].getExtremes().dataMin;
        var yMax = chart.yAxis[0].getExtremes().dataMax;

        chart.xAxis[0].setExtremes(xMin + (1 - zoomRatio) * xMax, xMax * zoomRatio);
        chart.yAxis[0].setExtremes(yMin + (1 - zoomRatio) * yMax, yMax * zoomRatio);
    };



    $('#containerchart').bind('mousewheel', function (event) {
        alert(111);
        event.preventDefault();

        if (event.deltaY > 0) {
            if (zoomRatio > 0.7) {
                zoomRatio = zoomRatio - 0.1;
                setZoom();
                cpw += 5;
                chart.update({
                    plotOptions: {
                        candlestick: {
                            pointWidth: cpw,
                        }

                    }
                });
            }

        }
        else if (event.deltaY < 0) {
            zoomRatio = zoomRatio + 0.1;
            setZoom();
            cpw -= 5;
            chart.update({
                plotOptions: {
                    candlestick: {
                        pointWidth: cpw,
                    }

                }
            });
        }
        //console.log("111"+ event.deltaY, event.deltaFactor, event.originalEvent.deltaMode, event.originalEvent.wheelDelta);
    });


    $('#resetZoom').click(function () {
        var xExtremes = chart.xAxis[0].getExtremes;
        var yExtremes = chart.yAxis[0].getExtremes;
        chart.xAxis[0].setExtremes(xExtremes.dataMin, xExtremes.dataMax);
        chart.yAxis[0].setExtremes(yExtremes.dataMin, yExtremes.dataMax);
        zoomRatio = 1;
    });

    $('#containerchart').mousedown(function () {
        mouseDown = 1;
    });

    $('#containerchart').mouseup(function () {
        mouseDown = 0;
    });

    $('#containerchart').mousemove(function (e) {
        if (mouseDown === 1) {
            if (e.pageX > lastX) {
                var diff = e.pageX - lastX;
                diff = diff * 3;
                var xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].setExtremes(xExtremes.min - diff, xExtremes.max - diff);
            }
            else if (e.pageX < lastX) {
                let diff = lastX - e.pageX;
                diff = diff * 3;
                let xExtremes = chart.xAxis[0].getExtremes();
                chart.xAxis[0].setExtremes(xExtremes.min + diff, xExtremes.max + diff);
            }

            if (e.pageY > lastY) {
                var ydiff = 0.1 * (e.pageY - lastY);
                var yExtremes = chart.yAxis[0].getExtremes();
                chart.yAxis[0].setExtremes(yExtremes.min + ydiff, yExtremes.max + ydiff);
            }
            else if (e.pageY < lastY) {
                let ydiff = 0.1 * (lastY - e.pageY);
                let yExtremes = chart.yAxis[0].getExtremes();
                chart.yAxis[0].setExtremes(yExtremes.min - ydiff, yExtremes.max - ydiff);
            }
        }
        lastX = e.pageX;
        lastY = e.pageY;
        console.log("lastX: " + lastX);
        console.log("lastY: " + lastY);
    });


});


// JSLint options:
/*global Highcharts, document */

//(function (H) {

//    //internal functions
//    function stopEvent(e) {
//        if (e) {
//            if (e.preventDefault) {
//                e.preventDefault();
//            }
//            if (e.stopPropagation) {
//                e.stopPropagation();
//            }
//            e.cancelBubble = true;
//        }
//    }

//    //the wrap
//    H.wrap(H.Chart.prototype, 'render', function (proceed) {
//        debugger;
//        var chart = this,
//            mapNavigation = chart.options.mapNavigation;

//        proceed.call(chart);

//        // Add the mousewheel event
//        H.addEvent(chart.container, document.onmousewheel === undefined ? 'DOMMouseScroll' : 'mousewheel', function (event) {
//            //alert(1);
//            var delta, extr, step, newMin, newMax, axis = chart.xAxis[0];
//            var dataMax = chart.xAxis[0].dataMax,
//                dataMin = chart.xAxis[0].dataMin,
//                newExtrMin,
//                newExtrMax;

//            e = chart.pointer.normalize(event);
//            // Firefox uses e.detail, WebKit and IE uses wheelDelta
//            delta = e.detail || -(e.wheelDelta / 120);
//            delta = delta < 0 ? 1 : -1;

//            if (chart.isInsidePlot(e.chartX - chart.plotLeft, e.chartY - chart.plotTop)) {
//                extr = axis.getExtremes();
//                step = (extr.max - extr.min) / 5 * delta;

//                if ((extr.min + step) <= dataMin) {
//                    //newExtrMin = dataMin;
//                    //newExtrMax = extr.max;
//                    cpw -= 5;
//                    chart.update({
//                        plotOptions: {
//                            candlestick: {
//                                pointWidth: cpw,
//                            }

//                        }
//                    });
//                } else if ((extr.max + step) >= dataMax) {
//                    //newExtrMin = extr.min;
//                    //newExtrMax = dataMax;
//                    cpw += 5;
//                    chart.update({
//                        plotOptions: {
//                            candlestick: {
//                                pointWidth: cpw,
//                            }

//                        }
//                    });
//                } else {
//                    //newExtrMin = extr.min + step;
//                    //newExtrMax = extr.max + step;
//                    chart.update({
//                        plotOptions: {
//                            candlestick: {
//                                pointWidth: cpw,
//                            }

//                        }
//                    });
//                }

//                //axis.setExtremes(newExtrMin, newExtrMax, true, false);

//            }

//            stopEvent(event); // Issue #5011, returning false from non-jQuery event does not prevent default
//            return false;
//        });
//    });
//}(Highcharts));

