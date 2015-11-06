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

namespace TechProFantasySoccer {
    public partial class TeamOverview : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                //Server.Transfer("Default.aspx", true);
                

            } else {
                //MembershipUser CurrentUser = Membership.GetUser(User.Identity.Name);
                if(!IsPostBack) { 
                    Response.Write(@"<script language='javascript'>alert('" + HttpContext.Current.User.Identity.Name + "|" +
                    User.Identity.GetUserId() + "|');</script>"); 
                }
   
            }

            //GridView1.DataBind();

            //Response.Write(SqlFantasyDataSource.SelectCommand);






            String strConnString1 = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con1 = new SqlConnection(strConnString1);
            SqlCommand cmd1 = new SqlCommand();

            cmd1.CommandText =
                "SELECT Players.[PlayerId], " +
                "FirstName AS First, LastName AS Last, [Cost], [ClubName] AS Club, [Positions].[PositionName] AS Position," +
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
                "GROUP BY Players.PlayerId, FirstName, LastName, Players.Cost, Clubs.ClubName, Positions.PositionName, Positions.PositionRef " +
                "ORDER BY Last";

            cmd1.Connection = con1;

            DataTable temp1 = new DataTable();
            con1.Open();
            //GridView1.EmptyDataText = "No Records Found";
            temp1.Load(cmd1.ExecuteReader());


            GridView1.DataSource = temp1;
            GridView1.DataBind();


















            /*DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Cost");
            table.Columns.Add("Position");


            DataRow dr = table.NewRow();
            dr["Name"] = "gerald";
            dr["Cost"] = "5000";
            dr["Position"] = "striker";


            table.Rows.Add(dr);

            GridView1.DataSource = table;

            GridView1.DataBind();*/

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Calculate fields
            decimal totalPoints = 0;
            decimal defenderPoints = 0;
            decimal midfielderPoints = 0;
            decimal strikerPoints = 0;


            /*cmd.CommandText = 
                "SELECT "+
                "Players.PositionRef AS Position, "+
                "SUM([PlayerStats].Goals) AS Goals, "+
                "SUM([PlayerStats].Shots) AS Shots,"+
                "SUM([PlayerStats].Assists) AS Assists,"+
                "SUM([PlayerStats].MinPlayed) AS MinPlayed,"+
                "SUM([PlayerStats].Fouls) AS Fouls,"+
                "SUM([PlayerStats].YellowCards) AS YC,"+
                "SUM([PlayerStats].RedCards) AS RC,"+
                "SUM([PlayerStats].GoalsAllowed) AS GA,"+
                "SUM([PlayerStats].SavesMade) AS Saves,"+
                "dbo.CalculateTotalFantasyPoints(SUM([PlayerStats].Goals),"+
								                "SUM([PlayerStats].Shots), "+
								                "SUM([PlayerStats].Assists),"+
								                "SUM([PlayerStats].MinPlayed),"+
								                "SUM([PlayerStats].Fouls),"+
								                "SUM([PlayerStats].YellowCards),"+
								                "SUM([PlayerStats].RedCards),"+
								                "SUM([PlayerStats].GoalsAllowed),"+
								                "SUM([PlayerStats].SavesMade),"+
								                "SUM([PlayerStats].CleanSheets),"+
								                "Players.PositionRef) AS 'Total Fantasy Pts' "+
                "FROM LineupHistory "+
                "INNER JOIN Players ON LineupHistory.PlayerId = Players.PlayerId "+
                "INNER JOIN PlayerStats ON PlayerStats.PlayerId = LineupHistory.PlayerId AND PlayerStats.Month = LineupHistory.Month "+
                "WHERE LineupHistory.UserId = 1 "+
                "GROUP BY Players.PositionRef";*/
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
            "    SUM([PlayerStats].YellowCards) AS YC," +
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
        "						SUM([PlayerStats].Shots), " +
    "							SUM([PlayerStats].Assists)," +
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
            "WHERE LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
            "GROUP BY Players.PositionRef";


            cmd.Connection = con;
            try {
                decimal goals = 0, goalPts = 0, shots = 0, shotPts = 0, assists = 0, assistPts = 0,
                    minPlayed = 0, minPlayedPts = 0, fouls = 0, foulPts = 0, YC = 0, YCPts = 0,
                    RC = 0, RCPts = 0, GC = 0, GCPts = 0, saves = 0, savePts = 0, cleanSheets = 0,
                    cleanSheetPts = 0;

                DataTable temp = new DataTable();
                con.Open();
                //GridView1.EmptyDataText = "No Records Found";
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

                GoalsPtsLabel.Text = goals.ToString() + " = " + goalPts.ToString() + " pts";
                ShotsPtsLabel.Text = shots.ToString() + " = " + shotPts.ToString() + " pts";
                AssistsPtsLabel.Text = assists.ToString() + " = " + assistPts.ToString() + " pts";
                MinPlayedPtsLabel.Text = minPlayed.ToString() + " = " + minPlayedPts.ToString() + " pts";
                FoulsPtsLabel.Text = fouls.ToString() + " = " + foulPts.ToString() + " pts";
                YCPtsLabel.Text = YC.ToString() + " = " + YCPts.ToString() + " pts";
                RCPtsLabel.Text = RC.ToString() + " = " + RCPts.ToString() + " pts";
                GCPtsLabel.Text = GC.ToString() + " = " + GCPts.ToString() + " pts";
                SavesMadePtsLabel.Text = saves.ToString() + " = " + savePts.ToString() + " pts";
                CleanSheetsPtsLabel.Text = cleanSheets.ToString() + " = " + cleanSheetPts.ToString() + " pts";



                /*foundRows = temp.Select("Position = 2");
                if(foundRows.Length > 0) {
                    MidfieldersPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";
                    goals += (decimal)foundRows[0]["Goals"];
                    goalPts += (decimal)foundRows[0]["GoalsPts"];
                }

                foundRows = temp.Select("Position = 3");
                if(foundRows.Length > 0) {
                    DefendersPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";
                }

                foundRows = temp.Select("Position = 4");
                if(foundRows.Length > 0) {
                    GoaliesPtsLabel.Text = foundRows[0]["Total Fantasy Pts"].ToString() + " pts";
                }*/

            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }



            /*cmd.CommandText = "SELECT Name FROM ScoringValues WHERE ScoringId = 3";

            cmd.Connection = con;
            try {
                //GridView1.EmptyDataText = "No Records Found";
                string text = (string)cmd.ExecuteScalar();
                Label1.Text = text;
            } finally {

            }*/
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable temp = (DataTable)GridView1.DataSource;
            temp.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            GridView1.DataSource = temp;
            GridView1.DataBind();
        }

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

    }



    /*
    QUERIES

    //
    SELECT Players.[PlayerId],
        FirstName AS First,
        LastName AS Last,
        [Cost],
        [ClubName] AS Club,
        [Positions].[PositionName] AS Position,
        [PlayerStats].[Month],[PlayerStats].Goals,[PlayerStats].Shots,[PlayerStats].Assists,
        [PlayerStats].MinPlayed AS 'Min Played'
	      ,[PlayerStats].Fouls,
        [PlayerStats].YellowCards AS YC,
        [PlayerStats].RedCards AS RC,
        [PlayerStats].GoalsAllowed AS GA,
        [PlayerStats].SavesMade AS Saves
        FROM [Players]
        INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef
        LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId
        INNER JOIN Clubs ON Clubs.ClubId = Players.ClubId


    // All players stats
    SELECT Players.[PlayerId],
        FirstName AS First,
        LastName AS Last,
        [Cost],
        [ClubName] AS Club,
        [Positions].[PositionName] AS Position,
        SUM([PlayerStats].Goals) AS Goals,
        SUM([PlayerStats].Shots) AS Shots,
        SUM([PlayerStats].Assists) AS Assists,
        SUM([PlayerStats].MinPlayed) AS 'Min Played',
        SUM([PlayerStats].Fouls) AS Fouls,
        SUM([PlayerStats].YellowCards) AS YC,
        SUM([PlayerStats].RedCards) AS RC,
        SUM([PlayerStats].GoalsAllowed) AS GA,
        SUM([PlayerStats].SavesMade) AS Saves
        FROM [Players]
        INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef
        LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId
        INNER JOIN Clubs ON Clubs.ClubId = Players.ClubId
        GROUP BY Players.PlayerId, FirstName, LastName, Players.Cost, Clubs.ClubName, Positions.PositionName
        ORDER BY Last


    //Stats for players on your lineup only
    SELECT Players.[PlayerId],
    FirstName AS First,
    LastName AS Last,
    [Cost],
    [ClubName] AS Club,
    [Positions].[PositionName] AS Position,
    SUM([PlayerStats].Goals) AS Goals,
    SUM([PlayerStats].Shots) AS Shots,
    SUM([PlayerStats].Assists) AS Assists,
    SUM([PlayerStats].MinPlayed) AS 'Min Played',
    SUM([PlayerStats].Fouls) AS Fouls,
    SUM([PlayerStats].YellowCards) AS YC,
    SUM([PlayerStats].RedCards) AS RC,
    SUM([PlayerStats].GoalsAllowed) AS GA,
    SUM([PlayerStats].SavesMade) AS Saves
    FROM [Players]
    INNER JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId
    INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef
    LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId
    INNER JOIN Clubs ON Clubs.ClubId = Players.ClubId
    WHERE LineupHistory.Month = 10--DATEPART(MONTH, GETDATE())
    AND LineupHistory.UserId = 1
    GROUP BY Players.PlayerId, FirstName, LastName, Players.Cost, Clubs.ClubName, Positions.PositionName
    ORDER BY Last











    */
}