document.getElementById('addNewTag').addEventListener('click', function (event) {
    event.preventDefault();
    submitForm();
});

function submitForm() {
    tinyMCE.triggerSave();
    var tagName = $('#ProductTag_Name').val();
    var tagLink = $('#ProductTag_Link').val();
    var tagDescription = $('#ProductTag_Description').val();
    if (document.getElementById('addNewTag').value == 'افزودن برچسب') {
        $.ajax({
            url: '/Admin/Products/CreateTag',
            method: 'POST',
            data: { name: tagName, link: tagLink, description: tagDescription },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#ProductTag_Name').val('');
                $('#ProductTag_Link').val('');
                tinyMCE.activeEditor.setContent('');
            }
        });
    }
    else if (document.getElementById('addNewTag').value == 'ویرایش برچسب') {
        $.ajax({
            url: '/Admin/Products/UpdateTag',
            method: 'POST',
            data: {
                id: $('#tag-id').val(), name: tagName, link: tagLink, description: tagDescription
            },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#ProductTag_Name').val('');
                $('#ProductTag_Link').val('');
                tinyMCE.activeEditor.setContent('');
                $('#tag-id').val('');
                $('#addNewTag').val('افزودن برچسب');
                $('#cancellEditTag').addClass('d-none');
            }
        });
    }
}

function changeEditTag(id, itemId) {
    if (id == 'editTagButton') {
        $.ajax({
            url: '/Admin/Products/GetTag',
            method: 'POST',
            data: { tagId: itemId },
            success: function (data) {
                $('#ProductTag_Name').val(data.productTag.name);
                $('#ProductTag_Link').val(data.productTag.link);
                tinymce.activeEditor.setContent(data.productTag.description);
            }
        });
        $('#tag-id').val(itemId);
        $('#addNewTag').val('ویرایش برچسب');
        $('#cancellEditTag').removeClass('d-none');
    }
    else if (id == 'cancellEditTag') {
        $('#ProductTag_Name').val('');
        $('#ProductTag_Link').val('');
        tinyMCE.activeEditor.setContent('');
        $('#addNewTag').val('افزودن برچسب');
        $('#cancellEditTag').addClass('d-none');
    }
}