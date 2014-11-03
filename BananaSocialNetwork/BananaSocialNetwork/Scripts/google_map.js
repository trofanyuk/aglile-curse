﻿
var markers = new Array();

//$(document).ready(function () {
//    GetMap();
//});

var geocoder;
var map;
// Функция загрузки
function GetMap() {
    geocoder = new google.maps.Geocoder();

    google.maps.visualRefresh = true;
    // установка основных координат
    var Odessa = new google.maps.LatLng(46.481447, 30.735585);

    // Установка общих параметров отображения карты, как масштаб, центральная точка и тип карты
    var mapOptions = {
        zoom: 15,
        center: Odessa,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    // Встраиваем гугл-карты в элемент на странице и получаем объект карты
    map = new google.maps.Map(document.getElementById("canvas"), mapOptions);



    google.maps.event.addListener(map, 'click', function (e) {

        placeMarker(e.latLng, map);
    });
}

function placeMarker(position, map) {
    alert();
    var marker = new google.maps.Marker({
        position: position,
        map: map
    });
    if (markers.length == 1) {
        markers[0].setMap(null);
    }

    markers[0] = marker;
    var GeoLong = document.getElementById('GeoLong');
    var GeoLat = document.getElementById('GeoLat');

    GeoLong.value = marker.position.lng();
    GeoLat.value = marker.position.lat();
    map.panTo(position);
}

function codeAddress() {
    var address = document.getElementById('Adress').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
            if (markers.length == 1) {
                markers[0].setMap(null);
            }

            markers[0] = marker;
            var GeoLong = document.getElementById('GeoLong');
            var GeoLat = document.getElementById('GeoLat');

            GeoLong.value = marker.position.lng();
            GeoLat.value = marker.position.lat();
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}