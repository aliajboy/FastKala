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


