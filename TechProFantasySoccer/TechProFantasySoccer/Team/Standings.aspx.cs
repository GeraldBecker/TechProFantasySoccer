using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer
{
    public partial class LeagueStandings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //Server.Transfer("Default.aspx", true);
                Response.Redirect("/Account/Login");
            }

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //populate grid with standings data
            cmd.CommandText =
                "SELECT " +
                "UserName AS 'Team Name', " +
                "UserName AS 'Salary Cap', " +
                "UserName AS 'Points Earned', " +
                "UserName AS 'Position', " +
                "UserName AS 'Striker Rank', " +
                "UserName AS 'Midfielder Rank', " +
                "UserName AS 'Defensive Rank', " +
                "UserName AS 'Goalie Rank', " +
                "UserName AS 'Current Month Points' " +
                "FROM AspNetUsers ";

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

            } finally {
                con.Close();
            }
        }

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
    }
}