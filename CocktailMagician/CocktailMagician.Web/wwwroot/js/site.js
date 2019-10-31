const serverResponseHandler = (serverData) => {
    if (serverData) {
        $('#add-ingredients-form').html(serverData);
    }
};


$('#load-add-ingredient-form').click(function () {
    $.get('/distribution/ingredient/add', serverResponseHandler)
});

$('#add-ingredient').on('submit', function (event) {
    event.preventDefault();

    const data = $(this).serialize();
    const url = $(this).attr('action');
    debugger
    $.post(url, data)
        .done(function () {
            console.log('success')
        })
});