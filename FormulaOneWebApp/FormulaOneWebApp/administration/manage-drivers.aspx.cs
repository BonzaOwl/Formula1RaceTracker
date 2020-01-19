using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormulaOneWebApp.administration
{
    public partial class manage_drivers : System.Web.UI.Page
    {
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadDrivers();
            }            
        }

        protected void LoadDrivers()

        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand("[GetData].[Get_Drivers_Detailed]", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                da.Fill(dt);

                x_list_all_drivers.DataSource = dt;
                x_list_all_drivers.DataBind();

            }
        }

        protected void x_edit_driver_Click(object sender, EventArgs e)
        {
            var CloseLink = (Control)sender;
            GridViewRow row = (GridViewRow)CloseLink.NamingContainer;

            int Driver_ID = Convert.ToInt32(row.Cells[0].Text);

            Session["DriverID"] = Driver_ID;

            Response.Redirect("edit-driver.aspx");            

        }

        protected void x_retire_driver_Click(object sender, EventArgs e)
        {            
            var CloseLink = (Control)sender;
            GridViewRow row = (GridViewRow)CloseLink.NamingContainer;

            int Driver_ID = Convert.ToInt32(row.Cells[0].Text);

            using (SqlConnection con = new SqlConnection(ConnString))
            {

                SqlCommand cmd = new SqlCommand("[dbo].[Retire_Driver]", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Driver_ID", SqlDbType.Int);
                cmd.Parameters["@Driver_ID"].Value = Driver_ID;                          

                try
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    string Error = ex.Message.ToString();
                }
            }

        }

        protected void x_list_all_drivers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton Retire = e.Row.FindControl("x_retire_driver") as LinkButton;

                //If the driver's retired don't show the button to retire them
                if (e.Row.Cells[3].Text.ToString() == "Retired")
                {
                    //Hide link button when driver is retired
                    Retire.Visible = false;
                }
            }                
        }
    }
}