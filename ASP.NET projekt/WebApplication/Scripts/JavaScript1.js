// JavaScript Document

$(document).ready(function () {
    var maxLimit = 150;

    $('#countDisplay').html(maxLimit + '<br />' + " tecken kvar");

    $('#txtAboutMe').keyup(function ()
    {
        var textLength = $('#txtAboutMe').val().length;
        var textRemaining = maxLimit - textLength;

        $('#countDisplay').html(textRemaining + '<br />' + " tecken kvar");

        if (textRemaining <= 50)
        {
            $('#countDisplay').css({
                color: 'red',
                "box-shadow": "1px 1px 2px 2px",
                width: "65px",
                height: "65px",
                display: "block"
            });
        }

        else
        {
            $('#countDisplay').css({
                color: 'black',
                "box-shadow": "1px 1px 2px 2px",
                width: "65px",
                height: "65px",
                display: "block"
            });
        }
    });
});