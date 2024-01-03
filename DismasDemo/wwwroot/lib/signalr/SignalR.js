
$.connection.hub.start()
    .done(function () { console.log("it worked") })
    .fail(alert("Error"));