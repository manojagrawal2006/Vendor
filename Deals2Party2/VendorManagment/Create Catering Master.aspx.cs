﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Create_Catering_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ddVendor_Name1.DataSource = GetData("SELECT[Vendor_Name], [Vendor_Id] FROM [Vendor_Master]");
            ddVendor_Name1.DataBind();
        }
    }
    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Master_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Vendor_Id", ddVendor_Name1.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Type_CD", ddVendorType.SelectedValue);
                
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                }
            }
        }
    }
    protected void Insert(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Master_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@Vendor_Id", ddVendor_Name1.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Type_CD", ddVendorType.SelectedValue);
                cmd.Parameters.AddWithValue("@Package_Name", txtPackage_Name.Text);
                cmd.Parameters.AddWithValue("@Package_Desc", txtPackage_Desc.Text);
                cmd.Parameters.AddWithValue("@Capacity", txtCapacity.Text);
                cmd.Parameters.AddWithValue("@Package_Price", txtPackage_Price.Text);
                //cmd.Parameters.AddWithValue("@Package_Ratings", txtPackage_Ratings.Text);
                //cmd.Parameters.AddWithValue("@Discount_Prct", txtDiscount_Prct.Text);
                //cmd.Parameters.AddWithValue("@Commision_Prct", txtCommision_Prct.Text);
                cmd.Parameters.AddWithValue("@Package_Img_Path", txtPackage_Img_Path.Text);
                cmd.Parameters.AddWithValue("@Cuisines_Type_ID",ddCuisinesType.SelectedValue);
                cmd.Parameters.AddWithValue("@Dish_Type_Id",ddDishType.SelectedValue);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        this.BindRepeater();
    }
    protected void OnEdit(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        this.ToggleElements(item, true);
    }

    private void ToggleElements(RepeaterItem item, bool isEdit)
    {
        //Toggle Buttons.
        item.FindControl("lnkEdit").Visible = !isEdit;
        item.FindControl("lnkUpdate").Visible = isEdit;
        item.FindControl("lnkCancel").Visible = isEdit;
        item.FindControl("lnkDelete").Visible = !isEdit;

        //Toggle Labels.
        item.FindControl("lblPackage_Name").Visible     = !isEdit;
        item.FindControl("lblPackage_Desc").Visible     = !isEdit;
        item.FindControl("lblCapacity").Visible         = !isEdit;
        item.FindControl("lblPackage_Price").Visible    = !isEdit;
        item.FindControl("lblDishType").Visible = !isEdit;

        //Toggle TextBoxes.
        item.FindControl("ddCuisinesType").Visible = isEdit;
        item.FindControl("ddDishType").Visible = isEdit;
        item.FindControl("imageFilePath").Visible = isEdit;
        item.FindControl("txtPackage_Name").Visible     = isEdit;
        item.FindControl("txtPackage_Desc").Visible     = isEdit;
        item.FindControl("txtCapacity").Visible         = isEdit;
        item.FindControl("txtPackage_Price").Visible    = isEdit;
        (item.FindControl("ddCuisinesType") as DropDownList).SelectedIndex = (int.Parse((item.FindControl("hCuisinesType") as HiddenField).Value) - 1);
        (item.FindControl("ddDishType") as DropDownList).SelectedIndex = int.Parse((item.FindControl("hDishType") as HiddenField).Value) - 1;

    }
    protected void OnUpdate(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        int Vender_Pkg_Mst_Id = int.Parse((item.FindControl("lblVender_Pkg_Mst_Id") as Label).Text);
        string Package_Name = (item.FindControl("txtPackage_Name") as TextBox).Text.Trim();
        string Package_Desc = (item.FindControl("txtPackage_Desc") as TextBox).Text.Trim();
        string Capacity = (item.FindControl("txtCapacity") as TextBox).Text.Trim();
        string Package_Price = (item.FindControl("txtPackage_Price") as TextBox).Text.Trim();
        //string Package_Ratings = (item.FindControl("txtPackage_Ratings") as TextBox).Text.Trim();
        //string Discount_Prct = (item.FindControl("txtDiscount_Prct") as TextBox).Text.Trim();
        //string Commision_Prct = (item.FindControl("txtCommision_Prct") as TextBox).Text.Trim();
        string Package_Img_Path = (item.FindControl("imageFilePath") as System.Web.UI.HtmlControls.HtmlInputFile).Value.Trim();
        int CuisinesType = (int.Parse((item.FindControl("hCuisinesType") as HiddenField).Value));
        int DishType = int.Parse((item.FindControl("hDishType") as HiddenField).Value);


        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Master_CRUD"))
            {
                FileStream FS = new FileStream(Package_Img_Path, FileMode.Open, FileAccess.Read);
                byte[] img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
                cmd.Parameters.AddWithValue("@Vendor_Id", ddVendor_Name1.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Type_CD", ddVendorType.SelectedValue);
                cmd.Parameters.AddWithValue("@Package_Name", Package_Name);
                cmd.Parameters.AddWithValue("@Package_Desc", Package_Desc);
                cmd.Parameters.AddWithValue("@Capacity", Capacity);
                cmd.Parameters.AddWithValue("@Package_Price", Package_Price);
                //cmd.Parameters.AddWithValue("@Package_Ratings", Package_Ratings);
                //cmd.Parameters.AddWithValue("@Discount_Prct", Discount_Prct);
                //cmd.Parameters.AddWithValue("@Commision_Prct", Commision_Prct);
                cmd.Parameters.AddWithValue("@Cuisines_Type_ID", CuisinesType);
                cmd.Parameters.AddWithValue("@Dish_Type_Id", DishType);
                
                cmd.Parameters.AddWithValue("@Package_Img_Path", Package_Img_Path);
                cmd.Parameters.Add("@Image_Blob", SqlDbType.Image).Value = img;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        this.BindRepeater();
    }
    protected void OnCancel(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        this.ToggleElements(item, false);
    }
    protected void OnDelete(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        int Vender_Pkg_Mst_Id = int.Parse((item.FindControl("lblVender_Pkg_Mst_Id") as Label).Text);

        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Master_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        this.BindRepeater();
    }

    protected void ddVendor_Name1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindRepeater();
    }
}