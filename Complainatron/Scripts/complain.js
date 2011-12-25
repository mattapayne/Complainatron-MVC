$(window).load(function () {

    var map;
    var initialLocation;

    $("#submit").click(function () {
        if (map && initialLocation) {
            $("#latitude").val(initialLocation.lat());
            $("#longitude").val(initialLocation.lng());
            $("#complain-form").submit();
        }
    });

    var initialize = function () {

        var siberia = new google.maps.LatLng(60, 105);
        var newyork = new google.maps.LatLng(40.69847032728747, -73.9514422416687);
        var browserSupportFlag = new Boolean();

        var myOptions = {
            zoom: 6,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("map"), myOptions);

        // Try W3C Geolocation (Preferred)
        if (navigator.geolocation) {
            browserSupportFlag = true;
            navigator.geolocation.getCurrentPosition(function (position) {
                initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                map.setCenter(initialLocation);
                setPin(initialLocation);
            }, function () {
                handleNoGeolocation(browserSupportFlag);
            });
            // Try Google Gears Geolocation
        } else if (google.gears) {
            browserSupportFlag = true;
            var geo = google.gears.factory.create('beta.geolocation');
            geo.getCurrentPosition(function (position) {
                initialLocation = new google.maps.LatLng(position.latitude, position.longitude);
                map.setCenter(initialLocation);
                setPin(initialLocation);
            }, function () {
                handleNoGeoLocation(browserSupportFlag);
            });
            // Browser doesn't support Geolocation
        } else {
            browserSupportFlag = false;
            handleNoGeolocation(browserSupportFlag);
        }

        function handleNoGeolocation(errorFlag) {
            if (errorFlag == true) {
                initialLocation = newyork;
            } else {
                initialLocation = siberia;
            }
            map.setCenter(initialLocation);
        }

        function setPin(initialLocation) {
            var marker = new google.maps.Marker({
                position: initialLocation,
                map: map
            });
        }
    }

    initialize();
});