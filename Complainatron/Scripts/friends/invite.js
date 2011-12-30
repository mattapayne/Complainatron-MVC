$(function () {

    var friendsJoinRequestCallback = function (response) {
        console.log(response);
    };

    $("#invite-friends").livequery("click", (function (e) {
        e.preventDefault();
        FB.ui({
            method: 'apprequests',
            message: 'Join Complainatron'
        }, friendsJoinRequestCallback);
    }));
});