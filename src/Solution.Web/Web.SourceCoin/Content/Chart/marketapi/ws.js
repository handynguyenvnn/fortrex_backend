// this is where you paste your api key
var apiKey = "24ebc9e24ac40c431343dd0aa4267a0fabb4815f4db9e8a30aee24928e4683d3";
var ccStreamer = new WebSocket('wss://streamer.cryptocompare.com/v2?api_key=' + apiKey);
ccStreamer.onopen = function onStreamOpen() {
    var subRequest = {
        "action": "SubAdd",
        "subs": ["24~CCCAGG~BTC~USD~m"]
    };
    ccStreamer.send(JSON.stringify(subRequest));
}

ccStreamer.onmessage = function onStreamMessage(message) {
   // console.log(event.data);
    var message = JSON.parse(event.data);
    if (message.TYPE.toString()==="24") {
        //console.log("Received from Cryptocompare: " + JSON.stringify(message));
    }
    //console.log("Item 1: " + message.TYPE.toString());
    //console.log("Item 2: " + message.TYPE.toString());
    //console.log("Received from Cryptocompare: " + message);
}
