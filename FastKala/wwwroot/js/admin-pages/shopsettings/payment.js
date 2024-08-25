class paymentData {
    constructor(id,name,type,apikey) {
        this.id = id;
        this.name = name;
        this.type = type;
        this.apikey = apikey;
    }
}

function addPaymentSetting() {
    $('#payment-settings-modal').modal('show');
}

async function editGateWay(paymentId) {
    const name = $('#name-' + paymentId);
    const type = $('#type-' + paymentId);
    const key = $('#key-' + paymentId);

    if (name.attr('disabled')) {
        name.attr('disabled', false);
        type.attr('disabled', false);
        key.attr('disabled', false);
    }
    else {
        name.attr('disabled', true);
        type.attr('disabled', true);
        key.attr('disabled', true);
        const paymentInit = new paymentData(paymentId, name.val(), type.val(), key.val());
        const res = await $.ajax({
            url: '/Admin/ShopSettings/UpdateGateway',
            method: 'POST',
            data: paymentInit
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
}

async function removeGateway(paymentId) {
    const res = await $.ajax({
        url: '/Admin/ShopSettings/RemovePayment',
        method: 'DELETE',
        data: { id: paymentId }
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
        window.location.reload();
    }

}