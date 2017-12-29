<asp:Content ID="BodyContent"  ContentPlaceHolderID="MainContent"  runat="server">
<%@ Page Language="C#" Title="Create Catering Package Offers" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeFile="CreatePackageOffers.aspx.cs" Inherits="CreatePackageOffers" %>
       <h1>Create Catering Package Offers</h1>
         <div>
            <h3><asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></h3>
       <table style="height: 109px; width: 100%" border="1">
           <tr >
               <td  Width="20%" >Select Vendor</td>
               <td><asp:DropDownList ID="ddVendor_Name1" runat="server" AutoPostBack="true"  DataTextField="Vendor_Name" DataValueField="Vendor_Id" OnSelectedIndexChanged="ddVendor_Name_SelectedIndexChanged"  ></asp:DropDownList></td> 
           </tr>
           <tr><td>Package Name</td> 
               <td><asp:DropDownList ID="ddPackageName" runat="server" AutoPostBack="true"   DataTextField="Package_Name" DataValueField="Vender_Pkg_Mst_Id" OnSelectedIndexChanged="ddPackageName_SelectedIndexChanged"></asp:DropDownList></td>
           </tr>
           <tr><td>Package Base Price</td> 
               <td><asp:Label ID="lblBasePrice" runat="server" Text=""></asp:Label></td>
           </tr>
           <tr><td>Package Description</td> 
               <td><asp:Label ID="lblPackageDesc" runat="server" Text=""></asp:Label></td>
           </tr>
       </table>
        <table cellspacing="0" rules="all" border="1"  style="border-collapse: collapse; width: 100%" >
                    <tr>
                        <th scope="col" style="width: 20%">   Order Range From	</th>
                        <th scope="col" style="width: 20%">    Order Range From	</th>
                        <th scope="col" style="width: 10%">   Offer Price		</th>
                        <th scope="col" style="width: 20%">   Discount %  </th>
                        <th scope="col" style="width: 20%">   Delivery Upto (km)  </th>
                        <th scope="col" style="width: 5%">   Action		</th>
                </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:HiddenField ID="lblVendor_Caterer_Package_Offers" runat="server" Value='<%# Eval("Vendor_Caterer_Package_Offers") %>' Visible = "false" />
                        <asp:Label ID="lblRangeFrom" runat="server" Text='<%# Eval("RangeFrom") %>'  />
                        <asp:TextBox ID="txtRangeFrom" runat="server" Text='<%# Eval("RangeFrom") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblRangeTo" runat="server" Text='<%# Eval("RangeTo") %>' />
                        <asp:TextBox ID="txtRangeTo" runat="server" Text='<%# Eval("RangeTo") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblOffer_Price" runat="server" Text='<%# Eval("Offer_Price") %>' />
                        <asp:TextBox ID="txtOffer_Price" runat="server" Text='<%# Eval("Offer_Price") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblOffer_Discount_Prct" runat="server" Text='<%# Eval("Offer_Discount_Prct") %>' />
                        <asp:TextBox ID="txtOffer_Discount_Prct" runat="server" Text='<%# Eval("Offer_Discount_Prct") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblDeliveryupto" runat="server" Text='<%# Eval("Deliveryupto") %>' />
                        <asp:TextBox ID="txtDeliveryupto" runat="server" Text='<%# Eval("Deliveryupto") %>' Visible="false" />
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
                  <tr>
                    <td>
                        <asp:TextBox ID="txtRangeFrom" runat="server" Text='<%# Eval("RangeFrom") %>'/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRangeTo" runat="server" Text='<%# Eval("RangeTo") %>'/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOffer_Price"  runat="server" Text='<%# Eval("Offer_Price") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtOffer_Discount_Prct"  runat="server" Text='<%# Eval("Offer_Discount_Prct") %>'/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeliveryupto"  runat="server" Text='<%# Eval("Delivery_up_to") %>'/>
                    </td>
                      
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                    </td>
            </tr>
            </table>
        </div>
 </asp:Content>