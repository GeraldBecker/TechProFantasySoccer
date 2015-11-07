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
                "				                Players.PositionRef) AS 'Total Fantasy Pts' " +
                "FROM Players " +
                "LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId " +
                "LEFT OUTER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN Clubs ON Players.ClubId = Clubs.ClubId " +
                "LEFT OUTER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId ";
            //System.Diagnostics.Debug.WriteLine("Inside the page load and clear variable is:" + clearButtonPress);
            if(IsPostBack && !clearButtonPress) {
                //System.Diagnostics.Debug.WriteLine("Inside the WHERE CLAUSE. ");
                


                bool anotherQuery = false;

                if(FirstNameTextBox.Text != "" || LastNameTextBox.Text != "" || ClubTextBox.Text != "" ||
                    LeagueTextBox.Text != "" || PositionDropDown.SelectedIndex != 0) {
                    //System.Diagnostics.Debug.WriteLine("Apparently they are not empty. ");
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
                "Clubs.ClubName, Leagues.LeagueName, Players.PlayerId " +
                "ORDER BY LastName, FirstName";

            

            cmd.Connection = con;
            try {
                DataTable temp = new DataTable();

                

                con.Open();
                PlayerSearchGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());


                /*HyperLinkColumn hplink = new HyperLinkColumn();
                hplink.Text = "Link";
                temp.Columns.Add(hplink.Text);

                for(int i = 0; i < temp.Rows.Count; i++) {
                    int playerId = (int)temp.Rows[i]["PlayerId"];
                    //temp.Rows[i]["Link"] = "<a href=\"./Players/ViewPlayer.aspx?player=" + playerId + "\">Click Here</a>";
                    //HyperLink tempo = new HyperLink();



                    temp.Rows[i][hplink.Text] = String.Format("<a href='./Players/ViewPlayer.aspx?player=" 
                        + playerId +"'>HAHA</a>");
                    //tempo.NavigateUrl = "./Players/ViewPlayer.aspx?player=" + playerId;
                    //tempo.Text = "Click Here";
                    //temp.Rows[i]["Link"] = tempo;

                }*/
                //temp.Columns.Remove("PlayerId");

                PlayerSearchGridView.DataSource = temp;
                PlayerSearchGridView.DataBind();
            } catch(System.Data.SqlClient.SqlException ex) {

            } finally {
                con.Close();
            }

            ModifyRows();
        }


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

        protected void PlayerSearchGridView_Sorting(object sender, GridViewSortEventArgs e) {
            DataTable temp = (DataTable)PlayerSearchGridView.DataSource;
            temp.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            PlayerSearchGridView.DataSource = temp;
            PlayerSearchGridView.DataBind();

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

        /*protected void PlayerSearchGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            PlayerSearchGridView.PageIndex = e.NewPageIndex;
            PlayerSearchGridView.DataBind();
        }*/

        protected void ClearEntries(object sender, EventArgs e) {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            ClubTextBox.Text = "";
            LeagueTextBox.Text = "";
            PositionDropDown.SelectedIndex = 0;
            clearButtonPress = true;
        }

        /*protected void PlayerSearchGridView_SelectedIndexChanged(Object sender, EventArgs e) {
            // Get the currently selected row using the SelectedRow property.
            GridViewRow row = PlayerSearchGridView.SelectedRow;

            // Display the first name from the selected row.
            // In this example, the third column (index 2) contains
            // the first name.
            //FirstNameTextBox.Text = "PICK:" + row.Cells[1].Text + ".";
            Response.Redirect("./Players/ViewPlayer?player=" + row.Cells[1].Text);
        }*/


    }
}