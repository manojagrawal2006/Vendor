using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

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
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //LinkButton imgShowHide = (sender as LinkButton);
        //GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        GridView gvOrders = (sender as GridView);
        int customerId = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Values[0]);
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM  dbo.Dishes_Master WHERE Dish_Id=@CustomerId"))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        gvCustomers.DataSource = GetData("select Distinct VM.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Vendor_Master VM on DM.Vendor_Id=VM.Vendor_Id join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  where VM.Vendor_ID =" + 1);
        gvCustomers.DataBind();
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gvOrders = (sender as GridView);
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvOrders.EditIndex)
        {
            (e.Row.Cells[0].Controls[0] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button imgShowHide = (sender as Button);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        var dishID = row.Cells[8].Text;
        var sql = "DELETE FROM  dbo.Dishes_Master WHERE Dish_Id=" + dishID;

        var connectionString = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"];
        SqlConnection con = new SqlConnection(connectionString.ToString());
        SqlCommand cmd = new SqlCommand("dbo.CreateMenu", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        con.Open();
        int k = cmd.ExecuteNonQuery();
        if (k != 0)
        {
            lblmsg.Text = "Record Inserted Succesfully into the Database at " + DateTime.Now.ToString();
            lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
            txtDishName.Value = string.Empty;
        }
        con.Close();


    }

    protected void Show_Hide_ChildGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        RepeaterItem item = (sender as ImageButton).Parent as RepeaterItem;
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
       // int Vender_Pkg_Mst_Id = int.Parse((item.FindControl("pnlOrders") as GridView).Text);
        if (imgShowHide.CommandArgument == "Show")
        {
            (item.FindControl("pnlOrders") as Panel).Visible = true;
            //row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string vendorId = ddVendor_Name.SelectedValue;
            string courseTypeID = (item.FindControl("lblCourse_Type_ID") as HiddenField).Value;

            //int vendorId = (int) gvCustomers.DataKeys[row.RowIndex].Value;
            //int courseTypeID = (int)gvCustomers.DataKeys[row.RowIndex].Value;


            Repeater gvOrders = (item.FindControl("pnlOrders") as Panel).FindControl("gvOrders") as Repeater;
            //gvOrders.ToolTip = vendorId.ToString();
            gvOrders.DataSource = GetData(string.Format("select DM.Price, VM.Vendor_Name,CuT.Description as CuisinesName" +
             ",Ct.Description CourseName,DT.Description DishType, DM.Description DishName ,DM.Dish_Id,DM.Cuisines_Type_ID,DM.Course_Type_ID,DM.Dish_Type_Id,VM.Vendor_ID, DM.Dish_Image_Path" +
             "  from dbo.Dishes_Master DM "
                 + " left join Vendor_Master VM on DM.Vendor_Id = VM.Vendor_Id "
                 + " Left Join Cuisines_Type CuT on DM.Cuisines_Type_ID = CuT.Cuisines_Type_ID  "
                 + " left join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  "
                 + " left join Dishes_Type DT on DM.Dish_Type_Id = DT.Dish_Type_Id " +
                 " Where VM.Vendor_ID={0} and CT.Course_Type_ID={1}", vendorId, courseTypeID));
            gvOrders.DataBind();
        }
        else
        {
            //row.FindControl("pnlOrders").Visible = false;
            (item.FindControl("pnlOrders") as Panel).Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }

    private void BindOrders(int customerId, int courseType, GridView gvOrders)
    {
        gvOrders.ToolTip = customerId.ToString();
        gvOrders.DataSource = GetData(string.Format("select DM.Price, VM.Vendor_Name,CuT.Description as CuisinesName" +
            ",Ct.Description CourseName,DT.Description DishType, DM.Description DishName ,DM.Dish_Id,DM.Cuisines_Type_ID,DM.Course_Type_ID,DM.Dish_Type_Id,VM.Vendor_ID" +
            "  from dbo.Dishes_Master DM "
                + " left join Vendor_Master VM on DM.Vendor_Id = VM.Vendor_Id "
                + " Left Join Cuisines_Type CuT on DM.Cuisines_Type_ID = CuT.Cuisines_Type_ID  "
                + " left join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  "
                + " left join Dishes_Type DT on DM.Dish_Type_Id = DT.Dish_Type_Id " +
                " Where VM.Vendor_ID={0} and CT.Course_Type_ID={1}", customerId, courseType));
        gvOrders.DataBind();
    }
    protected void OnChildGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvOrders = (sender as GridView);
        gvOrders.PageIndex = e.NewPageIndex;
        BindOrders(1, 1, gvOrders);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

      var connectionString = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"];
        SqlConnection con = new SqlConnection(connectionString.ToString());
        SqlCommand cmd = new SqlCommand("dbo.CreateMenu", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("Vendor_Id", ddVendor_Name.SelectedValue);
        cmd.Parameters.AddWithValue("Cuisines_Type_ID", ddCuisinesType.SelectedValue);
        cmd.Parameters.AddWithValue("Course_Type_ID", ddCourseType.SelectedValue);
        cmd.Parameters.AddWithValue("Dish_Type_ID", ddDishType.SelectedValue);
        cmd.Parameters.AddWithValue("Description", txtDishName.Value);
        cmd.Parameters.AddWithValue("Price", txtPrice.Text);
        cmd.Parameters.AddWithValue("Dish_Image_Path", imageFilePath1.Value);
        con.Open();
        int k = cmd.ExecuteNonQuery();
        if (k != 0)
        {
            lblmsg.Text = "Record Inserted Succesfully into the Database at " + DateTime.Now.ToString();
            lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
            txtDishName.Value = string.Empty;
        }
        con.Close();

         gvCustomers.DataSource = GetData("select Distinct VM.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Vendor_Master VM on DM.Vendor_Id=VM.Vendor_Id join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  where VM.Vendor_ID =" + 1);
            gvCustomers.DataBind();

        ddVendor_Name_SelectedIndexChanged(sender, e);
    }

    protected void gvOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DM.Dish_Id,DM.Cuisines_Type_ID, DM.Course_Type_ID,DM.Dish_Type_Id
                GridView gvOrders = (sender as GridView);
        //var v1 = gvOrders.SelectedRow.Cells[0].Text;
        //var v2 = gvOrders.SelectedRow.Cells[1].Text;
        //var v3 = gvOrders.SelectedRow.Cells[2].Text;
        //var v4 = gvOrders.SelectedRow.Cells[3].Text;
        //var v5 = gvOrders.SelectedRow.Cells[4].Text;
        //var v6 = gvOrders.SelectedRow.Cells[5].Text;
        //var v7 = gvOrders.SelectedRow.Cells[6].Text;
        var v8 = gvOrders.SelectedRow.Cells[7].Text;
        ddVendor_Name.SelectedValue= gvOrders.SelectedRow.Cells[7].Text;
        ddCuisinesType.SelectedValue = gvOrders.SelectedRow.Cells[9].Text;
        ddCourseType.SelectedValue = gvOrders.SelectedRow.Cells[10].Text;
        ddDishType.SelectedValue = gvOrders.SelectedRow.Cells[11].Text;
        txtDishName.Value = gvOrders.SelectedRow.Cells[5].Text;
        txtPrice.Text = gvOrders.SelectedRow.Cells[6].Text;
        lblDishID.Value= gvOrders.SelectedRow.Cells[8].Text;
    }

    protected void selct_SelectedIndexChanged1(object sender, GridViewSelectEventArgs e)
    {
        //txt_ID.Text = GridView1.Rows[e.NewSelectedIndex].Cells[1].Text;
        //txt_FN.Text = GridView1.Rows[e.NewSelectedIndex].Cells[2].Text;
        //txt_LN.Text = GridView1.Rows[e.NewSelectedIndex].Cells[3].Text;
        //txt_CT.Text = GridView1.Rows[e.NewSelectedIndex].Cells[4].Text;
    }

    protected void ddVendor_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList gvOrders = (sender as DropDownList);
       
        gvCustomers.DataSource = GetData("select Distinct Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID where Vendor_ID =" + gvOrders.SelectedValue.ToString());
        gvCustomers.DataBind();
    }

    //protected void gvCustomers_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    //gvCustomers.DataSource = GetData("select Distinct VT.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID where Vendor_ID =" + gvOrders.SelectedValue.ToString());
    //    gvCustomers.DataSource = GetData("select Distinct VM.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Vendor_Master VM on DM.Vendor_Id=VM.Vendor_Id join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  where VM.Vendor_ID =" + ddVendor_Name.SelectedValue.ToString());
    //    gvCustomers.DataBind();
    //}

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
//        Dish_Id Vendor_Id   Cuisines_Type_ID Course_Type_ID  Dish_Type_ID Description Price
//1   1   6   1   1   Cream of Tomato 110
   //     var dishID = lblDishID.Text;
        var sql = string.Format("update dbo.Dishes_Master set Vendor_Id={0}, Cuisines_Type_ID={1}, Course_Type_ID={2}, " +
            " Dish_Type_ID={3}, Description='{4}', Price={5} WHERE Dish_Id={6}" , ddVendor_Name.SelectedValue,ddCuisinesType.SelectedValue,ddCourseType.SelectedValue,
            ddDishType.SelectedValue,txtDishName.Value,txtPrice.Text,lblDishID.Value);

        var connectionString = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"];
        SqlConnection con = new SqlConnection(connectionString.ToString());
        SqlCommand cmd = new SqlCommand("dbo.CreateMenu", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        con.Open();
        int k = cmd.ExecuteNonQuery();
        if (k != 0)
        {
            lblmsg.Text = "Record Inserted Succesfully deleted from Database at " + DateTime.Now.ToString();
            lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
            txtDishName.Value = string.Empty;
        }
        con.Close();

        gvCustomers.DataSource = GetData("select Distinct VM.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Vendor_Master VM on DM.Vendor_Id=VM.Vendor_Id join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  where VM.Vendor_ID =" + 1);
        gvCustomers.DataBind();
    }

    protected void Insert(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Master_CRUD"))
            {
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Action", "INSERT");
                //cmd.Parameters.AddWithValue("@Vendor_Id", ddVendor_Name.SelectedValue);
                //cmd.Parameters.AddWithValue("@Package_Name", txtPackage_Name.Text);
                //cmd.Parameters.AddWithValue("@Package_Desc", txtPackage_Desc.Text);
                //cmd.Parameters.AddWithValue("@Capacity", txtCapacity.Text);
                //cmd.Parameters.AddWithValue("@Package_Price", txtPackage_Price.Text);
                ////cmd.Parameters.AddWithValue("@Package_Ratings", txtPackage_Ratings.Text);
                ////cmd.Parameters.AddWithValue("@Discount_Prct", txtDiscount_Prct.Text);
                ////cmd.Parameters.AddWithValue("@Commision_Prct", txtCommision_Prct.Text);
                //cmd.Parameters.AddWithValue("@Package_Img_Path", txtPackage_Img_Path.Text);
                //cmd.Connection = con;
                //con.Open();
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
        item.FindControl("lblCuisinesType").Visible = !isEdit;
        item.FindControl("lblCourseType").Visible = !isEdit;
        item.FindControl("lblDishType").Visible = !isEdit;
        item.FindControl("lbltDishName").Visible = !isEdit;
        //item.FindControl("lblPrice").Visible  = !isEdit;
        //item.FindControl("lblDiscount_Prct").Visible    = !isEdit;
        //item.FindControl("lblCommision_Prct").Visible   = !isEdit;
        item.FindControl("lblPrice").Visible = !isEdit;


        //Toggle TextBoxes.
        item.FindControl("imageFilePath").Visible = isEdit;
        item.FindControl("ddCuisinesType").Visible = isEdit;
        item.FindControl("ddCourseType").Visible = isEdit;
        item.FindControl("ddDishType").Visible = isEdit;
        item.FindControl("txtDishName").Visible = isEdit;
        item.FindControl("txtPrice").Visible = isEdit;
            (item.FindControl("ddCuisinesType") as DropDownList).SelectedIndex =(int.Parse( (item.FindControl("hCuisinesType") as HiddenField).Value) -1);
            (item.FindControl("ddCourseType") as DropDownList).SelectedIndex = int.Parse((item.FindControl("hCourseType") as HiddenField).Value)-1 ;
            (item.FindControl("ddDishType") as DropDownList).SelectedIndex = int.Parse((item.FindControl("hDishType") as HiddenField).Value) -1 ;
       
    }
    protected void OnReadImgURL(object sender, EventArgs e)
    {
    }

        protected void OnUpdate(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;

        int CuisinesType = (int.Parse((item.FindControl("hCuisinesType") as HiddenField).Value));
        int CourseType = int.Parse((item.FindControl("hCourseType") as HiddenField).Value);
        int DishType = int.Parse((item.FindControl("hDishType") as HiddenField).Value);
        string DishName = (item.FindControl("txtDishName") as TextBox).Text;
        string Price = (item.FindControl("txtPrice") as TextBox).Text;
        int DishID = int.Parse((item.FindControl("lblDishID") as HiddenField).Value);
        string Package_Img_Path = (item.FindControl("imageFilePath") as System.Web.UI.HtmlControls.HtmlInputFile).Value.Trim();

        var sql = string.Format("update dbo.Dishes_Master set Vendor_Id={0}, Cuisines_Type_ID={1}, Course_Type_ID={2}, " +
                   " Dish_Type_ID={3}, Description='{4}', Price={5},Dish_Image_Path='{6}' WHERE Dish_Id={7}", ddVendor_Name.SelectedValue,
                   CuisinesType, CourseType, DishType, DishName, Price, Package_Img_Path, DishID);

        var connectionString = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"];
        SqlConnection con = new SqlConnection(connectionString.ToString());
        SqlCommand cmd = new SqlCommand("dbo.CreateMenu", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        con.Open();
        int k = cmd.ExecuteNonQuery();
        if (k != 0)
        {
            lblmsg.Text = "Record Inserted Succesfully deleted from Database at " + DateTime.Now.ToString();
            lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
            txtDishName.Value = string.Empty;
        }
        con.Close();

        //gvCustomers.DataSource = GetData("select Distinct VM.Vendor_ID, CT.Course_Type_ID,Ct.Description CourseName  from dbo.Dishes_Master DM join Vendor_Master VM on DM.Vendor_Id=VM.Vendor_Id join Course_Type CT on DM.Course_Type_ID = CT.Course_Type_ID  where VM.Vendor_ID =" + 1);
        //gvCustomers.DataBind();

        ImageButton imgShowHide = (item.Parent.Parent.FindControl("imgShow") as ImageButton);

        Show_Hide_ChildGrid(imgShowHide, e);
        Show_Hide_ChildGrid(imgShowHide, e);
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

        int DishID = int.Parse((item.FindControl("lblDishID") as HiddenField).Value);
        var sql = "DELETE FROM  dbo.Dishes_Master WHERE Dish_Id=" + DishID;

        var connectionString = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"];
        SqlConnection con = new SqlConnection(connectionString.ToString());
        SqlCommand cmd = new SqlCommand("dbo.CreateMenu", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        con.Open();
        int k = cmd.ExecuteNonQuery();
        if (k != 0)
        {
            lblmsg.Text = "Record Inserted Succesfully into the Database at " + DateTime.Now.ToString();
            lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
            txtDishName.Value = string.Empty;
        }
        con.Close();
        ImageButton imgShowHide = (item.Parent.Parent.FindControl("imgShow") as ImageButton);

        Show_Hide_ChildGrid(imgShowHide, e);
        Show_Hide_ChildGrid(imgShowHide, e);
    }
    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["Deals2PartyDBConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Vendor_Caterer_Package_Master_CRUD"))
            {
                //cmd.Parameters.AddWithValue("@Action", "SELECT");
                //cmd.Parameters.AddWithValue("@Vendor_Id", ddVendor_Name1.SelectedValue);
                //using (SqlDataAdapter sda = new SqlDataAdapter())
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Connection = con;
                //    sda.SelectCommand = cmd;
                //    using (DataTable dt = new DataTable())
                //    {
                //        sda.Fill(dt);
                //        Repeater1.DataSource = dt;
                //        Repeater1.DataBind();
                //    }
                //}
            }
        }
    }

}