function loadParentCategories() {
    $('#Category_ParentId').html('');
    var opt = document.createElement('option');
    opt.value = 0;
    opt.innerHTML = 'دسته بندی اصلی';
    $('#Category_ParentId').append(opt);
    $.ajax({
        url: '/Admin/Products/GetCategories',
        method: 'Get',
        success: function (data) {
            for (var i = 0; i < data.categories.length; i++) {
                var opt = document.createElement('option');
                opt.value = data.categories[i].id;
                opt.innerHTML = data.categories[i].name;
                $('#Category_ParentId').append(opt);
            }
        }
    });
}

document.getElementById('addNewCategory').addEventListener('click', function (event) {
    event.preventDefault();
    submitForm();
});

function submitForm() {
    tinyMCE.triggerSave();
    var categoryName = $('#Category_Name').val();
    var categoryLink = $('#Category_Link').val();
    var categoryDescription = $('#Category_Description').val();
    var categoryParentId = $('#Category_ParentId').val();
    if (document.getElementById('addNewCategory').value == 'افزودن دسته بندی') {
        $.ajax({
            url: '/Admin/Products/CreateCategory',
            method: 'POST',
            data: { name: categoryName, link: categoryLink, description: categoryDescription, parentId: categoryParentId },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#Category_Name').val('');
                $('#Category_Link').val('');
                $('#Category_ParentId').val(0);
                tinyMCE.activeEditor.setContent('');
            }
        });
        loadParentCategories();
    }
    else if (document.getElementById('addNewCategory').value == 'ویرایش دسته بندی') {
        $.ajax({
            url: '/Admin/Products/UpdateCategory',
            method: 'POST',
            data: {
                id: $('#category-id').val(), name: categoryName, link: categoryLink, description: categoryDescription, parentId: categoryParentId
            },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#Category_Name').val('');
                $('#Category_Link').val('');
                tinyMCE.activeEditor.setContent('');
                $('#Category_ParentId').val('');
                $('#category-id').val('');
                $('#addNewCategory').val('افزودن دسته بندی');
                $('#cancellEditCategory').addClass('d-none');
                loadParentCategories();
            }
        });
    }
}

function changeEditCategory(id, itemId) {
    if (id == 'editCategoryButton') {
        $.ajax({
            url: '/Admin/Products/GetCategory',
            method: 'POST',
            data: { categoryId: itemId },
            success: function (data) {
                $('#Category_Name').val(data.category.name);
                $('#Category_Link').val(data.category.link);
                tinymce.activeEditor.setContent(data.category.description);
                $('#Category_ParentId').val(data.category.parentId);
            }
        });
        $('#category-id').val(itemId);
        $('#addNewCategory').val('ویرایش دسته بندی');
        $('#cancellEditCategory').removeClass('d-none');
    }
    else if (id == 'cancellEditCategory') {
        $('#Category_Name').val('');
        $('#Category_Link').val('');
        tinyMCE.activeEditor.setContent('');
        $('#Category_ParentId').val(0);
        $('#addNewCategory').val('افزودن دسته بندی');
        $('#cancellEditCategory').addClass('d-none');
    }
}