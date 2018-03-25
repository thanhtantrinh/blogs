$(document).ready(function () {
    

var sendfile = {
    init: function () {
        sendfile.regEvents();
    },
    regEvents: function () {



        $('#btnSendFile').off('click').on('click', function () { 

            var name = $('#txtName').val();
            var mobile = $('#txtMobile').val();
            var address = $('#txtAddress').val();
            var email = $('#txtEmail').val();
            var product = $('#txtProduct').val();
            var company = $('#txtCompany').val();

            if (email != "" && mobile != "") {
                $('#show_process').show();
                $.ajax({
                    url: '/Content/Send',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        name: name,
                        mobile: mobile,
                        address: address,
                        email: email,
                        productid: product,
                        company: company,
                    },
                    success: function (res) {
                        if (res.Status == true) {                            
                            sendfile.resetForm();
                            $('#show_process').hide();
                            $('#myModal').modal('hide');
                            window.open(res.Result)
                        }
                    }
                });
            }

        });




    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtMobile').val('');
        $('#txtAddress').val('');
        $('#txtEmail').val('');
        $('#txtProduct').val('');
        $('#txtCompany').val('');
    }
}
sendfile.init();

});