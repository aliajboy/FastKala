async function likeComment(commentId) {
    try {
        await $.ajax({
            url: '/Product',
            method: 'POST',
            data: { id: commentId, liked: true }
        });
    } catch (error) {
        console.log("likeComment Error: " + error.stack);
    }
}

async function dislikeComment(commentId) {
    try {
        await $.ajax({
            url: '/Product',
            method: 'POST',
            data: { id: commentId, liked: false }
        });
    } catch (error) {
        console.log("dislikeComment Error: " + error.stack);
    }
}

async function addToCart(productId, quantity) {
    try {
        const res = await $.ajax({
            url: '/AddToCard',
            method: 'POST',
            data: { productId: productId, quantity: parseInt(quantity) }
        });

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

        if (res.operationStatus === 0) {
            if ($('.count-cart').html() == '0') {

                const newTotalPrice = (parseInt(quantity) * parseInt($('.amount').html().toString().replaceAll(',', ''))).toLocaleString();

                const firstAddToCard = `<div class="widget-shopping-cart">
                    <div class="widget-shopping-cart-content">
                        <div class="wrapper">
                            <div class="scrollbar" id="style-1">
                                <div class="force-overflow w-100">
                                    <ul class="product-list-widget">
                                            <li class="mini-cart-item" id="product-${productId}">
                                                <div class="mini-cart-item-content">
                                                    <a href="javascript:void(0)" class="mini-cart-item-close" productId="${productId}">
                                                        <i class="mdi mdi-close"></i>
                                                    </a>
                                                    <a href="/Product/${productId}" class="mini-cart-item-image d-block">
                                                        <img src="${$('#img-product-zoom').attr('src')}">
                                                    </a>
                                                    <span class="product-name-card">
                                                        ${$('.product-title').html().toString()}
                                                    </span>
                                                    <div class="variation">
                                                        <span class="variation-n">

                                                        </span>
                                                        <p class="mb-0"></p>
                                                    </div>
                                                    <div class="header-basket-list-item-color-badge">
                                                    </div>
                                                    <div class="quantity">
                                                        <span class="quantity-Price-amount">
                                                            ${$('.amount').html()}
                                                            <span>تومان</span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="mini-card-total">
                            <strong>قیمت کل : </strong>
                            <span class="price-total">${newTotalPrice}</span>
                            <span>تومان</span>
                        </div>
                        <div class="mini-card-button">
                            <a href="/cart" class="view-card">مشاهده سبد خرید</a>
                            <a href="checkout.html" class="card-checkout">تسویه حساب</a>
                        </div>
                    </div>
                 </div>`;

                $('.count-cart').html(parseInt(quantity));
                $('.price-cart').html(newTotalPrice);
                $('.header-cart-basket').append(firstAddToCard);
            }
            else {
                if ($('#product-' + productId).length < 1) {
                    const htmlString = `<li class="mini-cart-item">
                    <div class="mini-cart-item-content">
                    <a asp-controller="Products" asp-action="Product" asp-route-id="@item.ProductId" class="mini-cart-item-close">
                        <i class="mdi mdi-close"></i>
                    </a>
                    <a asp-controller="Products" asp-action="Product" asp-route-id="@item.ProductId" class="mini-cart-item-image d-block">
                        <img src="${$('#img-product-zoom').attr('src')}">
                    </a>
                    <span class="product-name-card">
                        ${$('.product-title').html().toString()}
                    </span>
                    <div class="variation">
                        <span class="variation-n">
                
                        </span>
                        <p class="mb-0"></p>
                    </div>
                    <div class="header-basket-list-item-color-badge">
                    </div>
                    <div class="quantity">
                        <span class="quantity-Price-amount">
                            ${$('.amount').html()}
                        </span>
                    </div>
                </div>
                </li>`;
                    $('.count-cart').html(parseInt($('.count-cart').html()) + parseInt(quantity));
                    $('.product-list-widget').append(htmlString);
                }
                let totalPrice = parseInt($('.price-total').html().toString().replaceAll(',', ''));
                const newTotalPrice = (totalPrice + (parseInt(quantity) * parseInt($('.amount').html().toString().replaceAll(',', '')))).toLocaleString();
                $('.price-total').html(newTotalPrice);
                $('.price-cart').html(newTotalPrice);
            }
            Toast.fire({
                icon: 'success',
                title: 'به سبد خرید اضافه شد'
            });
            $('.mini-cart-item-close').on('click', function () {
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
                            data: { productId: $('.mini-cart-item-close').attr('productId') }
                        });

                        if (res.operationStatus === 0) {
                            Swal.fire({
                                title: 'حذف شد!',
                                confirmButtonText: 'باشه',
                                icon: 'success'
                            }).then(function () {
                                window.location.reload();
                            });
                        }
                        else {
                            Swal.fire({
                                title: 'خطا در حذف آیتم!',
                                confirmButtonText: 'باشه',
                                icon: 'error'
                            });
                        }
                    }
                });
            });
        }
        else {
            Toast.fire({
                icon: 'error',
                title: 'خطا در افزودن به سبد خرید'
            });
        }
    } catch (error) {
        console.log("dislikeComment Error: " + error.stack);
    }
}