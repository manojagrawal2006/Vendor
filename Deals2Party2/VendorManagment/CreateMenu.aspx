<%@ Page Language="C#" Title="Catering Menu Creation" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeFile="CreateMenu.aspx.cs" Inherits="CreateMenu" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript" src='/jquery/jquery.min.js' ></script>
        <script type="text/javascript" src='/JavaScriptSpellCheck/include.js' ></script>
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
                if (input.id =='MainContent_imageFilePath1') {
                    $('#imgDishImage1')
                        .attr('src', e.target.result)
                        .width(150)
                        .height(200);
                }
                else
                {
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

 
        <h1>Create Menu</h1>
        <div>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Cuisines_Type_ID], [Description] FROM [Cuisines_Type]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Course_Type_ID], [Description] FROM [Course_Type]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Dish_Type_Id], [Description] FROM [Dishes_Type]"></asp:SqlDataSource>       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Deals2PartyDBConnectionString %>" SelectCommand="SELECT [Vendor_Name], [Vendor_Id] FROM [Vendor_Master]"></asp:SqlDataSource>
        </div>
        <div style="width:90%">
            <h3><asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></h3>
       <table runat="server" style="width: 100%;" border="1">
           <tr >
               <td style="width:30%" >Select Vendor</td>
               <td><asp:DropDownList ID="ddVendor_Name" runat="server" AutoPostBack="true" DataSourceID="SqlDataSource1" DataTextField="Vendor_Name" DataValueField="Vendor_Id" Width="80%" OnSelectedIndexChanged="ddVendor_Name_SelectedIndexChanged"></asp:DropDownList></td> 
           </tr>
        </table>
        <Table style="width: 100%"  border="1">
         <tr>
           <td style="width: 15%">Cuisines Type</td> 
           <td style="width: 15%">Course Type</td> 
           <td style="width: 15%">Dish Type</td>
           <td style="width: 15%">Dish Name</td>
           <td style="width: 10%">Price</td>
           <td style="width: 20%">Dish Image</td>
             <td>Action</td>
          </tr>
           <tr>
                <td> <asp:DropDownList ID="ddCuisinesType" Width="95%" runat="server" DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="Cuisines_Type_ID"></asp:DropDownList></td>
                <td><asp:DropDownList ID="ddCourseType"  Width="95%"  runat="server" DataSourceID="SqlDataSource3" DataTextField="Description" DataValueField="Course_Type_ID"></asp:DropDownList></td>
                <td><asp:DropDownList ID="ddDishType"  Width="95%"  runat="server" DataSourceID="SqlDataSource4" DataTextField="Description" DataValueField="Dish_Type_Id"></asp:DropDownList></td>
                <td><textarea id="txtDishName"  Width="95%"  runat="server"></textarea></td>
                <td><asp:TextBox ID="txtPrice"  Width="95%"  runat="server"></asp:TextBox></td>
                <td> <input runat="server" type='file' id="imageFilePath1" onchange="readURL(this,true);" />
                     <img id="imgDishImage1" src="#" width="100%" height="100%" alt="your image" />
                </td>
                <td><asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnSave_Click" /></td>

                <asp:HiddenField ID="lblDishID" runat="server"></asp:HiddenField>
           </tr>
       </Table>
     </div>
     <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse; width: 100%" >
                <tr>
                        <th scope="col" style="width: 5%">   	</th>
                        <th scope="col" style="width: 20%">    Course Name	</th>
                        <th scope="col" ></th>
                </tr>
    <asp:Repeater ID="gvCustomers" runat="server">
       <HeaderTemplate>
       </HeaderTemplate>
       <ItemTemplate>
            <tr>
                <td>
                        <asp:HiddenField ID="lblVendor_Id" runat="server" value='<%# Eval("Vendor_Id") %>' />
                        <asp:HiddenField ID="lblCourse_Type_ID" runat="server" value='<%# Eval("Course_Type_ID") %>' />
                        <asp:ImageButton ID="imgShow" runat="server" OnClick="Show_Hide_ChildGrid" ImageUrl="~/images/plus.png" CommandArgument="Show" />

                </td>
                  <td><asp:Label ID="txtCourseName" runat="server"><%# Eval("CourseName") %></asp:Label></td>
            <td>                
                <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
            <asp:Repeater ID="gvOrders" runat="server">
            <HeaderTemplate>
              <tr>
               <td style="width: 15%">Cuisines Type</td> 
               <td style="width: 15%">Course Type</td> 
               <td style="width: 15%">Dish Type</td>
               <td style="width: 15%">Dish Name</td>
               <td style="width: 10%">Price</td>
               <td style="width: 20%">Dish Image</td>
                 <td>Action</td>
              </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Label id="lblCuisinesType"  Width="100%" Height="100%"  runat="server"><%# Eval("CuisinesName") %></asp:Label>
                        <asp:HiddenField id="hCuisinesType" runat="server" Value='<%# Eval("Cuisines_Type_ID") %>' />
                        <asp:DropDownList ID="ddCuisinesType" Visible="false"   Width="100%" Height="100%" runat="server"  DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="Cuisines_Type_ID"></asp:DropDownList>
                    </td>
                    <td> <asp:Label ID="lblCourseType"  Width="100%" Height="100%"  runat="server" Text='<%# Eval("CourseName")%>' />
                        <asp:HiddenField id="hCourseType"   runat="server" Value='<%# Eval("Course_Type_ID") %>' />
                        <asp:DropDownList ID="ddCourseType" Visible="false" Width="100%" Height="100%"  runat="server" DataSourceID="SqlDataSource3" DataTextField="Description" DataValueField="Course_Type_ID"></asp:DropDownList>
                        
                    </td>
                    <td><asp:Label ID="lblDishType"  Width="100%" Height="100%"  runat="server" Text='<%# Eval("DishType") %>' />
                        <asp:HiddenField id="hDishType" runat="server" Value='<%# Eval("Dish_Type_Id") %>' />
                        <asp:DropDownList ID="ddDishType" Visible="false"  Width="100%" Height="100%"   runat="server" DataSourceID="SqlDataSource4" DataTextField="Description" DataValueField="Dish_Type_Id"></asp:DropDownList></td>
                    <td><asp:Label ID="lbltDishName"  Width="100%" Height="100%"  runat="server" ><%# Eval("DishName") %></asp:Label>
                        <asp:TextBox id="txtDishName" Visible="false" Text='<%# Eval("DishName") %>'  Width="100%" Height="100%"    runat="server"/></td>
                    <td><asp:Label ID="lblPrice" runat="server"   Width="100%" Height="100%"  Text='<%# Eval("Price") %>' />
                        <asp:TextBox ID="txtPrice" Visible="false"  Width="100%" Height="100%" Text='<%# Eval("Price") %>'   runat="server"/></td>
                    <td><input runat="server" type='file' id="imageFilePath" Visible="false" onchange="readURL(this);" />
                     <img id="blah" src="<%# Eval("Dish_Image_Path") %>" width="50px" height="50px" alt="your image" /></td>

                    
                    <td>
                        <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" OnClick="OnEdit" />
                        <asp:LinkButton ID="lnkUpdate" Text="Update" runat="server" Visible="false" OnClick="OnUpdate" />
                        <asp:LinkButton ID="lnkCancel" Text="Cancel" runat="server" Visible="false" OnClick="OnCancel" />
                        <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" OnClick="OnDelete" OnClientClick="return confirm('Do you want to delete this row?');" />
                    </td>
                    <asp:HiddenField ID="lblDishID" Value='<%# Eval("Dish_Id") %>' runat="server"></asp:HiddenField>
               </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
                </asp:Panel>
        </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </table>
    
</asp:Content>


