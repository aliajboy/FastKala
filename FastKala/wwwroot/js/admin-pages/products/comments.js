// Remove Comment
function removeComment(id) {
    $.ajax({
        url: '/Admin/Products/RemoveComment',
        method: 'POST',
        data: {
            commentId: id
        }
    }).done(function (result) {
        if (result.operationStatus == 0) {
            $('#comment-' + id).remove();
        }
    });
}

// Verify Comment
function verifyComment(commentId) {
    $.ajax({
        url: '/Admin/Products/VerifyComment',
        method: 'POST',
        data: {
            id: commentId
        }
    }).done(function (result) {
        if (result.operationStatus == 0) {
            $('#comment-' + commentId).removeClass('bg-label-warning');
            $('#verifySpan-' + commentId).remove();
            $('#commentSender-' + commentId).append('<i class= "bx bxs-badge-check text-success" > </i>');
        }
    });
}

// Open Edit Modal
function openModal(commentId) {
    $.ajax({
        url: '/Admin/Products/GetComment',
        method: 'POST',
        data: {
            id: commentId
        }
    }).done(function (result) {
        $('#commentId').val(commentId);
        $('#commentDescription').val(result.productComment.description);
    });
    $('#editCommentModal').modal('show');
}

//Edit Comment
function editComment() {
    $.ajax({
        url: '/Admin/Products/EditComment',
        method: 'POST',
        data: {
            id: $('#commentId').val(),
            comment: $('#commentDescription').val()
        }
    }).done(function (result) {
        if (result.operationStatus == 0) {
            $('#description-' + $('#commentId').val()).html($('#commentDescription').val());
            $('#editCommentModal').modal('hide');
        }
    });
}