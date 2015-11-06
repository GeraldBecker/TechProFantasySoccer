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
            //GridView1.EmptyDataText = "No Records Found";
            midfieldertbl.Load(midfieldercmd.ExecuteReader());

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

            tbStrikers.DataSource = strikertbl;
            tbStrikers.Visible = true;
            tbStrikers.DataBind();

            SqlCommand benchcmd = new SqlCommand();
            benchcmd.CommandText =
                "SELECT Players.FirstName AS First, LastName AS Last " +
                "FROM Players " +
                "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                //"AND Players.PositionRef = 1 " +
                "AND LineupHistory.Active = 0 " +
                "ORDER BY Last";

            benchcmd.Connection = con1;
            DataTable benchtbl = new DataTable();
            benchtbl.Load(benchcmd.ExecuteReader());

            tbBench.DataSource = benchtbl;
            tbBench.Visible = true;
            tbBench.DataBind();

        }
    }
}