using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity; 

/// <summary>
/// Author: Gerald
/// Style Author: Becky
/// </summary>
namespace TechProFantasySoccer {
    /// <summary>
    /// Displays a user's team and all their players. Also shows the points and scores.
    /// </summary>
    public partial class TeamOverview : System.Web.UI.Page {

        /// <summary>
        /// Loads all the stats for the player's team
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

            //Display avail cap remaining
            cmd.CommandText =
                "SELECT " +
                "dbo.GetSalaryCap(AspNetUsers.Id) AS 'Salary Cap Remaining' " +
                "FROM AspNetUsers " +
                "WHERE Id = '" + User.Identity.GetUserId() + "'";

            cmd.Connection = con;
            try {
                con.Open();
                string cap = cmd.ExecuteScalar().ToString();
                availCapLabel.Text = cap;

            } catch(System.Data.SqlClient.SqlException ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            } finally {
                con.Close();
            }

            //Populate the grid of players with fantasy points
            cmd.CommandText =
                "SELECT " +
                "Players.PlayerId AS PlayerId, FirstName AS First, LastName AS Last, [Cost], [ClubName] AS Club, [Positions].[PositionName] AS Position," +
                "SUM([PlayerStats].Goals) AS Goals, SUM([PlayerStats].Shots) AS Shots, SUM([PlayerStats].Assists) AS Assists," +
                "SUM([PlayerStats].MinPlayed) AS 'Min Played', SUM([PlayerStats].Fouls) AS Fouls, " +
                "SUM([PlayerStats].YellowCards) AS YC, SUM([PlayerStats].RedCards) AS RC, SUM([PlayerStats].GoalsAllowed) AS GA, " +
                "SUM([PlayerStats].SavesMade) AS Saves, SUM([PlayerStats].CleanSheets) AS CS, " +
                "dbo.CalculateTotalFantasyPoints(SUM([PlayerStats].Goals), " +
                "						        SUM([PlayerStats].Shots),  " +
                "						        SUM([PlayerStats].Assists), " +
                "						        SUM([PlayerStats].MinPlayed), " +
                "						        SUM([PlayerStats].Fouls), " +
                "						        SUM([PlayerStats].YellowCards), " +
                "						        SUM([PlayerStats].RedCards), " +
                "						        SUM([PlayerStats].GoalsAllowed), " +
                "						        SUM([PlayerStats].SavesMade), " +
                "						        SUM([PlayerStats].CleanSheets), " +
                "						        Positions.PositionRef) AS 'Total Fantasy Pts' " +
                "FROM [Players] " +
                "INNER JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId " +
                "INNER JOIN Clubs ON Clubs.ClubId = Players.ClubId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId()+  "' "+
                "GROUP BY Players.PlayerId, FirstName, LastName, Players.Cost, Clubs.ClubName, Positions.PositionName, Positions.PositionRef, Players.PlayerId " +
                "ORDER BY Last";
            
            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();
                con.Open();
                TeamGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());

                /*for(int i = 0; i < temp.Rows.Count; i++) {
                    TOTALCAP -= (int)temp.Rows[i]["Cost"];
                }
                AvailCap = TOTALCAP.ToString("C");*/

                TeamGridView.DataSource = temp;
                TeamGridView.DataBind();
                ModifyRows();
                //TeamGridView.Columns[0].Visible = false;
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }

            
            //Populate the fantasy points earned fields
            cmd.CommandText =
            "SELECT " +
            "Players.PositionRef AS Position," +
            "SUM([PlayerStats].Goals) AS Goals," +
            "dbo.CalculateFantasyPointsByPosition(1, SUM([PlayerStats].Goals), Players.PositionRef) AS GoalsPts," +
            "SUM([PlayerStats].Shots) AS Shots," +
            "dbo.CalculateFantasyPointsByPosition(2, SUM([PlayerStats].Shots), Players.PositionRef) AS ShotsPts," +
            "SUM([PlayerStats].Assists) AS Assists," +
            "dbo.CalculateFantasyPointsByPosition(6, SUM([PlayerStats].Assists), Players.PositionRef) AS AssistsPts," +
            "SUM([PlayerStats].MinPlayed) AS MinPlayed," +
            "dbo.CalculateFantasyPointsByPosition(10, SUM([PlayerStats].MinPlayed), Players.PositionRef) AS MinPlayedPts," +
            "SUM([PlayerStats].Fouls) AS Fouls," +
            "dbo.CalculateFantasyPointsByPosition(3, SUM([PlayerStats].Fouls), Players.PositionRef) AS FoulsPts," +
            "SUM([PlayerStats].YellowCards) AS YC," +
            "dbo.CalculateFantasyPointsByPosition(4, SUM([PlayerStats].YellowCards), Players.PositionRef) AS YCPts," +
            "SUM([PlayerStats].RedCards) AS RC," +
            "dbo.CalculateFantasyPointsByPosition(5, SUM([PlayerStats].RedCards), Players.PositionRef) AS RCPts," +
            "SUM([PlayerStats].GoalsAllowed) AS GA," +
            "dbo.CalculateFantasyPointsByPosition(8, SUM([PlayerStats].GoalsAllowed), Players.PositionRef) AS GAPts," +
            "SUM([PlayerStats].SavesMade) AS Saves," +
            "dbo.CalculateFantasyPointsByPosition(9, SUM([PlayerStats].SavesMade), Players.PositionRef) AS SavesPts," +
            "SUM([PlayerStats].CleanSheets) AS CleanSheets," +
            "dbo.CalculateFantasyPointsByPosition(7, SUM([PlayerStats].CleanSheets), Players.PositionRef) AS CleanSheetPts," +
            "dbo.CalculateTotalFantasyPoints(SUM([PlayerStats].Goals)," +
            "					SUM([PlayerStats].Shots), " +
            "					SUM([PlayerStats].Assists)," +
            "					SUM([PlayerStats].MinPlayed)," +
            "					SUM([PlayerStats].Fouls)," +
            "					SUM([PlayerStats].YellowCards)," +
            "					SUM([PlayerStats].RedCards)," +
            "					SUM([PlayerStats].GoalsAllowed)," +
            "					SUM([PlayerStats].SavesMade)," +
            "					SUM([PlayerStats].CleanSheets)," +
            "					Players.PositionRef) AS 'Total Fantasy Pts' " +
            "FROM LineupHistory " +
            "INNER JOIN Players ON LineupHistory.PlayerId = Players.PlayerId " +
            "INNER JOIN PlayerStats ON PlayerStats.PlayerId = LineupHistory.PlayerId AND PlayerStats.Month = LineupHistory.Month " +
            "WHERE LineupHistory.UserId = '" + User.Identity.GetUserId() + "' AND LineupHistory.Active = 'True' " +
            "GROUP BY Players.PositionRef";


            cmd.Connection = con;
            try {
                decimal goals = 0, goalPts = 0, shots = 0, shotPts = 0, assists = 0, assistPts = 0,
                    minPlayed = 0, minPlayedPts = 0, fouls = 0, foulPts = 0, YC = 0, YCPts = 0,
                    RC = 0, RCPts = 0, GC = 0, GCPts = 0, saves = 0, savePts = 0, cleanSheets = 0,
                    cleanSheetPts = 0;

                DataTable temp = new DataTable();
                con.Open();
                temp.Load(cmd.ExecuteReader());

                DataRow[] foundRows;

                for(int i = 1; i <= 4; i++) {
                    foundRows = temp.Select("Position = " + i);
                    if(foundRows.Length > 0) {
                        if(i == 1)
                            StrikersPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";
                        else if(i == 2)
                            MidfieldersPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";
                        else if(i == 3)
                            DefendersPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";
                        else if(i == 4)
                            GoaliesPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";

                        goals += (int)foundRows[0]["Goals"];
                        goalPts += (decimal)foundRows[0]["GoalsPts"];
                        shots += (int)foundRows[0]["Shots"];
                        shotPts += (decimal)foundRows[0]["ShotsPts"];
                        assists += (int)foundRows[0]["Assists"];
                        assistPts += (decimal)foundRows[0]["AssistsPts"];
                        minPlayed += (int)foundRows[0]["MinPlayed"];
                        minPlayedPts += (decimal)foundRows[0]["MinPlayedPts"];
                        fouls += (int)foundRows[0]["Fouls"];
                        foulPts += (decimal)foundRows[0]["FoulsPts"];
                        YC += (int)foundRows[0]["YC"];
                        YCPts += (decimal)foundRows[0]["YCPts"];
                        RC += (int)foundRows[0]["RC"];
                        RCPts += (decimal)foundRows[0]["RCPts"];
                        GC += (int)foundRows[0]["GA"];
                        GCPts += (decimal)foundRows[0]["GAPts"];
                        saves += (int)foundRows[0]["Saves"];
                        savePts += (decimal)foundRows[0]["SavesPts"];
                        cleanSheets += (int)foundRows[0]["CleanSheets"];
                        cleanSheetPts += (decimal)foundRows[0]["CleanSheetPts"];
                    }
                }

                GoalsLabel.Text = goals.ToString();
                GoalsPtsLabel.Text = goalPts.ToString() + " pts";
                ShotsLabel.Text = shots.ToString();
                ShotsPtsLabel.Text = shotPts.ToString() + " pts";
                AssistsLabel.Text = assists.ToString();
                AssistsPtsLabel.Text = assistPts.ToString() + " pts";
                MinPlayedLabel.Text = minPlayed.ToString();
                MinPlayedPtsLabel.Text = minPlayedPts.ToString() + " pts";
                FoulsLabel.Text = fouls.ToString();
                FoulsPtsLabel.Text = foulPts.ToString() + " pts";
                YCLabel.Text = YC.ToString();
                YCPtsLabel.Text = YCPts.ToString() + " pts";
                RCLabel.Text = RC.ToString();
                RCPtsLabel.Text = RCPts.ToString() + " pts";
                GCLabel.Text = GC.ToString();
                GCPtsLabel.Text = GCPts.ToString() + " pts";
                SavesMadeLabel.Text = saves.ToString();
                SavesMadePtsLabel.Text = savePts.ToString() + " pts";
                CleanSheetsLabel.Text = cleanSheets.ToString();
                CleanSheetsPtsLabel.Text = cleanSheetPts.ToString() + " pts";

                FantasyPointsLabel.Text = "" + (goalPts + shotPts + assistPts + minPlayedPts + foulPts + YCPts + RCPts + GCPts + savePts + cleanSheetPts) + " pts";

            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }
        }

        /// <summary>
        /// The sorting function for the grid table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TeamGridView_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable temp = (DataTable)TeamGridView.DataSource;
            temp.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            TeamGridView.DataSource = temp;
            TeamGridView.DataBind();
            ModifyRows();
        }

        /// <summary>
        /// Determines the correct sort direction. 
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
        /// Sets the alternating colour schemes using css. It also adds a clickable link for the table row to 
        /// view the player.
        /// </summary>
        private void ModifyRows() {
            try {
                if(TeamGridView.Rows.Count > 0)
                    TeamGridView.HeaderRow.Cells[0].CssClass = "hidden";
            } catch(System.NullReferenceException ex) {

            }

            for(int i = 0; i < TeamGridView.Rows.Count; i++) {
                string classList = "selectedblackout";

                TeamGridView.Rows[i].Cells[0].Attributes.Add("class", "hidden");


                TeamGridView.Rows[i].Attributes.Add("data-href", "/Players/ViewPlayer.aspx?player=" +
                    TeamGridView.Rows[i].Cells[0].Text);

                if((i % 2) == 1)
                    classList += " alternaterow";

                TeamGridView.Rows[i].Attributes.Add("class", classList);
                
            }

        }

    }



   
}