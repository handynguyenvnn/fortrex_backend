//import { Order_Type_Buy, Order_Type_Sell } from './commonenum.js';
Number.prototype.amountformat = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};
$(window).click(function (e) {
    _statusMenuMobile = false;
    $("#sidebar").removeClass("m-mobile");
});
window.addEventListener("load", function () {
    buildData.accountBalance();
});
function isValidateUsername(str) {
    var pattern = new RegExp('^(?=.*[a-z])([a-z0-9._]+)$');
    return pattern.test(str);
}
var _primaryCoin = 'FBT';
var Order_Type_Buy = 'BUY';
var Order_Type_Sell = 'SELL';
var _cameras = [];
var _balance = null;
var _scanner = null;
var _balance_valid = 0;
var balance_transfer_cp = 0;
var balance_transfer_fb = 0;
var _totalCamera = 0;
var _totalBalance = 0;
var _statusMenuMobile = false;
let _statusSelectBalance = true;
//var statushietools = false;
var buildData = {
    loading: function () {
        $.blockUI({
            theme: true,
            //message: "<p><img src='/Images/loaders.gif?v=1' alt='' /></p>",
            message: "<p class='loader loader-icon'><svg width='30px' height='30px' fill='#4FB95C' viewBox='0 0 30 30'> <circle cx='4' cy='4' r='4'></circle>  <circle cx='4' cy='26' r='4'></circle> <circle cx='26' cy='4' r='4'></circle>  <circle cx='26' cy='26' r='4'></circle> </svg></p>",
            css: { top: '20%' }
        });
    },
    stopLoading: function () {
        $.unblockUI();
    },
    showNotify: function (message, title, typeNotify, delays) {
        if (typeNotify === "" || typeNotify === null) {
            typeNotify = "success";
        }
        if (title === "" || title === null) {
            title = "Notify";
        }
        //var typeIcon = "fa fa-exclamation";
        var typeIcon = "", titelIcon = "";
        var colortype = "red";
        if (typeNotify === "success") {
            titelIcon = "<img class='notify-icon' src='/images/icon/success-circle.svg?v=5.9'/>";
        } else {
            titelIcon = "<img class='notify-icon' src='/images/icon/bell.svg?v=5.9'/>";
        }
        if (typeNotify === "success") {
            //typeIcon = "fa fa-check"; 
            colortype = "#29C359";
        }
        if (delays === "" || delays === null) {
            delays = 8000;
        }
        $.notify({
            title: titelIcon + "<strong style='color:" + colortype + "'>" + title + "</strong>",
            icon: typeIcon,
            message: message
        }, {
            type: typeNotify,
            animate: {
                enter: 'animated fadeInUp',
                exit: 'animated fadeOutRight',
                delay: 10
            },
            placement: {
                from: "top",
                align: "right"
            },
            offset: 20,
            spacing: 10,
            z_index: 9031,
            delay: delays
        });
    },
    showNotifyCenter: function (message, title, typeNotify, delays) {
        if (typeNotify === "" || typeNotify === null) {
            typeNotify = "success";
        }
        if (title === "" || title === null) {
            title = "Notify";
        }
        //var typeIcon = "fa fa-exclamation";
        var typeIcon = "", titelIcon = "";
        var colortype = "red";
        if (typeNotify === "success") {
            titelIcon = "<img class='notify-icon' src='/images/icon/success-circle.svg?v=5.9'/>";
        } else {
            titelIcon = "<img class='notify-icon' src='/images/icon/bell.svg?v=5.9'/>";
        }
        if (typeNotify === "success") {
            //typeIcon = "fa fa-check"; 
            colortype = "#29C359";
        }
        if (delays === "" || delays === null) {
            delays = 8000;
        }
        $.notify({
            title: titelIcon + "<strong style='color:" + colortype + "'>" + title + "</strong>",
            icon: typeIcon,
            message: message
        }, {
            type: typeNotify,
            animate: {
                enter: 'animated fadeInDown',
                exit: 'animated fadeOutUp',
                delay: 1
            },
            placement: {
                from: "top",
                align: "center"
            },
            offset: 10,
            spacing: 10,
            z_index: 9031,
            delay: delays
        });
    },
    initializeClock: function (div_id, time) {
        var clock = document.getElementById(div_id);
        var daysSpan = clock.querySelector('.days');
        var hoursSpan = clock.querySelector('.hours');
        var minutesSpan = clock.querySelector('.minutes');
        var secondsSpan = clock.querySelector('.seconds');

        function updateClock() {
            var total = Date.parse(time) - Date.parse(new Date());
            var seconds = Math.floor((total / 1000) % 60);
            var minutes = Math.floor((total / 1000 / 60) % 60);
            var hours = Math.floor((total / (1000 * 60 * 60)) % 24);
            var days = Math.floor(total / (1000 * 60 * 60 * 24));
            if (daysSpan !== null && hoursSpan !== null && minutesSpan !== null && secondsSpan !== null) {
                daysSpan.innerHTML = days;
                hoursSpan.innerHTML = ('0' + hours).slice(-2);
                minutesSpan.innerHTML = ('0' + minutes).slice(-2);
                secondsSpan.innerHTML = ('0' + seconds).slice(-2);
            }

            if (total <= 0) {
                clearInterval(timeinterval);
            }
        }
        updateClock();
        var timeinterval = setInterval(updateClock, 1000);
    },
    redirectUrl: function (url) {
        setTimeout(function () { window.location = url; }, 1500);
    },
    accountBalance: function () {
        $.ajax({
            url: '/office/AccountBalance',
            type: 'POST',
            data: JSON.stringify(),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                _balance = result;
                setSelectAccount();
                $("#total-balance-real").html(result._usd);
                $("#total-balance-demo").html(result._usdDemo);
                $("#total-balance-fb").html(result._usd);
                value_balance_real = replaceComma(_balance._usd.replace('$', ''));
                balance_transfer_fb = (isNaN(value_balance_real) || value_balance_real < 0 || value_balance_real === "" || value_balance_real === null) ? 0 : parseFloat(value_balance_real);
            },
            error: function () {
            }
        });
    },
    CopytradeAccountBalance: function () {
        $.ajax({
            url: '/office/AccountBalanceCopytrade',
            type: 'POST',
            data: JSON.stringify(),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                //$("#total-balance-cp").html("$" + result.toString());
                value_amount = (isNaN(result) || result < 0 || result === "" || result === null) ? 0 :parseFloat(result);
                $("#total-balance-cp").empty().text("$" + value_amount);
                 balance_transfer_cp = value_amount;
            },
            error: function () {
            }
        });
    },
    listInvestment: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            data = new jqueryRenderTable();
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;

                data.addColumn('number', 'Deal ID');
                data.addColumn('string', 'Revenue Type');
                data.addColumn('string', 'Asset');
                data.addColumn('string', 'Start Time');
                data.addColumn('string', 'Strike Rate');
                data.addColumn('string', 'Close Time');
                data.addColumn('string', 'Current Rate');
                data.addColumn('string', 'Invested Amount');
                data.addColumn('string', 'Payout, %');
                data.addColumn('string', 'Time Left');
                data.addColumn('string', 'Actions');

                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        var col1 = "";
                        if ((i + 1) % 2 === 0) {
                            col1 = '<span class="sold-thumb"><i class="la la-arrow-down"></i></span>';
                        }

                        var tmpRow = [
                            (i + 1),
                            col1,
                            "BTC/USD",
                            item._createOn,
                            "8810.9",
                            item._createOn,
                            "8812.1",
                            "USD 12",
                            "30%",
                            "00:00:12",
                            item._action
                        ];

                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow2 = ["There are no data to display."];
                    data.addRow(tmpRow2, 5);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow3 = ["There are no data to display."];
                data.addRow(tmpRow3, 5);
                self.drawTable(data);
            }
        }
        jqueryLoadList.init(url, row, "investment-container");
    },
    // Transfer
    processTransfer: function () {
        var amountInput = $("#transfer-amount").val();
        var _fromto = "";
        if (!_fromtowalletforbit) {
            _fromto = "FBOPTION_FOCOPYTRADE";
        } else {
            _fromto = "FOCOPYTRADE_FBOPTION";
        }

        var amount = parseFloat(amountInput);
        if (amount > 0) {
            var data = {
                from_to: _fromto,
                amount: amount
            };
            $.ajax({
                url: "/transfer",
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                success: function (result) {
                    if (result !== null) {
                        buildData.showNotify(result.Message, "Notify", result.ClassCss, 5000);
                    }
                    buildData.accountBalance();
                    buildData.CopytradeAccountBalance();
                },
                error: function () {
                    buildData.showNotify("Transfer invalid value", "Error", "danger");
                }

            });
        }

    },
    //WithDraw
    listWithdraw: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('number', 'Id');
                data.addColumn('string', 'Status');
                data.addColumn('string', 'Push');
                data.addColumn('string', 'Fee');
                data.addColumn('string', 'Pull');
                data.addColumn('string', 'CreateDate');
                data.addColumn('string', 'ApproveDate');
                data.addColumn('string', 'HashCode');
                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        var rowamoutset = "", rowfee = "", rowamountget = "";

                        if (item.FromTypeName === "FBT") {
                            rowamoutset = item.strAmountSet + " " + item.FromTypeName;
                            rowfee = item.Fee + " " + item.FromTypeName;
                            rowamountget = item.strAmountGet + " " + item.FromTypeName;
                        } else {
                            rowamoutset = "$" + item.strAmountSet;
                            rowfee = "$" + item.Fee;
                            rowamountget = "$ " + item.strAmountGet;
                        }
                        var linkhashcode = "";
                        item.HashCode = item.HashCode !== null ? item.HashCode : '';
                        if (item.HashCode !== null && item.HashCode.indexOf("https://") <= 0) {
                            var linkhref = "https://etherscan.io/tx/" + item.HashCode;
                            linkhashcode = "<a href=" + linkhref + ">" + item.HashCode + "</a>";
                        } else {
                            linkhashcode = "<a href=" + item.HashCode + ">" + item.HashCode + "</a>";
                        }
                        var tmpRow = [
                            i + 1,
                            item.StatusName,
                            rowamoutset,
                            rowfee,
                            rowamountget,
                            item.strCreateDate,
                            item.strApproveDate,
                            linkhashcode
                        ];

                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 8);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 8);
                self.drawTable(data);
            }
        }
        jqueryLoadList.init(url, row, "withdraw-container");
    },
    listWithdrawoffice: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('number', 'Id');
                data.addColumn('string', 'Status');
                data.addColumn('string', 'Push');
                data.addColumn('string', 'Fee');
                data.addColumn('string', 'CreateDate');
                data.addColumn('string', 'ApproveDate');
                data.addColumn('string', 'HashCode');
                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        var rowamoutset = "", rowfee = "", rowamountget = "";

                        if (item.FromTypeName === "FBT") {
                            rowamoutset = item.strAmountSet + " " + item.FromTypeName;
                            rowfee = item.Fee + " " + item.FromTypeName;
                            rowamountget = item.strAmountGet + " " + item.FromTypeName;
                        } else {
                            rowamoutset = "$" + item.strAmountSet;
                            rowfee = "$" + item.Fee;
                            rowamountget = "$ " + item.strAmountGet;
                        }
                        var linkhashcode = "";
                        item.HashCode = item.HashCode !== null ? item.HashCode : '';
                        if (item.HashCode !== null && item.HashCode.indexOf("https://") <= 0) {
                            var linkhref = "https://etherscan.io/tx/" + item.HashCode;
                            linkhashcode = "<a href=" + linkhref + ">" + item.HashCode + "</a>";
                        } else {
                            linkhashcode = "<a href=" + item.HashCode + ">" + item.HashCode + "</a>";
                        }
                        var tmpRow = [
                            i + 1,
                            item.StatusName,
                            rowamoutset,
                            rowfee,
                            item.strCreateDate,
                            item.strApproveDate,
                            linkhashcode
                        ];

                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 8);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 8);
                self.drawTable(data);
            }
        }
        jqueryLoadList.init(url, row, "withdraw-container");
    },
    listArbittrageTransaction: function (url, row) {

        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('number', 'Id');
                data.addColumn('string', 'Coin Pair');
                data.addColumn('string', 'Buy Exchange');
                data.addColumn('string', 'Sell Exchange');
                data.addColumn('decimal', 'Buy Price');
                data.addColumn('decimal', 'Sell Price');
                data.addColumn('decimal', 'Percent Difference');
                data.addColumn('string', 'Trade At');
                data.addColumn('string', 'Transaction Id');

                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        var cointrade = "<span class='badge badge-info' style='padding: 5px 8px;background: #58BF00'>" + item.CoinPair.toUpperCase() + "</span>";
                        var percent = "<span class='badge badge-warning' style='color: #fff;padding: 5px 8px'>" + item.PercentDifference + "%" + "</span>";
                        var tmpRow = [
                            item.id,
                            cointrade,
                            item.BuyExchange,
                            item.SellExchange,
                            "<span style='color: red'>" + item.BuyPrice + "</span>",
                            "<span style='color: #58BF00'>" + item.SellPrice + "</span>",
                            percent,
                            item.TradeAt,
                            item.TransactionID
                        ];

                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 8);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 8);
                self.drawTable(data);
            }
        };
        jqueryLoadList.init(url, row, "ArbittrageTransaction-Lst");
    },
    validateWithdraw: function () {
        var amountInput = $("#withdraw-amount").val();

        var amount = parseFloat(amountInput);
        var method = $("#id-select-type").val();
        var type = $("#withdraw-wallet-name").val();
        if (method === "bncterc20") {
            var walletbnct = $("#wallet-bnct-erc20").val();
            $("#wallet-address").val(walletbnct);
            $("#type-wallet-withdraw").css("display", "none");
        } else {
            $("#type-wallet-withdraw").css("display", "block");
            if (type === "eth") {
                var walleteth = $("#wallet-eth").val();
                $("#wallet-address").val(walleteth);
            } else if (type === "btc") {
                var walletbtc = $("#wallet-btc").val();
                $("#wallet-address").val(walletbtc);
            }
        }

        if (amount > 0) {
            var data = {
                amount: amount,
                type: type,
                method: method
            };
            $.ajax({
                url: "/withdraw-confirm",
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                success: function (result) {
                    var str = "";
                    if (method === "bncterc20") {
                        str = "<div>Fee: " + result.Fee + " " + _primaryCoin + "</div>";
                        str += "<div>Withdraw: " + result.Withdraw + " " + _primaryCoin + "</div>";
                    } else {
                        str = "<div><span class='brightGreen-color'>Fee:</span>" + "<span class='white-color'> $" + result.Fee + "</span></div>";
                        str += "<div><span class='brightGreen-color'>Withdraw: </span>" + "<span class='white-color'>" + result.Withdraw + " " + type.toUpperCase() + "</span></div>";
                    }
                    $("#txt-result").html(str);
                    if (result.Meg !== "" && result.Meg !== null) {

                        buildData.showNotify(result.Meg);
                    }
                },
                error: function (result) {
                    buildData.showNotify("Invalid value", "Error", "danger");
                    $("#txt-result").html('');
                }
            });
        }
    },
    validateSell: function () {
        var amountInput = $("#stock-amount").val();
        var amount = parseFloat(amountInput);
        if (amount > 0) {
            var data = {
                amount: amount
            };
            $.ajax({
                url: "/office/sellComfirm",
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                success: function (result) {
                    var rel = "$ " + result;
                    $("#stock-amount-usd").val(rel);
                },
                error: function (result) {
                }
            });
        }
    },
    //Transaction history
    listTransaction: function (url, row) {
        jqueryLoadList.getCustomData = function () {
            var self = this;
            var data = {
                pageIndex: self.pageIndex,
                pageSize: self.pageSize,
                type: $("#cb-transactiontype").val(),
                from_date: $("#from-date").val(),
                to_date: $("#to-date").val()
            };
            return data;
        };
        jqueryLoadList.displayData_v2 = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('number', 'No');
                data.addColumn('string', 'Amount');
                //data.addColumn('number', 'Profit');
                data.addColumn('string', 'Description');
                data.addColumn('string', 'From');
                data.addColumn('string', 'CreateOn');
                //data.addColumn('string', 'Type Bonus');


                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        _descriptionIcon = "";
                        if (item.Type === 1) {
                            _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/transhistory-tradeicon.png'>";
                        } else if (item.Type === 8) {
                            _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/transhistory-profiticon.png'>";
                        } else if (item.Type === 6) {
                            _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/transhistory-bonusicon.png'>";
                        } else if (item.Type === 11) {
                            _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/transhistory-bonusicon.png'>";
                        } else if (item.Type === 7) {
                            _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/transhistory-bonusicon.png'>";
                        }
                        else if (item.Type === 12) {
                            if (item.Amount >= 0) {
                                _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/received_fb.png'>";
                            } else {
                                _descriptionIcon = "<img class='trade-history-icon' src='/images/icon/transfer_fb.png'>";
                            }
                          
                        }


                        var numamount = "";
                        if (item.Amount >= 0) {
                            if (item.Type === 1) {
                                numamount = "<span style='color: rgb(136, 136, 136)'>" + item.StrAmount + "</span>";
                            } else {
                               numamount = "<span style='color:rgb(17, 208, 61)'>" + item.StrAmount + "</span>";
                            }

                        } else {
                            if (item.Type === 12) {
                                Amount_trans_fl = (isNaN(item.Amount) || item.Amount === "" || item.Amount === null) ? 0 : parseFloat(item.Amount*-1);
                                numamount = "<span style='color: red'>" + "-$" + Amount_trans_fl + "</span>";
                            } else {
                                numamount = "<span style='color: rgb(136, 136, 136)'>" + item.StrAmount + "</span>";
                            }
                            
                        }
                        var tmpRow = [
                            (i + 1),
                            numamount,
                            //"",
                            _descriptionIcon + item.Description,
                            "<i class='fa fa-user'></i> " + item.FromUser,
                            "<img class='trade-history-icon' src='/images/icon/transhistory-timeicon.png'>" + item.StrCreateOn
                            //"" 
                        ];
                        data.addRow(tmpRow);
                    }
                    self.drawTable_v2(data);

                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 7);
                    self.drawTable_v2(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 7);
                self.drawTable_v2(data);
            }
        };
        jqueryLoadList.init_v2(url, row, "transaction-container");
    },
    //Trading history
    listTrading: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('number', 'ID');
                data.addColumn('number', 'Assets');
                data.addColumn('string', 'Pair Name');
                data.addColumn('number', 'Opening Price');
                data.addColumn('string', 'Open Time');
                data.addColumn('number', 'Closing Price');
                data.addColumn('string', 'Close Time');
                data.addColumn('string', 'Option');
                data.addColumn('number', 'Amount');
                data.addColumn('string', 'Result');
                data.addColumn('number', 'Profit');
                data.addColumn('string', 'Account');


                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        var _iscall = "";
                        var _isdemo = "";
                        if (item.IsCall === true) {
                            _iscall = "<span class='badge badge-buy'><i class='la la-arrow-up'></i> HIGHER</span>";
                        } else {
                            _iscall = "<span class='badge badge-sell'><i class='la la-arrow-down'></i> LOWER</span>";
                        }
                        if (item.IsDemo === true) {
                            _isdemo = "Practice";
                        } else {
                            _isdemo = "Real";
                        }

                        var _iswin = "";
                        var _profit = "";
                        if (item.Status === 1) {
                            _iswin = "<span class='badge badge-win'>WIN</span>";
                            _profit = "<span style='color: rgb(17, 208, 61)'>+$" + item._profit + "</span>";
                        } else if (item.Status === -1) {
                            _iswin = "<span class='badge badge-lose'>LOSE</span>";
                            _profit = "<span style='color: red'>-$" + item._profit + "</span>";
                        } else if (item.Status === 2) {
                            _iswin = "--";
                            _profit = "<span>+$" + item._amount + "</span>";
                        } else if (item.Status === 0) {
                            _iswin = "--";
                            _profit = "";
                        }

                        var tmpRow = [
                            (i + 1),
                            "<img class='pair-ico' src='/images/symbol/flags/" + item.PairName.replace('/', '_') + ".png'>",
                            item.PairName,
                            "$" + item.OpeningPrice,
                            "<img class='trade-history-icon' src='/images/icon/transhistory-timeicon.png'>" + item.CreateTimeStr,
                            "$" + item.ClosingPrice,
                            "<img class='trade-history-icon' src='/images/icon/transhistory-timeicon.png'>" + item.CompleteOnStr,
                            _iscall,
                            "-$" + item._amount,
                            _iswin,
                            _profit,
                            _isdemo
                        ];
                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 9);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 9);
                self.drawTable(data);
            }
        };
        jqueryLoadList.init(url, row, "trading-container");
    },
    //Short_Trading
    ListTradingSimple: function () {
        buildfunction.clickaudio();
        buildfunction._setHideByResize(true);
        $("#slide-tradepairs").addClass('d-none');
        $("#slide-tradinghistory").removeClass('d-none');
        $("#tabtradinghistory a").addClass('active');
        $("#tabtradingpair a").removeClass('active');
        $("#slide2").removeClass("fade").addClass('active');
        $("#slide1").removeClass('active').addClass("fade");
        var data = {
            pageIndex: 0,
            pageSize: 10
        };

        $.ajax({
            url: '/office/tradinglist',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildfunction.loadingSlideLeft();
            },
            success: function (result) {
                var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

                buildfunction.stopLoadingSlideLeft();
                if (result.Result.length > 0) {
                    $("#left-history li").remove();
                    for (var i = 0; i < result.Result.length; i++) {
                        var rows = "";
                        var item = result.Result[i];
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
                        //let num = Date.parse(item.CompleteOnStr);
                        //const timeStamp = num.toString().slice(num.lenght - 6, num.lenght) + item.Id;
                        const timeStamp = item.Id;
                        rows += "<li id='" + timeStamp + "' class='media h-trading'>";
                        rows += "<div class='text-left mr-3 time'>";
                        rows += "<h6><span style='color: White'>" + item.CompleteOnStr.slice(11, 16) + "</span></h6>";
                        rows += "<span class='text-small'>" + months[new Date(item.CompleteOnStr).getMonth()] + " " + new Date(item.CompleteOnStr).getDate() + "</span>";

                        rows += "</div>";

                        rows += "<img class='mr-3' src='/images/symbol/flags/" + item.PairName.replace('/', '_') + ".png'>";
                        rows += "<div class='mr-3 '>";
                        rows += "<h6 class='ml-0 pl-0'>" + item.PairName + "</h6>";
                        rows += "<span class='text-small'>" + _iscall + "</span>";
                        rows += "</div>";
                        rows += "<div class='text-right ml-2 profit-right'>";
                        rows += "<h5>" + _profit + "</h5>";
                        rows += "<span class='text-small'>" + "-$" + item._amount + "</span>";
                        rows += "</div>";
                        rows += "</li>";

                        $("#left-history").append(rows);

                    }
                    //$("#slide-tradinghistory").append("");
                    //self.drawTable(data);
                } else {
                    $("#showmore").addClass("empty");
                    $("#showmore").empty().append("<img src='/images/icon/empty.png'> There are no data to display");
                }
            },
            error: function () {

                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
            }
        });
    },
    listAuthorization: function (url, row) {
        jqueryLoadList.displayData_v2 = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('string', 'Date');
                data.addColumn('string', 'Action');
                data.addColumn('decimal', 'IP');
                data.addColumn('decimal', 'Status');

                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        var action = '<input disabled style="background: none;border: 0px;box-shadow: none;font-size: 14px;" value="' + item.UserAgent.substring(0, 41) + ' ..." title="' + item.UserAgent + '" type="text"/>';
                        var tmpRow = [
                            item.CreateOn,
                            action,
                            item.IPAddress,
                            item.Status
                        ];

                        data.addRow(tmpRow);
                    }
                    self.drawTable_v2(data);
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 4);
                    self.drawTable_v2(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 4);
                self.drawTable_v2(data);
            }
        }
        jqueryLoadList.init_v2(url, row, "my-author-container");
    },
    listAccount: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();
                data.addColumn('number', 'No');
                data.addColumn('string', 'Create On');
                data.addColumn('string', 'Account');
                data.addColumn('string', 'IsActive');
                data.addColumn('string', 'Nation');
                data.addColumn('string', 'Total Trading');
                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];
                        node = "";
                        if (item.ReferralId === 1) {
                            node = "<span href='#' class='mb-0 btn-sm btn btn-outline-warning round'>Left</span>";
                        } else if (item.ReferralId === 2) {
                            node = "<span class='mb-0 btn-sm btn btn-outline-danger round'>Right</span>";
                        }
                        var isactiveuser = "";
                        if (item.IsActive === true) {
                            isactiveuser = "<i class='fa fa-check'></i>";
                        } else {
                            isactiveuser = "<i class='fa fa-close'></i>";
                        }
                        var _totaltrading = "--";
                        if (item.TotalTrading > 0) {
                            _totaltrading = item.strTotalTrading;
                        }
                        var tmpRow = [
                            (i + 1),
                            item.StrCreateOn,
                            item.Username,
                            isactiveuser,
                            "Not public",
                            "<i class='la la-dollar warning'></i>" + _totaltrading
                        ];
                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow2 = ["There are no data to display."];
                    data.addRow(tmpRow2, 6);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow3 = ["There are no data to display."];
                data.addRow(tmpRow3, 6);
                self.drawTable(data);
            }
        }
        jqueryLoadList.init(url, row, "account-container");
    },
    openCamera: function () {
        var self = this;
        let scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
        scanner.addListener('scan', function (content) {
            $("#address-wallet").val(content);
            if (self._totalCamera === 1) {
                self._scanner.stop(self._cameras[0]);
            } else if (self._totalCamera === 2) {
                self._scanner.stop(self._cameras[1]);
            }
            buildData.displayUsername();
            $('#camera-modal').modal('hide');
        });
        Instascan.Camera.getCameras().then(function (cameras) {
            self._cameras = cameras;
            self._scanner = scanner;
            self._totalCamera = self._cameras.length;
            if (self._totalCamera === 1) {
                self._scanner.start(self._cameras[0]);
            }
            else if (self._totalCamera === 2) {
                self._scanner.start(self._cameras[1]);
            } else {
                alert('No cameras found.');
            }
        }).catch(function (e) { });
        self._cameras = [];
        self._scanner = null;
        self._totalCamera = 0;
        $('#camera-modal').modal('show');
    },
    closeCamera: function () {
        if (this._totalCamera === 1) {
            this._scanner.stop(this._cameras[0]);
        } else if (this._totalCamera === 2) {
            this._scanner.stop(this._cameras[1]);
        }
        this._cameras = [];
        this._scanner = null;
        this._totalCamera = 0;
        $('#camera-modal').modal('hide');
    },
    displayUsername: function () {
        data = { "wallet": $("#address-wallet").val(), "type": $("#dropmethodpay").val() }
        $.ajax({
            url: '/transfer-username',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result !== "" && result !== null) {
                    $("#display-username").text("Address Wallet: " + result);
                } else {
                    $("#display-username").text("Address Wallet: Not found");
                }
            },
            error: function () {
            }
        });
    },
    listTicket: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.total = result.Optional;
                data = new jqueryRenderTable();

                data.addColumn('number', 'Id');
                data.addColumn('string', 'Fullname');
                data.addColumn('string', 'Email');
                data.addColumn('string', 'Subject');
                data.addColumn('string', 'Meassages');
                data.addColumn('string', 'Phone Number');
                data.addColumn('string', 'CreateAt');
                data.addColumn('string', 'Reply By');
                data.addColumn('string', 'Reply Messages');
                data.addColumn('string', 'Reply Date');
                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];

                        var tmpRow = [
                            i + 1,
                            item.FullName,
                            item.Email,
                            item.Subject,
                            item.Messages,
                            item.PhoneNumber,
                            item.CreateAtstr,
                            item.ReplyBy,
                            item.ReplyMessages,
                            item.ModifyDatastr
                        ];

                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 10);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 10);
                self.drawTable(data);
            }
        }
        jqueryLoadList.init(url, row, "ticketlist-container");
    },
    getInfoUserTooltip: function (saft, id) {
        $.ajax({
            url: "/api/user/getusertree/" + id,
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                var date = new Date(data.CreateOn);
                var str = "";
                str += "<div style='text-align:center;border-bottom: 1px solid #ccc;padding-bottom:7px;'>" + data.FullName.toUpperCase() + "</div>";
                str += "<div class='detaiTree'>- Total Invest: $" + data.TotalInvest.amountformat(2) + "</div>";

                str += "<div class='detaiTree'>- Total Left: $" + data.TotalLeft.amountformat(2) + "</div>";
                str += "<div class='detaiTree'>- Total Right: $" + data.TotalRight.amountformat(2) + "</div>";
                str += "<div class='detaiTree'>- Sponsor: " + data.Sponsor.toUpperCase() + "</div>";
                // str += "<div class='detaiTree'>- Add Tree: " + date.toDateString("yyyy-MM-dd") + "</div>";
                document.getElementById("tip2").innerHTML = str;
                tooltip.pop(saft, '#tip2');
            }
        });
    },
    listNotify: function (url, row) {
        jqueryLoadList.displayData = function (result) {
            var self = this;
            if (result.Message === null || result.Message === "") {
                self.Total = result.Optional;
                data = new jqueryRenderTable();
                //data = new RenderTable();
                data.addColumn('number', 'Id');
                data.addColumn('string', 'Title');
                data.addColumn('string', 'CreateDay');
                data.addColumn('string', 'Description');

                if (result.Result.length > 0) {
                    for (var i = 0; i < result.Result.length; i++) {
                        var item = result.Result[i];

                        item.CreateDate = item.CreateDate;

                        var tmpRow = [
                            item.Id,
                            item.Title,
                            item.CreateDatestr,
                            item.Body
                        ];
                        data.addRow(tmpRow);
                    }
                    self.drawTable(data);
                    $(".table-list table").addClass("table");
                }
                else {
                    var tmpRow1 = ["There are no data to display."];
                    data.addRow(tmpRow1, 5);
                    self.drawTable(data);
                }
            }
            else {
                var tmpRow2 = ["There are no data to display."];
                data.addRow(tmpRow2, 6);
                self.drawTable(data);
            }
        }
        jqueryLoadList.init(url, row, "notifylist-container");
    },
    _pushOrder: function (typeorder) {
        buildfunction.soundbookorder();
        var order = $('#order_amount').val();
        if (order > 0) {
            var data = {
                'marketName': $('#select-trade').val(),
                'amount': $('#order_amount').val(),
                'isCall': typeorder === Order_Type_Buy ? 1 : 0,
                'isDemo': _statusSelectBalance ? false : true,
                'formatdecimal': pairconfig.decimal
            };
            $.ajax({
                url: '/orders/book',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    //buildData.loading();
                },
                success: function (data) {
                    //buildData.stopLoading();
                    var result = data.Result;
                    if (result <= 0) {
                        // buildfunction._removeOrderbooks(pricebook);
                    }
                    if (result > 0) {
                        //console.log("price cur: " + data.CurrentPrice);
                        buildData.showNotify("Success", "Notify", "", 1000);
                        buildfunction.flagPrice(typeorder.toLowerCase(), data.CurrentPrice);
                        var _balance = $("#h-total-balance").val();
                        var balance = _balance - order;
                        $("#h-total-balance").val(balance);
                        buildData.accountBalance();
                    } else if (result === -2) {
                        $("#order_amount").empty().val("");
                        buildData.showNotify("Balance is not enough", "Failure", "danger", 500);
                        buildData.accountBalance();
                    } else if (result === 0) {
                        buildData.showNotify("Expired time on order", "Failure", "danger", 0);
                    } else if (result === -1) {
                        window.location.reload();
                    } else if (result === -3) {
                        buildData.showNotifyCenter("The system is updating. Please come back later", "Notify", "danger", 10000);
                    } else {
                        buildData.showNotify("Please try again", "Failure", "danger", 0);
                    }
                },
                error: function () {
                    buildData.showNotify("Please try again", "Failure", "danger");
                    buildfunction._removeOrderbooks(pricebook);
                }
            });
        }
    },
    // Init text amount
    _setValueAmountInit: function () {
        let amount = $("#order_amount").val();
        Amount_fl = (isNaN(amount) || amount < 0 || amount === "" || amount === null) ? 0 : parseFloat(amount);

        if (_balance_valid >= 1) {
            if (_balance_valid <= Amount_fl) {
                $("#order_amount").empty().val(_balance_valid);
            }
        } else {

            $("#order_amount").empty().val(0);
            // buildData.showNotify("Your available balance, not enough!.The minimum amount is $1/n Deposit now!", "Alert", "danger", 0);
        }

    },
    _fillOrder: function (num) {

        let amount = $("#order_amount").val();
        checkvalidAmount = (isNaN(amount) || amount < 0 || amount === "" || amount === null) ? 0 : parseFloat(amount);

        sumAmount = checkvalidAmount + num;

        if (sumAmount >= 0) { // Plus number
            if (sumAmount > _balance_valid) {
                buildData.showNotify("Your available balance, not enough!", "Alert", "danger", 1500);
                $("#order_amount").empty().val("");
            } else {
                $("#order_amount").val(sumAmount);
            }
        } else if (sumAmount < 0) {//Minus number
            buildData.showNotify("The minimum amount is $1 and is not higher than your available balance!", "Alert", "danger", 1500);
            $("#order_amount").empty().val("");
        }
    },
    _assetsfilter: function () {
        var value = $("#keyassetsname").val().toLowerCase();
        $("#tradepairs-main LI").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    },
    _callCheckValidNumber: function callCheckValidNumber() {
        function setInputFilter(textbox, inputFilter) {
            ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
                textbox.addEventListener(event, function () {
                    // get value when change in text
                    var value_amount = document.getElementById("order_amount").value;
                    value_amount = (isNaN(value_amount) || value_amount < 0 || value_amount === "" || value_amount === null) ? 0 : parseFloat(value_amount);
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                        //show value

                        buildfunction._valueTempWin(value_amount);
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    } else {
                        this.value = "";
                    }

                });
            });
        }

        //check validate input (limited )
        setInputFilter(document.getElementById("order_amount"), function (value) {
            //return /^\d?\d*[.]?\d{0,4}$/.test(value);
            if (_balance_valid > 0) {
                //return /^([1-9]|$)\d?\d*[.]?\d{0,4}$/.test(value) && (value === "" || parseFloat(value, 0) <= _balance_valid)
                return /^([1-9]|$)\d?\d*[.]?\d{0,4}$/.test(value) && (value === "" || parseFloat(value, 0) <= _balance_valid)
            }
            //} else {
            //    return /^([1-9]|$)\d?\d*[.]?\d{0,4}$/.test(value) && (value === "")
            //}
        });
    },

    // check validate amount tranfer 
    _callCheckValidAmountTranfer: function CallCheckValidAmountTranfer() {
        function setInputAmountTranfer(textbox, inputFilter) {
            ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
                textbox.addEventListener(event, function () {
                    // get value when change in text
                    var value_amount = document.getElementById("transfer-amount").value;
                    value_amount = (isNaN(value_amount) || value_amount < 0 || value_amount === "" || value_amount === null) ? 0 : parseFloat(value_amount);
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                        //show result after input 

                        //buildfunction._valueTempWin(value_amount);
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    } else {
                        this.value = "";
                    }

                });
            });
        }
        //check validate input (limited )
        setInputAmountTranfer(document.getElementById("transfer-amount"), function (value) {
            return /^([0-9]|$)\d?\d*[.]?\d{0,2}$/.test(value);
        });
    },
    //_check_balance_transfer
    _check_balance_transfer: function () {
        if (document.getElementById("from-address").value == true) {
            amount = $("#total-balance-fb").val();
        } else  {
            amount = $("#total-balance-cp").val();
        }
        checkvalidAmount = (isNaN(amount) || amount < 0 || amount === "" || amount === null) ? 0 : parseFloat(amount);

        return alert(checkvalidAmount);


       
    },
    _mousedown: function (num) {
        mousedown(num);
    },
    _mouseup: function () {
        mouseup();
    },
    _showLeftMenuHub: function () {
        if (!_statusMenuMobile) {
            $("#sidebar").addClass("m-mobile");
            _statusMenuMobile = true;
        } else {
            $("#sidebar").removeClass("m-mobile");
            buildfunction._setHideByResize(true);
            _statusMenuMobile = false;
        }

    },
    _showLeftMenu: function () {

        if (!_statusMenuMobile) {
            $("#sidebar").addClass("m-mobile");
            _statusMenuMobile = true;
        } else {
            $("#sidebar").removeClass("m-mobile");
            _statusMenuMobile = false;
        }
    },
    initpagedeposit: function initpagedeposit(s) {
        $('#qrcode').html('');
        $('#depositwallet').empty();

        var walletname = s;

        if (walletname != "-1") {

            var data = {
                'symbol': s
            };
            $("#depositModalLabel").empty().text(" " + s + " (Wallet format is ERC20) ");
            $("#text-CoinSymbol").empty().text(" " + s + " (Wallet format is ERC20) ");
            $(".textnote-Coin").empty().text(" " + s + " (Wallet format is ERC20) ");
            $("#deposit-icon").attr("src", "/images/symbol/" + s + ".svg");

            $.ajax({
                url: '/office/DepositLoadAddress',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    if (result.Result !== null) {
                        $('#depositwallet').text(result.Result.CoinAddress);
                        //$("#depositModalLabel").empty().text(" "+s +" (Wallet format is ERC20) ");
                        //$("#text-CoinSymbol").empty().text(" " +s + " (Wallet format is ERC20) ");
                        //$(".textnote-Coin").empty().text(" " +s + " (Wallet format is ERC20) ");
                        //$("#deposit-icon").attr("src", "/images/symbol/" + s + ".svg");
                        $('#qrcode').qrcode({ width: 160, height: 160, text: result.Result.CoinAddress });
                    } else {
                        $.alert({
                            title: 'Notify',
                            theme: 'modern',
                            boxWidth: '500px',
                            useBootstrap: false,
                            content: 'This feature not available in your Country.<br/>This feature will be available in your country in the next 2 weeks.<br>Thanks Best & Regards',
                            icon: "fa fa-warning",
                            animation: 'scale',
                            closeAnimation: 'scale',
                            buttons: {
                                okay: {
                                    text: "Ok, got it",
                                    btnClass: 'btn-warning',
                                    action: function () {
                                    }
                                }
                            }
                        });
                    }
                },
                error: function () {
                    buildData.stopLoading();
                    buildData.showNotify("Invalid", "Error", "danger");
                }
            });
        }
    },
    initpagedepositfbtc: function initpagedepositfbtc(s) {
        $('#qrcode').html('');
        $('#depositwallet').empty();

        var walletname = s;
        var fbtc_sym = 'fbtc';
        if (walletname != "-1") {

            var data = {
                'symbol': s
            };
            $("#depositModalLabel").empty().text(" " + fbtc_sym + " (Wallet format is ERC20) ");
            $("#text-CoinSymbol").empty().text(" " + fbtc_sym + " (Wallet format is ERC20) ");
            $(".textnote-Coin").empty().text(" " + fbtc_sym + " (Wallet format is ERC20) ");
            $("#deposit-icon").attr("src", "/images/symbol/" + fbtc_sym + ".svg");

            $.ajax({
                url: '/office/DepositLoadAddress',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    if (result.Result !== null) {
                        $('#depositwallet').text(result.Result.CoinAddress);
                        //$("#depositModalLabel").empty().text(" "+s +" (Wallet format is ERC20) ");
                        //$("#text-CoinSymbol").empty().text(" " +s + " (Wallet format is ERC20) ");
                        //$(".textnote-Coin").empty().text(" " +s + " (Wallet format is ERC20) ");
                        //$("#deposit-icon").attr("src", "/images/symbol/" + s + ".svg");
                        $('#qrcode').qrcode({ width: 160, height: 160, text: result.Result.CoinAddress });
                    } else {
                        $.alert({
                            title: 'Notify',
                            theme: 'modern',
                            boxWidth: '500px',
                            useBootstrap: false,
                            content: 'This feature not available in your Country.<br/>This feature will be available in your country in the next 2 weeks.<br>Thanks Best & Regards',
                            icon: "fa fa-warning",
                            animation: 'scale',
                            closeAnimation: 'scale',
                            buttons: {
                                okay: {
                                    text: "Ok, got it",
                                    btnClass: 'btn-warning',
                                    action: function () {
                                    }
                                }
                            }
                        });
                    }
                },
                error: function () {
                    buildData.stopLoading();
                    buildData.showNotify("Invalid", "Error", "danger");
                }
            });
        }
    }
};
var mouse = false;
function mousedown(num) {
    mouse = true;
    callEvent(num);
}
function mouseup() {
    mouse = false;
}
function callEvent(num) {
    if (mouse) {
        // do whatever you want
        // it will continue executing until mouse is not released
        setTimeout(buildData._fillOrder(num), 1);
    }
    else
        return;
}

$(document).ready(function () {
    $('#drop-method-pay').on('change', function (e) {
        var _type = $("#drop-method-pay").val();
        $.ajax({
            url: '/office/loadpaymethod',
            type: 'POST',
            data: JSON.stringify({ type: _type }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                var mo;
                var price;
                if (_type === "usd") {
                    mo = "fa fa-dollar";
                    price = "Total Balance : <i class='" + mo + "'></i>" + result.Result;
                } else {
                    mo = "curr fa fa-";
                    price = "Total Balance : " + result.Result + _primaryCoin + " ≈ ($" + result.Total + ")";
                }
                //else {
                //    $("#stock-notication").html('');
                //}
                $("#total-balance").html(price);
            },
            error: function () {
            }
        });
    });
    $('#btn-invest-confirm').on('click', function () {
        $.alert({
            title: 'Confirm',
            theme: 'modern',
            boxWidth: '300px',
            useBootstrap: false,
            content: 'Are you sure?',
            icon: "fa fa-warning",
            animation: 'scale',
            closeAnimation: 'scale',
            buttons: {
                okay: {
                    text: "Ok, got it",
                    btnClass: 'btn-danger',
                    action: function () {
                        $('#btn-invest').click();
                    }
                }, cancel: function () {
                }
            }
        });
    });
    $('#btn-invest-confirm2').on('click', function () {
        $.alert({
            title: 'Confirm',
            theme: 'modern',
            boxWidth: '300px',
            useBootstrap: false,
            content: 'Are you sure?',
            icon: "fa fa-warning",
            animation: 'scale',
            closeAnimation: 'scale',
            buttons: {
                okay: {
                    text: "Ok, got it",
                    btnClass: 'btn-danger',
                    action: function () {
                        $('#btn-invest').click();
                    }
                }, cancel: function () {
                }
            }
        });
    });
    $('#btn-user-call-event_1').on('click', function () {
        console.log("btn-user-call-event click");
        buildfunction.flagPrice("buy");
        console.log("isdemo:" + _statusSelectBalance);
        var order = $('#order_amount').val();
        if (order > 0) {
            var data = {
                'marketName': $('#select-trade').val(),
                'amount': order,
                'isCall': 1,
                'isDemo': _statusSelectBalance ? false : true
            };
            $.ajax({
                url: '/Office/UserOrder',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    if (result > 0) {

                        buildData.showNotify("Success", "Notify", "");

                    } else {
                        buildData.showNotify(result, "Error", "danger");
                    }
                    buildData._accountBalance();
                },
                error: function () {
                    buildData.stopLoading();
                }
            });
        }
    });
    $('#btn-user-put-event_1').on('click', function () {
        console.log("btn-user-put-event click");
        buildfunction.flagPrice("sell");
        var order = $('#order_amount').val();
        if (order > 0) {
            var data = {
                'marketName': $('#select-trade').val(),
                'amount': $('#order_amount').val(),
                'isCall': 0,
                'isDemo': _statusSelectBalance ? false : true
            };
            $.ajax({
                url: '/Office/UserOrder',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    if (result > 0) {
                        buildData.showNotify("Success", "Notify", "");
                        var _balance = $("#h-total-balance").val();
                        var balance = _balance - order;
                        $("#h-total-balance").val(balance);
                        buildData._accountBalance();
                    } else {
                        buildData.showNotify(result, "Error", "danger");
                    }
                },
                error: function () {
                    buildData.stopLoading();
                }
            });
        }
    });
    $('#btn-invest').click(function () {
        $('#btn-invest-confirm').text('Waiting...');
        $('#btn-invest-confirm').prop('disabled', true);
        var data = {
            'amount': $('#drop-package-amount').val()
        };
        $.ajax({
            url: '/investment',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                $('#btn-invest-confirm').text('Upgrade');
                if (result.EnableAuthy) {
                    $('#code-digit').focus();
                }
                if (result.Success) {
                    $.alert({
                        title: '',
                        theme: 'modern',
                        boxWidth: '500px',
                        useBootstrap: false,
                        content: "<img src='/Images/congratulation.svg?v=5.9' style='height: 60px;margin-bottom: 10px;'/><p>Successfully Your packages: </p><p> Trader:  $" + $('#drop-package-amount').val() + "</p>" + "<p> Username: " + $("#hddusername").val() + "<br/></p><p>Bots packages has been processed </p>",
                        icon: 'fa fa-success',
                        animation: 'scale',
                        closeAnimation: 'scale',
                        buttons: {
                            okay: {
                                text: "Close",
                                btnClass: 'btn-warning',
                                action: function () {
                                    location.reload();
                                }
                            }
                        }
                    });
                    jqueryLoadList.loadList();
                } else {
                    buildData.showNotify(result.Message, "Notify", result.ClassCss);
                }
                $('#btn-invest-confirm').prop('disabled', false);
            },
            error: function () {
                $('#btn-invest-confirm').text('Upgrade');
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
                $('#btn-invest-confirm').prop('disabled', false);
            }
        });
    });
    $('#btn-Login').click(function () {
        var username = $('#Username').val();
        var password = $('#Password').val();
        var enable = true;
        if (username === "") {
            enable = false;
            $('#Username').css("border-color", "red");
        }
        if (password === "") {
            enable = false;
            $('#Password').css("border-color", "red");
        }
        if (enable) {
            $('#btn-Login').text('Logging in...');
            var data = {
                'username': $('#Username').val(),
                'password': $('#Password').val(),
                'fACode': $('#FACode').val(),
                'remember': $('#Remember').val(),
                'returnUrl': $('#ReturnUrl').val(),
                'response': grecaptcha.getResponse()
            };
            $.ajax({
                url: '/login',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {

                    buildData.stopLoading();
                    $('#btn-Login').html('Login');
                    grecaptcha.reset();
                    if (result.EnableAuthy) {
                        $('#enableAuthy').css('display', 'block');
                        $('#FACode').focus();
                    } else {

                        if (result.Success) {
                            buildData.showNotifyCenter(result.Message, "Notify", result.ClassCss);
                            buildData.redirectUrl(result.RedirectUrl);
                        } else {
                            buildData.showNotifyCenter(result.Message, "Notify", "warning");
                        }
                    }
                },
                error: function () {
                    grecaptcha.reset();
                    $('#btn-Login').html('Login');
                    buildData.stopLoading();
                    buildData.showNotify("Invalid", "Error", "danger");
                }
            });
        }
    });

    $('#btn-register').click(function () {
        $('#btn-register-confirm').text('Waiting...');
        var enable = true;
        var username = $('#Username').val();
        var email = $('#Email').val();
        if (username === "" || isValidateUsername(username.toLowerCase()) === false) {
            enable = false;
            $('#Username').css("border-color", "red");
            buildData.showNotifyCenter("Username invalid", "Notify", "warning");
            $('#Username').focus();
        }


        if (enable) {
            var data = {
                'referralId': $('#ReferralId').val(),
                'fullname': $('#Fullname').val(),
                'email': $('#Email').val(),
                'username': $('#Username').val(),
                'password': $('#Password').val(),
                'passwordComfirm': $('#PasswordComfirm').val(),
                'country': $('#PhoneNatural').val(),
                'phone': $('#Phone').val(),
                //'termpolicy': $('#termpolicy').val()
                'termpolicy': $('#invalidCheck').is(":checked")
            };
            $.ajax({
                url: '/register',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    $('#btn-register-confirm').html('<span class="ic_login">Create a new account</span>');
                    if (result.RedirectUrl !== null && result.RedirectUrl !== "") {
                        buildData.showNotifyCenter(result.Message);
                        buildData.redirectUrl(result.RedirectUrl);
                    } else {
                        buildData.showNotifyCenter(result.Message, "Notify", "warning");
                    }
                },
                error: function () {
                    $('#btn-register-confirm').html('<span class="ic_login">Create a new account</span>');
                    buildData.stopLoading();
                    buildData.showNotify("Invalid", "Error", "warning");
                }
            });
        }
    });

    $('#btn-register-link').click(function () {
        $('#btn-register-link-confirm').text('Waiting...');
        var enable = true;
        var username = $('#Username').val();
        if (username === "" || isValidateUsername(username.toLowerCase()) === false) {
            enable = false;
            $('#Username').css("border-color", "red");
            alert("The username only use uppercase or number");
        }
        if (enable) {
            var data = {
                'referralId': $('#ReferralId').val(),
                'email': $('#Email').val(),
                'username': $('#Username').val(),
                'password': $('#Password').val(),
                'passwordComfirm': $('#PasswordComfirm').val(),
                'country': $('#PhoneNatural').val(),
                'phone': $('#Phone').val(),
                'termpolicy': $('#invalidCheck').is(":checked")
            };
            $.ajax({
                url: '/register-by',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    $('#btn-register-link-confirm').html('<span class="ic_login">Register</span>');
                    if (result.RedirectUrl !== null && result.RedirectUrl !== "") {
                        buildData.showNotify(result.Message);
                        buildData.redirectUrl(result.RedirectUrl);
                    } else {
                        buildData.showNotify(result.Message, "Notify", result.ClassCss);
                    }
                },
                error: function () {
                    $('#btn-register-link-confirm').html('<span class="ic_login">Register</span>');
                    buildData.stopLoading();
                    buildData.showNotify("Invalid", "Error", "danger");
                }
            });
        }
    });

    $('#address-wallet').focusout(function () {
        var wallet = $("#address-wallet").val();
        if (wallet !== "" && wallet !== null) {
            buildData.displayUsername();
        }
    });

    $('#btn-transfer').click(function () {
        $('#btn-transfer-confirm').prop('disabled', true);
        var data = {
            "amount": $("#transfer-amount").val(),
            "wallet": $("#address-wallet").val(),
            "type": $("#dropmethodpay").val(),
            "codeDigit": $("#code-digit").val()
        }

        $('#btn-transfer-confirm').text('Waiting...');
        $.ajax({
            url: '/transfer',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                $('#btn-transfer-confirm').text('Transfer');
                if (result.EnableAuthy) {
                    $('#code-digit').focus();
                }
                buildData.showNotify(result.Message, "Notify", result.ClassCss);
                $('#btn-transfer-confirm').prop('disabled', false);
            },
            error: function () {
                $('#btn-transfer-confirm').text('Transfer');
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
                $('#btn-transfer-confirm').prop('disabled', false);
            }
        });
    });

    $('#id-select-type').on('change', function () {
        //$("#apply-amount-withdraw").html('');
        //var method = $("#id-select-type").val();
        //$.ajax({
        //    url: '/office/getDataWithdraw',
        //    type: 'POST',
        //    data: JSON.stringify({ method: method}),
        //    dataType: 'json',
        //    contentType: 'application/json; charset=utf-8',
        //    success: function (result) {
        //        $("#apply-amount-withdraw").html(result.Message);
        //        buildData.validateWithdraw();
        //    },
        //    error: function () {
        //    }
        //});      

        var _type = $("#id-select-type").val();
        $.ajax({
            url: '/office/loadpaymethod',
            type: 'POST',
            data: JSON.stringify({ type: _type }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (_type === "usd") {
                    var amountusd = $("#balance-usd").val();
                    $("#withdraw-amount").val(amountusd);
                } else {
                    $("#withdraw-amount").val(result.Message);
                }
                buildData.validateWithdraw();
            },
            error: function () {
            }
        });

    });

    $('#withdraw-wallet-name').on('change', function () {
        buildData.validateWithdraw();
    });

    $('#btn-withdraw-comfirm').on('click', function () {
        $.alert({
            title: 'Confirm',
            theme: 'modern',
            boxWidth: '300px',
            useBootstrap: false,
            content: 'Are you sure?',
            icon: 'fa fa-warning',
            animation: 'scale',
            closeAnimation: 'scale',
            buttons: {
                okay: {
                    text: "Ok, got it",
                    //btnClass: 'btn-success',
                    btnClass: 'btn-submit',
                    action: function () {
                        $('#btn-withdraw').click();
                    }
                },
                cancel: {
                    btnClass: 'btn-cancel',
                    action: function () {

                    }
                }
            }
        });
    });

    $('#btn-withdraw').click(function () {
        $('#btn-withdraw-comfirm').prop('disabled', true);
        $('#btn-withdraw-comfirm').text('Waiting...');
        var amountInput = $("#withdraw-amount").val();
        var type = $("#withdraw-wallet-name").val();
        var amount = parseFloat(amountInput);

        var data = {
            'amount': amount,
            'codeDigit': '',// $("#code-digit").val(),
            'address': $("#wallet-address").val(),
            'type': type,
            'method': $("#id-select-type").val()
        };

        $.ajax({
            url: '/withdraw',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                $('#btn-withdraw-comfirm').html('<i class="fa fa-sign-out"></i> Withdraw');
                if (result.EnableAuthy) {
                    $('#code-digit').focus();
                }
                if (result.Success) {
                    buildData.showNotify(result.Message, "Notify", result.ClassCss, 1000000);
                    jqueryLoadList.loadList();
                } else {
                    buildData.showNotify(result.Message, "Notify", result.ClassCss, 1000000);
                }
                $('#btn-withdraw-comfirm').prop('disabled', false);
            },
            error: function () {
                $('#btn-withdraw-comfirm').html('<i class="fa fa-sign-out"></i> Withdraw');
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
                $('#btn-withdraw-comfirm').prop('disabled', false);
            }
        });
    });

    $('#btn-profile').click(function () {
        $('#btn-profile-confirm').text('Waiting...');
        var data = {
            'fullName': $('#FullName').val(),
            'phone': $('#Phone').val(),
            //'walletBTC': $('#WalletCoin').val(),
            //'walletETH': $('#WalletETH').val(),
            //'WalletXRP': $('#WalletXRP').val(),
            //'WalletBCH': $('#WalletBCH').val(),
            //'WalletBNCT': $('#WalletBNCT').val(),
            //'code': $('#Code').val(),
            'codeDigit': $('#code-digit').val()
        };
        $.ajax({
            url: '/Office/UserProfile',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                buildData.showNotifyCenter(result.Message, "Notify", result.ClassCss);
                $('#btn-profile-confirm').text('Update');
                if (result.EnableAuthy) {
                    $('#FACode').focus();
                }
            },
            error: function () {
                $('#btn-profile-confirm').html('<span class="ic_login">Update</span>');
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error 1", "danger");
            }
        });
    });

    $('#btn-change-password').click(function () {
        var pass = $('#txt-password').val();
        var passNew = $('#txt-password-new').val();
        var passNewRe = $('#txt-password-new-re').val();
        var enable = true;
        if (pass === '') {
            enable = false;
            $('#txt-password').css("border-color", "red");
        }
        if (passNew === '' || passNew.length < 8) {
            enable = false;
            buildData.showNotify("Password less than 8 characters", "Error", "danger");
            $('#txt-password-new').css("border-color", "red");
        }
        if (passNewRe === '') {
            enable = false;
            $('#txt-password-new-re').css("border-color", "red");
        }
        if (passNew !== passNewRe) {
            $('#txt-password-new-re').css("border-color", "red");
            buildData.showNotify("Password comfirm is incorrect", "Error", "danger");
            enable = false;
        }

        if (enable) {
            $('#btn-change-password-confirm').text('Waiting...');
            var data = {
                'pass': pass,
                'passNew': passNew,
                'passNewRe': passNewRe
            };
            $.ajax({
                url: '/password-change',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    $('#btn-change-password-confirm').html('Update');
                    buildData.stopLoading();
                    if (result.Success) {
                        buildData.showNotify(result.Message);
                        buildData.redirectUrl(result.RedirectUrl);
                    }
                    else {
                        buildData.showNotify(result.Message, "Notify", result.ClassCss);
                    }
                },
                error: function () {
                    $('#btn-change-password-confirm').html('Update');
                    buildData.stopLoading();
                    buildData.showNotify("Invalid", "Error", "danger");
                }
            });
        } else {
            return;
        }
    });

    $('#btn-authenticator').click(function () {
        var codeDigit = $('#CodeDigit').val();
        var authen = $('#authentication-modal');
        if (codeDigit !== '') {
            var text = $('#btn-authenticator').text();
            $('#btn-authenticator').text('Waiting...');
            var data = {
                'userUniqueKey': $('#UserUniqueKey').val(),
                'barcodeImageUrl': $('#BarcodeImageUrl').val(),
                'setupCode': $('#SetupCode').val(),
                'codeDigit': codeDigit
            };
            $.ajax({
                url: '/account/security',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    if (result === 1) {
                        $('#SetupCode').val('');
                        $('#CodeDigit').val('');

                        if (text === 'Enable') {
                            $('#btn-authenticator').text('Disable');
                        } else {
                            $('#btn-authenticator').text('Enable');
                        }
                        if (authen !== null) {
                            authen.modal('hide');
                        }
                        window.location.reload();
                    } else if (result === -1) {
                        buildData.showNotify("Code not veryfied", "Notify", "danger");
                        $('#btn-authenticator').text(text);
                    }
                    else {
                        buildData.showNotify("Please input 6 digit", "Notify", "danger");
                        $('#btn-authenticator').text(text);
                    }
                },
                error: function () {
                    buildData.stopLoading();
                    $('#btn-authenticator').text(text);
                }
            });
        }
        else {
            $("#CodeDigit").focus();
        }
    });

    $('#btn-forgot-password').click(function () {
        var mail = $('#txtEmail').val();
        if (mail !== "") {
            $('#btn-forgot-password').text('Waiting...');
            var data = {
                'email': mail
            };
            $.ajax({
                url: '/forgotpassword',
                type: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    buildData.loading();
                },
                success: function (result) {
                    buildData.stopLoading();
                    $('#btn-forgot-password').text('Reset');
                    if (result.Success) {
                        buildData.showNotify("Please check email to change your password", 'Next step, ', "success", 10000);
                        $('#txtEmail').val('');
                    } else {
                        buildData.showNotify(result.Message, "Notification! ", result.ClassCss);
                    }
                },
                error: function () {
                    buildData.stopLoading();
                    $('#btn-forgot-password').html('Submit');
                }
            });
        }
    });

    $('#btn-get-password').click(function () {
        var passNew = $('#txt-password-new').val();
        var passNewRe = $('#txt-password-new-re').val();
        if (passNew !== '' && passNewRe !== '') {
            if (passNew !== passNewRe) {
                buildData.showNotify("Password comfirm is incorrect", "Notify", "danger");
            } else {
                $('#btn-get-password').text('Waiting...');
                var data = {
                    'passNew': passNew,
                    'passNewRe': passNewRe,
                    'token': $('#userToken').val()
                };
                $.ajax({
                    url: '/getpassword',
                    type: 'POST',
                    data: JSON.stringify(data),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    beforeSend: function () {
                        buildData.loading();
                    },
                    success: function (result) {
                        buildData.stopLoading();
                        $('#btn-get-password').html('Save');
                        buildData.showNotify(result.Message, "Notify", result.ClassCss);
                        if (result.RedirectUrl !== "" && result.RedirectUrl !== null) {
                            buildData.redirectUrl(result.RedirectUrl);
                        }
                    },
                    error: function () {
                        buildData.stopLoading();
                        $('#btn-get-password').html('Save');
                    }
                });
            }
        }
    });

    $('#btn-transfer-confirm').on('click', function () {
        if ($("#transfer-amount").val() > 0) {
            $.alert({
                title: 'Confirm',
                theme: 'modern',
                boxWidth: '350px',
                useBootstrap: false,
                content: 'Are you sure ?',
                icon: 'icon-transfer svgrotate',
                animation: 'scale',
                closeAnimation: 'scale',
                buttons: {
                    okay: {
                        text: "Ok, got it",
                        btnClass: 'btn-submit',
                        action: function () {
                            buildData.processTransfer();
                        }
                    },
                    cancel: {
                        btnClass:'btn-cancel',
                        action: function () {

                        }
                    }
                }
            });
        } else {
                buildData.showNotify("The minimum amount greater than 0 and is not higher than your available balance!", "Alert", "danger", 1500);
                $("transfer-amount").empty().val("");
            }

    });

    $('#dropmethodpay').on('change', function (e) {
        var _type = $("#dropmethodpay").val();
        $.ajax({
            url: '/office/loadpaymethod',
            type: 'POST',
            data: JSON.stringify({ type: _type }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                var mo, price;
                if (_type === "usd") {
                    mo = _type === "usd" ? "<i class='fa fa-dollar'></i> " : "";
                    price = "Total Balance : " + mo + result.Result;
                } else {
                    price = "Total Balance : " + result.Result + _primaryCoin;
                }

                var fee = "Transfer fee : " + result.Optional + " %";
                $("#total-price").html(price);
                $("#total-fee").html(fee);
                buildData.displayUsername();
            },
            error: function () {
            }
        });
    });

    $('#btn-sell-confirm').on('click', function () {
        if ($("#stock-amount").val() > 0) {
            $.confirm({
                title: 'Confirm!',
                content: 'Are you sure?',
                buttons: {
                    confirm: {
                        text: 'Confirm',
                        btnClass: 'btn-blue',
                        action: function () {
                            $('#btn-sell').click();
                        }
                    },
                    cancel: function () {
                    }
                }
            });
        }
    });

    $('#btn-sell').click(function () {
        $('#btn-sell-comfirm').prop('disabled', true);
        $('#btn-sell-comfirm').text('Waiting...');
        var data = {
            'amount': $("#stock-amount").val(),
            'codeDigit': $("#code-digit").val()
        };

        $.ajax({
            url: '/account/sell',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                $('#btn-sell-comfirm').text('Sell');
                if (result.EnableAuthy) {
                    $('#code-digit').focus();
                }
                if (result.Success) {
                    buildData.showNotify(result.Message);
                } else {
                    buildData.showNotify(result.Message, "Notify", result.ClassCss);
                }
                $('#btn-sell-comfirm').prop('disabled', false);
            },
            error: function () {
                $('#btn-sell-comfirm').text('Sell');
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
                $('#btn-sell-comfirm').prop('disabled', false);
            }
        });
    });

    $('#btnActiveAuthenticator').click(function () {
        $('#authentication-modal').modal('show');
    });
    $('#btn-change-email').click(function () {
        $('#btn-change-email-confirm').text("Waiting...");
        var codeold = $('#txt-codeverifyoldemail').val();
        var newemail = $('#txt-new-email').val();
        var codenew = $('#txt-codeverifynewemail').val();
        var data = {
            'codeold': codeold,
            'newemail': newemail,
            'codenew': codenew
        };
        $.ajax({
            url: '/office/ChangeEmailAprrove',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                $.alert({
                    title: 'Success!',
                    theme: 'modern',
                    boxWidth: '400px',
                    useBootstrap: false,
                    content: result.Message,
                    icon: 'fa fa-success',
                    animation: 'scale',
                    closeAnimation: 'scale',
                    buttons: {
                        okay: {
                            text: "Ok",
                            btnClass: 'btn-warning',
                            action: function () {
                                location.reload();
                            }
                        }
                    }
                });
            },
            error: function () {
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
            }
        });
    });
    $('#btn-getcodeoldemail').click(function () {
        $('#btn-getcodeoldemail').text('Waiting...');
        $('#btn-getcodeoldemail').attr('disable');
        var type = "oldemail";
        var email = $('#Email').val();
        SendCodeChangeEmail(type, email);
    });
    $('#btn-getcodenewemail').click(function () {
        var type = "newemail";
        var email = $('#txt-new-email').val();
        if (email.length <= 0 || email === "") {

            $.alert({
                title: 'Notify!',
                theme: 'modern',
                boxWidth: '400px',
                useBootstrap: false,
                content: "Please enter new email",
                icon: 'fa fa-warning',
                animation: 'scale',
                closeAnimation: 'scale',
                buttons: {
                    okay: {
                        text: "Ok",
                        btnClass: 'btn-warning'
                    }
                }
            });
            return;
        }
        $('#btn-getcodenewemail').text('Waiting...');
        $('#btn-getcodenewemail').attr('disable');
        SendCodeChangeEmail(type, email);

    });

    $('#send-ticket-confirm').on('click', function () {
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure?',
            buttons: {
                confirm: {
                    text: 'Confirm',
                    btnClass: 'btn-blue',
                    action: function () {
                        $('#send-ticket').click();
                    }
                },
                cancel: function () {
                }
            }
        });
    });
    $('#send-ticket').click(function () {

        $('#send-ticket').text('Waiting...');
        $('#send-ticket').attr('disable');
        var data = {
            'title': $("#t-subject").val(),
            'fullname': $("#t-fullname").val(),
            'email': $("#t-email").val(),
            'phonenumber': $("#t-phonenumber").val(),
            'messages': $("#t-messages").val()
        };
        $.ajax({
            url: '/office/ticket',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                buildData.showNotifyCenter(result.Message, "Notify", result.ClassCss, 15000);
                jqueryLoadList.loadList();
            },
            error: function () {
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
            }
        });

    });
    //$('#order25').click(function () {
    //    Orderfill(25);
    //});
    //$('#order50').click(function () {
    //    Orderfill(50);
    //});
    //$('#fillamountplus').click(function () {
    //});

    $('#tradepairs-main').on('click', 'li', function () {
        $('#tradepairs-main li.active').removeClass('active');
        $(this).addClass('active');
    });
    function SendCodeChangeEmail(type, email) {
        var data = {
            'type': type,
            'newemail': email
        };
        $.ajax({
            url: '/office/ChangeEmailSendCodeVerify',
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                buildData.loading();
            },
            success: function (result) {
                buildData.stopLoading();
                if (type === "oldemail") {
                    $('#btn-getcodeoldemail').text('Get Code');
                    $('#btn-getcodeoldemail').removeAttr('disable');
                } else if (type === "newemail") {
                    $('#btn-getcodenewemail').text('Get Code');
                    $('#btn-getcodenewemail').removeAttr('disable');
                }
                $.alert({
                    title: 'Success!',
                    theme: 'modern',
                    boxWidth: '400px',
                    useBootstrap: false,
                    content: result.Message,
                    icon: 'fa fa-success',
                    animation: 'scale',
                    closeAnimation: 'scale',
                    buttons: {
                        okay: {
                            text: "Ok",
                            btnClass: 'btn-warning'
                        }
                    }
                });
            },
            error: function () {
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
            }
        });
    }
    function Orderfill(per) {
        if (per === 100) {
            $("#order_amount").val($("#h-total-balance").val().replace(',', ''));
        }

    }
    $("#keynameforex").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbForexmarkets tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
    $("#keynamecrypto").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbmarkets tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
    $("#keyassetsname").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tradepairs-main LI").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
    $("#fillamountminus").on("mousedown", function () {
        mousedown();
    });
    $("#fillamountminus").on("mouseup", function () {
        mouseup();
    });
    $('#acreal').on('click', function () {
        _statusSelectBalance = true;
        setSelectAccount();
    });
    $('#acdemo').on('click', function () {
        _statusSelectBalance = false;
        setSelectAccount();
    });

});

function setSelectAccount() {
    _balance_valid = 0;
    if (_statusSelectBalance) {
        $("#total-balance").html(_balance._usd);
        //$("#accname").text("Actual Balance");
        $("#accname").text("LIVE ACCOUNT");
        $("#li-panel-balance").addClass("isReal");
        //_balance_valid = parseFloat(replaceComma(_balance._usd.replace('$', '')));
        value_balance_real = replaceComma(_balance._usd.replace('$', ''));
        _balance_valid = (isNaN(value_balance_real) || value_balance_real < 0 || value_balance_real === "" || value_balance_real === null) ? 0 : parseFloat(value_balance_real);

    } else {
        $("#total-balance").html(_balance._usdDemo);
        $("#accname").text("DEMO ACCOUNT");
        $("#li-panel-balance").removeClass("isReal");
        //_balance_valid = parseFloat(replaceComma(_balance._usdDemo.replace('$', '')));
        value_balance_demo = replaceComma(_balance._usdDemo.replace('$', ''));
        _balance_valid = (isNaN(value_balance_demo) || value_balance_demo < 0 || value_balance_demo === "" || value_balance_demo === null) ? 0 : parseFloat(value_balance_demo);

    }

}

// remove comma (1,2545.5)
function replaceComma(num) {
    return num.replace(/,/g, '');
};

$(document).on('click', '.fill-amount-max', function (event) {
    $("#order_amount").val($("#total-balance").text().replace(',', '').replace(',', '').replace('$', ''));
});
//$(document).on('click', '.highcharts-arrow-left', function (event) {
//   // alert(1);

//    debugger;
//    if (statushietools === false) {
//        $("#slider-volume").removeClass("slider-volume").addClass("slider-volume-left");
//        statushietools = true;
//    } else {
//        $("#slider-volume").removeClass("slider-volume-left").addClass("slider-volume");
//        statushietools = false;
//    }
//});
//$(document).on('click', '.highcharts-arrow-right', function (event) {
//    //alert(1);
//    if (statushietools === false) {
//        $('#slider-volume').removeClass("slider-volume").addClass("slider-volume-left");
//        statushietools = true;
//    } else {
//        $('#slider-volume').removeClass("slider-volume-left").addClass("slider-volume");
//        statushietools = false;
//    }
//});




// enter amount








