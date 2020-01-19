using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace FormulaOneWebApp.administration
{
    public partial class edit_driver : System.Web.UI.Page
    {
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;
        public static int DriverID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DriverID"] == null)
            {
                Response.Redirect("manage-drivers.aspx");
            }
            else
            {
                if(!IsPostBack)
                {
                    PopulateDropDown();                    
                }

                DriverID = Convert.ToInt32(Session["DriverID"]);
                Get_Driver_Details();
            }
        }

        public void PopulateDropDown()
        {
            SqlConnection Conn = new SqlConnection(ConnString);

            SqlCommand GetTeamscmd = new SqlCommand("[GetData].[Get_Teams]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            Conn.Open();

            using (SqlDataReader ddlTeams = GetTeamscmd.ExecuteReader())
            {
                try
                {
                    x_driver_team.DataSource = ddlTeams;
                    x_driver_team.DataValueField = "Team_ID";
                    x_driver_team.DataTextField = "Team_Name";
                    x_driver_team.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            SqlCommand GetCountriescmd = new SqlCommand("[GetData].[Get_Countries]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader ddlCountries = GetCountriescmd.ExecuteReader())
            {
                try
                {
                    x_driver_nationality.DataSource = ddlCountries;
                    x_driver_nationality.DataValueField = "Country_ID";
                    x_driver_nationality.DataTextField = "printable_name";
                    x_driver_nationality.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            //Close the connection to the database
            Conn.Close();
        }

        public void Get_Driver_Details()
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                try
                {
                    con.Open();

                    SqlCommand GetDriverDetails = new SqlCommand("[GetData].[Get_Driver_Details]", con);
                    GetDriverDetails.CommandType = CommandType.StoredProcedure;

                    GetDriverDetails.Parameters.Add("@Driver_ID", SqlDbType.VarChar, 100).Value = DriverID;

                    SqlDataReader reader = GetDriverDetails.ExecuteReader();

                    DataTable t1 = new DataTable();

                    t1.Load(reader);

                    if (t1.Rows.Count > 0)
                    {
                        x_driver_first_name.Text = t1.Rows[0]["Forename"].ToString();
                        x_driver_last_name.Text = t1.Rows[0]["Surname"].ToString();
                        x_driver_nationality.SelectedValue = t1.Rows[0]["Country_ID"].ToString();
                        x_driver_team.SelectedValue = t1.Rows[0]["Team_ID"].ToString();                       

                    }

                    con.Close();

                }

                catch (Exception ex)
                {
                    string Error = ex.Message.ToString();
                }
            }
        }

        protected void x_save_btn_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            using SqlConnection Conn = new SqlConnection(ConnString);
            try
            {
                Conn.Open();

                SqlCommand SaveResults = new SqlCommand("[dbo].[Update_Driver]", Conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SaveResults.Parameters.Add("@Driver_ID", SqlDbType.Int);
                SaveResults.Parameters["@Driver_ID"].Value = DriverID;

                SaveResults.Parameters.Add("@Forename", SqlDbType.NVarChar, (50));
                SaveResults.Parameters["@Forename"].Value = x_driver_first_name.Text;

                SaveResults.Parameters.Add("@Surname", SqlDbType.NVarChar, (50));
                SaveResults.Parameters["@Surname"].Value = x_driver_last_name.Text;

                SaveResults.Parameters.Add("@Country_ID", SqlDbType.Int);
                SaveResults.Parameters["@Country_ID"].Value = x_driver_nationality.SelectedValue;

                SaveResults.Parameters.Add("@Team_ID", SqlDbType.Int);
                SaveResults.Parameters["@Team_ID"].Value = x_driver_team.SelectedValue;

                SaveResults.ExecuteNonQuery();

                Conn.Close();

            }

            catch (Exception ex)
            {
                string ErrorThrown = ex.Message.ToString();
            }
        }

    }
}