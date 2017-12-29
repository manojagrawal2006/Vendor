<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeFile="TestLocation.aspx.cs" Inherits="TestLocation" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js "></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=true"></script>

    <div id="map_canvas" style="width: 300px; height: 300px">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var latlng = new google.maps.LatLng(-34.397, 150.644);
            var myOptions = {
                zoom: 8,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        });
    </script>
</asp:Content>