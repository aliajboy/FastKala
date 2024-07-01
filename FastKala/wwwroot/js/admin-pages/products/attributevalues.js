document.getElementById('addNewAttributeValue').addEventListener('click', function (event) {
    event.preventDefault();
    submitForm();
});

function submitForm() {
    if (document.getElementById('addNewAttributeValue').value == 'افزودن') {
        var attributeName = $('#AttributeValue_Name').val();
        var attributeValue = $('#AttributeValue_value').val();
        var attributeId = $('#Attribute_Id').val();

        $.ajax({
            url: '/Admin/Products/AddAttributeValue',
            method: 'POST',
            data: { ValueName: attributeName, ValueLink: attributeValue, id: attributeId },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#AttributeValue_Name').val('');
                $('#AttributeValue_value').val('');
            }
        });
    }
    else if (document.getElementById('addNewAttributeValue').value == 'ویرایش') {
        var attributeName = $('#AttributeValue_Name').val();
        var attributeValue = $('#AttributeValue_value').val();
        var attributeId = $('#Attribute_Id').val();
        var attributeValueId = $('#attributeValueId').val();

        $.ajax({
            url: '/Admin/Products/UpdateAttributeValue',
            method: 'POST',
            data: { name: attributeName, value: attributeValue, id: attributeValueId, attributeId: attributeId },
            success: function (partial) {
                $('#table-data').html(partial);
                $('#addNewAttributeValue').val('افزودن');
                $('#cancellEditAttributeValue').addClass('d-none');
                $('#AttributeValue_Name').val('');
                $('#AttributeValue_value').val('');
            }
        });
    }
}

function changeEditAttributeValue(id, itemId) {
    if (id == 'editAttributeValueButton') {
        $.ajax({
            url: '/Admin/Products/GetAttributeValue?attributeId=' + itemId,
            method: 'GET',
            success: function (data) {
                $('#AttributeValue_Name').val(data.attributeValue.name);
                $('#AttributeValue_value').val(data.attributeValue.value);
            }
        });
        $('#addNewAttributeValue').val('ویرایش');
        $('#cancellEditAttributeValue').removeClass('d-none');
        $('#attributeValueId').val(itemId);
    }
    else if (id == 'cancellEditAttributeValue') {
        $('#AttributeValue_Name').val('');
        $('#AttributeValue_value').val('');
        $('#addNewAttributeValue').val('افزودن');
        $('#cancellEditAttributeValue').addClass('d-none');
    }
}

function removeAttributeValue(valueId) {
    $.ajax({
        url: '/Admin/Products/RemoveAttributeValue',
        method: 'POST',
        data: {
            attributeValueId: valueId, attributeId: $('#Attribute_Id').val()
        },
        success: function (partial) {
            $('#table-data').html(partial);
        }
    });
}