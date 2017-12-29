<%@ Page Language="C#" Title="Vendor Master" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="VendorMaster" %>
<asp:Content ID="BodyContent"  ContentPlaceHolderID="MainContent"  runat="server">
     
<%--     <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC_xl6rnJXCIXbnkbBBtK6tN6kaIDdUh4c&libraries=places&project=deals2party" > </script>--%>
          <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js "></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyC_xl6rnJXCIXbnkbBBtK6tN6kaIDdUh4c&sensor=true"></script>

        <script type="text/javascript">
            $(function () {
                $("[src*=minus]").each(function () {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove()
                });
            });
            function readURL(input, flag) {
                // alert(1);
                //alert(input.parent.find("input").name);
                //alert(input.id =='MainContent_imageFilePath1');
                //alert($('#imageFilePath1').name);
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        if (input.id == 'MainContent_imageFilePath1') {
                            $('#imgDishImage1')
                                .attr('src', e.target.result)
                                .width(150)
                                .height(200);
                        }
                        else {
                            //$('#imgDishImage1')
                            //    .attr('src', e.target.result)
                            //    .width(150)
                            //    .height(200);
                            $(input)[0].parentNode.childNodes['2'].src = e.target.result;
                            //.attr('src', e.target.result)
                            //.width(150)
                            //.height(200);
                        }
                    };

                    reader.readAsDataURL(input.files[0]);
                }
            };
            $("input").change(function (e) {

                //alert($("input"));
                for (var i = 0; i < e.originalEvent.srcElement.files.length; i++) {

                    var file = e.originalEvent.srcElement.files[i];

                    var img = document.createElement("img");

                    alert(img.name);
                    var reader = new FileReader();
                    reader.onloadend = function () {
                        img.src = reader.result;
                    }
                    reader.readAsDataURL(file);

                    $("input").after(img);
                    $('#imgDishImage').after(img);
                }
            });
</script>
    <h1>Vendor Master</h1>
  <%--    <script async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC_xl6rnJXCIXbnkbBBtK6tN6kaIDdUh4c&libraries=places&callback=initMap&project=deals2party">
    </script>--%>
    <div class="form-horizontal">
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="ADD" OnClick="btnAdd_Click"  /></td>
                <td>
                    <asp:Button ID="btnEdit" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
                                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false" OnClick="btnSave_Click" /></td>
               <td>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" /></td>
            </tr>
        </table>
        <table border="1">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT * FROM [Vendor_Type]"></asp:SqlDataSource>
                <tr><td style="width:20%">Vendor Name	</td><td style="width:80%">
                    <asp:DropDownList ID="ddVendorName" runat="server" AutoPostBack="true"  DataSourceID="SqlDataSource2" DataTextField="Vendor_Name" DataValueField="Vendor_Id" OnSelectedIndexChanged="ddVendorName_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server"  ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Vendor_Id], [Vendor_Name] FROM [Vendor_Master]"></asp:SqlDataSource>
                    <asp:TextBox Width="95%" CssClass="form-control" ID="txtVendorName" Visible="false" runat="server"></asp:TextBox>
                </td>            
                </tr>
            <tr style="border-width:thick">
                <td  style="width:20%">Vendor Type	</td><td style="width:80%">
                <asp:DropDownList ID="ddVendorType" CssClass="form-control" DataTextField="Description" DataValueField="Vendor_Type_CD" runat="server" DataSourceID="SqlDataSource1" ClientIDMode="AutoID">
                </asp:DropDownList>
                    
                </td>
                </tr>

                <tr><td style="width:20%">Owner name</td><td style="width:80%">
                <asp:TextBox Width="95%" CssClass="form-control" ID="txtOwnerName" runat="server"></asp:TextBox>
                </td>            
                </tr>
            <tr><td style="width:20%">Registration_Number</td><td style="width:80%">
                <asp:TextBox ID="txtRegistration" CssClass="form-control" Width="95%" runat="server"></asp:TextBox>
                </td>
                </tr><tr><td style="width:20%">Establishment_Date</td><td style="width:80%">
                    <asp:Calendar ID="dtEstablishment" Width="330px" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                        <DayStyle BackColor="#CCCCCC" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    </asp:Calendar>
                </td>            
            </tr>
                <tr><td style="width:20%">Email_id	</td><td style="width:80%">
                <asp:TextBox ID="txtEmail" CssClass="form-control" Width="95%" runat="server" TextMode="Email"></asp:TextBox>
                </td>
                </tr><tr><td style="width:20%">Address</td><td style="width:80%">
                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>            
                </tr><tr><td style="width:20%">City</td><td style="width:80%">
                <asp:TextBox ID="txtCity" CssClass="form-control" Width="95%" runat="server"></asp:TextBox>
                </td>
                </tr><tr><td style="width:20%">State</td><td style="width:80%">
                <asp:TextBox ID="txtState" CssClass="form-control" Width="95%" runat="server"></asp:TextBox>
                </td>            
                </tr><tr><td style="width:20%">Pin</td><td style="width:80%">
                <asp:TextBox ID="txtPin" CssClass="form-control" Width="95%" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                </tr>
                <tr><td style="width:20%">Location</td> <td style="width:80%">
                         <div id="map_canvas" style="width: 600px; height: 600px"> </div>
                   
                    </td>
                    </tr>
            <tr><td style="width:20%">Contact No</td><td style="width:80%">
                <asp:TextBox ID="txtContactNo" CssClass="form-control" Width="95%" runat="server" TextMode="Phone"></asp:TextBox>
                </td>            
                </tr><tr><td style="width:20%">Website URL</td><td style="width:80%">
                <asp:TextBox ID="txtWebURL" CssClass="form-control" Width="95%" runat="server" TextMode="Url"></asp:TextBox>
                </td>
                </tr><tr><td style="width:20%">LogoPath</td>
<%--                    <td style="width:80%">
                <asp:FileUpload ID="flLogoPath" CssClass="form-control" Width="95%" runat="server" onchange="readURL(this);" />
                    <img id="blah" src="<%# Eval("Package_Img_Path") %>" width="50px" height="50px" alt="your image" />
                </td>            --%>
                             <td><input runat="server" type='file' id="imageFilePath"  onchange="readURL(this);" />
                     <img id="blah" src="<%# Eval("Package_Img_Path") %>" width="50px" height="50px" alt="your image" /></td>
                    <td>
                </tr>
        </table>
            </div>

    </div>
   <script type="text/javascript">
       var map;
       var marker;
   $(document).ready(function () {
                changeLocation('<% =double.Parse(Session["Latitude"].ToString())%>', '<% =double.Parse(Session["Longitude"].ToString())%>',true,0);
       });
       function changeLocation(lan, lon, CenterPositionandZoom, test) {
           var latlng = new google.maps.LatLng(lan, lon);
           var myOptions;
           if (CenterPositionandZoom) {
               myOptions = {
                   zoom: 15,
                   center: latlng,
                   mapTypeId: google.maps.MapTypeId.ROADMAP
               };
           }
           else {
               myOptions = {
                   zoom: 15,
                   mapTypeId: google.maps.MapTypeId.ROADMAP
               }
           }
         map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

           document.getElementById('txtLatitude').value = lan
             document.getElementById('txtLongitude').value = lon;
             
           var marker = new google.maps.Marker({
               position: latlng,
               map: map,
           });
           google.maps.event.addListener(map, 'click', function (e) {

               //Deermine the location where the user has clicked.
               var location = e.latLng;
               document.getElementById('txtLatitude').value = location.lat();
               document.getElementById('txtLongitude').value = location.lng();
               marker.setMap(null);
               marker = new google.maps.Marker({
                   position: location,
                   map: map
               });

           });
       }
    </script>
</asp:Content>
