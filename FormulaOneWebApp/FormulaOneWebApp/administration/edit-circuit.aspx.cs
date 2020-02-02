using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FormulaOneWebApp.administration
{
    public partial class edit_circuit : System.Web.UI.Page
    {
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;
        public static int CircuitID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CircuitID"] == null)
            {
                Response.Redirect("manage-circuits.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    PopulateDropDown();
                }

                CircuitID = Convert.ToInt32(Session["CircuitID"]);
                Get_Circuit_Details();
            }
        }

        public void PopulateDropDown()
        {
            SqlConnection Conn = new SqlConnection(ConnString);

            SqlCommand GetTeamscmd = new SqlCommand("[GetData].[Get_Country]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            Conn.Open();

            using (SqlDataReader ddlTeams = GetTeamscmd.ExecuteReader())
            {
                try
                {
                    x_circuit_country.DataSource = ddlTeams;
                    x_circuit_country.DataValueField = "Country_ID";
                    x_circuit_country.DataTextField = "printable_name";
                    x_circuit_country.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            SqlCommand GetCircuitTypescmd = new SqlCommand("[GetData].[Circuit_Types]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader ddlCircuitTypes = GetCircuitTypescmd.ExecuteReader())
            {
                try
                {
                    x_circuit_type.DataSource = ddlCircuitTypes;
                    x_circuit_type.DataValueField = "CircuitType_ID";
                    x_circuit_type.DataTextField = "Circuit_Type";
                    x_circuit_type.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            SqlCommand GetCitycmd = new SqlCommand("[GetData].[Get_City]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader ddlCity = GetCitycmd.ExecuteReader())
            {
                try
                {

                    x_circuit_city.DataSource = ddlCity;
                    x_circuit_city.DataValueField = "City_ID";
                    x_circuit_city.DataTextField = "printable_name";
                    x_circuit_city.DataBind();

                }
                catch (Exception ex)
                {
                    string ErrorThrown = ex.Message.ToString();
                }
            }

            //Close the connection to the database
            Conn.Close();
        }

        public void Get_Circuit_Details()
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                try
                {
                    con.Open();

                    SqlCommand GetDriverDetails = new SqlCommand("[GetData].[Get_Circuit_Details]", con);
                    GetDriverDetails.CommandType = CommandType.StoredProcedure;

                    GetDriverDetails.Parameters.Add("@Circuit_ID", SqlDbType.VarChar, 100).Value = CircuitID;

                    SqlDataReader reader = GetDriverDetails.ExecuteReader();

                    DataTable t1 = new DataTable();

                    t1.Load(reader);

                    if (t1.Rows.Count > 0)
                    {
                        x_circuit_name.Text = t1.Rows[0]["Circuit_Name"].ToString();
                        x_grand_prix_name.Text = t1.Rows[0]["Grands_Prix_Name"].ToString();
                        x_circuit_length.Text = t1.Rows[0]["Last_length_used"].ToString();
                        x_circuit_type.SelectedValue = t1.Rows[0]["CircuitType_ID"].ToString();
                        x_circuit_country.SelectedValue = t1.Rows[0]["Country_ID"].ToString();
                        x_circuit_city.SelectedValue = t1.Rows[0]["City_ID"].ToString();                                              

                    }

                    con.Close();

                }

                catch (Exception ex)
                {
                    string Error = ex.Message.ToString();
                }
            }
            Session.Remove("CircuitID");
        }

    }
}