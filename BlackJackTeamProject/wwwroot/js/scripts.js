$(document).ready(function () {
    console.log("pageload");
    $(".switch").click(function () {
        $("#first").toggle();
        $("#game").toggle();
    });
});