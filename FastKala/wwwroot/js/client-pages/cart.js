async function changeProductQuantity(inputQuantityElement, productId) {
    const res = await $.ajax({
        url: '/ChangeCartValue',
        method: 'POST',
        data: { quantity: inputQuantityElement.value, productId: productId }
    });

    if (res.operationStatus === 0) {
        const productPrice = parseInt($('#product-price-' + productId).html().replaceAll(',', ''));
        $('#total-amount-' + productId).html((parseInt(inputQuantityElement.value) * productPrice).toLocaleString());

        let totalPrice = 0;
        $('[id^="total-amount-"]').each(function () {
            const innerHtml = $(this).html();
            const parsedValue = parseInt(innerHtml.replace(/,/g, ''), 10);
            totalPrice += parsedValue;
        });

        $('#total-without-shipping').html(totalPrice.toLocaleString() + ' تومان');
    }
    else {
        const Toast = Swal.mixin({
            toast: true,
            position: 'center',
            showConfirmButton: false,
            timer: 2000,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        });

        Toast.fire({
            icon: 'error',
            title: 'خطا در بروزرسانی سبد خرید'
        });
    }
}

async function removeCartItem(productId) {
    Swal.fire({
        text: "آیا مطمئن هستید حذف شود؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر'
    }).then(async (result) => {
        if (result.isConfirmed) {
            const res = await $.ajax({
                url: '/RemoveCartItem',
                method: 'POST',
                data: { productId: productId }
            });

            if (res.operationStatus === 0) {
                Swal.fire({
                    title: 'حذف شد!',
                    confirmButtonText: 'باشه',
                    icon: 'success'
                });
                $('#product-' + productId).remove();
                $('#cartitem-' + productId).remove();
            }
            else {
                Swal.fire({
                    title: 'خطا در حذف آیتم!',
                    confirmButtonText: 'باشه',
                    icon: 'error'
                });
            }
        }
    })
}