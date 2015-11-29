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
    /// Adds a player to the fantasy app. Player cost, club and names are added. 
    /// </summary>
    public partial class CreatePlayer : System.Web.UI.Page {
        /// <summary>
        /// Loads the drop downs with clubs so that they can be chosen. Provides text box entries for the player
        /// name and cost.
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
            
            DisplayPlayers();
        }

        /// <summary>
        /// Displays the table of players already added. Clicking a player will allow them to be edited.
        /// </summary>
        private void DisplayPlayers() {
            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "SELECT " +
                "Players.PlayerId AS PlayerId, " +
                "FirstName AS First, " +
                "LastName AS Last, " +
                "Cost, " +
                "Clubs.ClubName AS 'Club Name', " +
                "Leagues.LeagueName AS 'League Name', " +
                "Positions.PositionName AS Position " +
                "FROM Players " +
                "LEFT OUTER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN Clubs ON Players.ClubId = Clubs.ClubId " +
                "LEFT OUTER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId " +
                "ORDER BY Leagues.LeagueName, Clubs.ClubName, LastName, FirstName";



            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();



                con.Open();
                PlayerGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());


                PlayerGridView.DataSource = temp;
                PlayerGridView.DataBind();
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }

            ModifyRows();
        }

        /// <summary>
        /// Adds alternating colours for each row and provides a link to edit the player as an admin.
        /// </summary>
        private void ModifyRows() {
            for(int i = 0; i < PlayerGridView.Rows.Count; i++) {
                string classList = "selectedblackout";

                PlayerGridView.Rows[i].Attributes.Add("data-href", "./EditPlayerInfo.aspx?player=" +
                    PlayerGridView.Rows[i].Cells[0].Text);

                if((i % 2) == 1)
                    classList += " alternaterow";

                PlayerGridView.Rows[i].Attributes.Add("class", classList);

            }
        }

        /// <summary>
        /// Inserts the player to the database and to the fantasy website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e) {
            string firstName = FirstNameTextBox.Text;
            FirstNameTextBox.Text = "";
            string lastName = LastNameTextBox.Text;
            LastNameTextBox.Text = "";
            string cost = CostTextBox.Text;
            CostTextBox.Text = "";
            string club = ClubDropDown.SelectedValue;
            string position = PositionDropDown.SelectedValue;

            ClubsDataSource.InsertParameters["FirstName"].DefaultValue = firstName;
            ClubsDataSource.InsertParameters["LastName"].DefaultValue = lastName;
            ClubsDataSource.InsertParameters["Cost"].DefaultValue = cost;
            ClubsDataSource.InsertParameters["Position"].DefaultValue = position;
            ClubsDataSource.InsertParameters["Club"].DefaultValue = club;

            ClubsDataSource.Insert();
            DisplayPlayers();
        }

        /// <summary>
        /// Controls the indexing of players.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PlayerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            PlayerGridView.PageIndex = e.NewPageIndex;
            PlayerGridView.DataBind();
            ModifyRows();
        }
    }
}