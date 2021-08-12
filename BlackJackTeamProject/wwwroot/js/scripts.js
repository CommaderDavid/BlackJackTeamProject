// $(document).ready(function () {
//     console.log("pageload");
//     $(".switch").click(function () {
//         $("#first").toggle();
//         $("#game").toggle();
//     });
// });

let dealerIsRunning = false;
var clear;
let rndNumber = parseInt(Math.random() * 10000000);
console.log(rndNumber);

function hitDealer() {
    fetch('http://localhost:5000/dealerhit/' + rndNumber, {
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

function showActive(currentPlayer) {
    let highlightId = currentPlayer + 1;
    $(".player").css("border", "none");
    $("#player" + highlightId).css("border", "solid red 3px");
}

function showRounds(currentRound, totalRounds) {
    let sentence = "Round " + currentRound + " of " + totalRounds;
    $("#rounds").html(sentence);
}


function showAllHands() {
    fetch('http://localhost:5000/getallhands/' + rndNumber, {
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
            showRounds(data.currentRound, data.totalRounds);
            showActive(data.currentPlayer);
            if (data.gameState === "roundover") {
                clearInterval(clear);
                clear = undefined;
            }
            console.log(clear, "clear");
            $(".player" + " " + "div").empty();
            $("#dealer" + " " + "div").empty();
            console.log(data, "data");
            dealer = data.hands.splice(-1);
            dealer[0].forEach(function (c) {
                let char = c.rank.charAt(0);
                if (char === '1') {
                    char = 10;
                }
                let address = 'http://localhost:5000/img/Cards/card' + c.suit + char + '.png'
                let img = document.createElement('img');
                img.src = address;
                img.style.width = '100px';
                $("#dealer" + " " + "div")[0].appendChild(img);
            });
            let index = 0;
            data.hands.forEach(function (h) {
                index++;
                h.forEach(function (c) {
                    let char = c.rank.charAt(0);
                    if (char === '1') {
                        char = 10;
                    }
                    let address = 'http://localhost:5000/img/Cards/card' + c.suit + char + '.png'
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
    fetch('http://localhost:5000/hold/' + rndNumber, {
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
    fetch('http://localhost:5000/hit/' + rndNumber, {
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

function createPlayerDivs(num) {
    for (var i = 1; i <= num; i++) {
        let div = '<div class="player" id="player' + i + '"><div></div></div>'
        $("#game").append(div);
    }
}

function createPlayers(playerNumber) {
    //get value from form
    let rounds = $("#numberRounds").val();
    fetch('http://localhost:5000/makeplayer/' + playerNumber + "/" + rndNumber + "/" + rounds, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'same-origin', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        }
    }).then(x => {
        createPlayerDivs(playerNumber);
        startGame()
    })
}

function startGame() {
    fetch('http://localhost:5000/start/' + rndNumber, {
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


$(document).ready(function () {
    $("#game").toggle();

    $("#newroundButton").click(function (e) {
        e.preventDefault();
        console.log("adslkfjasd;fa");
        startGame();
    })

    $('#numberPlayers').submit(function (e) {
        e.preventDefault();
        let numberPlayers = $('#players').val();
        createPlayers(numberPlayers);

        $("#game").toggle();
    })


    $('#HitButton').click(function (e) {
        e.preventDefault();
        hitMe();
    });

    $('#StandButton').click(function (e) {
        e.preventDefault();
        stand();
    });

});






