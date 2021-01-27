function statusChangeCallback(response) {
    if (response.status === 'connected') {
        $.ajax({
            type: "GET",
            url: 'api/checklogin?type=1&token=' + response.authResponse.accessToken,
            success: function (response) {
                if (response != "false") {
                    location.href = response;
                }
            }
        });
    }
}

  function checkLoginState() {
    FB.getLoginStatus(function(response) {
      statusChangeCallback(response);
    });
  }

  window.fbAsyncInit = function() {
      FB.init({
        appId      : '228258691024536',
        cookie     : true,  // enable cookies to allow the server to access 
        xfbml      : true,  // parse social plugins on this page
        version    : 'v2.9' // use graph api version 2.8
      });
      //FB.getLoginStatus(function(response) {
      //  statusChangeCallback(response);
      //});
  };

  (function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
  }(document, 'script', 'facebook-jssdk'));