// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#formPlayer").submit(function (event) {
        var PlayerInput = $("input#name").val();

        $(".name").text(PlayerInput);
        $(".name").show();

        event.preventDefault();
        fetch('http://localhost:5000/makeplayer/david', {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'same-origin', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        }).then(x=>{
            $("input#name").val() = "";
        })

        
    });
});