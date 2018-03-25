var cart = {
    init: function () {
        cart.regEvents();
    },
    reloadCartView: function () {
        $.ajax({
            url: '/Cart/ViewCart',            
            dataType: 'json',
            type: 'POST',
            success: function (res) {                
                if (res.status == true) {
                    $("#viewcart").html(res.result);
                }
            }
        })
    },
    reloadCart: function(){
        $.ajax({
            url: '/Cart/Index',            
            dataType: 'json',
            type: 'POST',
            success: function (res) {                
                if (res.status == true) {
                    $("#viewcart").html(res.result);                    
                }
            }
        })
    },
    regEvents: function () {        

        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        
        $('#btnUpdate').on('click', function () {
            console.log("click ");
            var listProduct = $('.Qty');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    console.log(res);
                    if (res.status == true) {
                        cart.reloadCartView();
                        $("#cartList").html(res.result);
                    }
                }
            });            
        });

        $('#btnDeleteAll').off('click').on('click', function () {            
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        cart.reloadCart();
                        cart.reloadCartView();
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {  
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    console.log(res);
                    if (res.status == true) {
                        cart.reloadCartView();
                        $("#cartList").html(res.result);
                    }
                }
            })
        });
    }
}
cart.init();