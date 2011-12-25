$(function () {
    $("#ComplaintText").livequery("keyup", function (e) {
        var maxchar = 255;
        var cnt = $(this).val().length;
        var remainingchar = maxchar - cnt;
        if (remainingchar < 0) {
            $('#LengthDisplay').html('Remaining characters: 0');
            $(this).val($(this).val().slice(0, 255));
        } else {
            $('#LengthDisplay').html('Remaining characters: ' + remainingchar);
        }
    });

    $(".existing-tag").livequery("click", function () {
        var value = $(this).text().trim();
        var currentTagsString = $("#Tags").val();

        if (currentTagsString) {
            //if currentTags was 'monkey, test, boy' - we should have an array of 3 items
            var tags = currentTagsString.split(", ");

            if (tags == null) {
                tags = [];
            }

            //create a jquery array
            tags = $.makeArray(tags);

            //ensure all whitespace is trimmed
            for (var i = 0; i < tags.length; i++) {
                tags[i] = $.trim(tags[i]);
                if (tags[i] == value) {
                    return;
                }
            }

            tags.push(value);

            //create a new string value
            var newTags = tags.join(", ");

            $("#Tags").val(newTags);
        }
        else {
            $("#Tags").val(value);
        }
    });
});