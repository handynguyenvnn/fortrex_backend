var model_dialog = $('#user-modal');

function addUser(self, id, note) {
    var code = $(self).attr("id");
    model_dialog.find('#model-uid').val(note);
    model_dialog.find('#model-code').val(code);
    model_dialog.modal('show');
}

model_dialog.find('.btn-primary').on('click', function () {
    var username = model_dialog.find('#model-username').val();
    var email = model_dialog.find('#model-email').val();
    var pass = model_dialog.find('#model-password').val();
    var passconfirm = model_dialog.find('#model-password-comfirm').val();
    var referal = model_dialog.find('#model-code').val();
    var noteId = model_dialog.find('#model-uid').val();
    var packageId = model_dialog.find('#drop-package-amount').val();

    enable = true;
    if (username === "" || isValidateUsername(username.toLowerCase()) === false) {
        enable = false;
        $('#Username').css("border-color", "red");
        buildData.showNotifyCenter("Username invalid", "Notify", "warning");
        $('#model-username').focus();
    }

    if (enable) {
        var data = {
            'referralId': referal,
            'fullname': username,
            'email': email,
            'username': username,
            'password': pass,
            'passwordComfirm': passconfirm,
            'nodeUid': noteId,
            'packageId': packageId
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
                if (result.Success) {
                    $.alert({
                        title: '',
                        theme: 'modern',
                        boxWidth: '500px',
                        useBootstrap: false,
                        content: "<img src='/Images/checked.svg?v=5.9' /><p>Successfully Your packages: </p><p> Trader:  $" + model_dialog.find('#drop-package-amount').val() + "</p>"+"<p> Username: " + username.toUpperCase() + "<br/></p><p>Bots packages has been processed </p>" ,
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
                } else {
                    buildData.showNotify(result.Message, "Notify", result.ClassCss);
                }
            },
            error: function () {
               
                buildData.stopLoading();
                buildData.showNotify("Invalid", "Error", "danger");
            }
        });
    }
});