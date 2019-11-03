﻿//const serverResponseHandler = (serverData) => {
//    if (serverData) {
//        $('#add-ingredients-form').html(serverData);
//    }
//};


//$('#load-add-ingredient-form').click(function () {
//    $.get('/distribution/ingredient/add', serverResponseHandler)
//});

//$('#add-ingredient').on('submit', function (event) {
//    event.preventDefault();

//    const data = $(this).serialize();
//    const url = $(this).attr('action');

//    $.post(url, data)
//        .done(function () {
//            console.log('success')
//        })
//});
//$('#list-of-ingredients').multiselect({
       
//    });


$('#load-add-ingredient-form').click(function () {
    let div = $('#ingrediens-div');
    div.show();
});


$('#ingrediens-to-db').click(function () {
    let name = $('#ingredient-name').val();
    let unit = $('#ingredient-unit').val();
    let data = {
        'Name': name,
        'Unit': unit
    }

    $.ajax({
        type: "POST",
        url: "/Distribution/Ingredient/Add",
        data: JSON.stringify(data),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        dataType: 'json',
        success: function (response) {
            $('#list-of-ingredients').append(new Option(response.name, response.id))
        },
        error: function (msg) {
            console.dir(msg);
        }
    })
});

$('#list-of-ingredients').multiselect();