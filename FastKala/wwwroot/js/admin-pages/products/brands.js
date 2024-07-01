document.getElementById('addNewBrand').addEventListener('click', function (event) {
    event.preventDefault();
    submitForm();
});

function submitForm() {
    tinyMCE.triggerSave();
    var brandName = $('#Brand_Name').val();
    var brandLink = $('#Brand_Link').val();
    var brandDescription = $('#Brand_Description').val();
    if (document.getElementById('addNewBrand').value == 'افزودن برند') {
        $.ajax({
            url: '/Admin/Products/CreateBrand',
            method: 'POST',
            data: { name: brandName, link: brandLink, description: brandDescription },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#Brand_Name').val('');
                $('#Brand_Link').val('');
                tinyMCE.activeEditor.setContent('');
            }
        });
    }
    else if (document.getElementById('addNewBrand').value == 'ویرایش برند') {
        $.ajax({
            url: '/Admin/Products/UpdateBrand',
            method: 'POST',
            data: {
                id: $('#brand-id').val(), name: brandName, link: brandLink, description: brandDescription
            },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#Brand_Name').val('');
                $('#Brand_Link').val('');
                tinyMCE.activeEditor.setContent('');
                $('#brand-id').val('');
                $('#addNewBrand').val('افزودن برند');
                $('#cancellEditBrand').addClass('d-none');
            }
        });
    }
}

function changeEditBrand(id, itemId) {
    if (id == 'editBrandButton') {
        $.ajax({
            url: '/Admin/Products/GetBrand',
            method: 'POST',
            data: { brandId: itemId },
            success: function (data) {
                $('#Brand_Name').val(data.brand.name);
                $('#Brand_Link').val(data.brand.link);
                tinymce.activeEditor.setContent(data.brand.description);
            }
        });
        $('#brand-id').val(itemId);
        $('#addNewBrand').val('ویرایش برند');
        $('#cancellEditBrand').removeClass('d-none');
    }
    else if (id == 'cancellEditBrand') {
        $('#Brand_Name').val('');
        $('#Brand_Link').val('');
        tinyMCE.activeEditor.setContent('');
        $('#addNewBrand').val('افزودن برند');
        $('#cancellEditBrand').addClass('d-none');
    }
}