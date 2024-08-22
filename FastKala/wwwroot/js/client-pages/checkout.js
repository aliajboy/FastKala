function acceptTerms() {
    if ($('#AcceptTerms').is(":checked")) {
        $('.btn-Order').attr("disabled", false);
    }
    else {
        $('.btn-Order').attr("disabled", true);
    }
}