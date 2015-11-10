using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity; 


namespace TechProFantasySoccer.Admin {
    public partial class AddLeague : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            string user = User.Identity.GetUserId();
            cmd.CommandText =
                "SELECT Access " +
                "FROM AccessLevel " +
                "WHERE UserId = '" + user + "'";

            cmd.Connection = con;
            try {
                con.Open();
                int accessLevel = (int)cmd.ExecuteScalar();

                if (accessLevel != 1)
                    Response.Redirect("~/");

            } catch (System.Data.SqlClient.SqlException ex) {
                Response.Redirect("~/");
            } finally {
                con.Close();
            }

            cmd.CommandText =
                "SELECT " +
                "Leagues.LeagueName AS 'League Name' " +
                "FROM Leagues " +
                "ORDER BY Leagues.LeagueName";
            cmd.Connection = con;

            try {
                DataTable temp = new DataTable();
                con.Open();
                LeagueGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());

                LeagueGridView.DataSource = temp;
                LeagueGridView.DataBind();
            } catch (System.Data.SqlClient.SqlException ex) {
                ;
            } finally {
                con.Close();
            }

            ModifyRows();
        }

        

        private void ModifyRows() {
            for (int i = 0; i < LeagueGridView.Rows.Count; i++) {
                string classList = "selectedblackout";

                LeagueGridView.Rows[i].Attributes.Add("data-href", "../Players/ViewPlayer.aspx?player=" +
                    LeagueGridView.Rows[i].Cells[0].Text);

                if ((i % 2) == 1)
                    classList += " alternaterow";

                LeagueGridView.Rows[i].Attributes.Add("class", classList);

            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e) {
            string leagueName = LeagueNameTextBox.Text;
            LeagueNameTextBox.Text = "";

            FantasyDataSource.InsertParameters["LeagueName"].DefaultValue = leagueName;

            FantasyDataSource.Insert();

            Response.Redirect("./AddLeague");
        }
    }
}