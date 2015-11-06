using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer {
    public partial class PlayerSearch : System.Web.UI.Page {
        bool clearButtonPress = false;


        protected void Page_Load(object sender, EventArgs e) {
            

            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                //Server.Transfer("Default.aspx", true);

            }

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Populate the grid with all fantasy points earned including each month
            cmd.CommandText =
                "SELECT " +
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
                "				                Players.PositionRef) AS 'Total Fantasy Pts' " +
                "FROM Players " +
                "LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId " +
                "LEFT OUTER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN Clubs ON Players.ClubId = Clubs.ClubId " +
                "LEFT OUTER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId ";
            System.Diagnostics.Debug.WriteLine("Inside the page load and clear variable is:" + clearButtonPress);
            if(IsPostBack && !clearButtonPress) {
                System.Diagnostics.Debug.WriteLine("Inside the WHERE CLAUSE. ");
                


                bool anotherQuery = false;

                if(FirstNameTextBox.Text != "" || LastNameTextBox.Text != "" || ClubTextBox.Text != "" ||
                    LeagueTextBox.Text != "" || PositionDropDown.SelectedIndex != 0) {
                        System.Diagnostics.Debug.WriteLine("Apparently they are not empty. ");
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
                "Clubs.ClubName, Leagues.LeagueName " +
                "ORDER BY LastName, FirstName";

            

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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

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