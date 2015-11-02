using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer {
    public partial class TeamOverview : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
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