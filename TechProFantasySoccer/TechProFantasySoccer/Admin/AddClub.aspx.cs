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

/// <summary>
/// Author: Gerald
/// </summary>
namespace TechProFantasySoccer.Admin {
    /// <summary>
    /// Adds a club to the fantasy app.
    /// </summary>
    public partial class AddTeam : System.Web.UI.Page {
        /// <summary>
        /// Populates the dropdown list of leagues and adds textboxes for club entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Displays all the clubs currently in the system.
        /// </summary>
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

        /// <summary>
        /// Processes the adding of a club.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e) {
            string teamName = TeamNameTextBox.Text;
            TeamNameTextBox.Text = "";
            string league = LeagueDropDown.Text;


            FantasyDataSource.InsertParameters["ClubName"].DefaultValue = teamName;
            FantasyDataSource.InsertParameters["League"].DefaultValue = league;

            try {
                FantasyDataSource.Insert();
            } catch(System.Data.SqlClient.SqlException) {

            }

            DisplayClubs();
        }

        /// <summary>
        /// Alternates the colours of the grid table. 
        /// </summary>
        private void ModifyRows() {
            for(int i = 0; i < ClubGridView.Rows.Count; i++) {
                string classList = "selectedblackout";

                //ClubGridView.Rows[i].Attributes.Add("data-href", "../Players/ViewPlayer.aspx?player=" +
                //    ClubGridView.Rows[i].Cells[0].Text);

                if((i % 2) == 1)
                    classList += " alternaterow";

                ClubGridView.Rows[i].Attributes.Add("class", classList);

            }
        }

        protected void ClubGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            ClubGridView.PageIndex = e.NewPageIndex;
            ClubGridView.DataBind();
            ModifyRows();
        }
    }
}