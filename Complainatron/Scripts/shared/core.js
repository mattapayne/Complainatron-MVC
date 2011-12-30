window.fbAsyncInit = function () {
    FB.Canvas.setAutoResize();
};

$(function () {

    var aboutLink = $("#AboutLink").val();
    var complainLink = $("#ComplainLink").val();
    var friendsLink = $("#FriendsLink").val();
    var signed_request = $("input#signed_request").val();
    var chartLink = $("#ChartLink").val() + "?signed_request=" + signed_request;
    var mapLink = $("#MapLink").val() + "?signed_request=" + signed_request;

    $("#map").click(function (e) {
        e.preventDefault();
        $.showLoading();

        var buttons = {};

        buttons["Close"] = function () {
            $.hideLoading();
            $(this).dialog("close");
        };

        var iframe = $("<iframe />");
        iframe.attr("src", mapLink).attr("height", 450).attr("width", 450).attr("scrolling", "no").attr("frameborder", 0);
        $.modalDialog("Mapped Complaints", iframe, 550, null, buttons, null);
    });

    $("#chart").click(function (e) {
        e.preventDefault();
        $.showLoading();
        var buttons = {};

        buttons["Close"] = function () {
            $.hideLoading();
            $(this).dialog("close");
        };

        var iframe = $("<iframe />");
        iframe.attr("src", chartLink).attr("height", 450).attr("width", 450).attr("scrolling", "no").attr("frameborder", 0);
        $.modalDialog("Graphed Complaints", iframe, 550, null, buttons, null);
    });

    $("#complain").click(function (e) {
        e.preventDefault();
        $.showLoading();
        $.get(complainLink, { 'signed_request': signed_request }, function (data) {

            var buttons = {};

            buttons["Close"] = function () {
                $.hideLoading();
                $(this).dialog("destroy");
            };

            buttons["Submit"] = function () {

                $.showLoading("Please wait ...");

                var formAction = $("#complain-form").attr("action");

                var dialog = $(this);

                $.post(formAction, $("#complain-form").serialize(), function (response) {
                    var html = $(response);
                    var error = html.find(".validation-summary-errors");
                    if (error && error.length > 0) {
                        dialog.dialog("destroy");
                        $.hideLoading();
                        $.modalDialog("Complain!!", html, 450, 330, buttons);
                    }
                    else {
                        $.hideLoading();
                        dialog.dialog("destroy");
                        $("#complaints").prepend(html);
                        FB.XFBML.parse();
                        //update tags
                        var tagUrl = $("#TagListUrl").val();
                        $("#tags").load(tagUrl, { 'signed_request': signed_request });
                    }
                });
            };

            $.modalDialog("Complain!!", data, 450, 330, buttons);
        });
    });

    $("#friends").click(function (e) {
        e.preventDefault();
        $.showLoading();
        $.get(friendsLink, { 'signed_request': signed_request }, function (data) {

            var buttons = {};

            buttons["Close"] = function () {
                $.hideLoading();
                $(this).dialog("close");
            };

            $.modalDialog("Friends Using Complainatron", data, null, null, buttons, null);
        });
    });

    $("#about").click(function (e) {
        e.preventDefault();
        $.showLoading();

        $.get(aboutLink, { 'signed_request': signed_request }, function (data) {

            var buttons = {};

            buttons["Close"] = function () {
                $.hideLoading();
                $(this).dialog("close");
            };

            $.modalDialog("About Complainatron", data, null, null, buttons, null);
        });
    });
});

$.extend({
    modalDialog: function (title, html, height, width, buttons) {

        var h = height != null ? height : 500;
        var w = width != null ? width : 500;
        var t = title != null ? title : "Dialog";

        $.hideLoading();

        $("<div></div>").html(html).dialog({
            title: t,
            height: h,
            width: h,
            buttons: buttons
        }).show();
    }
});

$.extend({
    showLoading: function (text) {
        text = text == null ? "Loading ..." : text;
        $(document.body).mask(text);
    }
});

$.extend({
    hideLoading: function () {
        $(document.body).unmask();
    }
});

