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

let prevBoldedLink;

function toggleDisplayLink(checkbox) {
    var checkboxValue = checkbox.value;
    var linkId = "link-" + checkboxValue;
    var link = document.getElementById(linkId);

    if (checkbox.checked) {
        link.classList.remove("d-none");
    } else {
        link.classList.add("d-none");
    }
}

function updateMainCategory(categoryId) {
    document.getElementById("Product_MainCategoryId").value = categoryId;

    if (prevBoldedLink) {
        prevBoldedLink.style.fontWeight = 'normal';
    }
    var link = document.getElementById("link-" + categoryId);
    link.style.fontWeight = 'bold';
    link.style.fontSize = '18px';
    prevBoldedLink = link;
}

// Function to toggle the visibility of sale quantity inputs
function toggleSaleQuantityInputs(isVisible) {
    $("#min-sale-quantity, #sale-quantity-step").toggle(isVisible);
}

// Function to toggle the visibility of stock quantity input
function toggleStockQuantityInput(isVisible) {
    $("#stock-quantity-input").toggleClass("invisible", !isVisible);
}

// Event listener for the "manage-sale-quantity" checkbox
$("#manage-sale-quantity").on('change', function () {
    toggleSaleQuantityInputs(this.checked);
});

// Event listener for the "manage-stock-quantity" checkbox
$("#manage-stock-quantity").on('change', function () {
    toggleStockQuantityInput(this.checked);
});

$(function () {

    let productProsIndex = 1;
    let productConsIndex = 1;

    $('#addProductPros').on('click', function () {
        const newInputField = `<div class="input-group mb-2">
                                                                                                                                                                            <input class="form-control" name="ProductPros[${productProsIndex}]" type="text" />
                                                                                                                                                                            <button class="btn btn-primary remove-button" type="button">-</button>
                                                                                                                                                                        </div>`;
        $('#product-pros').append(newInputField);
        productProsIndex++;
    });

    $('#product-pros').on('click', '.remove-button', function () {
        $(this).parent().remove();
        // Adjust indexes of remaining inputs
        $('#product-pros .input-group').each(function (i) {
            $(this).find('input[type="text"]').attr('name', `ProductPros[${i}]`);
        });
        productProsIndex--; // Decrement index after removal
    });

    $('#addProductCons').on('click', function () {
        const newInputField = `<div class="input-group mb-2">
                                                                                                                                                                            <input class="form-control" name="ProductCons[${productConsIndex}]" type="text" />
                                                                                                                                                                            <button class="btn btn-danger remove-button" type="button">-</button>
                                                                                                                                                                        </div>`;
        $('#product-cons').append(newInputField);
        productConsIndex++;
    });

    $('#product-cons').on('click', '.remove-button', function () {
        $(this).parent().remove();
        // Adjust indexes of remaining inputs
        $('#product-cons .input-group').each(function (i) {
            $(this).find('input[type="text"]').attr('name', `ProductCons[${i}]`);
        });
        productConsIndex--; // Decrement index after removal
    });
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

    // Add a click event listener to the close button
    closeButton.onclick = function () {
        // Enable the selected option
        $("#select-attribute option[value='" + selectedValue + "']").prop('disabled', false);
    };

    divElement.appendChild(attrName);
    divElement.appendChild(selectElementInsideDiv);
    divElement.appendChild(closeButton);

    // Add the div element to the document
    document.getElementById('v-pills-features').appendChild(divElement);

    $('#select-attribute-' + selectedValue).select2({
        ajax: {
            url: "/Admin/Products/GetAttributeValues",
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
        templateResult: formatRepo,
        templateSelection: formatRepoSelection,
        language: {
            inputTooShort: function () {
                return 'حداقل یک کاراکتر وارد نمایید';
            },
            noResults: function () {
                return "نتیجه ای یافت نشد";
            }
        }
    });

    selectElement.selectedIndex = 0;

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


$('#tag-select').select2({
    ajax: {
        url: "/Admin/Products/GetTags",
        method: 'Get',
        processResults: function (data, params) {

            params.page = params.page || 1;

            return {
                results: data.productTags,
                pagination: {
                    more: (params.page * 30) < data.total_count
                }
            };
        },
        cache: true
    },
    placeholder: 'جستجو برچسب ها...',
    templateResult: function (repo) {
        if (repo.loading) {
            return repo.name;
        }

        var divElement = document.createElement('div');
        divElement.className = 'select2-result-tag';
        divElement.innerHTML = repo.name;

        return divElement;
    },
    templateSelection: function (repo) {
        return repo.name || repo.text;
    },
    language: {
        inputTooShort: function () {
            return 'حداقل یک کاراکتر وارد نمایید';
        },
        noResults: function () {
            return "نتیجه ای یافت نشد";
        }
    }
});

$('#brand-select').select2({
    ajax: {
        url: "/Admin/Products/GetBrands",
        method: 'Get',
        processResults: function (data, params) {

            params.page = params.page || 1;

            return {
                results: data.productTags,
                pagination: {
                    more: (params.page * 30) < data.total_count
                }
            };
        },
        cache: true
    },
    placeholder: 'جستجو برند ها...',
    templateResult: function (repo) {
        if (repo.loading) {
            return repo.name;
        }

        var divElement = document.createElement('div');
        divElement.className = 'select2-result-tag';
        divElement.innerHTML = repo.name;

        return divElement;
    },
    templateSelection: function (repo) {
        return repo.name || repo.text;
    },
    language: {
        inputTooShort: function () {
            return 'حداقل یک کاراکتر وارد نمایید';
        },
        noResults: function () {
            return "نتیجه ای یافت نشد";
        }
    }
});

$("#addproductform").on('submit', function (eventObj) {
    const attributeSelects = $('.select2-hidden-accessible').map(function () { return this.id; });
    const checkedCategories = $("input[name='categories']:checked");

    let index = 0;

    for (var i = 0; i < checkedCategories.length; i++) {
        let checked = checkedCategories[i].value;
        $("<input />").attr("type", "hidden")
            .attr("name", "Product.CategoriesRelations[" + i + "].CategoryId")
            .attr("value", checked)
            .appendTo("#addproductform");
    }
    for (var i = 0; i < attributeSelects.length; i++) {
        let a = attributeSelects[i].split('-')[0];
        switch (a) {
            case 'brand':
                a = "b";
                break;
            case 'tag':
                let tags = $('#' + attributeSelects[i]).select2('data');
                for (let x = 0; x < tags.length; x++) {
                    $("<input />").attr("type", "hidden")
                        .attr("name", "Product.TagsRelations[" + x + "].TagId")
                        .attr("value", tags[x].id)
                        .appendTo("#addproductform");
                }
                break;
            case 'select':
                let items = $('#' + attributeSelects[i]).select2('data');
                for (let x = 0; x < items.length; x++) {
                    $("<input />").attr("type", "hidden")
                        .attr("name", "Product.AttributesRelations[" + index + "].AttributeValueId")
                        .attr("value", items[x].id)
                        .appendTo("#addproductform");
                    index++;
                }
                break;
        }
    }
    return true;
});

function showImage(input, targetId) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();
        reader.onload = function (e) {
            $('#' + targetId).attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function showImageGallery(input) {
    if (input.files.length > 0) {
        for (var i = 0; i < input.files.length; i++) {
            var newImageColumn = document.createElement('div');
            newImageColumn.className = 'col-4';
            document.getElementById('gallery-images').appendChild(newImageColumn);

            var newImage = document.createElement('div');
            newImage.className = 'border rounded-3 col-12';
            newImage.style.aspectRatio = '1 / 1';
            newImageColumn.appendChild(newImage);

            var innerImage = document.createElement('img');
            innerImage.className = 'w-100';
            innerImage.src = URL.createObjectURL(input.files[i]);

            newImage.appendChild(innerImage);
        }
    }
}