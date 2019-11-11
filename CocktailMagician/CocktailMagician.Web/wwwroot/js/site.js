let isFormLoadedForCocktailReview = true;
$('#load-show-comments-form-cocktail').click(function () {
    let div = $('#cocktailReview-div');
    if (isFormLoadedForCocktailReview) {
        div.show();
        isFormLoadedForCocktailReview = false;
    }
    else {
        div.hide();
        isFormLoadedForCocktailReview = true;
    }
});

let isFormLoadedForBarReview = true;
$('#load-show-comments-form-bar').click(function () {
    let div = $('#barReview-div');
    if (isFormLoadedForBarReview) {
        div.show();
        isFormLoadedForBarReview = false;
    }
    else {
        div.hide();
        isFormLoadedForBarReview = true;
    }
});

$('#load-add-ingredient-form').click(function () {
    let div = $('#ingrediens-div');
    div.show();
});


$('#ingrediens-to-db').click(function () {
    let name = $('#ingredient-name').val();
    let unit = $('#ingredient-unit').val();
    let data = {
        'Name': name,
        'Unit' : unit
    }

    $.ajax({
        type: "POST",
        url: "/Distribution/Ingredients/Add",
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
            let div = $('#ingrediens-div');
            div.hide();

        },
        error: function (msg) {
            console.dir(msg);
        }
    })
});

$('.load-add-city-form').click(function (event) {
    event.preventDefault();
    let cityDiv = $('#city-div');
    cityDiv.show();
});


$('#city-to-db').click(function () {
    let cityName = $('#city-name').val();
    let cityData = {
        'Name': cityName
    }

    $.ajax({
        type: "POST",
        url: "/Distribution/City/Add",
        data: JSON.stringify(cityData),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        dataType: 'json',
        success: function (response) {
            $('#list-of-cities').append(new Option(response.name, response.id))
            let cityDiv = $('#city-div');
            cityDiv.hide();
        },
        error: function (msg) {
            console.dir(msg);
        }
    })
});

$(document).ready(function () {
    $('.multiple-select2').select2();
});


