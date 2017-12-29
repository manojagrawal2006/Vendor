using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Device.Location;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class VendorMaster : System.Web.UI.Page
{

    //  //  GooglePoint GP;
    protected void Page_Load(object sender, EventArgs e)
    {
        //        //Add event handler for PushpinMoved event
        //        //GoogleMapForASPNet1.MapClicked += new GoogleMapForASPNet.MapClickedHandler(OnMapClicked);
        //        ////lat = double.Parse(Session["Latitude"].ToString());
        //        ////lon = double.Parse(Session["Longitude"].ToString());

        //        //GP  = new GooglePoint();

        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "changeLocation(" + Session["Latitude"].ToString() + "," + Session["Longitude"].ToString() + ",false,2)", true);
        }
    }
    //        {
    //          //  if (!(string.IsNullOrEmpty(Session["Latitude"].ToString()) || string.IsNullOrEmpty(Session["Longitude"].ToString())))
    //            {


    //        //        GoogleMapForASPNet1.GoogleMapObject.APIKey = "AIzaSyC_xl6rnJXCIXbnkbBBtK6tN6kaIDdUh4c";


    //                //GP.Latitude = double.Parse(Session["Latitude"].ToString());
    //                //GP.Longitude = double.Parse(Session["Longitude"].ToString());
    //                ////Set GP as center point.
    //                //GoogleMapForASPNet1.GoogleMapObject.CenterPoint = GP;

    //                //Clear any existing
    //                //GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
    //                ////Add geocoded GP to GoogleMapObject
    //                //GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP);
    //                //GoogleMapForASPNet1.GoogleMapObject.RecenterMap = true;

    //               // GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("CenterPoint", double.Parse(lat.ToString()), double.Parse(lon.ToString()));


    //                //GoogleMapForASPNet1.Visible = true;



    //            }
    //        }

    //    }

    //    //Add event handler for Map Click event
    //    //void OnMapClicked(double dLat, double dLng)
    //    //{
    //    //    //lat = dLat; 
    //    //    //lon = dLng;
    //    //    //Print clicked map positions
    //    //    lblPushpin1.Text = "(" + dLat.ToString() + "," + dLng.ToString() + ")";
    //    //    //Generate new id for google point
    //    //    string sID = "Point1";
    //    //    GP = new GooglePoint(sID, dLat, dLng);
    //    //    GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
    //    //    GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP);
    //    //}

    protected void ddVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        var Location = string.Empty;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Master_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Vendor_Id", ddVendorName.SelectedValue);
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddVendorType.SelectedIndex = dt.Rows[0]["Vendor_Type_CD"] == DBNull.Value ? 0 : int.Parse(dt.Rows[0]["Vendor_Type_CD"].ToString()) - 1;
                            txtOwnerName.Text = dt.Rows[0]["Owner_name"].ToString();
                            txtEmail.Text = dt.Rows[0]["Email_id"].ToString();
                            txtState.Text = dt.Rows[0]["State"].ToString();
                            txtAddress.Text = dt.Rows[0]["Address"].ToString();
                            txtCity.Text = dt.Rows[0]["City"].ToString();
                            txtPin.Text = dt.Rows[0]["Pin"].ToString();
                            txtContactNo.Text = dt.Rows[0]["Contact_No"].ToString();
                            txtWebURL.Text = dt.Rows[0]["WebsiteURL"].ToString();
                            //flLogoPath. = dt.Rows[0]["LogoPath"].ToString();
                            txtRegistration.Text = dt.Rows[0]["Registration_Number"].ToString();
                            dtEstablishment.SelectedDate = Convert.ToDateTime(dt.Rows[0]["Establishment_Date"].ToString());
                            Location = dt.Rows[0]["Location"].ToString();

                            if (!string.IsNullOrEmpty(Location) && Location.IndexOf("|") > 0)
                            {
                                Session["Latitude"] = Location.Split('|').ToList()[0].ToString();
                                Session["Longitude"] = Location.Split('|').ToList()[1].ToString();
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "changeLocation(" + Location.Split('|').ToList()[0].ToString() + "," + Location.Split('|').ToList()[1].ToString() + ",false,3)", false);
                            }
                            else
                            {
                                Session["Latitude"] = Session["BaseLatitude"];
                                Session["Longitude"] = Session["BaseLongitude"];
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "changeLocation(" + Session["Latitude"].ToString() + "," + Session["Longitude"].ToString() + ",true,4)", true);
                            }
                        }
                    }
                    con.Close();
                }
            }

        }
    }

    //    protected void btnSave_Click(object sender, EventArgs e)
    //    {
    //        //int Vender_Pkg_Mst_Id = int.Parse(ddPackageName.SelectedValue);

    //        //string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
    //        //using (SqlConnection con = new SqlConnection(constr))
    //        //{
    //        //    using (SqlCommand cmd = new SqlCommand("sp_Vendor_Master_CRUD"))
    //        //    {
    //        //        cmd.CommandType = CommandType.StoredProcedure;
    //        //        cmd.Parameters.AddWithValue("@Action", "DELETE");
    //        //        cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
    //        //        cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Menu_ID", DBNull.Value);
    //        //        cmd.Parameters.AddWithValue("@Course_Type_ID", DBNull.Value);
    //        //        cmd.Parameters.AddWithValue("@Dish_ID", DBNull.Value);
    //        //        cmd.Connection = con;
    //        //        con.Open();
    //        //        cmd.ExecuteNonQuery();
    //        //        con.Close();
    //        //    }
    //        //}
    //        //foreach (GridViewRow row in gvCustomers.Rows)
    //        //{
    //        //    int Course_Type_ID = int.Parse((row.FindControl("lblCourse_Type_ID") as HiddenField).Value);
    //        //    int MaxSelection = (row.FindControl("txtMaxSelection") as TextBox).Text == string.Empty ? 0 : Convert.ToInt32((row.FindControl("txtMaxSelection") as TextBox).Text);


    //        //    GridView gvOrders;

    //        //    using (SqlConnection con = new SqlConnection(constr))
    //        //    {
    //        //        using (SqlCommand cmd = new SqlCommand("sp_Vendor_Master_CRUD"))
    //        //        {
    //        //            cmd.CommandType = CommandType.StoredProcedure;
    //        //            cmd.Parameters.AddWithValue("@Action", "INSERT");
    //        //            cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
    //        //            cmd.Parameters.AddWithValue("@Course_Type_ID", Course_Type_ID);
    //        //            cmd.Parameters.AddWithValue("@pkg_Course_Max_Selection", MaxSelection);
    //        //            cmd.Connection = con;
    //        //            con.Open();
    //        //            cmd.ExecuteNonQuery();
    //        //            con.Close();
    //        //        }
    //        //    }

    //        //    gvOrders = row.FindControl("gvOrders") as GridView;
    //        //    foreach (GridViewRow rowChild in gvOrders.Rows)
    //        //    {
    //        //        bool DishIDValue = (rowChild.FindControl("chkDishID") as CheckBox).Checked;

    //        //        if (!DishIDValue)
    //        //            continue;

    //        //        int Dish_ID = int.Parse((rowChild.FindControl("lblDish_Id") as HiddenField).Value);
    //        //        using (SqlConnection con = new SqlConnection(constr))
    //        //        {
    //        //            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Master_CRUD"))
    //        //            {
    //        //                cmd.CommandType = CommandType.StoredProcedure;
    //        //                cmd.Parameters.AddWithValue("@Action", "INSERT");
    //        //                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
    //        //                cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Menu_ID", DBNull.Value);
    //        //                cmd.Parameters.AddWithValue("@Course_Type_ID", Course_Type_ID);
    //        //                cmd.Parameters.AddWithValue("@Dish_ID", Dish_ID);
    //        //                cmd.Connection = con;
    //        //                con.Open();
    //        //                cmd.ExecuteNonQuery();
    //        //                con.Close();
    //        //            }
    //        //        }


    //        //    }

    //        //}
    //    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        var Location = string.Empty;
        TextBox Latitude = (TextBox)Master.FindControl("txtLatitude");
        TextBox Longitude = (TextBox)Master.FindControl("txtLongitude");
        using (SqlConnection con = new SqlConnection(constr))
        {   
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Master_CRUD"))
            {
                FileStream FS = new FileStream(imageFilePath.PostedFile.FileName, FileMode.Open, FileAccess.Read);
                byte[] img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@Vendor_Id",               ddVendorName.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Type_CD",          ddVendorType.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Name",             ddVendorName.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Owner_name",              txtOwnerName.Text);
                cmd.Parameters.AddWithValue("@Email_id",                txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address",                 txtAddress.Text);
                cmd.Parameters.AddWithValue("@City",                    txtCity.Text);
                cmd.Parameters.AddWithValue("@State",                   txtCity.Text);
                cmd.Parameters.AddWithValue("@Pin",                     txtPin.Text);
                cmd.Parameters.AddWithValue("@Contact_No",              txtContactNo.Text);
                cmd.Parameters.AddWithValue("@WebsiteURL",              txtWebURL.Text);
                cmd.Parameters.AddWithValue("@LogoPath",                imageFilePath.ToString());
                cmd.Parameters.AddWithValue("@Registration_Number",     txtRegistration.Text);
                cmd.Parameters.AddWithValue("@Establishment_Date",      dtEstablishment.SelectedDate);
                cmd.Parameters.AddWithValue("@Location",                Latitude.Text + "|" + Longitude.Text);
                cmd.Parameters.Add("@Logo", SqlDbType.Image).Value = img;

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        var Location = string.Empty;
        TextBox Latitude = (TextBox)Master.FindControl("txtLatitude");
        TextBox Longitude = (TextBox)Master.FindControl("txtLongitude");
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Master_CRUD"))
            {
                FileStream FS = new FileStream(imageFilePath.ToString(), FileMode.Open, FileAccess.Read);
                byte[] img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@Vendor_Id", DBNull.Value);
                cmd.Parameters.AddWithValue("@Vendor_Type_CD", ddVendorType.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Name", txtVendorName.Text);
                cmd.Parameters.AddWithValue("@Owner_name", txtOwnerName.Text);
                cmd.Parameters.AddWithValue("@Email_id", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@State", txtCity.Text);
                cmd.Parameters.AddWithValue("@Pin", txtPin.Text);
                cmd.Parameters.AddWithValue("@Contact_No", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@WebsiteURL", txtWebURL.Text);
                cmd.Parameters.AddWithValue("@LogoPath", imageFilePath.ToString());
                cmd.Parameters.AddWithValue("@Registration_Number", txtRegistration.Text);
                cmd.Parameters.AddWithValue("@Establishment_Date",dtEstablishment.SelectedDate.ToShortDateString());
                cmd.Parameters.AddWithValue("@Location", Latitude.Text + "|" + Longitude.Text);
                cmd.Parameters.Add("@Logo", SqlDbType.Image).Value = img;

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtVendorName.Visible = true;
        btnSave.Visible = true;
        ddVendorName.Visible = false;
        btnEdit.Visible = false;
        ddVendorType.SelectedIndex=0;
        txtOwnerName.Text=string.Empty;
        txtEmail.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtPin.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtWebURL.Text = string.Empty;
        txtRegistration.Text = string.Empty;
        dtEstablishment.SelectedDate = DateTime.Now.Date;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
}
