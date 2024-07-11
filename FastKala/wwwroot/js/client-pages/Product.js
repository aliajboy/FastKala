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
            url: '/Products/AddToCard',
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
            Toast.fire({
                icon: 'success',
                title: 'به سبد خرید اضافه شد'
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