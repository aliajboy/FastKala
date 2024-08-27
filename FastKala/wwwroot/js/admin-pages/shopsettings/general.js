let stateId = $('#ShopSettings_TownId').children("option:selected").val();
let cityId = $('#ShopSettings_CityId').children("option:selected").val();

async function loadCities() {
    const res = await $.ajax({
        url: '/Order/GetCities',
        method: 'POST',
        data: {
            id: $(this).val()
        }
    });
    if (res != null || res != undefined) {
        const selectElement = $('#ShopSettings_CityId');
        selectElement.html('');
        res.forEach(item => {
            const option = $('<option></option>').val(item.cityId).text(item.city);
            selectElement.append(option);
        });
        stateId = $('#ShopSettings_TownId').children("option:selected").val();
        cityId = $('#ShopSettings_CityId').children("option:selected").val();
    }
    else {
        Swal.fire({
            title: 'خطا در دریافت لیست شهر ها',
            confirmButtonText: 'باشه',
            icon: 'error'
        });
    }
}

$('#ShopSettings_TownId').on('change', loadCities);