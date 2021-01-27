
(function ($) {
    "use strict";
    jQuery(document).ready(function ($) {
        loadvolumetrade();
    });

}(jQuery));	

setInterval(loadvolumetrade, 2300);
function loadvolumetrade() {
    $.ajax({
        type: "POST",
        url: '/manage/Totalvolumebuysell',
        data: {},
        success: function (result) {
            if (result.length > 0) {
                var itemrows = "";
                for (var i = 0; i < result.length; i++) {
                    var item = result[i];
                    var btnbuydisable = "", btnselldisable="";
                    if (item.IsActive && item.TypeRandom) {
                        btnbuydisable = "disable";
                    }
                    else if (item.IsActive && !item.TypeRandom) {
                        btnselldisable = "disable";
                    }
                    var btnactionpush = " <a id='id-buy" + item.PairName + "' href='javascript:void(0);' onclick=autobotrade('" + item.PairName + "',true) class='btn btn-warning btn-push " + btnbuydisable+"'>Buy Win</a>";
                    btnactionpush += " <a id='id-sell" + item.PairName + "' href='javascript:void(0);' onclick=autobotrade('" + item.PairName + "',false) class='btn btn-warning btn-push " + btnselldisable +"'>Sell Win</a>";
                    var tr = "<tr ><td>" + item.PairName + "</td><td>" + item.TotalBuy + "</td><td>" + item.TotalSell + "</td><td>" + btnactionpush
                        + " </td></tr>"
                    itemrows += tr;
                }

                $("#render-rows-volumetrade").empty().append(itemrows);
            }

        }
    });
}

function autobotrade(pair, type) {
    debugger;
    if (type) {
        $("#id-buy" + pair).text("waiting...");
    } else {
        $("#id-sell" + pair).text("waiting...");
    }
    $.ajax({
        url: '/manage/Random_Orders_WinLose_Update',
        type: 'POST',
        data: JSON.stringify({ "pairname": pair, "isTypeRandom": type }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (type) {
                $("#id-buy" + pair).text("Buy Win");
            } else {
                $("#id-sell" + pair).text("Sell Win");
            }
           
        },
        error: function () {
            alert("update not success");
        }
    });
}