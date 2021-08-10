// $(document).ready(function () {
//     console.log("pageload");
//     $(".switch").click(function () {
//         $("#first").toggle();
//         $("#game").toggle();
//     });
// });

let dealerIsRunning = false;

function hitDealer() {
    fetch('http://localhost:5000/dealerhit', {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'same-origin', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        }
    }).then(results => {
        showHand();
    });

}


function startDealer() {
    // create interval which calls dealer hit
    setInterval(function () {
        hitDealer();
    }, 1000);
}

function showHand() {
    fetch('http://localhost:5000/getactivehand', {
        method: 'GET', // *GET, POST, PUT, DELETE, etc.
        mode: 'same-origin', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        }
    }).then(results => {
        results.json().then(data => {
            if (data.index === -1) {
                if (dealerIsRunning === false) {
                    startDealer();
                    dealerIsRunning = true;
                }
                $("#dealer" + " " + "div").empty();
                data.hand.forEach(function (h) {
                    let address = 'http://localhost:5000/img/Cards/card' + h.suit + h.rank.charAt(0) + '.png'
                    console.log(address);
                    let img = document.createElement('img');
                    img.src = address;
                    img.style.width = '100px';
                    $("#dealer" + " " + "div")[0].appendChild(img);
                });
            }
            else {
                $("#player" + (data.index + 1) + " " + "div").empty();
                data.hand.forEach(function (h) {
                    let address = 'http://localhost:5000/img/Cards/card' + h.suit + h.rank.charAt(0) + '.png'
                    console.log(address);
                    let img = document.createElement('img');
                    img.src = address;
                    img.style.width = '100px';
                    $("#player" + (data.index + 1) + " " + "div")[0].appendChild(img);
                });
            }
        });
    });
}

function stand() {
    fetch('http://localhost:5000/hold', {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'same-origin', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        }
    }).then(results => {
        showHand();
    });
}




function hitMe() {
    fetch('http://localhost:5000/hit', {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'same-origin', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        }
    }).then(results => {
        showHand();
    });

}

$(document).ready(function () {
    $('#StartButton').click(function (e) {
        e.preventDefault();
        fetch('http://localhost:5000/start', {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'same-origin', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        }).then(x => { showHand(); });
    })

    $('#HitButton').click(function (e) {
        e.preventDefault();
        hitMe();
    });

    $('#StandButton').click(function (e) {
        e.preventDefault();
        stand();
    });

    $(".formPlayer").submit(function (e) {
        e.preventDefault();
        var playerInput = e.target.childNodes[1].value;
        console.log(playerInput);
        // $(".name").text(playerInput);
        // $(".formPlayer").hide();


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






