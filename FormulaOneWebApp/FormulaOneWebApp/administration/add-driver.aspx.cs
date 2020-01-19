using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FormulaOneWebApp.admin
{
    public partial class add_driver : System.Web.UI.Page
    {
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PopulateDropDown();
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

        protected void x_save_btn_Click(object sender, EventArgs e)
        {
            
            var Fname = x_driver_first_name.Text;
            var Sname = x_driver_last_name.Text;
            int Team = Convert.ToInt32(x_driver_team.SelectedValue);
            int Nationality = Convert.ToInt32(x_driver_nationality.SelectedValue);

            //Need to make sure that the result doesn't already exists
            bool DriverExists = DriverCheck(Fname,Sname,Team,Nationality);

            if (DriverExists == false)
            {
                SaveData();
            }
            else
            {
                return;
            }
        }

        private bool DriverCheck(string Fname, string Sname, int Team, int Nationality)
        {
            bool Exists = false;

            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    Conn.Open();

                    using SqlCommand SaveResults = new SqlCommand("[dbo].[Driver_Check]", Conn);
                    SaveResults.CommandType = CommandType.StoredProcedure;

                    SaveResults.Parameters.Add("@Forename", SqlDbType.NVarChar,(50));
                    SaveResults.Parameters["@Forename"].Value = Fname;

                    SaveResults.Parameters.Add("@Surname", SqlDbType.NVarChar,(50));
                    SaveResults.Parameters["@Surname"].Value = Sname;

                    SaveResults.Parameters.Add("@Team_ID", SqlDbType.Int);
                    SaveResults.Parameters["@Team_ID"].Value = Team;

                    SaveResults.Parameters.Add("@Country_ID", SqlDbType.Int);
                    SaveResults.Parameters["@Country_ID"].Value = Nationality;

                    SaveResults.Parameters.Add("@State", SqlDbType.Int).Direction = ParameterDirection.Output;

                    SaveResults.ExecuteNonQuery();

                    int State = Convert.ToInt32(SaveResults.Parameters["@State"].Value);

                    Conn.Close();

                    if (State == 1)
                    {
                        Exists = true;
                    }
                    else
                    {
                        Exists = false;
                    }
                }

                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            return Exists;
        }

        public void SaveData()
        {
            using SqlConnection Conn = new SqlConnection(ConnString);
            try
            {
                Conn.Open();

                SqlCommand SaveResults = new SqlCommand("[dbo].[Insert_Driver]", Conn)
                {
                    CommandType = CommandType.StoredProcedure
                };                

                SaveResults.Parameters.Add("@Forename", SqlDbType.NVarChar,(50));
                SaveResults.Parameters["@Forename"].Value = x_driver_first_name.Text;

                SaveResults.Parameters.Add("@Surname", SqlDbType.NVarChar, (50));
                SaveResults.Parameters["@Surname"].Value = x_driver_last_name.Text;

                SaveResults.Parameters.Add("@Country_ID", SqlDbType.Int);
                SaveResults.Parameters["@Country_ID"].Value = x_driver_nationality.SelectedValue;

                SaveResults.Parameters.Add("@Team_ID", SqlDbType.Int);
                SaveResults.Parameters["@Team_ID"].Value = x_driver_team.SelectedValue;

                //SaveResults.Parameters.Add("@State", SqlDbType.Int).Direction = ParameterDirection.Output;

                SaveResults.ExecuteNonQuery();

                //int State = Convert.ToInt32(SaveResults.Parameters["@State"].Value);

                Conn.Close();

                //if (State == 1)
                //{
                //    var Winning = "True";
                //}
                //else
                //{
                //    var Winning = "False";
                //}

            }

            catch (Exception ex)
            {
                string ErrorThrown = ex.Message.ToString();                
            }
        }

    }
}