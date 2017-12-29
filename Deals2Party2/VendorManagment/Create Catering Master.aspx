<%@ Page Language="C#" Title="Create Catering Master" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeFile="Create Catering Master.aspx.cs" Inherits="Create_Catering_Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
         <h1>Package Master</h1>
        <div>
              <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Cuisines_Type_ID], [Description] FROM [Cuisines_Type]"></asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Dish_Type_Id], [Description] FROM [Dishes_Type]"></asp:SqlDataSource>       
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Package_Type_Id], [Package_Type] FROM [Package_Types]"></asp:SqlDataSource>       
        </div>
        <div>
            <table style="height: 50px; width: 100%"  border="1">
                <tr >
                    <td width="20%">Select Vendor</td>
                    <td><asp:DropDownList ID="ddVendor_Name1" runat="server" AutoPostBack="true"  DataTextField="Vendor_Name" DataValueField="Vendor_Id" Width="80%" OnSelectedIndexChanged="ddVendor_Name1_SelectedIndexChanged"  ></asp:DropDownList></td> 
                </tr>
                <tr >
                    <td width="20%">Service Type</td>
                    <td><asp:DropDownList ID="ddVendorType" runat="server" AutoPostBack="True"  DataTextField="Description" DataValueField="Vendor_Type_CD" Width="80%" OnSelectedIndexChanged="ddVendor_Name1_SelectedIndexChanged" DataSourceID="SqlDataSource1"  ></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Vendor_Type_CD], [Description] FROM [Vendor_Type]"></asp:SqlDataSource>
                    </td> 
                </tr>

             </table>

        </div>
        <div>
        <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse; width: 100%" >
                    <tr>
                        <th scope="col" style="width: 10%">   Service Type	</th>
                        <th scope="col" style="width: 10%">   Package Type	</th>
                        <th scope="col" style="width: 10%">   Package_Name	</th>
                        <th scope="col" style="width: 15%">    Package_Desc	</th>
                        <th style="width: 10%">Cuisines Type</th> 
                        <th style="width: 10%">Dish Type</th>
                        <th scope="col" style="width: 10%">   Capacity		</th>
                       <th scope="col" style="width: 10%">   Package_Price	</th>
                        <th scope="col" style="width: 15%">   Package_Img_Path</th>
                          <th scope="col" style="width: 5%">   Action		</th>
                </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
               
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblVendorType" runat="server" Text='<%# Eval("VendorType") %>' />
                        <asp:TextBox ID="txtVendorType" runat="server" Text='<%# Eval("VendorType") %>' Visible="false" />
                    </td>
                    <td><asp:Label id="lblPackageType"  Width="100%" Height="100%"  runat="server"><%# Eval("Package_Type") %></asp:Label>
                        <asp:HiddenField id="hPackageType" runat="server" Value='<%# Eval("Package_Type_Id") %>' />
                        <asp:DropDownList ID="ddPackageType" Visible="false"   Width="100%" Height="100%" runat="server"  DataSourceID="SqlDataSource3" DataTextField="Package_Type" DataValueField="Package_Type_Id"></asp:DropDownList>
                    </td>

                    <td>
                        <asp:Label ID="lblVendor_Id" runat="server" Text='<%# Eval("Vendor_Id") %>' Visible = "false" />
                        <asp:Label ID="lblVender_Pkg_Mst_Id" runat="server" Text='<%# Eval("Vender_Pkg_Mst_Id") %>' Visible = "false" />
                        <asp:Label ID="lblPackage_Name" runat="server" Text='<%# Eval("Package_Name") %>' />
                        <asp:TextBox ID="txtPackage_Name" runat="server" Text='<%# Eval("Package_Name") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblPackage_Desc" runat="server" Text='<%# Eval("Package_Desc") %>' />
                        <asp:TextBox ID="txtPackage_Desc" runat="server" Text='<%# Eval("Package_Desc") %>' Visible="false" />
                    </td>
                    <td><asp:Label id="lblCuisinesType"  Width="100%" Height="100%"  runat="server"><%# Eval("CuisinesName") %></asp:Label>
                        <asp:HiddenField id="hCuisinesType" runat="server" Value='<%# Eval("Cuisines_Type_ID") %>' />
                        <asp:DropDownList ID="ddCuisinesType" Visible="false"   Width="100%" Height="100%" runat="server"  DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="Cuisines_Type_ID"></asp:DropDownList>
                    </td>
                    <td><asp:Label ID="lblDishType"  Width="100%" Height="100%"  runat="server" Text='<%# Eval("DishType") %>' />
                        <asp:HiddenField id="hDishType" runat="server" Value='<%# Eval("Dish_Type_Id") %>' />
                        <asp:DropDownList ID="ddDishType" Visible="false"  Width="100%" Height="100%"   runat="server" DataSourceID="SqlDataSource4" DataTextField="Description" DataValueField="Dish_Type_Id"></asp:DropDownList>
                    </td>

                    <td>
                        <asp:Label ID="lblCapacity" runat="server" Text='<%# Eval("Capacity") %>' />
                        <asp:TextBox ID="txtCapacity" runat="server" Text='<%# Eval("Capacity") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblPackage_Price" runat="server" Text='<%# Eval("Package_Price") %>' />
                        <asp:TextBox ID="txtPackage_Price" runat="server" Text='<%# Eval("Package_Price") %>' Visible="false" />
                    </td>
                    <td>6<input runat="server" type='file' id="imageFilePath" Visible="false" onchange="readURL(this);" />
                     <img id="blah" src="<%# Eval("Package_Img_Path") %>" width="50px" height="50px" alt="your image" /></td>
                     
<%--                    <td>
                        <asp:Label ID="lblPackage_Img_Path" runat="server" Text='<%# Eval("Package_Img_Path") %>' />

                        <td>
                            <td>
                           <input runat="server" type='file' id="File1" Visible="false" onchange="readURL(this);" />
                     <img id="blah" src="<%# Eval("Package_Img_Path") %>" width="50px" height="50px" alt="your image" /></td>

                            <input runat="server" type='file' id="imageFilePath" Visible="false" onchange="readURL(this);" />
                     <img id="txtPackage_Img_Path"  src="<%# Eval("Package_Img_Path") %>" width="50px" height="50px" alt="your image" />


                        </td>
                    </td>--%>
                    <td>
                        <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" OnClick="OnEdit" />
                        <asp:LinkButton ID="lnkUpdate" Text="Update" runat="server" Visible="false" OnClick="OnUpdate" />
                        <asp:LinkButton ID="lnkCancel" Text="Cancel" runat="server" Visible="false" OnClick="OnCancel" />
                        <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" OnClick="OnDelete" OnClientClick="return confirm('Do you want to delete this row?');" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
  
            </FooterTemplate>
        </asp:Repeater>
                  <tr>
                    <td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddPackageType" Visible="false"   Width="100%" Height="100%" runat="server"  DataSourceID="SqlDataSource3" DataTextField="Package_Type" DataValueField="Package_Type_Id"></asp:DropDownList>
                    </td>

                    <td>
                        <asp:TextBox ID="txtPackage_Name" Width="95%" runat="server" Text='<%# Eval("Package_Name") %>'/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPackage_Desc" Width="95%" runat="server" Text='<%# Eval("Package_Desc") %>'/>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddCuisinesType" Visible="false"   Width="100%" Height="100%" runat="server"  DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="Cuisines_Type_ID"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddDishType" Visible="false"  Width="100%" Height="100%"   runat="server" DataSourceID="SqlDataSource4" DataTextField="Description" DataValueField="Dish_Type_Id"></asp:DropDownList>
                    </td>

                    <td>
                        <asp:TextBox ID="txtCapacity" Width="95%" runat="server" Text='<%# Eval("Capacity") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPackage_Price" Width="95%" runat="server" Text='<%# Eval("Package_Price") %>' />
                    </td>
                    <%--<td>
                        <asp:TextBox ID="txtPackage_Ratings" Width="95%" runat="server" Text='<%# Eval("Package_Ratings") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDiscount_Prct" Width="95%" runat="server" Text='<%# Eval("Discount_Prct") %>'/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCommision_Prct" Width="95%" runat="server" Text='<%# Eval("Commision_Prct") %>' />
                    </td>--%>
                    <td>
                        <asp:TextBox ID="txtPackage_Img_Path" Width="95%" runat="server" Text='<%# Eval("Package_Img_Path") %>'/>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                    </td>
            </tr>
            </table>
        </div>
 </asp:Content>
