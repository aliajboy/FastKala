async function likeComment(commentId) {
    try {
        const res = await $.ajax({
            url: '/Product',
            method: 'POST',
            data: { id: commentId, liked: true }
        });
    } catch (error) {

    }
}

async function dislikeComment(commentId) {
    try {
        const res = await $.ajax({
            url: '',
            method: 'POST',
            data: { id: commentId, liked: false }
        });
    } catch (error) {

    }
}