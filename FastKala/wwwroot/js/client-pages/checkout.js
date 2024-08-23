function acceptTerms() {
    if ($('#AcceptTerms').is(":checked")) {
        $('.btn-Order').attr("disabled", false);
    }
    else {
        $('.btn-Order').attr("disabled", true);
    }
}

$('input[type=radio][name=ShippingMethod]').on('change', async function () {
    const res = await $.ajax({
        url: '/Order/GetShippingPrice',
        method: 'POST',
        data: {
            shipping: $(this).val()
        }
    });
    const orderAmount = $('#order-price').html().replaceAll(',', '');
    const shippingPrice = parseInt(res);
    $('#shipping-price').html(shippingPrice.toLocaleString());
    $('#total-price').html((shippingPrice + parseInt(orderAmount)).toLocaleString());
    $('#TotalPrice').val((shippingPrice + parseInt(orderAmount)));
});