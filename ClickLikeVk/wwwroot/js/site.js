// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function click(idPage) {
    console.log("Click working");
    $.ajax({
        url: '/Home/SaveClick',
        method: 'post',
        dataType: 'json',
        data: { idPage: idPage },
        success: function (data) {
            alert(JSON.stringify(data));
        },
        error: function (err) {
            alert(JSON.stringify(err));
        }

    });
}