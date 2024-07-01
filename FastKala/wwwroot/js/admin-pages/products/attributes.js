document.getElementById('addNewAttribute').addEventListener('click', function (event) {
    event.preventDefault();
    submitForm();
});

function submitForm() {
    if (document.getElementById('addNewAttribute').value == 'افزودن ویژگی') {
        var attributeName = $('#attribute-name').val();
        var attributeLink = $('#attribute-link').val();
        var attributeType = $('#attribute-type').val();

        $.ajax({
            url: '/Admin/Products/AddNewAttribute',
            method: 'POST',
            data: { attributeName: attributeName, attributeLink: attributeLink, attributeType: attributeType },
            success: function (partial) {
                $('#table-data').html(partial);
            }
        });
    }
    else if (document.getElementById('addNewAttribute').value == 'ویرایش ویژگی') {
        var attributeName = $('#attribute-name').val();
        var attributeLink = $('#attribute-link').val();
        var attributeType = $('#attribute-type').val();
        var attributeId = $('#attributeId').val();

        $.ajax({
            url: '/Admin/Products/UpdateAttribute',
            method: 'POST',
            data: { id: attributeId, name: attributeName, link: attributeLink, type: attributeType },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#attribute-name').val('');
                $('#attribute-link').val('');
                $('#attribute-type').val('1');
                $('#attributeId').val('');
                $('#addNewAttribute').val('افزودن ویژگی');
                $('#cancellEditAttribute').addClass('d-none');
            }
        });
    }
}

function changeEditAttribute(id, itemId) {
    if (id == 'editAttributeButton') {
        $.ajax({
            url: '/Admin/Products/GetAttribute',
            method: 'POST',
            data: { id: itemId },
            success: function (data) {
                $('#attribute-name').val(data.attribute.name);
                $('#attribute-link').val(data.attribute.link);
                $('#attribute-type').val(data.attribute.type);
            }
        });
        $('#attributeId').val(itemId);
        $('#addNewAttribute').val('ویرایش ویژگی');
        $('#cancellEditAttribute').removeClass('d-none');
    }
    else if (id == 'cancellEditAttribute') {
        $('#attribute-name').val('');
        $('#attribute-link').val('');
        $('#attribute-type').val('1');
        $('#addNewAttribute').val('افزودن ویژگی');
        $('#cancellEditAttribute').addClass('d-none');
    }
}