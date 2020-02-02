using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormulaOneWebApp.administration
{
    public partial class add_circuit : System.Web.UI.Page
    {
        private readonly string ConnString = ConfigurationManager.ConnectionStrings["F1RaceTracker"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateDropDown();
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

            SqlCommand GetCircuitDirectionscmd = new SqlCommand("[GetData].[Get_Circuit_Directions]", Conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader ddlCircuitDirections = GetCircuitDirectionscmd.ExecuteReader())
            {
                try
                {
                    x_circuit_direction.DataSource = ddlCircuitDirections;
                    x_circuit_direction.DataValueField = "CircuitDirection_ID";
                    x_circuit_direction.DataTextField = "Circuit_Direction";
                    x_circuit_direction.DataBind();

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
            SaveData();
        }

        public void SaveData()
        {
            using SqlConnection Conn = new SqlConnection(ConnString);
            try
            {
                Conn.Open();

                SqlCommand InsertCircuit = new SqlCommand("[dbo].[Insert_Circuit]", Conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                InsertCircuit.Parameters.Add("@CircuitName", SqlDbType.NVarChar, (60));
                InsertCircuit.Parameters["@CircuitName"].Value = x_circuit_name.Text.Trim();

                InsertCircuit.Parameters.Add("@GrandPrixName", SqlDbType.NVarChar, (70));
                InsertCircuit.Parameters["@GrandPrixName"].Value = x_grand_prix_name.Text.Trim();

                InsertCircuit.Parameters.Add("@Country_ID", SqlDbType.Int);
                InsertCircuit.Parameters["@Country_ID"].Value = x_circuit_country.SelectedValue;

                InsertCircuit.Parameters.Add("@City_ID", SqlDbType.Int);
                InsertCircuit.Parameters["@City_ID"].Value = x_circuit_city.SelectedValue;

                InsertCircuit.Parameters.Add("@Circuit_Type", SqlDbType.Int);
                InsertCircuit.Parameters["@Circuit_Type"].Value = x_circuit_type.SelectedValue;

                InsertCircuit.Parameters.Add("@CircuitDirection_ID", SqlDbType.Int);
                InsertCircuit.Parameters["@CircuitDirection_ID"].Value = x_circuit_direction.SelectedValue;

                InsertCircuit.ExecuteNonQuery();

                Conn.Close();

            }

            catch (Exception ex)
            {
                string ErrorThrown = ex.Message.ToString();
            }
        }        
    }
}