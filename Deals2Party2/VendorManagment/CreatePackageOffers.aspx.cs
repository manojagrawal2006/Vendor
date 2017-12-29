using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CreatePackageOffers : System.Web.UI.Page
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
    
    protected void ddPackageName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtPackage = GetData(String.Format("select * from Vendor_Caterer_Package_Master WHERE Vendor_Id={0} and Vender_Pkg_Mst_Id={1}", ddVendor_Name1.SelectedValue, ddPackageName.SelectedValue));
        lblPackageDesc.Text = dtPackage.Rows[0]["Package_Desc"].ToString();
        lblBasePrice.Text = dtPackage.Rows[0]["Package_Price"].ToString();
        this.BindRepeater();
    }


    protected void ddVendor_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddPackageName.DataSource = GetData(String.Format("select * from Vendor_Caterer_Package_Master WHERE Vendor_Id={0}", ddVendor_Name1.SelectedValue));
        ddPackageName.DataBind();
    }
    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Offers_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", ddPackageName.SelectedValue);
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
        txtOffer_Discount_Prct.Text = string.Empty;
        txtOffer_Price.Text = string.Empty;
        txtRangeFrom.Text = string.Empty;
        txtRangeTo.Text = string.Empty;
    }
    protected void Insert(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Offers_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", ddPackageName.SelectedValue);
                cmd.Parameters.AddWithValue("@RangeFrom", txtRangeFrom.Text);
                cmd.Parameters.AddWithValue("@RangeTo", txtRangeTo.Text);
                cmd.Parameters.AddWithValue("@Offer_Price", txtOffer_Price.Text);
                cmd.Parameters.AddWithValue("@Offer_Discount_Prct", txtOffer_Discount_Prct.Text);
                cmd.Parameters.AddWithValue("@Deliveryupto", txtDeliveryupto.Text);
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
        item.FindControl("lblRangeFrom").Visible = !isEdit;
        item.FindControl("lblRangeTo").Visible = !isEdit;
        item.FindControl("lblOffer_Price").Visible = !isEdit;
        item.FindControl("lblOffer_Discount_Prct").Visible = !isEdit;
        item.FindControl("lblDeliveryupto").Visible = !isEdit;


        
        //Toggle TextBoxes.
        item.FindControl("txtRangeFrom").Visible = isEdit;
        item.FindControl("txtRangeTo").Visible = isEdit;
        item.FindControl("txtOffer_Price").Visible = isEdit;
        item.FindControl("txtOffer_Discount_Prct").Visible = isEdit;
        item.FindControl("txtDeliveryupto").Visible = isEdit;
    }
    protected void OnUpdate(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        int Vendor_Caterer_Package_Offers = int.Parse((item.FindControl("lblVendor_Caterer_Package_Offers") as HiddenField).Value);
        string RangeFrom = (item.FindControl("txtRangeFrom") as TextBox).Text.Trim();
        string RangeTo = (item.FindControl("txtRangeTo") as TextBox).Text.Trim();
        string Offer_Price = (item.FindControl("txtOffer_Price") as TextBox).Text.Trim();
        string Offer_Discount_Prct = (item.FindControl("txtOffer_Discount_Prct") as TextBox).Text.Trim();
        string Deliveryupto = (item.FindControl("txtDeliveryupto") as TextBox).Text.Trim();
        

        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Offers_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Offers", Vendor_Caterer_Package_Offers);
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", ddPackageName.SelectedValue);
                cmd.Parameters.AddWithValue("@RangeFrom", RangeFrom);
                cmd.Parameters.AddWithValue("@RangeTo", RangeTo);
                cmd.Parameters.AddWithValue("@Offer_Price", Offer_Price);
                cmd.Parameters.AddWithValue("@Offer_Discount_Prct", Offer_Discount_Prct);
                cmd.Parameters.AddWithValue("@Deliveryupto", Deliveryupto);
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
        int Vendor_Caterer_Package_Offers = int.Parse((item.FindControl("lblVendor_Caterer_Package_Offers") as HiddenField).Value);

        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Offers_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Offers", Vendor_Caterer_Package_Offers);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        this.BindRepeater();
    }

}