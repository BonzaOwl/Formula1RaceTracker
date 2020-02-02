using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace FormulaOneWebApp
{
    public partial class results : System.Web.UI.Page
    {

        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                x_race_results_DataBind();
            }          

        }

        protected void x_race_results_DataBind()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand("[GetData].[Get_Results]", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    da.Fill(dt);

                    x_race_results.DataSource = dt;
                    x_race_results.DataBind();

                }

                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();

                }
            }
        }

        protected void x_results_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx", true);
        }
    }
}