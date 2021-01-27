
(function ($) {
	"use strict";
    jQuery(document).ready(function($){
        clockUpdate();
        setInterval(clockUpdate, 1000);
        //get current date
        var maindate=null;
            $.ajax({
                type: "GET",
                url: '/home/ServerTime',
                data: {},
                success: function (data) {
                    maindate = data;
                    //var d = new Date(maindate);
                    var t = maindate.split(/[- :]/)
                    var d = new Date(t[0], t[1] - 1, t[2], t[3], t[4], t[5]);
                    var strDate = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
                    var todayDate = $('#date');
                    //var todayDate2 = $('#today_date2');
                    todayDate.html( strDate );
                   // todayDate2.html("<i class='fas fa-calendar-alt'></i> " + strDate + " (UTC-7)");
                }
            });
        function clockUpdate() {
           
            $.ajax({
                type: "GET",
                url: '/home/ServerTime',
                data: {},
                success: function (data) {
                    var t = data.split(/[- :]/)
                    var date = new Date(t[0], t[1] - 1, t[2], t[3], t[4], t[5]);
                    //var date = new Date(data); //new Date();
                    function addZero(x) {
                        if (x < 10) {
                            return x = '0' + x;
                        } else {
                            return x;
                        }
                    }
                    function twelveHour(x) {
                        if (x > 12) {
                            return x = x;// - 12;
                        } else if (x == 0) {
                            return x = 12;
                        } else {
                            return x;
                        }
                    }
                    var h = addZero(twelveHour(date.getHours()));
                    var m = addZero(date.getMinutes());
                    var s = addZero(date.getSeconds());

                   // $('#live_clock').text(h + ':' + m + ':' + s)
                    $('#time').html("<i class='fas fa-clock'></i> " + h + ':' + m + ':' + s);
                    //$('#live_clock2').html("<i class='fas fa-clock'></i> " + h + ':' + m + ':' + s);
                }

            });
           
        }
    });
  
}(jQuery));	
