
//function TradesViewModel(flights) {
//    var self = this;
//    self.flights = ko.observableArray(flights);
//    ko.mapping.fromJS(flights, {}, self);
//    self.updateTrade = function (flight) {
       
//        var item = ko.utils.arrayFirst(flights, function (item) {
//            return item.Id === flight.Id;
//        });
//        self.flights.replace(item, flight);
//    };
//    self.addTrade = function (flight) {
       
        
//    };


//    //self.removeTrade = function (flight) {
//    //    var item = ko.utils.arrayFirst(flights, function (item) {
//    //        return item.Id === flight.Id;
//    //    });
//    //    self.flights.remove(item);
//    //};

//}
//$(function () {
$(document).ready(function () {
    var viewModel = null;
    var tradeHub = $.connection.jobHub;
    tradeHub.client.updateTrade = function (flight) {
        //viewModel.updateTrade(flight);
        // push notify
        if ($("#userencrypted").val() === flight.UserEncrypted) {
            let _mgsProfit = "";
            if (flight.Status === 1) {
                _mgsProfit = "Profit +$" + (flight.Amount);
                buildData.showNotify(_mgsProfit, "Notify", "success", 3000);
                buildfunction.soundoverorder();
            } else if (flight.Status === -1) {
                _mgsProfit = "Profit -$" + (flight.Amount);
                buildData.showNotify(_mgsProfit, "Notify", "danger", 3000);
                buildfunction.soundoverorder();
            }
            let _profit = "";
            //let num = Date.parse(flight._create_time);
            //const timeStamp = num.toString().slice(num.lenght - 6, num.lenght) + flight.Id;
            const timeStamp = flight.Id;
            if (flight.Status === 1) {
                _profit = "<span style='color: #2CAC40'>+$" + flight._profit + "</span>";
            } else if (flight.Status === -1) {
                _profit = "<span style='color: #DB4931'>-$" + flight._profit + "</span>";
            }
            $("#" + timeStamp + " .profit-right h5").append(_profit);
            //update balance
            setTimeout(function () {
                buildData.accountBalance();
            }, 3000);
        }
    };
    tradeHub.client.addTrade = function (flight) {
        //viewModel.addTrade(flight);
        if ($("#userencrypted").val() === flight.UserEncrypted) {
            var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

            var rows = "";
            var item = flight;
            var _profit = "";
            if (item.Status === 1) {
                _profit = "<span style='color: #2CAC40'>+$" + item._profit + "</span>";
            } else if (item.Status === -1) {
                _profit = "<span style='color: #DB4931'>-$" + item._profit + "</span>";
            } else if (item.Status === 2) {
                _profit = "<span>+$" + item._amount + "</span>";
            } else if (item.Status === 0) {
                _profit = "";
            }
            var _iscall = "";
            if (item.IsCall === true) {
                _iscall = "Higher";
            } else {
                _iscall = "Lower";
            }

            //let formatted_date = new Date(CompleteOnStr).getFullYear() + "-" + new Date(item.CompleteOn).getDate() + "-" + (new Date(item.CompleteOn).getMonth()+1)
            //let formatted_time = new Date(item.CompleteOn).getHours() + ":" + new Date(item.CompleteOn).getMinutes() + ":" + new Date(item.CompleteOn).getSeconds() 
            //let num = Date.parse(item._create_time);
            //const timeStamp = num.toString().slice(num.lenght - 6, num.lenght) + item.Id;
            const timeStamp = item.Id;
            rows += "<li id='" + timeStamp + "' class='media h-trading'>";
            rows += "<div class='text-left mr-3 time'>";
            rows += "<h6><span style='color: White'>" + item._create_time.slice(11, 16) + "</span></h6>";
            rows += "<span class='text-small'>" + months[new Date(item._create_time).getMonth()] + " " + new Date(item._create_time).getDate() + "</span>";
            rows += "</div>";
            rows += "<img class='mr-3' src='/images/symbol/flags/" + item.MarketName.replace('/', '_') + ".png'>";
            rows += "<div class='mr-3 '>";
            rows += "<h6 class='ml-0 pl-0'>" + item.MarketName.replace('_','/') + "</h6>";
            rows += "<span class='text-small'>" + _iscall + "</span>";
            rows += "</div>";
            rows += "<div class='text-right ml-2 profit-right'>";
            rows += "<h5>" + _profit + "</h5>";
            rows += "<span class='text-small'>" + "-$" + item._amount + "</span>";
            rows += "</div>";
            rows += "</li>";
            var newItem = document.createElement("LI");
            newItem = $.parseHTML(rows)[0];
            var listtrade = document.getElementById("left-history");
            listtrade.insertBefore(newItem, listtrade.childNodes[0]);
            //update balance
            setTimeout(function () {
                buildData.accountBalance();
            }, 3000);
            //if (self.flights().length < 10) {
            //    self.flights.unshift(flight);
            //} else {
            //    var last = self.flights.pop();
            //    self.flights.remove(last);
            //    self.flights.unshift(flight);
            //}
        }
    };

    //trade.client.removeTrade = function (flight) {
    //    viewModel.removeTrade(flight);
    //};

    //trade volume
    tradeHub.client.tradeVolume = function (flight) {
        const result = flight.split('-');
        console.log("random: "+ result);
        buildfunction._processing(result[0], result[1]);
    };
    //realtime candlestick
    //tradeHub.client.realtimeCandlestick = function (flight) {
    //    //console.log("TimeClose: " + flight.TimeClose*1000);
    //    //console.log("TimeOpen: " + flight.TimeOpen*1000);
    //    //console.log("ClosePrice: " + flight.ClosePrice);
    //    buildfunction._setDataRealtime(flight.TimeOpen*1000,flight.TimeClose*1000
    //        , flight.OpenPrice
    //        , flight.HighPrice
    //        , flight.LowPrice
    //        , flight.ClosePrice
    //        , flight.VolumeTo
    //        , 5);
        
    //};
    tradeHub.client.serverSetSecond = function (flight) {
        let second = flight;
        secondnow = flight;
        var show = 59 - second;
        document.getElementById("id-download-timer").innerHTML = "00 : " + show;
        //buildfunction._serverTime( parseInt(show-1));
        if (show <= 30) {
            $("#btn-user-call-event").prop('disabled', true);
            $("#btn-user-put-event").prop('disabled', true);
        } else {
            $("#btn-user-call-event").prop('disabled', false);
            $("#btn-user-put-event").prop('disabled', false);
        }

        if (show < 10) {
            show = "0" + show;
        }
        if (show <= 0) {
          
            buildfunction.chartRefresh();
            buildfunction.flagRefresh();
        }
       
        second += 1;
        if (second >= 60) {
            second = 0;
        }
    };

    $.connection.hub.start().done(function () {
        //changeSetPairname(window.sym);
        //tradeHub.server.setPairname(window.sym.replace('/', '_'));
        tradeHub.server.setUid($("#userencrypted").val());
        
        //tradeHub.server.showData().done(function (tradeData) {
        //    viewModel = new TradesViewModel(tradeData);
        //    ko.applyBindings(viewModel);
        //}).fail(function (error) {
        //    console.log('showData: ' + error);
            
        //});
            

        setInterval(function () {
            tradeHub.server.serverGetTime();
        }, 1000);
    })
        .fail(function (error) {
            console.log('SignalR fail: ' + error);
            //alert("Unable to connect to the fortrex.io service. Please refresh your browser");
            setTimeout(function () {
                $.connection.hub.start();
            }, 3000); 
        });
    $.connection.hub.disconnected(function () {
        buildData.showNotifyCenter("Disconnected", "Notify", "danger", 5000);
        if ($.connection.hub.lastError) { console.log("Disconnected. Reason: " + $.connection.hub.lastError.message); }
        setTimeout(function () {
            $.connection.hub.start();
        }, 3000); 
    });
    $.connection.hub.connectionSlow(function () {
        buildData.showNotifyCenter("Your network connection is not stable", "Notify", "danger", 10000);
        setTimeout(function () {
            $.connection.hub.start();
        }, 3000); 
    });
    $.connection.hub.error(function (error) {
        console.log('SignalR error: ' + error);
        setTimeout(function () {
            $.connection.hub.start();
        }, 1000); 
    });
});
function changeSetPairname(pair) {
    //tradeHub.server.setPairname(pair.replace('/', '_'));
}