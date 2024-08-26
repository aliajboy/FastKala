
let stateId = $('#TownId').children("option:selected").val();
let cityId = $('#CityId').children("option:selected").val();

$(function () {
    acceptTerms();
    changeShippingType();
});

function acceptTerms() {
    if ($('#AcceptTerms').is(":checked")) {
        $('.btn-Order').attr("disabled", false);
    }
    else {
        $('.btn-Order').attr("disabled", true);
    }
}

async function changeShippingType() {
    const res = await $.ajax({
        url: '/Order/GetShippingPrice',
        method: 'POST',
        data: {
            shipping: $('input[type=radio][name=ShippingMethod]:checked').val(), state: stateId, city: cityId
        }
    });
    const orderAmount = $('#order-price').html().replaceAll(',', '');
    const shippingPrice = parseInt(res);
    $('#shipping-price').html(shippingPrice.toLocaleString());
    $('#total-price').html((shippingPrice + parseInt(orderAmount)).toLocaleString());
    $('#TotalPrice').val((shippingPrice + parseInt(orderAmount)));
}

async function loadCities() {
    const res = await $.ajax({
        url: '/Order/GetCities',
        method: 'POST',
        data: {
            id: $(this).val()
        }
    });
    if (res != null || res != undefined) {
        const selectElement = $('#CityId');
        selectElement.html('');
        res.forEach(item => {
            const option = $('<option></option>').val(item.cityId).text(item.city);
            selectElement.append(option);
        });
        stateId = $('#TownId').children("option:selected").val();
        cityId = $('#CityId').children("option:selected").val();
        changeShippingType();
    }
}

$('input[type=checkbox][id=AcceptTerms]').on('change', acceptTerms);

$('input[type=radio][name=ShippingMethod]').on('change', changeShippingType);

$('select[name=TownId]').on('change', loadCities);
