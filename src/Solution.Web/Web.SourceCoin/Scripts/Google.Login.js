var googleUser = {};
var googleLogin = function () {
    gapi.load('auth2', function () {
        auth2 = gapi.auth2.init({
            client_id: '976830194155-jp89vrueo8qrmj6dbf2u56q8d85dj9kd.apps.googleusercontent.com',
            cookiepolicy: 'http://Web.SourceCoin.vn',
        });
        attachSignin(document.getElementById('customBtn'));
    });
};

function attachSignin(element) {
    auth2.attachClickHandler(element, {},
        function (googleUser) {
            $.ajax({
                type: "GET",
                url: 'api/checklogin?type=2&token=' + googleUser.Zi.access_token,
                success: function (response) {
                    if (response != "false") {
                        location.href = response;
                    }
                }
            });
        });
}

googleLogin();
