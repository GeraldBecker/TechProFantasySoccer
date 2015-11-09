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
                "FirstName AS 'Team Name', " +
                "LastName AS 'Salary Cap', " +
                "FirstName AS 'Points Earned', " +
                "LastName AS 'Position', " +
                "FirstName AS 'Striker Rank', " +
                "LastName AS 'Midfielder Rank', " +
                "FirstName AS 'Defensive Rank', " +
                "LastName AS 'Goalie Rank', " +
                "FirstName AS 'Current Month Points' " +
                "FROM Players ";
        }
    }
}