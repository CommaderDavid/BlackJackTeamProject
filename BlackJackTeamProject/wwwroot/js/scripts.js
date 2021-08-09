// $(document).ready(function () {
//     console.log("pageload");
//     $(".switch").click(function () {
//         $("#first").toggle();
//         $("#game").toggle();
//     });
// });

$(document).ready(function () {
    $('#StartButton').click(function (e) {
        fetch('http://localhost:5000/start', {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'same-origin', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        })
    });

    $(".formPlayer").submit(function (e) {
        e.preventDefault();
        var playerInput = e.target.childNodes[1].value;
        console.log(playerInput);
        $(".name").text(playerInput);
        $(".name").show();

        e.preventDefault();
        fetch('http://localhost:5000/makeplayer/' + playerInput, {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'same-origin', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        })
    });
});
