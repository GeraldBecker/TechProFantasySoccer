using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer.Players {
    public partial class ViewPlayer : System.Web.UI.Page {
        public string PlayerName = "View Player";
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                //Server.Transfer("Default.aspx", true);
                Response.Redirect("/Account/Login");
            }

            if(Request.QueryString["player"] == null) {
                Response.Redirect("ViewPlayer.aspx?player=2");
                //Response.Redirect("../PlayerSearch.aspx?player=2");
            }
            int playerId = int.Parse(Request.QueryString["player"]);

            

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //
            cmd.CommandText =
                "SELECT " +
                "FirstName AS First, " +
                "LastName AS Last, " +
                "Cost, " +
                "Clubs.ClubName, " +
                "Leagues.LeagueName, " +
                "Positions.PositionName AS Position " +
                "FROM Players " +
                "LEFT OUTER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN Clubs ON Players.ClubId = Clubs.ClubId " +
                "LEFT OUTER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId " +
                "WHERE Players.PlayerId = " + playerId;


            DataTable temp;
            cmd.Connection = con;
            try {
                temp = new DataTable();
                con.Open();
                temp.Load(cmd.ExecuteReader());

                if(temp.Rows.Count > 0) {
                    PlayerName = temp.Rows[0]["First"] + " " + temp.Rows[0]["Last"];

                    LeagueNameLabel.Text = (string)temp.Rows[0]["LeagueName"];
                    ClubNameLabel.Text = (string)temp.Rows[0]["ClubName"];
                    PositionLabel.Text = (string)temp.Rows[0]["Position"];
                    CostLabel.Text = ((int)temp.Rows[0]["Cost"]).ToString();
                }

            } catch(System.Data.SqlClient.SqlException ex) {

            } catch(System.InvalidCastException ex) {
                Response.Write("An error has occured converting a value.");
            } catch(System.IndexOutOfRangeException ex) {
                //Enter a valid player id.
            } finally {
                con.Close();
            }


            cmd.CommandText =
                "SELECT " +
                "PlayerStats.Month, " +
                "([PlayerStats].Goals) AS Goals, " +
                "([PlayerStats].Shots) AS Shots, " +
                "([PlayerStats].Assists) AS Assists, " +
                "([PlayerStats].MinPlayed) AS 'Min Played', " +
                "([PlayerStats].Fouls) AS Fouls, " +
                "([PlayerStats].YellowCards) AS YC, " +
                "([PlayerStats].RedCards) AS RC, " +
                "([PlayerStats].GoalsAllowed) AS GA, " +
                "([PlayerStats].SavesMade) AS Saves, " +
                "([PlayerStats].CleanSheets) AS CS, " +
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
                "FROM Players " +
                "LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId " +
                "WHERE Players.PlayerId = " + playerId + " " +
                "ORDER BY PlayerStats.Month ASC";
            try {
                temp = new DataTable();
                con.Open();
                FantasyDetailsGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());

                FantasyDetailsGridView.DataSource = temp;
                FantasyDetailsGridView.DataBind();

                decimal sumFantasyPts = 0;
                for(int i = 0; i < temp.Rows.Count; i++) {
                    if(temp.Rows[i]["Total Fantasy Pts"].ToString() != "")
                        sumFantasyPts += (decimal)temp.Rows[i]["Total Fantasy Pts"];
                }

                FantasyPointsLabel.Text = sumFantasyPts.ToString() + " pts";

            } catch(System.Data.SqlClient.SqlException ex) {

            } catch(System.InvalidCastException ex) {
                Response.Write("An error has occured." + ex);
            } finally {
                con.Close();
            }

        }
    }
}