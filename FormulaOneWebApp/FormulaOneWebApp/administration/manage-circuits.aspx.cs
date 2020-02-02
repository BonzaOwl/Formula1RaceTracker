using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormulaOneWebApp.administration
{
    public partial class manage_circuits : System.Web.UI.Page
    {
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadCircuits();
            }
        }

        protected void LoadCircuits()

        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand("[GetData].[Get_Circuits]", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                da.Fill(dt);

                x_list_all_circuits.DataSource = dt;
                x_list_all_circuits.DataBind();

            }
        }

        protected void x_edit_driver_Click(object sender, EventArgs e)
        {
            var CloseLink = (Control)sender;
            GridViewRow row = (GridViewRow)CloseLink.NamingContainer;

            int Circuit_ID = Convert.ToInt32(row.Cells[0].Text);

            Session["CircuitID"] = Circuit_ID;

            Response.Redirect("edit-circuit.aspx");
        }
    }
}