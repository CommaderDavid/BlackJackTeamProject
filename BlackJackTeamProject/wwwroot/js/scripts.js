// $(document).ready(function () {
//     console.log("pageload");
//     $(".switch").click(function () {
//         $("#first").toggle();
//         $("#game").toggle();
//     });
// });

$(document).ready(function () {
    $('#StartButton').click(function (e) {
        e.preventDefault();
        console.log("clicked");
        fetch('http://localhost:5000/makeplayer/david', {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'same-origin', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        })
            .then(function (response) {
                if (response.status >= 200 && response.status < 300) {
                    console.log("response 200")
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
                }
                else {
                    console.log("response not 200")
                }
            })
    });
});
