using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CreatePackage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (! this.IsPostBack)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int Vender_Pkg_Mst_Id = int.Parse(ddPackageName.SelectedValue);

        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Menu_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
                cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Menu_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Course_Type_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Dish_ID", DBNull.Value);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        foreach (GridViewRow row in gvCustomers.Rows)
        {
            int Course_Type_ID = int.Parse((row.FindControl("lblCourse_Type_ID") as HiddenField).Value);
            int MaxSelection = (row.FindControl("txtMaxSelection") as TextBox).Text==string.Empty ? 0: Convert.ToInt32((row.FindControl("txtMaxSelection") as TextBox).Text);
  

            GridView gvOrders;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Course_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id" , Vender_Pkg_Mst_Id);
                    cmd.Parameters.AddWithValue("@Course_Type_ID"         , Course_Type_ID);
                    cmd.Parameters.AddWithValue("@pkg_Course_Max_Selection", MaxSelection);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            gvOrders = row.FindControl("gvOrders") as GridView;
            foreach (GridViewRow rowChild in gvOrders.Rows)
            {
                bool DishIDValue = (rowChild.FindControl("chkDishID") as CheckBox).Checked;

                if (!DishIDValue)
                    continue;

                int Dish_ID = int.Parse((rowChild.FindControl("lblDish_Id") as HiddenField).Value);
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Menu_CRUD"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "INSERT");
                        cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", Vender_Pkg_Mst_Id);
                        cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Menu_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Course_Type_ID", Course_Type_ID);
                        cmd.Parameters.AddWithValue("@Dish_ID", Dish_ID);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }


            }

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void Show_Hide_ChildGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
 
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string vendorId = ddVendor_Name1.SelectedValue;
            string courseTypeID = (row.FindControl("lblCourse_Type_ID") as HiddenField).Value;

            //int vendorId = (int) gvCustomers.DataKeys[row.RowIndex].Value;
            //int courseTypeID = (int)gvCustomers.DataKeys[row.RowIndex].Value;


            GridView gvOrders = row.FindControl("gvOrders") as GridView; gvOrders.ToolTip = vendorId.ToString();
            //    gvOrders.DataSource = getChildData(string.Format("select DM.Price, VM.Vendor_Name,CuT.Description as CuisinesName" +
            //",Ct.Description CourseName,DT.Description DishType, DM.Description DishName ,DM.Dish_Id,DM.Cuisines_Type_ID,DM.Course_Type_ID,DM.Dish_Type_Id,VM.Vendor_ID" +
            //"  from dbo.Dishes_Master DM "
            //    + " left join Vendor_Master VM on DM.Vendor_Id = VM.Vendor_Id "
            //    + " Left Join Cuisines_Type CuT on DM.Cuisines_Type_ID = CuT.Cuisines_Type_ID  "
            //    + " left join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  "
            //    + " left join Dishes_Type DT on DM.Dish_Type_Id = DT.Dish_Type_Id " +
            //    " Where VM.Vendor_ID={0} and CT.Course_Type_ID={1}", vendorId, courseTypeID));
            gvOrders.DataSource = getChildData(courseTypeID);
            gvOrders.DataBind();
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }

    protected void ddVendor_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddCourseType.DataSource = GetData(String.Format("SELECT DISTINCT ct.* FROM Dishes_Master DM JOIN Vendor_Type VT ON DM.Vendor_Id=VT.Vendor_Type_CD  " +
        //    "JOIN [Course_Type] CT ON DM.Course_Type_ID=CT.Course_Type_ID WHERE Vendor_Id={0}", ddVendor_Name1.SelectedValue));
        //ddCourseType.DataBind();
        ddPackageName.DataSource = GetData(String.Format("select * from Vendor_Package_Master WHERE Vendor_Id={0}", ddVendor_Name1.SelectedValue));
        ddPackageName.DataBind();
        //gvCustomers.DataSource = GetData(String.Format("select Distinct VM.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Vendor_Master VM on DM.Vendor_Id=VM.Vendor_Id join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  where VM.Vendor_ID ={0}", ddVendor_Name1.SelectedValue));
        //gvCustomers.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddCourseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddDishType.DataSource = GetData(String.Format("SELECT DISTINCT DT.* FROM Dishes_Master DM JOIN Vendor_Type VT ON DM.Vendor_Id=VT.Vendor_Type_CD " +
        //    " JOIN Dishes_Type DT ON DT.Dish_Type_Id=DM.Dish_Type_ID  JOIN [Course_Type] CT ON DM.Course_Type_ID=CT.Course_Type_ID " +
        //    "	WHERE Vendor_Id={0} and DM.Course_Type_ID={1}", ddVendor_Name1.SelectedValue,ddCourseType.SelectedValue));
        //ddDishType.DataBind();
                                
    }


    protected void ddDishType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //chkDishMasterList.DataSource = GetData(String.Format("SELECT DISTINCT DM.* FROM Dishes_Master DM 	JOIN Vendor_Type VT ON DM.Vendor_Id=VT.Vendor_Type_CD " +
        //    " JOIN Dishes_Type DT ON DT.Dish_Type_Id=DM.Dish_Type_ID  JOIN [Course_Type] CT ON DM.Course_Type_ID=CT.Course_Type_ID " +
        //    "WHERE Vendor_Id={0} and DM.Course_Type_ID={1} and DM.Dish_Type_ID={2}", ddVendor_Name1.SelectedValue, ddCourseType.SelectedValue,ddDishType.SelectedValue));
        //chkDishMasterList.DataBind();
    }

    protected void ddPackageName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtPackage = GetData(String.Format("select * from Vendor_Package_Master WHERE Vendor_Id={0} and Vender_Pkg_Mst_Id={1}", ddVendor_Name1.SelectedValue,ddPackageName.SelectedValue));
        lblPackageDesc.Text = dtPackage.Rows[0]["Package_Desc"].ToString();
        lblBasePrice.Text = dtPackage.Rows[0]["Package_Price"].ToString();
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Course_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", ddPackageName.SelectedValue);
                cmd.Parameters.AddWithValue("@Course_Type_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@pkg_Course_Max_Selection", DBNull.Value);
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvCustomers.DataSource = dt;
                        gvCustomers.DataBind();
                    }
                    con.Close();
                }
            }

        }
    }

    protected DataTable getChildData(String Course_Type_ID)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Menu_CRUD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Vender_Pkg_Mst_Id", ddPackageName.SelectedValue);
                cmd.Parameters.AddWithValue("@Vendor_Caterer_Package_Menu_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Course_Type_ID", Course_Type_ID);
                cmd.Parameters.AddWithValue("@Dish_ID", DBNull.Value);
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                    con.Close();
                }
            }

        }
    }

}