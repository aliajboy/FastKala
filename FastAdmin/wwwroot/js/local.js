var itemCount = 0;
const removedItems = [];
var prositemCount = 1;
var consitemCount = 1;
$('#cmdAdd1').click(function () { addNewFeature(); });
$('#cmdAddpros1').click(function () { addNewPros(); });
$('#cmdAddcons1').click(function () { addNewCons(); });

function lastId() {
    var idlist = [];
    $("#features").find("div").each(function () {
        var id = $(this).attr("id").substring(4, $(this).attr("id").length);
        idlist.push(id);
    });

    idlist.sort(function (a, b) {
        return a - b;
    });

    idlist.reverse();


    return idlist[0];

}

function addNewFeature() {
    // Add new line with text field and button
    if (removedItems.length == 0) {
        itemCount++;
        var newItem = ' <div id="item' + itemCount + '" class="input-group mb-2">';
        newItem += '<input id="title' + itemCount + '" name="title' + itemCount + '" placeholder="عنوان" class="form-control" type="text"/>';
        newItem += '<input id="value' + itemCount + '" name="value' + itemCount + '" placeholder="مقدار" class="form-control" type="text"/>';
        newItem += '<input id="cmdAdd' + itemCount + '" type="button" class="btn btn-danger" value="حذف ویژگی"></input>';
        newItem += "</div>";

        $('#features').append(newItem);
        var thisItem = itemCount;
        $('#cmdAdd' + itemCount).click(function () { removeItem(thisItem); });
    }
    else {
        var id = removedItems[0];
        var newItem = ' <div id="item' + id + '" class="input-group mb-2">';
        newItem += '<input id="title' + id + '" name="title' + id + '" placeholder="عنوان" class="form-control" type="text"/>';
        newItem += '<input id="value' + id + '" name="value' + id + '" placeholder="مقدار" class="form-control" type="text"/>';
        newItem += '<input id="cmdAdd' + id + '" type="button" class="btn btn-danger" value="حذف ویژگی"></input>';
        newItem += "</div>";

        $('#features').append(newItem);
        thisItem = id;
        $('#cmdAdd' + id).click(function () { removeItem(thisItem); });
        removedItems.splice(0, 1);
    }
}

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

// add new input on btn click

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

function removeItem(i) {
    $('#item' + i).remove();
    removedItems.push(i);
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