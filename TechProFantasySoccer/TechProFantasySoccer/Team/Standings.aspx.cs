﻿using System;
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
/// Authors: Becky and Gerald
/// </summary>
namespace TechProFantasySoccer
{
    public partial class LeagueStandings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            //populate grid with standings data
            cmd.CommandText =
                "SELECT " +
                "UserName AS 'Team Name', " +
                "dbo.GetSalaryCap(AspNetUsers.Id) AS 'Salary Cap Remaining', " +
                "dbo.GetTotalFantasyPointsByUser(AspNetUsers.Id) AS 'Total Points', " +
                "dbo.GetTotalFantasyPointsByUserPosition(AspNetUsers.Id, 1) AS 'Striker Pts', " +
                "dbo.GetTotalFantasyPointsByUserPosition(AspNetUsers.Id, 2) AS 'Midfielder Pts', " +
                "dbo.GetTotalFantasyPointsByUserPosition(AspNetUsers.Id, 3) AS 'Defensive Pts', " +
                "dbo.GetTotalFantasyPointsByUserPosition(AspNetUsers.Id, 4) AS 'Goalie Pts', " +
                "dbo.GetTotalFantasyPointsByUserAndMonth(AspNetUsers.Id, " + DateTime.Now.Month + ") AS 'Current Month Points' " +
                "FROM AspNetUsers " + 
                "INNER JOIN AccessLevel ON AspNetUsers.Id = AccessLevel.UserId " +
                "WHERE Access IN (1, 2)";

            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();
                con.Open();
                StandingsGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());
                
                StandingsGridView.DataSource = temp;
                StandingsGridView.DataBind();
                ModifyRows();
            } catch(System.Data.SqlClient.SqlException ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            } finally {
                con.Close();
            }
        }

        /// <summary>
        /// Sets the alternating colour schemes using css. It also adds a clickable link for the table row to 
        /// view the players team.
        /// </summary>
        private void ModifyRows() {
            for(int i = 0; i < StandingsGridView.Rows.Count; i++) {
                string classList = "selectedblackout";
                
                StandingsGridView.Rows[i].Attributes.Add("data-href", "./ViewTeam?team=" +
                    StandingsGridView.Rows[i].Cells[0].Text);

                if((i % 2) == 1)
                    classList += " alternaterow";

                StandingsGridView.Rows[i].Attributes.Add("class", classList);

            }
        }

        protected void StandingsGridView_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable temp = (DataTable)StandingsGridView.DataSource;
            temp.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            StandingsGridView.DataSource = temp;
            StandingsGridView.DataBind();

            ModifyRows();
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