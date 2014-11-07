
// Albums // 

function showModal_Add_Album() {

    $.ajax({
        type: "GET",
        url: "/Album/AddAlbumPartial/",
        //data: someArguments,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { onError(errorData); }
    });
}

function close_add_album() {
    var dialog = $("#add_album_dialog").remove();
}

function confirm_add_album() {
    var alert_div = $("#Not_confirmed");
    var name = $("#Album_name");
    var location = $("#Adress");
    var geolong = $("#GeoLong").val();
    var geolat = $("#GeoLat").val();

    if (name.val() == "") {
        alert_div.css("visibility", "visible");
        return;
    }

    geolong = geolong.replace('.', ',');
    geolat = geolat.replace('.', ',');

    var album = {
        Name: name.val(),
        Adress: location.val(),
        GeoLong: geolong,
        GeoLat: geolat,
        DateCreate: Date()
    };

    $.ajax({
        type: "POST",
        url: "/Album/Create/",
        data: album
    });

    close_add_album();
    window.location = "";
}

// Photos //

function show_Add_Photo() {
    var album_id = $("#al_id").val().toString();
    $.ajax({
        type: "GET",
        url: "/Photo/AddPhotoPartial/" + album_id,
        //data: someArguments,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function close_add_photo() {
    var dialog = $("#add_photo_dialog").remove();
}