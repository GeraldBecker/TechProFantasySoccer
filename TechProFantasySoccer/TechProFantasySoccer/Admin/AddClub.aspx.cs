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
    public partial class AddTeam : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is an Admin
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isAdmin(user))
                Response.Redirect("~/AccessDenied");
            
            DisplayClubs();
        }

        private void DisplayClubs() {
            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText =
                "SELECT " +
                "Clubs.ClubName AS 'Club Name', " +
                "Leagues.LeagueName AS 'League Name' " +
                "FROM Clubs " +
                "INNER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId " +
                "ORDER BY Leagues.LeagueName, Clubs.ClubName";



            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();
                con.Open();
                ClubGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());


                ClubGridView.DataSource = temp;
                ClubGridView.DataBind();
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }

            ModifyRows();
        }

        protected void SubmitButton_Click(object sender, EventArgs e) {
            string teamName = TeamNameTextBox.Text;
            TeamNameTextBox.Text = "";
            string league = LeagueDropDown.Text;


            FantasyDataSource.InsertParameters["ClubName"].DefaultValue = teamName;
            FantasyDataSource.InsertParameters["League"].DefaultValue = league;

            FantasyDataSource.Insert();

            DisplayClubs();
        }

        private void ModifyRows() {
            for(int i = 0; i < ClubGridView.Rows.Count; i++) {
                string classList = "selectedblackout";

                ClubGridView.Rows[i].Attributes.Add("data-href", "../Players/ViewPlayer.aspx?player=" +
                    ClubGridView.Rows[i].Cells[0].Text);

                if((i % 2) == 1)
                    classList += " alternaterow";

                ClubGridView.Rows[i].Attributes.Add("class", classList);

            }
        }
    }
}