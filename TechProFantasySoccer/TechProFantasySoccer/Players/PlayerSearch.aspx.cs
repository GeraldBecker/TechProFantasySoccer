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
namespace TechProFantasySoccer {
    /// <summary>
    /// Allows users to search through the list of all players in the fantasy league. They can select a player by clicking on the row.
    /// </summary>
    public partial class PlayerSearch : System.Web.UI.Page {
        bool clearButtonPress = false;

        /// <summary>
        /// Loads all the players based on the search parameters. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is a member of the fantasy pool
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isUser(user))
                Response.Redirect("~/AccessDenied");

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Populate the grid with all fantasy points earned including each month
            cmd.CommandText =
                "SELECT " +
                "Players.PlayerId AS PlayerId, " +
                "FirstName AS First, " +
                "LastName AS Last, " +
                "Cost, " +
                "Clubs.ClubName, " +
                "Leagues.LeagueName, " +
                "Positions.PositionName AS Position, " +
                "SUM([PlayerStats].Goals) AS Goals, " +
                "SUM([PlayerStats].Shots) AS Shots, " +
                "SUM([PlayerStats].Assists) AS Assists, " +
                "SUM([PlayerStats].MinPlayed) AS 'Min Played', " +
                "SUM([PlayerStats].Fouls) AS Fouls, " +
                "SUM([PlayerStats].YellowCards) AS YC, " +
                "SUM([PlayerStats].RedCards) AS RC, " +
                "SUM([PlayerStats].GoalsAllowed) AS GA, " +
                "SUM([PlayerStats].SavesMade) AS Saves, " +
                "SUM([PlayerStats].CleanSheets) AS CS, " +
                "dbo.CalculateTotalFantasyPoints(SUM([PlayerStats].Goals), " +
                "				                SUM([PlayerStats].Shots),  " +
                "				                SUM([PlayerStats].Assists), " +
                "				                SUM([PlayerStats].MinPlayed), " +
                "				                SUM([PlayerStats].Fouls), " +
                "				                SUM([PlayerStats].YellowCards), " +
                "				                SUM([PlayerStats].RedCards), " +
                "				                SUM([PlayerStats].GoalsAllowed), " +
                "				                SUM([PlayerStats].SavesMade), " +
                "				                SUM([PlayerStats].CleanSheets), " +
                "				                Players.PositionRef) AS 'Total Fantasy Pts', " +
                "IIF (Players.Owned = 'False', 'Free', 'Owned' ) AS Avail " +
                "FROM Players " +
                "LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId " +
                "LEFT OUTER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN Clubs ON Players.ClubId = Clubs.ClubId " +
                "LEFT OUTER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId ";
            
            if(IsPostBack && !clearButtonPress) {
                bool anotherQuery = false;

                if(FirstNameTextBox.Text != "" || LastNameTextBox.Text != "" || ClubTextBox.Text != "" ||
                    LeagueTextBox.Text != "" || PositionDropDown.SelectedIndex != 0) {
                    cmd.CommandText += "WHERE ";
                    if(FirstNameTextBox.Text != "") {
                        cmd.CommandText += "FirstName LIKE '%" + FirstNameTextBox.Text + "%' ";
                        anotherQuery = true;
                    }
                    
                    if(LastNameTextBox.Text != "") {
                        if(anotherQuery)
                            cmd.CommandText += " AND ";
                        cmd.CommandText += "LastName LIKE '%" + LastNameTextBox.Text + "%' ";
                        anotherQuery = true;
                    }
                    
                    if(ClubTextBox.Text != "") {
                        if(anotherQuery)
                            cmd.CommandText += " AND ";
                        cmd.CommandText += "Clubs.ClubName LIKE '%" + ClubTextBox.Text + "%' ";
                        anotherQuery = true;
                    }
                    
                    if(LeagueTextBox.Text != "") {
                        if(anotherQuery)
                            cmd.CommandText += " AND ";
                        cmd.CommandText += "Leagues.LeagueName LIKE '%" + LeagueTextBox.Text + "%' ";
                        anotherQuery = true;
                    }
                    
                    if(PositionDropDown.SelectedIndex != 0) {
                        if(anotherQuery)
                            cmd.CommandText += " AND ";
                        cmd.CommandText += "Positions.PositionRef = " + PositionDropDown.SelectedValue + " ";
                        anotherQuery = true;
                    }
                }
            }
            clearButtonPress = false;

            cmd.CommandText += 
                "GROUP BY FirstName, LastName, Players.PositionRef, Positions.PositionName, Cost, " +
                "Clubs.ClubName, Leagues.LeagueName, Players.PlayerId, Players.Owned " +
                "ORDER BY LastName, FirstName";

            

            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();
                
                con.Open();
                PlayerSearchGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());
                
                PlayerSearchGridView.DataSource = temp;
                PlayerSearchGridView.DataBind();
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }

            ModifyRows();
        }

        /// <summary>
        /// Sets the alternating colour schemes using css. It also adds a clickable link for the table row to 
        /// view the player.
        /// </summary>
        private void ModifyRows() {
            for(int i = 0; i < PlayerSearchGridView.Rows.Count; i++) {
                string classList = "selectedblackout";



                //PlayerSearchGridView.Rows[i].Attributes.Add("class", "selectedblackout");
                PlayerSearchGridView.Rows[i].Attributes.Add("data-href", "./ViewPlayer.aspx?player=" +
                    PlayerSearchGridView.Rows[i].Cells[0].Text);

                if((i % 2) == 1)
                    classList += " alternaterow";

                PlayerSearchGridView.Rows[i].Attributes.Add("class", classList);
                
            }
        }

        /// <summary>
        /// Sorts the table content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PlayerSearchGridView_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable temp = (DataTable)PlayerSearchGridView.DataSource;
            temp.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            PlayerSearchGridView.DataSource = temp;
            PlayerSearchGridView.DataBind();

            ModifyRows();
        }

        /// <summary>
        /// Helper class to figure out the sort direction.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSortDirection(string column) {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if(sortExpression != null) {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if(sortExpression == column) {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if((lastDirection != null) && (lastDirection == "ASC")) {
                        sortDirection = "DESC";
                    }
                }
            }
            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        /// <summary>
        /// Selects the index page to display those players.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PlayerSearchGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            PlayerSearchGridView.PageIndex = e.NewPageIndex;
            PlayerSearchGridView.DataBind();
            ModifyRows();
        }

        /// <summary>
        /// Clears the search entry boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearEntries(object sender, EventArgs e) {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            ClubTextBox.Text = "";
            LeagueTextBox.Text = "";
            PositionDropDown.SelectedIndex = 0;
            clearButtonPress = true;
        }


    }
}