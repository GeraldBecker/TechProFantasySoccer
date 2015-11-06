using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Configuration;
using System.Data; 

namespace TechProFantasySoccer {
    public partial class FantasyPointsDetails : System.Web.UI.Page {
        public string UserName;

        protected void Page_Load(object sender, EventArgs e) {

            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                //Server.Transfer("Default.aspx", true);


            } else {
                if(!IsPostBack) {
                    //Response.Write(@"<script language='javascript'>alert('" + HttpContext.Current.User.Identity.Name + "|" +
                    //User.Identity.GetUserId() + "|');</script>");
                }
                UserName = HttpContext.Current.User.Identity.Name;
            }

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Populate the grid with all fantasy points earned including each month
            cmd.CommandText =
                "SELECT  " +
                "FirstName AS First, " +
                "LastName AS Last, " +
                "PlayerStats.Month, " +
                "Positions.PositionName AS Position, " +
                "([PlayerStats].Goals) AS Goals, " +
                "([PlayerStats].Shots) AS Shots, " +
                "([PlayerStats].Assists) AS Assists, " +
                "([PlayerStats].MinPlayed) AS 'Min Played', " +
                "([PlayerStats].Fouls) AS Fouls, " +
                "([PlayerStats].YellowCards) AS YC, " +
                "([PlayerStats].RedCards) AS RC, " +
                "([PlayerStats].GoalsAllowed) AS GA, " +
                "([PlayerStats].SavesMade) AS Saves, " +
                "dbo.CalculateTotalFantasyPoints(([PlayerStats].Goals), " +
				"				                ([PlayerStats].Shots),  " +
				"				                ([PlayerStats].Assists), " +
				"				                ([PlayerStats].MinPlayed), " +
				"				                ([PlayerStats].Fouls), " +
				"				                ([PlayerStats].YellowCards), " +
				"				                ([PlayerStats].RedCards), " +
				"				                ([PlayerStats].GoalsAllowed), " +
				"				                ([PlayerStats].SavesMade), " +
				"				                ([PlayerStats].CleanSheets), " +
				"				                Players.PositionRef) AS 'Total Fantasy Pts' " +
                "FROM LineupHistory " +
                "INNER JOIN Players ON LineupHistory.PlayerId = Players.PlayerId " +
                "INNER JOIN PlayerStats ON PlayerStats.PlayerId = LineupHistory.PlayerId AND PlayerStats.Month = LineupHistory.Month " +
                "INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "WHERE LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                "ORDER BY Month, LastName";

            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();
                con.Open();
                GridView1.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());


                GridView1.DataSource = temp;
                GridView1.DataBind();
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }


            //Calculate and populate the total fantasy points earned.
            cmd.CommandText =
                "SELECT " + 
                "SUM(dbo.CalculateTotalFantasyPoints(([PlayerStats].Goals), " +
				"				                ([PlayerStats].Shots),  " +
				"				                ([PlayerStats].Assists), " +
				"				                ([PlayerStats].MinPlayed), " +
				"				                ([PlayerStats].Fouls), " +
				"				                ([PlayerStats].YellowCards), " +
				"				                ([PlayerStats].RedCards), " +
				"				                ([PlayerStats].GoalsAllowed), " +
				"				                ([PlayerStats].SavesMade), " +
				"				                ([PlayerStats].CleanSheets), " +
				"				                Players.PositionRef)) AS 'Total Fantasy Pts' " +
                "FROM LineupHistory " +
                "INNER JOIN Players ON LineupHistory.PlayerId = Players.PlayerId " +
                "INNER JOIN PlayerStats ON PlayerStats.PlayerId = LineupHistory.PlayerId AND PlayerStats.Month = LineupHistory.Month " +
                "WHERE LineupHistory.UserId = '" + User.Identity.GetUserId() + "'";

            cmd.Connection = con;
            try {
                con.Open();
                /*DataRow[] row = (DataRow[])cmd.ExecuteScalar();

                double points = (double)row[0]["Total Fantasy Pts"];*/
                decimal points = (decimal)cmd.ExecuteScalar();
                FantasyPointsLabel.Text = points + " pts";
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }





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
}