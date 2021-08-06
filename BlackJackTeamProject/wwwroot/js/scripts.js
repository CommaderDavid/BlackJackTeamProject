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
        fetch('http://localhost:5000/makeplayer/david')
            .then(function (response) {
                if (response.status >= 200 && response.status < 300) {
                    console.log("response 200")
                    
                }
                else {
                    console.log("response not 200")
                    fetch('http://localhost:5000/start')
                }
            })
    });
});
