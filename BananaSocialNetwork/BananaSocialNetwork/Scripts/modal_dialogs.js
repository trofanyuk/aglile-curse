﻿
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

function show_Edit_Album() {
    var album_id = $("#al_id").val().toString();
    $.ajax({
        type: "GET",
        url: "/Album/EditAlbumPartial/" + album_id,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function save_edit_album() {
    var album =
        {
            Id: $("#al_id").val().toString(),
            GeoLong: $("#GeoLong").val().toString(),
            GeoLat: $("#GeoLat").val().toString(),
            Adress: $("#Adress").val().toString(),
            Name: $("#Album_name").val().toString()
        };

    $.ajax({
        type: "POST",
        url: "/Album/Edit/",
        data: album,
        success: function (viewHTML) {
            close_edit_album();
            $("#album_name").text(album.Name);
        },
        error: function (errorData) { }
    });
}

function close_edit_album() {
    var dialog = $("#edit_album_dialog").remove();
}

// Photos //

function show_Add_Photo() {
    var album_id = $("#al_id").val().toString();
    $.ajax({
        type: "GET",
        url: "/Photo/AddPhotoPartial/" + album_id,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function close_add_photo() {
    var dialog = $("#add_photo_dialog").remove();
}

function show_Edit_Photo() {
    var photo_id = $("#id_mes").val().toString();
    $.ajax({
        type: "GET",
        url: "/Photo/EditPhotoPartial/" + photo_id,
        success: function (viewHTML) {
            $("#galleryOverlay").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function save_edit_photo() {
    var photo =
        {
            Id: $("#id_mes").val().toString(),
            GeoLong: $("#GeoLong").val().toString(),
            GeoLat: $("#GeoLat").val().toString(),
            Adress: $("#Adress").val().toString()
        };

    $.ajax({
        type: "POST",
        url: "/Photo/Edit/",
        data: photo,
        success: function (viewHTML) {
            close_edit_photo()
        },
        error: function (errorData) { }
    });
}

function close_edit_photo() {
    var dialog = $("#edit_photo_dialog").remove();
}