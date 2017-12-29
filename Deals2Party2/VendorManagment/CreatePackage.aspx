<%@ Page Language="C#" Title="Catering Menu Creation" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeFile="CreatePackage.aspx.cs" Inherits="CreatePackage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <h1>Create Catering Package Detail</h1>
        <div>
            <h3><asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></h3>
       <table style="height: 109px; width: 100%">
           <tr >
               <td >Select Vendor</td>
               <td><asp:DropDownList ID="ddVendor_Name1" runat="server" AutoPostBack="true"  DataTextField="Vendor_Name" DataValueField="Vendor_Id" Width="80%" OnSelectedIndexChanged="ddVendor_Name_SelectedIndexChanged"  ></asp:DropDownList></td> 
               <td></td>
           </tr>
           <tr><td>Package Name</td> 
               <td><asp:DropDownList ID="ddPackageName" runat="server" AutoPostBack="true"   DataTextField="Package_Name" DataValueField="Vender_Pkg_Mst_Id" OnSelectedIndexChanged="ddPackageName_SelectedIndexChanged"></asp:DropDownList></td>
           </tr>
           <tr><td>Package Base Price</td> 
               <td>
                   <asp:Label ID="lblBasePrice" runat="server" Text=""></asp:Label></td>
           </tr>
           <tr><td>Package Description</td> 
               <td>
                   <asp:Label ID="lblPackageDesc" runat="server" Text=""></asp:Label></td>
           </tr>
            <%--<tr><td>Max selection</td>
               <td><asp:TextBox ID="txtselectAny" runat="server"></asp:TextBox></td>
           </tr>--%>
<%--           <tr><td>Course Type</td> 
               <td></td>
           </tr>--%>
<%--           <tr><td>Dish Type</td>
               <td><asp:DropDownList ID="ddDishType" runat="server"  AutoPostBack="true"  DataTextField="Description" DataValueField="Dish_Type_Id" OnSelectedIndexChanged="ddDishType_SelectedIndexChanged"></asp:DropDownList></td>
           </tr>--%>
<%--           <tr>
               <td>Dish Name</td>
               <td>
                   <asp:CheckBoxList ID="chkDishMasterList" DataValueField="Dish_ID" DataTextField="Description" runat="server">
                   </asp:CheckBoxList>
               </td>
               </tr>--%>
          
<%--           <tr><td>Dish Image</td>
               <td>TBD</td>
               </tr>
           <tr><td>Dish ID</td>
               <td><asp:Label ID="lblDishID" runat="server" Text=""></asp:Label></td>
               </tr>--%>
           <tr><td><asp:Button ID="btnSave" runat="server" Height="23px" OnClick="btnSave_Click" Text="Save" Width="169px" /></td>
               <td></td>
               </tr> 
       </table>
            <div>
                   <asp:GridView ID="gvCustomers" Width="100%" HeaderStyle-VerticalAlign="Middle" RowStyle-VerticalAlign="Middle" runat="server" AutoGenerateColumns="false" CssClass="Grid" DataKeyNames="Course_Type_ID" >
                        <Columns>
  <%--                          <asp:BoundField HeaderStyle-Width="0px" ItemStyle-Width="0px" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="Black" ItemStyle-BackColor="black" ItemStyle-ForeColor="White" DataField="Vendor_Id"  />
                            <asp:BoundField HeaderStyle-Width="0px" ItemStyle-Width="0px" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="Black" ItemStyle-BackColor="black" ItemStyle-ForeColor="White"  DataField="Course_Type_ID"/>
  --%>          <%--                <asp:BoundField ItemStyle-Width="20%" DataField="CourseName" HeaderStyle-BackColor="Black" ItemStyle-BackColor="black" ItemStyle-ForeColor="White"  HeaderText="Course Name" />--%>
<%--                          <asp:TemplateField  HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Label1" runat="server" Value ='<%# Eval("Course_Type_ID")%>'></asp:HiddenField>
                                    </ItemTemplate>
                            </asp:TemplateField>--%>
                          <asp:TemplateField  HeaderStyle-Width="5%">
                                <ItemTemplate>
                                     <asp:HiddenField ID="lblCourse_Type_ID" runat="server" Value ='<%# Eval("Course_Type_ID")%>'></asp:HiddenField>
                                    <asp:ImageButton ID="imgShow" runat="server"  OnClick="Show_Hide_ChildGrid" 
                                        ImageUrl="~/images/plus.png"
                                        CommandArgument="Show" />
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("CourseName")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Course Name
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="10%" >
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtMaxSelection" Text='<%# Eval("pkg_Course_Max_Selection")%>' />           
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Max Selection
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderStyle-Width="75%">
                                <ItemTemplate>
                                <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                <asp:GridView ID="gvOrders" runat="server" Width="100%" DataKeyNames="Dish_Id" AutoGenerateColumns="false" CssClass="ChildGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="50%">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="lblDish_Id" runat="server" Value ='<%# Eval("Dish_Id")%>'></asp:HiddenField>
                                                <asp:CheckBox ID="chkDishID" runat="server" Text = '<%# Eval("DishName")%>'  Checked='<%# Eval("DishSelected") %>'/>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Course Name
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DishType" HeaderStyle-Width="20%" HeaderText="DishType" SortExpression="DishType" />
                                        <asp:BoundField DataField="Price" HeaderText="Price"   />

            <%--                            <asp:BoundField DataField="CourseName"  HeaderText="CourseName" SortExpression="CourseName" />
                                        <asp:BoundField DataField="Vendor_Name" Visible="false" HeaderText="Vendor_Name" SortExpression="Vendor_Name" />
                                        <asp:BoundField DataField="CuisinesName" HeaderText="CuisinesName" SortExpression="CuisinesName" />
                                        <asp:BoundField DataField="DishType" HeaderText="DishType" SortExpression="DishType" />
                                        <asp:BoundField DataField="DishName" HeaderText="DishName" SortExpression="DishName" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="DishName" />
                                        <asp:BoundField DataField="Vendor_ID"  HeaderText="Vendor_ID" ReadOnly="True" />
                                        <asp:BoundField DataField="Dish_Id"  HeaderText="Dish_Id" ReadOnly="True" />
                                        <asp:BoundField DataField="Cuisines_Type_ID"  HeaderText="Cuisines_Type_ID"  ReadOnly="True" />
                                        <asp:BoundField DataField="Course_Type_ID"  HeaderText="Course_Type_ID"  ReadOnly="True" />
                                        <asp:BoundField DataField="Dish_Type_Id" HeaderText="Dish_Type_Id" ReadOnly="True"/>--%>
                                    </Columns>

                                </asp:GridView>
                            </asp:Panel>
                                    </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
            </div>

 <%--                  <div>
        <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse; width: 100%" >
                    <tr>
                        <th scope="col" style="width: 20%">   Course Type	</th>
                        <th scope="col" style="width: 10%">   Disk Price	</th>
                        <th scope="col" style="width: 10%">   Dish Name List		</th>
                        <th scope="col" style="width: 5%">   Action		</th>
                </tr>
                    <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
               
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblCourseType" runat="server"  />
                        <asp:DropDownList ID="ddCourseType" runat="server" AutoPostBack="true"   DataTextField="Description" DataValueField="Course_Type_ID" OnSelectedIndexChanged="ddCourseType_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblPackage_Desc" runat="server"/>
                        <asp:TextBox ID="txtPackage_Desc" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblCapacity" runat="server" />
                        <asp:TextBox ID="txtCapacity" runat="server" Text='<%# Eval("Capacity") %>' Visible="false" />
                    </td>
                   
                     <td>
                        <asp:Label ID="lblPackage_Img_Path" runat="server" Text='<%# Eval("Package_Img_Path") %>' />
                        <asp:TextBox ID="txtPackage_Img_Path" runat="server" Text='<%# Eval("Package_Img_Path") %>' Visible="false" />
                    </td>
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
            </table>
                       </div>
            <div>
                <asp:TreeView ID="TreeView1" runat="server" OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged">
                <Nodes>
                        <asp:TreeNode >
                         <asp:TreeNode >

                        </asp:TreeNode>
                        </asp:TreeNode>
                </Nodes>
            </asp:TreeView>
            </div>--%>
     </div>
    
</asp:Content>
