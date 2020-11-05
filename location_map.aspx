<%@ Page Language="C#" AutoEventWireup="true" CodeFile="location_map.aspx.cs" Inherits="location_map" EnableEventValidation="false" %>

<!DOCTYPE html>
<html>

<head>
    <title>Autocomplete search address form using google map and get data into form example </title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/datetimepicker.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.js"></script>
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.blockUI.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDHazUF4f5fKnhXQMRmXCecAxSmjsVB3Cc&callback=initMap"
        type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDHazUF4f5fKnhXQMRmXCecAxSmjsVB3Cc&signed_in=true&libraries=places&callback=initialize" async defer></script>


    <style type="text/css">
        .input-controls {
            margin-top: 10px;
            border: 1px solid transparent;
            border-radius: 2px 0 0 2px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            height: 32px;
            outline: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        }

        #searchInput {
            background-color: #fff;
            font-family: Roboto;
            font-size: 15px;
            font-weight: 300;
            margin-left: 12px;
            padding: 0 11px 0 13px;
            text-overflow: ellipsis;
            width: 50%;
        }

            #searchInput:focus {
                border-color: #4d90fe;
            }

        .row {
            margin: 0px;
        }
    </style>
    <script>



        function Reg_validation() {

            var txt_serch_loc = document.getElementById('<%=searchInput.ClientID %>');
           var txt_loc = document.getElementById('<%=location.ClientID %>');
           var txt_lat = document.getElementById('<%=lat.ClientID %>');
           var txt_long = document.getElementById('<%=lng.ClientID %>');
           var txt_area = document.getElementById('<%=txt_area.ClientID %>');

           if (txt_loc.value == "") {
               alert("Please Add Location...");
               txt_serch_loc.focus();
               return false;
           }

           if (txt_lat.value == "") {
               alert("Please Add Location...");
               txt_serch_loc.focus();
               return false;
           }

           if (txt_long.value == "") {
               alert("Please Add Location...");
               txt_serch_loc.focus();
               return false;
           }

           if (txt_area.value == "") {
               alert("Please Add Location...");
               txt_serch_loc.focus();
               return false;
           }

       }
       function AllowAlphabet_address(e) {
           if (null != e) {
               isIE = document.all ? 1 : 0
               keyEntry = !isIE ? e.which : e.keyCode;
               if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||
                   (keyEntry == '32') || (keyEntry == '38') || ((keyEntry == '39') && (keyEntry == '34')) || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||
                   (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '92'))
                   return true;
               else {
                   // alert('Please Enter Only Character values.');
                   return false;
               }
           }
       }
    </script>
</head>
<body>
    <form runat="server">

        <asp:TextBox runat="server" ID="searchInput" class="input-controls" type="text" placeholder="Enter a location" />
        <div class="map" id="map" style="width: 100%; height: 430px;"></div>
        <br />

        <div class="form_area">
            <div class="row">
                <div class="col-sm-3 col-xs-12">
                    Genrated Address :
                         <asp:TextBox runat="server" CssClass="form-control" type="text" Rows="3" TextMode="MultiLine"
                             name="location" ID="location" onKeyPress="return AllowAlphabet_address(event)" />
                </div>
                <div class="col-sm-3 col-xs-12">
                    Lattitude :
                         <asp:TextBox runat="server" CssClass="form-control" type="text" name="lat" ID="lat" onKeyPress="return AllowAlphabet_address(event)" value="18.5204" />
                </div>
                <div class="col-sm-3 col-xs-12">
                    Longitude
                         <asp:TextBox runat="server" CssClass="form-control" type="text" name="lng" ID="lng" onKeyPress="return AllowAlphabet_address(event)" value="73.8567" />
                </div>
                <div class="col-sm-2 col-xs-12">
                    Area :
                         <asp:TextBox runat="server" CssClass="form-control" type="text" name="area" ID="txt_area" onKeyPress="return AllowAlphabet_address(event)" />
                </div>
            </div>
            <div class="row text-center">
                <asp:Button ID="btn_modal_save" CssClass="btn btn-success" OnClick="btn_modal_save_Click" OnClientClick="return Reg_validation()" runat="server" Text="Save" />

                <asp:Button runat="server" ID="btn_close" Text="Close" OnClientClick="goBack();" CssClass="btn btn-danger" />
            </div>

        </div>

        <script>
            /* script */
            //$(document).ready(function () {
            //    initialize();
            //});
            function goBack() {
                alert("Are You Sure ,You want to close");
                window.history.back();
                alert("2");
            }
            function initialize() {
                var lat = document.getElementById('<%=lat.ClientID %>').value;
        var lng = document.getElementById('<%=lng.ClientID %>').value;
        //alert(lat);
        //alert(lng);
        var latlng = new google.maps.LatLng(lat, lng);
        var map = new google.maps.Map(document.getElementById('map'), {
            center: latlng,
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.TERRAIN
        });

        var marker = new google.maps.Marker({
            map: map,
            position: latlng,
            draggable: true,
            anchorPoint: new google.maps.Point(0, -29)
        });

        var rad = 500;

        var circle = new google.maps.Circle({
            map: map,
            radius: rad,          // IN METERS.
            fillColor: '#FF6600',
            fillOpacity: 0.3,
            strokeColor: "#FFF",
            strokeWeight: 0,  // DON'T SHOW CIRCLE BORDER.
        });

        circle.bindTo('center', marker, 'position');

        var input = document.getElementById('<%=searchInput.ClientID %>');
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
        var geocoder = new google.maps.Geocoder();
        var autocomplete = new google.maps.places.Autocomplete(input);
        autocomplete.bindTo('bounds', map);
        var infowindow = new google.maps.InfoWindow();
        autocomplete.addListener('place_changed', function () {

            infowindow.close();
            marker.setVisible(false);

            var place = autocomplete.getPlace();
            if (!place.geometry) {
                window.alert("Autocomplete's returned place contains no geometry");
                return;
            }

            // If the place has a geometry, then present it on a map.
            if (place.geometry.viewport) {
                map.fitBounds(place.geometry.viewport);

            } else {
                map.setCenter(place.geometry.location);

                map.setZoom(17);
            }

            marker.setPosition(place.geometry.location);
            marker.setVisible(true);


            bindDataToForm(place.formatted_address, place.geometry.location.lat(), place.geometry.location.lng());
            infowindow.setContent(place.formatted_address);
            infowindow.open(map, marker);

        });


        // this function will work on marker move event into map 
        google.maps.event.addListener(marker, 'dragend', function () {
            geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        bindDataToForm(results[0].formatted_address, marker.getPosition().lat(), marker.getPosition().lng());
                        infowindow.setContent(results[0].formatted_address);
                        infowindow.open(map, marker);
                    }
                }
            });
        });
    }

    function bindDataToForm(address, lat, lng) {
        document.getElementById('<%=location.ClientID %>').value = address;
        document.getElementById('<%=lat.ClientID %>').value = lat;
        document.getElementById('<%=lng.ClientID %>').value = lng;
        document.getElementById('<%=txt_area.ClientID %>').value(rad);
        ShowCurrentTime(address, lat, lng);
    }

    function ShowCurrentTime(address, lat, lng) {
        PageMethods.setlatlng(address, lat, lng);
    }

    google.maps.event.addDomListener(window, 'load', initialize);

        </script>


    </form>
</body>
</html>
