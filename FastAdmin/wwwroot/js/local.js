var itemCount = 0;
const removedItems = [];
var prositemCount = 1;
var consitemCount = 1;
$('#cmdAddpros1').click(function () { addNewPros(); });
$('#cmdAddcons1').click(function () { addNewCons(); });

function addNewPros() {
    var thisItem = prositemCount;
    // Change button to Remove button
    $('#cmdAddpros' + prositemCount).attr('value', '-');
    $('#cmdAddpros' + prositemCount).off();
    $('#cmdAddpros' + prositemCount).click(function () { removePros(thisItem); });

    // Add new line with text field and button
    prositemCount++;
    var newItem = ' <div id="prositem' + prositemCount + '" class="input-group mb-2">'
    newItem += '<input id="pros' + prositemCount + '" name="pros' + prositemCount + '" class="form-control" type="text"/>';
    newItem += '<input id="cmdAddpros' + prositemCount + '" type="button" class="btn btn-primary" value="+"></input>';
    newItem += "</div>";

    $('#product-pros').append(newItem);
    $('#cmdAddpros' + prositemCount).click(function () { addNewPros(); });
}
function addNewCons() {
    var thisItem = consitemCount;
    // Change button to Remove button
    $('#cmdAddcons' + consitemCount).attr('value', '-');
    $('#cmdAddcons' + consitemCount).off();
    $('#cmdAddcons' + consitemCount).click(function () { removeCons(thisItem); });

    // Add new line with text field and button
    consitemCount++;
    var newItem = ' <div id="consitem' + consitemCount + '" class="input-group mb-2">'
    newItem += '<input id="cons' + consitemCount + '" name="cons' + consitemCount + '" class="form-control" type="text"/>';
    newItem += '<input id="cmdAddcons' + consitemCount + '" type="button" class="btn btn-danger" value="+"></input>';
    newItem += "</div>";

    $('#product-cons').append(newItem);
    $('#cmdAddcons' + consitemCount).click(function () { addNewCons(); });
}
function removePros(i) {
    $('#prositem' + i).remove();
}
function removeCons(i) {
    $('#consitem' + i).remove();
}

// display Manage Product Quantity Inputs
$("#manage-sale-quantity").change(function () {
    if (this.checked) {
        $("#min-sale-quantity").removeClass("d-none");
        $("#sale-quantity-step").removeClass("d-none");
    }
    else {
        $("#min-sale-quantity").addClass("d-none");
        $("#sale-quantity-step").addClass("d-none");
    }
});

// display Manage Product Stock Inputs
$("#manage-stock-quantity").change(function () {
    if (this.checked) {
        $("#stock-quantity-input").removeClass("invisible");
    }
    else {
        $("#stock-quantity-input").addClass("invisible");
    }
});

// Add Product Feature
function updateInputNames() {
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
        updateInputNames();
    }

    if (event.target && event.target.id === 'cmdAdd1') {
        // Handle the click event for the add button (assuming cmdAdd1 is the ID of the add button)
        // Code to add new input fields
        var container = document.getElementById('features'); // Replace with the actual ID of the container element
        var newInputHtml = '<input type="text" class="form-control product-feature" name="Product.ProductFeatures[0].TitleName" placeholder="عنوان"><input type="text" class="form-control product-feature" name="Product.ProductFeatures[0].Value" placeholder="مقدار"><button id="removeFeature" class="btn btn-danger">حذف ویژگی</button>';
        var newRow = document.createElement('div');
        newRow.className = 'input-group mb-2';
        newRow.innerHTML = newInputHtml;
        container.appendChild(newRow);

        // After adding the new input fields, update the input names
        updateInputNames();
    }
});