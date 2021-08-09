// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#formPlayer").submit(function (event) {
        var PlayerInput = $("input#name").val();

        $(".name").text(PlayerInput);
        $(".name").show();

        event.preventDefault();
    });
});