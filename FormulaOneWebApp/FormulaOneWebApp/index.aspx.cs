using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace FormulaOneWebApp
{
    public partial class index : System.Web.UI.Page
    {        
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;
        int Total;
        int Bonus_Point;
        int Total_Points;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NextRace();
                PopulateDropDown();
            }
        }

        private void NextRace()
        {
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    Conn.Open();

                    SqlCommand SaveResults = new SqlCommand("[GetData].[Get_Next_Race]", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = SaveResults.ExecuteReader())
                    {
                        DataTable t1 = new DataTable();

                        t1.Load(reader);

                        if (t1.Rows.Count > 0)
                        {
                            var GrandsPrixName = t1.Rows[0]["Grands_Prix_Name"].ToString();
                            var CircuitName = t1.Rows[0]["Circuit_Name"].ToString();
                            var RaceDateStr = Convert.ToDateTime(t1.Rows[0]["Race_Date"]);

                            x_next_race.Text = "The next race is ";
                            x_next_race.Text = x_next_race.Text + GrandsPrixName;
                            x_next_race.Text = x_next_race.Text + " taking place on ";
                            x_next_race.Text = x_next_race.Text + RaceDateStr.ToString("dd-MM-yyyy");
                            x_next_race.Text = x_next_race.Text + " at " + CircuitName;


                        }
                    }
                    Conn.Close();
                }

                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }
        }

        public void PopulateDropDown()
        {
            SqlConnection Conn = new SqlConnection(ConnString);

            SqlCommand GetDepartmentscmd = new SqlCommand("[GetData].[Get_Drivers]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            Conn.Open();

            using (SqlDataReader ddlTypeOfRequest = GetDepartmentscmd.ExecuteReader())
            {
                try
                {
                    x_driver_drop.DataSource = ddlTypeOfRequest;
                    x_driver_drop.DataValueField = "Driver_ID";
                    x_driver_drop.DataTextField = "DriverName";
                    x_driver_drop.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            SqlCommand GetRaceTypescmd = new SqlCommand("[GetData].[Get_Race_Types]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader ddlRaceType = GetRaceTypescmd.ExecuteReader())
            {
                try
                {
                    x_race_type_drop.DataSource = ddlRaceType;
                    x_race_type_drop.DataValueField = "Race_Type_ID";
                    x_race_type_drop.DataTextField = "Race_Type";
                    x_race_type_drop.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            SqlCommand GetCircuitscmd = new SqlCommand("[GetData].[Get_Circuits]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader ddlCircuits = GetCircuitscmd.ExecuteReader())
            {
                try
                {
                    x_circuits_drop.DataSource = ddlCircuits;
                    x_circuits_drop.DataValueField = "Circuit_ID";
                    x_circuits_drop.DataTextField = "Circuit_Name";
                    x_circuits_drop.DataBind();

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
            PanVerification.Visible = false;
            x_state.Text = string.Empty;

            int DriverID = Convert.ToInt32(x_driver_drop.SelectedValue);
            int TrackID = Convert.ToInt32(x_circuits_drop.SelectedValue);
            int RaceType = Convert.ToInt32(x_race_type_drop.SelectedValue);
            var Date = x_date.Text;

            DateTime RaceDate = DateTime.ParseExact(x_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //Need to make sure that the result doesn't already exists
            bool ResultExists = ResultCheck(DriverID,TrackID,RaceType,RaceDate);
            
            if (ResultExists == false)
            {
                SaveData();
            } else
            {
                x_state.Text = "Result already exists";
                PanVerification.Visible = true;

                return;
            }
        }

        public void SaveData()
        {
            using SqlConnection Conn = new SqlConnection(ConnString);
            try
            {
                Conn.Open();

                SqlCommand SaveResults = new SqlCommand("[dbo].[Insert_Race_Data]", Conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                DateTime StartDate = DateTime.ParseExact(x_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                SaveResults.Parameters.Add("@Race_Date", SqlDbType.DateTime);
                SaveResults.Parameters["@Race_Date"].Value = StartDate;

                SaveResults.Parameters.Add("@Driver_ID", SqlDbType.Int);
                SaveResults.Parameters["@Driver_ID"].Value = x_driver_drop.SelectedValue;

                SaveResults.Parameters.Add("@Circuit_ID", SqlDbType.Int);
                SaveResults.Parameters["@Circuit_ID"].Value = x_circuits_drop.SelectedValue;

                int position = Convert.ToInt32(x_position_drop.SelectedValue);

                var Retired = x_retired.SelectedValue;

                //If the driver retired from the race set the final position to zero
                if (Retired == "1")
                {
                    SaveResults.Parameters.Add("@Final_Position", SqlDbType.Int);
                    SaveResults.Parameters["@Final_Position"].Value = 0;
                }
                //Else set the position to actual finish position for that driver
                else
                {
                    SaveResults.Parameters.Add("@Final_Position", SqlDbType.Int);
                    SaveResults.Parameters["@Final_Position"].Value = position;
                }

                var RaceType = x_race_type_drop.SelectedValue;
                SaveResults.Parameters.Add("@Points", SqlDbType.Int);

                //Points are only obtained during race, pre race you get nothing.
                if (RaceType != "5")
                {
                    Total_Points = 0;
                    SaveResults.Parameters["@Points"].Value = Total_Points;
                }
                else
                {
                    Total_Points = Points(position);
                    
                    if (position <= 10)
                    {
                        Bonus_Point = FastestLapPoints();
                        Total_Points += Bonus_Point;
                    }

                    SaveResults.Parameters["@Points"].Value = Total_Points;
                }                               

                SaveResults.Parameters.Add("@Race_Type", SqlDbType.Int);
                SaveResults.Parameters["@Race_Type"].Value = x_race_type_drop.SelectedValue;

                SaveResults.Parameters.Add("@State", SqlDbType.Int).Direction = ParameterDirection.Output;

                SaveResults.ExecuteNonQuery();

                int State = Convert.ToInt32(SaveResults.Parameters["@State"].Value);

                Conn.Close();

                if (State == 1)
                {
                    x_state.Text = "Record Sucessfully Inserted";
                    PanVerification.Visible = true;

                }
                else
                {
                    PanVerification.Visible = true;
                    x_state.Text = "Record wasn't inserted";

                }

            }

            catch (Exception ex)
            {
                string ErrorThrown = ex.Message.ToString();
                PanVerification.Visible = true;
                x_state.Text = "There was a problem saving the record, the error procided was " + ErrorThrown;
                x_state.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool ResultCheck(int DriverID, int TrackID,int RaceType, DateTime RaceDate)
        {
            bool Exists = false;

            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    Conn.Open();

                    using SqlCommand SaveResults = new SqlCommand("[dbo].[Check_Race_Results]", Conn);
                    SaveResults.CommandType = CommandType.StoredProcedure;

                    SaveResults.Parameters.Add("@Race_Date", SqlDbType.DateTime);
                    SaveResults.Parameters["@Race_Date"].Value = RaceDate;

                    SaveResults.Parameters.Add("@Driver_ID", SqlDbType.Int);
                    SaveResults.Parameters["@Driver_ID"].Value = DriverID;

                    SaveResults.Parameters.Add("@Race_Type", SqlDbType.Int);
                    SaveResults.Parameters["@Race_Type"].Value = RaceType;

                    SaveResults.Parameters.Add("@Circuit_ID", SqlDbType.Int);
                    SaveResults.Parameters["@Circuit_ID"].Value = TrackID;

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

        private int Points(int position)
        {
            switch (position)
            {
                case 0:
                    Total = 0;
                    break;
                case 1:
                    Total = 25;
                    break;
                case 2:
                    Total = 18;
                    break;
                case 3:
                    Total = 15;
                    break;
                case 4:
                    Total = 12;
                    break;
                case 5:
                    Total = 10;
                    break;
                case 6:
                    Total = 8;
                    break;
                case 7:
                    Total = 6;
                    break;
                case 8:
                    Total = 4;
                    break;
                case 9:
                    Total = 2;
                    break;
                case 10:
                    Total = 1;
                    break;
                case 11:
                    Total = 0;
                    break;
                case 12:
                    Total = 0;
                    break;
                case 13:
                    Total = 0;
                    break;
                case 14:
                    Total = 0;
                    break;
                case 15:
                    Total = 0;
                    break;
                case 16:
                    Total = 0;
                    break;
                case 17:
                    Total = 0;
                    break;
                case 18:
                    Total = 0;
                    break;
                case 19:
                    Total = 0;
                    break;

            }

            return Total;
        }

        private int FastestLapPoints()
        {
            bool FastestLap = x_fastest_lap_chk.Checked;

            int BonusPoint = 0;

            if (FastestLap == true)
            {
                BonusPoint = 1;
            }

            return BonusPoint;

        }

        protected void x_view_results_Click(object sender, EventArgs e)
        {
            Response.Redirect("results.aspx", true);
        }

        protected void x_clear_btn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            x_date.Text = string.Empty;

            PanVerification.Visible = false;
            x_state.Text = string.Empty;
        }

        protected void x_admin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/administration/index.aspx");
        }
    }

}