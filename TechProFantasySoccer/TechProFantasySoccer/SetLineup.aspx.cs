using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Data; 

namespace TechProFantasySoccer {
    public partial class SetLineup : System.Web.UI.Page {
        //the number of goalies currently dressed
        private int goalieCount;
        //the number of defenders currently dressed
        private int defenderCount;
        //the number of midfielders currently dressed
        private int midfielderCount;
        //the number of strikers currently dressed
        private int strikerCount;
        //the number of players on the bench
        private int benchCount;

        public int BenchCount { get { return benchCount; } set { benchCount = value; } }
        public int DefenderCount { get { return defenderCount; } set { defenderCount = value; } }
        public int MidfielderCount { get { return midfielderCount; } set { midfielderCount = value; } }
        public int StrikerCount { get { return strikerCount; } set { strikerCount = value; } }
        public int GoalieCount { get { return goalieCount; } set { goalieCount = value; } }

        
        protected void Page_Load(object sender, EventArgs e) {
            String strConnString1 = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con1 = new SqlConnection(strConnString1);
            SqlCommand defendercmd = new SqlCommand();


            //Defenders
            defendercmd.CommandText =
                "SELECT Players.FirstName AS First, LastName AS Last " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 3 " +
                "AND LineupHistory.Active = 1 " +
                "ORDER BY Last";

            defendercmd.Connection = con1;

            DataTable defendertbl = new DataTable();
            con1.Open();
            //GridView1.EmptyDataText = "No Records Found";
            defendertbl.Load(defendercmd.ExecuteReader());
            defenderCount = defendertbl.Rows.Count;

            tbDefenders.DataSource = defendertbl;
            tbDefenders.Visible = true;
            tbDefenders.DataBind();

            //Midfielders
            SqlCommand midfieldercmd = new SqlCommand();
            midfieldercmd.CommandText =
                "SELECT Players.FirstName AS First, LastName AS Last " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 2 " +
                "AND LineupHistory.Active = 1 " +
                "ORDER BY Last";

            midfieldercmd.Connection = con1;

            DataTable midfieldertbl = new DataTable();
            midfieldertbl.Load(midfieldercmd.ExecuteReader());
            midfielderCount = midfieldertbl.Rows.Count;

            tbMidfielders.DataSource = midfieldertbl;
            tbMidfielders.Visible = true;
            tbMidfielders.DataBind();

            SqlCommand strikercmd = new SqlCommand();
            strikercmd.CommandText =
                "SELECT Players.FirstName AS First, LastName AS Last " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 1 " +
                "AND LineupHistory.Active = 1 " +
                "ORDER BY Last";

            strikercmd.Connection = con1;
            DataTable strikertbl = new DataTable();
            strikertbl.Load(strikercmd.ExecuteReader());
            strikerCount = strikertbl.Rows.Count;

            tbStrikers.DataSource = strikertbl;
            tbStrikers.Visible = true;
            tbStrikers.DataBind();

            SqlCommand benchcmd = new SqlCommand();
            benchcmd.CommandText =
                "SELECT Players.FirstName AS First, LastName AS Last, Positions.PositionName AS Position " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "JOIN Positions ON Positions.PositionRef = Players.PositionRef " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND LineupHistory.Active = 0 " +
                "ORDER BY Positions.PositionRef DESC, Last";

            benchcmd.Connection = con1;
            DataTable benchtbl = new DataTable();
            benchtbl.Load(benchcmd.ExecuteReader());
            benchCount = benchtbl.Rows.Count;

            tbBench.DataSource = benchtbl;
            tbBench.Visible = true;
            tbBench.DataBind();

            SqlCommand goaliecmd = new SqlCommand();
            goaliecmd.CommandText =
                "SELECT Players.FirstName AS First, LastName AS Last, Positions.PositionName AS Position " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "JOIN Positions ON Positions.PositionRef = Players.PositionRef " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND LineupHistory.Active = 1 " +
                "AND Players.PositionRef = 0 " +
                "ORDER BY Last";

            goaliecmd.Connection = con1;
            DataTable goalietbl = new DataTable();
            goalietbl.Load(goaliecmd.ExecuteReader());
            goalieCount = goalietbl.Rows.Count;

            lbGoalie.DataSource = goalietbl;
            lbGoalie.Visible = true;
            lbGoalie.DataBind();

        }
    }
}