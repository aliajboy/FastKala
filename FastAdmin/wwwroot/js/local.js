// Add Product Feature
function updateFeaturesInputNames() {
    // Select all the input fields within the container
    var inputFields = document.querySelectorAll('.product-feature');

    // Loop through each pair of input fields to update their name attributes
    for (var i = 0; i < inputFields.length; i += 2) {
        inputFields[i].name = 'Product.ProductFeatures[' + (i / 2) + '].TitleName'; // Update the name attribute for TitleName
        inputFields[i + 1].name = 'Product.ProductFeatures[' + (i / 2) + '].Value'; // Update the name attribute for Value
    }
}

document.addEventListener('click', function (event) {
    if (event.target && event.target.id == 'removeFeature') {
        // Handle the click event for the remove button
        var removeButton = event.target;
        var parentRow = removeButton.parentElement; // Assuming the remove button is within a row element
        parentRow.remove(); // Remove the corresponding row

        // After removing the row, update the input names
        updateFeaturesInputNames();
    }

    if (event.target && event.target.id === 'addFeatureButton') {
        // Handle the click event for the add button (assuming addFeatureButton is the ID of the add button)
        // Code to add new input fields
        var container = document.getElementById('features'); // Replace with the actual ID of the container element
        var newInputHtml = '<input type="text" class="form-control product-feature" name="Product.ProductFeatures[0].TitleName" placeholder="عنوان"><input type="text" class="form-control product-feature" name="Product.ProductFeatures[0].Value" placeholder="مقدار"><button id="removeFeature" class="btn btn-danger">حذف ویژگی</button>';
        var newRow = document.createElement('div');
        newRow.className = 'input-group mb-2';
        newRow.innerHTML = newInputHtml;
        container.appendChild(newRow);

        // After adding the new input fields, update the input names
        updateFeaturesInputNames();
    }
});



// Get the select element
var selectElement = document.getElementById('select-attribute'); // Replace 'yourSelectElementId' with the actual id of your select element

// Add onchange event listener
selectElement.onchange = function () {
    // Disable the select element
    $("#select-attribute option:selected").attr('disabled', 'disabled');

    // Get the value of the selected option
    var selectedValue = selectElement.value;

    // Create the div element with the alert and select elements
    var divElement = document.createElement('div');
    divElement.className = 'alert alert-secondary alert-dismissible fade show py-2';
    divElement.role = 'alert';
    divElement.id = 'attribute-' + selectedValue;

    var attrName = document.createElement('span');
    attrName.innerHTML = $("#select-attribute option:selected").text();

    var selectElementInsideDiv = document.createElement('select');
    selectElementInsideDiv.className = 'form-select';
    selectElementInsideDiv.setAttribute('aria-label', 'انتخاب ویژگی');
    selectElementInsideDiv.setAttribute('multiple', 'multiple');
    selectElementInsideDiv.id = 'select-attribute-' + selectedValue;

    var closeButton = document.createElement('button');
    closeButton.type = 'button';
    closeButton.className = 'btn-close';
    closeButton.setAttribute('data-bs-dismiss', 'alert');
    closeButton.setAttribute('aria-label', 'Close');

    divElement.appendChild(attrName);
    divElement.appendChild(selectElementInsideDiv);
    divElement.appendChild(closeButton);

    // Add the div element to the document
    document.getElementById('v-pills-features').appendChild(divElement);

    $('#select-attribute-' + selectedValue).select2({
        ajax: {
            url: "/Products/GetAttributeValues",
            dataType: 'json',
            data: function (params) {
                return {
                    search: params.term,
                    attributeId: selectedValue
                };
            },
            processResults: function (data, params) {

                params.page = params.page || 1;

                return {
                    results: data.results,
                    pagination: {
                        more: (params.page * 30) < data.total_count
                    }
                };
            },
            cache: true
        },
        placeholder: 'جستجو ' + $("#select-attribute option:selected").text(),
        minimumInputLength: 1,
        templateResult: formatRepo,
        templateSelection: formatRepoSelection
    });

    function formatRepo(repo) {
        if (repo.loading) {
            return repo.text;
        }

        var divElement = document.createElement('div');
        divElement.className = 'select2-result-attribute-value';
        divElement.innerHTML = repo.name;

        return divElement;
    }

    function formatRepoSelection(repo) {
        return repo.name || repo.text;
    }
}