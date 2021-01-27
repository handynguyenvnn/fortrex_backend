//let socket = new WebSocket("wss://javascript.info/article/websocket/demo/hello");
let API_KEY = 'UXFLDIGRUWQAF43J0J9O';
let conn = new WebSocket('wss://stream.cryptowat.ch/connect?apikey=' + API_KEY);

conn.onmessage('message', function (msg) {
    console.log('msg: ' + msg);
    const d = JSON.parse(msg.toString());

    // The server will always send an AUTHENTICATED signal when you establish a valid connection
    // At this point you can subscribe to resources
    if (d.authenticationResult && d.authenticationResult.status === 'AUTHENTICATED') {
        subscribeTo(conn, ['instruments:9:trades']);
    }

    // Market data comes in a marketUpdate
    // In this case, we're expecting trades so we look for marketUpdate.tradesUpdate
    if (d.marketUpdate && d.marketUpdate.tradesUpdate) {
        for (let trade of d.marketUpdate.tradesUpdate.trades) {
            console.log(`BTC/USD trade on market ${d.marketUpdate.market.marketId}: ${trade.timestampNano} ${trade.priceStr} ${trade.amountStr}`);

            // That's it! It's that easy to tap in to the global crypto market pipeline.
        }
    }
});

// Helper method for subscribing to resources
function subscribeTo(conn, resources) {
    conn.send(JSON.stringify({
        subscribe: {
            subscriptions: resources.map((resource) => { return { streamSubscription: { resource: resource } } })
        }
    }));
}



//socket.onopen = ('message', function (msg) {
   
//});

//socket.onmessage = ('message', function (msg){
//    //alert(`[message] Data received from server: ${event.data}`);
//    const d = JSON.parse(msg.toString());

//    // The server will always send an AUTHENTICATED signal when you establish a valid connection
//    // At this point you can subscribe to resources
//    if (d.authenticationResult && d.authenticationResult.status === 'AUTHENTICATED') {
//        subscribeTo(conn, ['instruments:9:trades']);
//    }

//    // Market data comes in a marketUpdate
//    // In this case, we're expecting trades so we look for marketUpdate.tradesUpdate
//    if (d.marketUpdate && d.marketUpdate.tradesUpdate) {
//        for (let trade of d.marketUpdate.tradesUpdate.trades) {
//            console.log(`BTC/USD trade on market ${d.marketUpdate.market.marketId}: ${trade.timestampNano} ${trade.priceStr} ${trade.amountStr}`);

//            // That's it! It's that easy to tap in to the global crypto market pipeline.
//        }
//    }
//});

//socket.onclose = function (event) {
//    if (event.wasClean) {
//        alert(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
//    } else {
//        // e.g. server process killed or network down
//        // event.code is usually 1006 in this case
//        alert('[close] Connection died');
//    }
//};

//socket.onerror = function (error) {
//    alert(`[error] ${error.message}`);
//};