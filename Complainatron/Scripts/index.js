$(function () {

    var newyork = new google.maps.LatLng(40.69847032728747, -73.9514422416687);
    var map;

    $(".like").click(function () {
        var idElem = $(this).parent("div").find("input[type=hidden]#id");
        $("#like-form").append(idElem).submit();
    });

    $(".dislike").click(function () {
        var idElem = $(this).parent("div").find("input[type=hidden]#id");
        $("#dislike-form").append(idElem).submit();
    });

    var loadComplaints = function () {

        var myOptions = {
            zoom: 1,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("map"), myOptions);
        var openWindow = null;

        $.getJSON("/complaint/complaints", null, function (response) {
            var count = 1;
            $.each(response, function () {
                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng((this.Latitude + count), (this.Longitude + count++)),
                    map: map,
                    title: this.Username
                });

                var infoWindow = new google.maps.InfoWindow({
                    content: "<div>" + this.ComplaintText + "</div>"
                });

                google.maps.event.addListener(marker, "click", function () {
                    if (openWindow) {
                        openWindow.close();
                    }
                    openWindow = infoWindow;
                    infoWindow.open(map, marker);
                });
            });
            var lastComplaint = response[response.length - 1];

            if (lastComplaint) {
                map.setCenter(new google.maps.LatLng(lastComplaint.Latitude, lastComplaint.Longitude));
            }
            else {
                map.setCenter(newyork);
            }
        });
    };

    loadComplaints();
});