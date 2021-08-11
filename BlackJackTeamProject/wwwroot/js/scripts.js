// $(document).ready(function () {
//     console.log("pageload");
//     $(".switch").click(function () {
//         $("#first").toggle();
//         $("#game").toggle();
//     });
// });

let dealerIsRunning = false;
var clear;

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
        showAllHands();
    });
}

function startDealer() {

    if (clear === undefined) {
        clear = setInterval(function () {
            hitDealer();
        }, 1000);
    }

    // create interval which calls dealer hit
}


function showAllHands() {
    fetch('http://localhost:5000/getallhands', {
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
            console.log(data.gameState, "gammme stateafsdfdssdfa")
            if (data.gameState === "roundover") {
                console.log("clearrreredddadfsfads");
                clearInterval(clear);
            }
            console.log(clear, "clear");
            $(".player" + " " + "div").empty();
            $("#dealer" + " " + "div").empty();
            console.log(data, "data");
            dealer = data.hands.splice(-1);
            dealer[0].forEach(function (c) {
                let address = 'http://localhost:5000/img/Cards/card' + c.suit + c.rank.charAt(0) + '.png'
                let img = document.createElement('img');
                img.src = address;
                img.style.width = '100px';
                $("#dealer" + " " + "div")[0].appendChild(img);
            });
            let index = 0;
            data.hands.forEach(function (h) {
                index++;
                h.forEach(function (c) {
                    let address = 'http://localhost:5000/img/Cards/card' + c.suit + c.rank.charAt(0) + '.png'
                    let img = document.createElement('img');
                    img.src = address;
                    img.style.width = '100px';
                    $("#player" + (index) + " " + "div")[0].appendChild(img);
                });
            });
            if (data.gameState === "dealer" && dealerIsRunning === false) {
                dealerIsRunning = true;
                startDealer();
            }

        }

        );
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
    }).then(x => { showAllHands(); });
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
        showAllHands();
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
        }).then(x => { showAllHands(); });
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






