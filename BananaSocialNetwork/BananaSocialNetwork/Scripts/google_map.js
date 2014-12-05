
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

function GetMapNews(arr) {

    divs = document.getElementsByClassName("canvas_map");
   
    var k = 1;
    for (var i = 0; i < divs.length; i++) {
      
            geocoder = new google.maps.Geocoder();

            google.maps.visualRefresh = true;
        // установка основных координат
            //alert(arr);
            //alert(arr[k+1]);
            //alert(arr[k]);
            var Odessa = new google.maps.LatLng(arr[k + 1], arr[k]);
            //alert(arr);
           
            
            // Установка общих параметров отображения карты, как масштаб, центральная точка и тип карты
            var mapOptions = {
                zoom: 9,
                center: Odessa,
                mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
            };
            // Встраиваем гугл-карты в элемент на странице и получаем объект карты

            var myLatlng = new google.maps.LatLng(arr[k + 1], arr[k]);

        // Встраиваем гугл-карты в элемент на странице и получаем объект карты
            map = new google.maps.Map(divs.item(i), mapOptions);

           // new google.maps.Map(divs.item(i), mapOptions);

            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map
            });
            k = k + 2;
    }
}


function GetAllPhotosMap(arr, zoom) {

    
    geocoder = new google.maps.Geocoder();

    google.maps.visualRefresh = true;
    // установка основных координат
    var Odessa = new google.maps.LatLng(0, 0);

    // Установка общих параметров отображения карты, как масштаб, центральная точка и тип карты
    var mapOptions = {
        zoom: zoom,
        center: Odessa,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };
    // Встраиваем гугл-карты в элемент на странице и получаем объект карты
    map = new google.maps.Map(document.getElementById("canvas"), mapOptions);

   
    //var panoramioLayer = new google.maps.panoramio.PanoramioLayer();
    //panoramioLayer.setMap(map);

    //var photoPanel = document.getElementById('photo-panel');
    //map.controls[google.maps.ControlPosition.RIGHT_TOP].push(photoPanel);
    //var Odessa = new google.maps.LatLng(arr[2], arr[0]);
    //var mapOptions = {
    //    zoom: 1,
    //    center: Odessa,
    //    mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    //};
    for (var k = 1; k < arr.length; k=k+2) {

        
        // Встраиваем гугл-карты в элемент на странице и получаем объект карты

        var myLatlng = new google.maps.LatLng(arr[k], arr[k-1]);
        
        //alert(arr);
        //alert(arr[k]);
       // alert(nameAlbums[k - 1]);
        // Встраиваем гугл-карты в элемент на странице и получаем объект карты
       // map = new google.maps.Map(divs.item(i), mapOptions);
       // var map = new google.maps.Map(document.getElementById('canvas'), mapOptions);
        // new google.maps.Map(divs.item(i), mapOptions);

        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map
        });
        //var infowindow = new google.maps.InfoWindow({
        //    content: nameAlbums[k-1],
        //    position: myLatlng
        //});
       // infowindow.open(map);

       
    }
}

function placeMarker(position, map) {



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
    //alert(marker.position.lng()).replace('.', ',');
    GeoLong.value = String(marker.position.lng()).replace('.', ',');
    GeoLat.value = String(marker.position.lat()).replace('.', ',');
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
            //alert(marker.position.lng()).replace('.', ',');
            GeoLong.value = String(marker.position.lng()).replace('.', ',');
            GeoLat.value = String(marker.position.lat()).replace('.', ',');

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function GetMapMarker() {
    geocoder = new google.maps.Geocoder();

    google.maps.visualRefresh = true;
    // установка основных координат
    var Odessa = new google.maps.LatLng(GeoLat, GeoLong);

    // Установка общих параметров отображения карты, как масштаб, центральная точка и тип карты
    var mapOptions = {
        zoom: 15,
        center: Odessa,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    var myLatlng = new google.maps.LatLng(GeoLat, GeoLong);

    // Встраиваем гугл-карты в элемент на странице и получаем объект карты
    map = new google.maps.Map(document.getElementById("canvas"), mapOptions);
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map
    });

    markers[0] = marker;

    google.maps.event.addListener(map, 'click', function (e) {

        placeMarker(e.latLng, map);
    });
}

function ShowPhotoLocation() {
    geocoder = new google.maps.Geocoder();

    google.maps.visualRefresh = true;
    // установка основных координат
    var Odessa = new google.maps.LatLng(GeoLat, GeoLong);

    // Установка общих параметров отображения карты, как масштаб, центральная точка и тип карты
    var mapOptions = {
        zoom: 15,
        center: Odessa,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    var myLatlng = new google.maps.LatLng(GeoLat, GeoLong);

    // Встраиваем гугл-карты в элемент на странице и получаем объект карты
    map = new google.maps.Map(document.getElementById("canvas"), mapOptions);
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map
    });


}