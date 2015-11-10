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

        static String strConnString1 = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
        SqlConnection con1 = new SqlConnection(strConnString1);
        protected void Page_Load(object sender, EventArgs e) {
            
            SqlCommand defendercmd = new SqlCommand();


            //DEFENDERS
            defendercmd.CommandText =
                "SELECT concat(Players.FirstName, ' ', Players.LastName) AS Name, LineupHistory.Active AS Active, " +
                        "Players.FirstName, Players.LastName " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 3 " +
                "ORDER BY Name";


            defendercmd.Connection = con1;

            DataTable defendertbl = new DataTable();
            con1.Open();

            defendertbl.Load(defendercmd.ExecuteReader());
            DataRow[] activeDefenders = defendertbl.Select("Active = 1");
            //Get the bench defenders
            DataView inActiveDefenders = defendertbl.DefaultView;
            inActiveDefenders.RowFilter = ("Active = 0");

            DropDownList[] defenderddls = new DropDownList[4];
            for (int i = 0; i < 4; i++) {
                DropDownList ddlDefender = new DropDownList();
                ddlDefender.Width = 300;
                ddlDefender.Height = 30;
                ddlDefender.DataSource = inActiveDefenders;
                ddlDefender.DataTextField = "Name";
                ddlDefender.DataBind();
                //ddlDefender.AutoPostBack = true;

                //If there are active players, set the initial value to those players
                if (activeDefenders.Length > i) {
                    ddlDefender.Items.Insert(0, activeDefenders[i]["Name"].ToString());
                } else {
                    ddlDefender.Items.Insert(0, "Select a player");
                }
                
                DefenderPanel.Controls.Add(ddlDefender);
                defenderddls[i] = ddlDefender;
            }
            

            //MIDFIELDERS
            SqlCommand midfieldercmd = new SqlCommand();
            midfieldercmd.CommandText =
                "SELECT concat(Players.FirstName, ' ', Players.LastName) AS Name, LineupHistory.Active AS Active " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 2 " +
                "ORDER BY Name";

            midfieldercmd.Connection = con1;

            DataTable midfieldertbl = new DataTable();

            midfieldertbl.Load(midfieldercmd.ExecuteReader());

            DataRow[] activeMidfielders = midfieldertbl.Select("Active = 1");
            //Get the bench defenders
            DataView inActiveMidfielders = midfieldertbl.DefaultView;
            inActiveMidfielders.RowFilter = ("Active = 0");

            DropDownList[] midfielderddls = new DropDownList[4];
            for (int i = 0; i < 4; i++) {
                DropDownList ddlMidfielder = new DropDownList();
                ddlMidfielder.Width = 300;
                ddlMidfielder.Height = 30;
                ddlMidfielder.DataSource = inActiveMidfielders;
                ddlMidfielder.DataTextField = "Name";
                ddlMidfielder.DataBind();

                //If there are active players, set the initial value to those players
                if (activeMidfielders.Length > i) {
                    ddlMidfielder.Items.Insert(0, activeMidfielders[i]["Name"].ToString());
                } else {
                    ddlMidfielder.Items.Insert(0, "Select a player");
                }

                MidfielderPanel.Controls.Add(ddlMidfielder);
                midfielderddls[i] = ddlMidfielder;
            }


            //STRIKERS
            SqlCommand strikercmd = new SqlCommand();
            strikercmd.CommandText =
                "SELECT concat(Players.FirstName, ' ', Players.LastName) AS Name, LineupHistory.Active AS Active " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 1 " +
                "ORDER BY Name";

            strikercmd.Connection = con1;

            DataTable strikertbl = new DataTable();

            strikertbl.Load(strikercmd.ExecuteReader());

            DataRow[] activeStrikers = strikertbl.Select("Active = 1");
            //Get the bench defenders
            DataView inActiveStrikers = strikertbl.DefaultView;
            inActiveStrikers.RowFilter = ("Active = 0");

            DropDownList[] strikerddls = new DropDownList[4];
            for (int i = 0; i < 2; i++) {
                DropDownList ddlStriker = new DropDownList();
                ddlStriker.Width = 300;
                ddlStriker.Height = 30;
                ddlStriker.DataSource = inActiveStrikers;
                ddlStriker.DataTextField = "Name";
                ddlStriker.DataBind();

                //If there are active players, set the initial value to those players
                if (activeStrikers.Length > i) {
                    ddlStriker.Items.Insert(0, activeStrikers[i]["Name"].ToString());
                } else {
                    ddlStriker.Items.Insert(0, "Select a player");
                }

                StrikerPanel.Controls.Add(ddlStriker);
                midfielderddls[i] = ddlStriker;
            }


            //GOALIES
            SqlCommand goaliecmd = new SqlCommand();
            goaliecmd.CommandText =
                "SELECT concat(Players.FirstName, ' ', Players.LastName) AS Name, LineupHistory.Active AS Active " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "JOIN Positions ON Positions.PositionRef = Players.PositionRef " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "AND Players.PositionRef = 4 " +
                "ORDER BY Name";

            goaliecmd.Connection = con1;

            DataTable goalietbl = new DataTable();

            goalietbl.Load(goaliecmd.ExecuteReader());

            DataRow[] activeGoalies = goalietbl.Select("Active = 1");
            //Get the bench defenders
            DataView inActiveGoalies = goalietbl.DefaultView;
            inActiveGoalies.RowFilter = ("Active = 0");

            DropDownList[] goalieddls = new DropDownList[4];
            for (int i = 0; i < 1; i++) {
                DropDownList ddlGoalie = new DropDownList();
                ddlGoalie.Width = 300;
                ddlGoalie.Height = 30;
                ddlGoalie.DataSource = inActiveGoalies;
                ddlGoalie.DataTextField = "Name";
                ddlGoalie.DataBind();

                //If there are active players, set the initial value to those players
                if (activeGoalies.Length > i) {
                    ddlGoalie.Items.Insert(0, activeGoalies[i]["Name"].ToString());
                } else {
                    ddlGoalie.Items.Insert(0, "Select a player");
                }

                GoaliePanel.Controls.Add(ddlGoalie);
                goalieddls[i] = ddlGoalie;
            }


            //BENCH
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
        }
    }
}