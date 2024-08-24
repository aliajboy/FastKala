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
        $('#price-cart').html(totalPrice.toLocaleString());
        $('.price-total').html(totalPrice.toLocaleString());
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

                let totalPrice = 0;
                $('[id^="total-amount-"]').each(function () {
                    const innerHtml = $(this).html();
                    const parsedValue = parseInt(innerHtml.replace(/,/g, ''), 10);
                    totalPrice += parsedValue;
                });

                $('#price-cart').html(totalPrice.toLocaleString());
                $('.count-cart').html(parseInt($('.count-cart').html()) - 1);

                if (totalPrice === 0) {
                    $('.widget-shopping-cart').remove();
                    $('.main-row').html(`<section class="cart-home">
                    <div class="post-item-cart d-block order-2">
                        <div class="content-page">
                            <div class="cart-form">
                                <div class="cart-empty text-center d-block">
                                    <div class="cart-empty-img mb-4 mt-4">
                                        <img src="/images/shopping-cart.png">
                                    </div>
                                    <p class="cart-empty-title">سبد خرید شما در حال حاضر خالی است.</p>
                                    <div class="return-to-shop">
                                        <a href="/" class="backward btn btn-secondary">بازگشت به صفحه اصلی</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>`);
                }
                else {
                    $('#total-without-shipping').html(totalPrice.toLocaleString() + ' تومان');
                    $('.price-total').html(totalPrice.toLocaleString());
                }
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