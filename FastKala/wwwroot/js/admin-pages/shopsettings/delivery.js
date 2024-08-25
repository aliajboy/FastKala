class shippingData {
    constructor(id, price, freeShippingPrice, extraPrice) {
        this.id = id;
        this.price = price;
        this.freeShippingPrice = freeShippingPrice;
        this.extraPrice = extraPrice;
    }
}

async function toggleShipping(shippingId) {
    const res = await $.ajax({
        url: '/Admin/ShopSettings/ToggleShipping',
        method: 'POST',
        data: { id: shippingId }
    });

    if (res.operationStatus != 0) {
        Swal.fire({
            title: "خطا",
            text: res.message,
            icon: "error",
            confirmButtonText: "باشه"
        });
    }
}

async function saveShippingSettings() {
    const shippingId = $('#saveButton').attr('shippingId');
    const basePrice = $('#basePrice').val();
    const freePrice = $('#freePrice').val();
    const extraPrice = $('#extraPrice').val();
    const shippingInstance = new shippingData(shippingId, basePrice, freePrice, extraPrice);

    const res = await $.ajax({
        url: '/Admin/ShopSettings/UpdateShipping',
        method: 'POST',
        data: shippingInstance
    });

    if (res.operationStatus != 0) {
        Swal.fire({
            title: "خطا",
            text: res.message,
            icon: "error",
            confirmButtonText: "باشه"
        });
    }
    else {
        Swal.fire({
            title: "موفق",
            text: res.message,
            icon: "success",
            confirmButtonText: "باشه"
        });
    }
    $('#shipping-settings-modal').modal('hide');
}

async function showSettingsModal(shippinId) {
    const shipping = await $.ajax({
        url: '/Admin/ShopSettings/GetShippingData',
        method: 'POST',
        data: { id: shippinId }
    });

    if (shipping != null && shipping != undefined) {
        $('#basePrice').val(shipping.price);
        $('#freePrice').val(shipping.freeShippingPrice);
        $('#extraPrice').val(shipping.extraPrice);
        if (shipping.type == 0) {
            $('#basePrice').attr('disabled', true);
            $('#extraPrice').attr('disabled', false);
        } else {
            $('#basePrice').attr('disabled', false);
            $('#extraPrice').attr('disabled', true);
        }
        $('#saveButton').attr('shippingId', shippinId);
        $('.modal-title').html($('#shippingName-' + shippinId).html());
        $('#shipping-settings-modal').modal('show');
    }
    else {
        Swal.fire({
            title: "خطا",
            text: "اتصال با سرور برقرار نشد!",
            icon: "error",
            confirmButtonText: "باشه"
        });
    }
}

$('#shipping-settings-modal').on('hidden.bs.modal', function () {
    $('#saveButton').attr('shippingId', 0);
    $('#basePrice').val('');
    $('#freePrice').val('');
    $('#extraPrice').val('');
})