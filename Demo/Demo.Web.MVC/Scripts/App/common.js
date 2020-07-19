// set options for every ajax call
$.ajaxPrefilter(function (options) {
    // set cross domain proxy url
    if (options.crossDomain) {
        options.url = "http://mydomain.net/proxy/" + encodeURIComponent(options.url);
        options.crossDomain = false;
    }

    // set header for ajax request anti forgery token
    if (!options.beforeSend) {
        options.beforeSend = function (xhr) {
            xhr.setRequestHeader('__RequestVerificationToken', $("input:hidden[name=\"__RequestVerificationToken\"]").val());
        }
    }

    // set variable to send cookies with request or not
    options.xhrFields = {
        withCredentials: true
    };
});

$(document).ajaxStart(function () {
    // Whenever an Ajax request is about to be sent, jQuery checks whether there are any other outstanding Ajax requests.
    // If none are in progress, jQuery triggers the ajaxStart event.
    // set loader
    $("#loading").show();
});

$(document).ajaxStop(function () {
    // Whenever an Ajax request completes, jQuery checks whether there are any other outstanding Ajax requests. 
    // If none remain, jQuery triggers the ajaxStop event.
    // destroy loader
    $("#loading").hide();
});

$(document).ajaxError(function (event, request, settings) {
    // Whenever an Ajax request completes with an error, jQuery triggers the ajaxError event.
    // log error message
    $("#msg").append("<li>Error requesting page " + settings.url + "</li>");
});
