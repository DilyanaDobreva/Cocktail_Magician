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
        'Unit': unit
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
            //let div = $('#ingrediens-div');
            //div.hide();

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

function ShowModal(id) {
    $(`.${id}`).modal('show');
}
function DeleteValue(id) {
    $(`.${id}`).modal('hide');
    let trId = id + '+testId';
    let button = document.getElementById(trId);
    //button.remove();

    $.ajax(
        {
            type: 'Post',
            url: 'Ingredients/Delete',
            data: {
                'id': id
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                //'Accept': 'application/json',
                //'Content-Type': 'application/json'
            },
            success: function () {
                button.remove();
            }
        })
}


$(document).ready(function () {
    $('.multiple-select2').select2();
    $('#show-reviews').click(function () {
        $('#bars-content').hide();
        $('#review-content').show();
        $('#show-bars').css("background-color", "#da1b5e").css("color", "white").css("cursor", "pointer");
        $('#show-reviews').css("background-color", "white").css("color", "gray").css("cursor", "default");
    });
    $('#show-bars').click(function () {
        $('#review-content').hide();
        $('#bars-content').show();
        $('#show-reviews').css("background-color", "#da1b5e").css("color", "white").css("cursor", "pointer");
        $('#show-bars').css("background-color", "white").css("color", "gray").css("cursor", "default");
    });

    // Actual JS:

    const navbarContainer = $('nav .container');

    window.addEventListener('scroll', function () {
        const logoHeight = $('#logo').height();
        const topOffset = window.scrollY;

        if (topOffset > logoHeight) {
            navbarContainer.css({
                position: 'fixed',
                top: 0,
                zIndex: 99
            });
        } else {
            navbarContainer.css({
                position: 'relative'
            });
        }
    });
});

$('#logOut-button').click(function () {
    $.ajax(
        {
            type: 'Post',
            url: 'Users/Auth/Logout',
            //headers: {
            //    RequestVerificationToken:
            //        $('input:hidden[name="__RequestVerificationToken"]').val(),
        })
})

$('#bar-review').click(function () {
    //const userName = $('#userName').val();
    let rating = document.querySelector('input[name="rating"]:checked').value;
    let comment = $('#comment').val();
    let barId = $('#barId').val();

    let viewModel = {
        "Comment": comment,
        "Id": barId,
        "Rating": rating
    }
    console.log(viewModel)

    $.ajax({
        type: "post",
        url: "/Distribution/Bars/BarReview",
        data: JSON.stringify(viewModel),
        contentType: "application/json",
        cache: false,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function () {
            //$('#add-new-bar-review').prepend(responseData)
            $('#no-review').hide();
        }
        //error: function () {
        //    alert('No Valid Data');
    })
})

$('#cocktail-review').click(function () {
    //const userName = $('#userName').val();
    let rating = document.querySelector('input[name="rating"]:checked').value;
    let comment = $('#comment').val();
    let barId = $('#cocktailId').val();

    let viewModel = {
        "Comment": comment,
        "Id": barId,
        "Rating": rating
    }

    $.ajax({
        type: "post",
        url: "/Distribution/Cocktails/CocktailReview",
        data: JSON.stringify(viewModel),
        contentType: "application/json",
        cache: false,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (responseData) {
            $('#review-content').prepend(responseData)
            $('#no-review').hide()
        }
        //error: function () {
        //    alert('No Valid Data');
    })
})

$('#load-bar-reviews').click(function () {
    let barId = $('#barId').val();
    $.ajax({
        type: "get",
        url: "/Distribution/Bars/ShowReviews/" + barId,
        success: function (receivedData) {
            //$('.add-new-bar-review').empty();
            //$('#add-new-bar-review').hide();
            $('#add-new-bar-review').append(receivedData);
        }
    })
})

$('#close-bar-reviews').click(function () {
    $('.add-new-bar-review').empty();
})

$('#log-out-button').click(function () {
    $.ajax(
        {
            type: 'Post',
            url: '/Users/Auth/Logout',
            data: JSON.stringify(),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                //'Accept': 'application/json',
                //'Content-Type': 'application/json'
            },
        })
})