// admin
function find_user() {
    var search = $('#search_field').val();
    var type = $('#type_s option:selected').text();
    var json = "";

    if (search != '') {
        $.ajax({
            async: false,
            type: "GET",
            url: "/Admin/SearchUser/",
            data: {
                search: search,
                type: type
            },
            success: function (res) {
                json = res;
            },
            error: function (errorData) {}
        });
        $.ajax({
            async: false,
            type: "POST",
            url: "/Admin/SearchUserView/",
            data: { json: json },
            success: function (html) {
                var u_container = $('#users');
                u_container.empty();
                u_container.append(html);
            },
            error: function (errorData) {
                alert("error!");
            }
        });
    }
}

function reset()
{
    all_users();
    all_news();
    all_albums();
    all_photos();
}

function all_news()
{
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/AllNews/",
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/NewsUserView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#news');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function all_albums() {
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/AllAlbums/",
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/AlbumsUserView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#albums');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function all_photos() {
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/AllPhotos/",
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/PhotosAlbumView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#photos');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function all_users() {
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/AllUsers/",
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/SearchUserView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#users');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function set_user_info(id)
{
    set_user_news(id);
    set_user_albums(id);
    set_user_photos(id);
}

function set_user_news(id)
{
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/NewsUser/",
        data: {
            iduser : id
        },
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/NewsUserView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#news');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function set_user_albums(id) {
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/AlbumsUser/",
        data: {
            iduser: id
        },
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/AlbumsUserView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#albums');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function set_album_photos(id) {
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/PhotosAlbum/",
        data: {
            idalbum: id
        },
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/PhotosAlbumView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#photos');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function set_user_photos(id) {
    var json = "";
    $.ajax({
        async: false,
        type: "GET",
        url: "/Admin/PhotosUser/",
        data: {
            iduser: id
        },
        success: function (res) {
            json = res;

            $.ajax({
                async: false,
                type: "POST",
                url: "/Admin/PhotosAlbumView/",
                data: { json: json },
                success: function (html) {
                    var u_container = $('#photos');
                    u_container.empty();
                    u_container.append(html);
                },
                error: function (errorData) {
                    alert("error!");
                }
            });
        },
        error: function (errorData) { }
    });
}

function delete_user(id)
{
    $.ajax({
        type: "GET",
        url: "/Account/Delete/",
        data: { id: id },
        success: function (viewHTML) {
            $("#user_placeholder_" + id).remove();
        },
        error: function (errorData) {
            alert("Something wrong!");
        }
    });
}

function delete_comment(idComment)
{
    $.ajax({
        type: "GET",
        url: "/Comment/DeleteConfirmed/",
        data: { id: idComment },
        success: function (viewHTML) {
            $("#comment_placeholder_" + idComment).remove();
        },
        error: function (errorData) {
            alert("Something wrong!");
        }
    });
}
///  search  ///

function find_peoples() {
    var search = $('#search_field').val();
    if (search != '') {
        $.ajax({
            type: "GET",
            url: "/Search/SearchOnName/",
            data: { search: search },
            success: function (viewHTML) {
                var div = $("#find_result");
                div.html("");
                div.append(viewHTML);
            },
            error: function (errorData) { }
        });
    }
}

function add_friend(userId, elem) {
    $.ajax({
        type: "GET",
        url: "/Search/AddFriend/",
        data: { idFriend: userId },
        success: function (viewHTML) {
            $(elem).attr('disabled', 'disabled');
            $(elem).css('background-color', 'white');
            $(elem).css('color', '#555');
            $(elem).val('Request sent');
        },
        error: function (errorData) { }
    });
}

//  Profile index //

function confirm_friend(userId, elem) {
    $.ajax({
        type: "POST",
        url: "/Profile/ConfirmFriend/",
        data: { idFriend: userId },
        success: function (viewHTML) {
            $(elem).attr('disabled', 'disabled');
            $(elem).css('background-color', 'white');
            $(elem).css('color', '#555');
            $(elem).css('border', 'none');
            $(elem).val('Your friend');

            var profile = $('.profile');
            if (profile) {
                document.location = "/Profile/Index?userId=" + userId;
            }
        },
        error: function (errorData) { }
    });
}

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
    var uid = $("#uid").val();

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
        data: { json: album, id: uid },
        success: function () {
            window.location.href = "/Profile/Index?userId=" + uid;
        },
        error: function (errorData) {
            window.location.href = "/Profile/Index?userId=" + uid;
        }
    });
}

function show_Edit_Album(album_id) {
    $.ajax({
        type: "GET",
        url: "/Album/EditAlbumPartial/" + album_id,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function save_edit_album(al_id) {
    var album =
        {
            Id: al_id,
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
            $("#album_name_" + al_id).text(album.Name);
        },
        error: function (errorData) { }
    });
}

function close_edit_album() {
    var dialog = $("#edit_album_dialog").remove();
}

function delete_album_profile(al_id, u_id) {
    $.ajax({
        type: "GET",
        url: "/Album/DeleteConfirmed/",
        data: { id: al_id },
        success: function (viewHTML) {
            document.location = "/Profile/Index?userId=" + u_id;
        },
        error: function (errorData) { }
    });
}

function delete_album(al_id)
{
    $.ajax({
        type: "GET",
        url: "/Album/DeleteConfirmed/",
        data: { id: al_id },
        success: function (viewHTML) {
            $("#album_placeholder_" + al_id).remove();
        },
        error: function (errorData) { }
    });
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

function show_Edit_Photo(photo_id) {
    $.ajax({
        type: "GET",
        url: "/Photo/EditPhotoPartial/" + photo_id,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function save_edit_photo(id_photo) {
    var photo =
        {
            Id: id_photo,
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

function show_Photo_in_map(photo_id) {
    $.ajax({
        type: "GET",
        url: "/Photo/PhotoInMapPartial/" + photo_id,
        success: function (viewHTML) {
            $("body").append(viewHTML);
        },
        error: function (errorData) { }
    });
}

function close_photo_in_map() {
    var dialog = $("#photo_int_map").remove();
}

function delete_photo(photo_id)
{
    $.ajax({
        type: "GET",
        url: "/Photo/DeleteConfirmed/",
        data: { id: photo_id },
        success: function (viewHTML) {
            $("#photo_placeholder_" + photo_id).remove();
        },
        error: function (errorData) { }
    });
}