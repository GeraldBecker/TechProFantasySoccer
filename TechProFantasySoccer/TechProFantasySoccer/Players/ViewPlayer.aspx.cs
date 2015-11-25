using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace TechProFantasySoccer.Players {
    public partial class ViewPlayer : System.Web.UI.Page {
        public string PlayerName = "View Player";
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is a member of the fantasy pool
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isUser(user))
                Response.Redirect("~/AccessDenied");

            if(Request.QueryString["player"] == null) {
                Response.Redirect("PlayerSearch");
                //Response.Redirect("ViewPlayer.aspx?player=2");
            }
            int playerId = 0;
            try {
                playerId = int.Parse(Request.QueryString["player"]);
            } catch(FormatException ex) {
                Response.Redirect("PlayerSearch");
            }
            Session["playerId"] = playerId;

            //Turn off transaction buttons for now
            AddPlayerBtn.Visible = false;
            TradePlayerBtn.Visible = false;
            DropPlayerBtn.Visible = false;


            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT Value FROM Settings WHERE KeyId = 1";
            cmd.Connection = con;
            bool transfersEnabled = false;
            try {
                con.Open();
                int value = (int)cmd.ExecuteScalar();
                if(value == 1)
                    transfersEnabled = true;
            } catch(NullReferenceException) {

            } finally {
                con.Close();
            }

            //Get player information
            cmd.CommandText =
                "SELECT " +
                "FirstName AS First, " +
                "LastName AS Last, " +
                "Cost, " +
                "Clubs.ClubName, " +
                "Leagues.LeagueName, " +
                "Players.Owned, " +
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

                    if(temp.Rows[0]["Owned"].ToString() == "False" && transfersEnabled) {
                        AddPlayerBtn.Visible = true;
                    }
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


            //Configure Add, Drop buttons and set the owner's team name
            cmd.CommandText = "SELECT AspNetUsers.UserName, " +
		                        "LineupHistory.Month " +
                                "FROM Players " +
                                "LEFT OUTER JOIN LineUpHistory ON Players.PlayerId = LineupHistory.PlayerId " +
                                "LEFT OUTER JOIN AspNetUsers ON LineupHistory.UserId = AspNetUsers.Id " +
                                "WHERE Players.PlayerId = " + playerId + " AND(DATEPART(MONTH, GETDATE()) = Month " +
                                "                            OR DATEPART(MONTH, DATEADD(MONTH, 1, GETDATE())) = Month) " +
                                "ORDER BY Month";
            try {
                temp = new DataTable();
                con.Open();
                temp.Load(cmd.ExecuteReader());

                DataRow[] row = temp.Select("Month = " + DateTime.Now.Month);
                if(row.Length > 0) {
                    OwnedByLabel.Text = (string)row[0]["Username"];
                    //AddPlayerBtn.Visible = false;

                    if(row[0]["Username"].ToString() == User.Identity.Name && transfersEnabled) {
                        DropPlayerBtn.Visible = true;
                        TradePlayerBtn.Visible = true;
                    }
                } else {
                    OwnedByLabel.Text = "This player is a free agent.";
                }
                

            } catch (System.Data.SqlClient.SqlException ex) {

            } catch (System.InvalidCastException ex) {
                Response.Write("An error has occured." + ex);
            } finally {
                con.Close();
            }

        }

        protected void PlayerFunctionBtn_Click(object sender, EventArgs e) {
            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            int playerId = int.Parse(Session["playerId"].ToString());
            
            

            if (sender == AddPlayerBtn) {
                bool ableToAdd = false;
                cmd.CommandText =
                "SELECT " +
                "Players.Owned " +
                "FROM Players " +
                "WHERE Players.PlayerId = " + playerId;


                DataTable temp;
                
                try {
                    temp = new DataTable();
                    con.Open();
                    temp.Load(cmd.ExecuteReader());

                    if (temp.Rows.Count > 0 && temp.Rows[0]["Owned"].ToString() == "False") {
                        System.Diagnostics.Debug.WriteLine("Able to add the player");
                        ableToAdd = true;
                    } else {
                        System.Diagnostics.Debug.WriteLine("NOT add the player");
                    }

                } catch (System.Data.SqlClient.SqlException ex) {

                } catch (System.InvalidCastException ex) {
                    Response.Write("An error has occured converting a value.");
                } catch (System.IndexOutOfRangeException ex) {
                    //Enter a valid player id.
                } finally {
                    con.Close();
                }

                if(ableToAdd) {
                    try {
                        cmd.CommandText =
                            "INSERT INTO LineupHistory (UserId, PlayerId, Month, Active) " +
                            "VALUES ('" + User.Identity.GetUserId() + "', " + playerId + ", " +
                            DateTime.Now.Month + ", 'False')";

                        con.Open();
                        cmd.ExecuteScalar();

                        cmd.CommandText = "UPDATE Players SET Owned = 'True' WHERE PlayerId = " + playerId;
                        cmd.ExecuteScalar();
                        Response.Redirect("/Team/TeamOverview");
                    } catch(InvalidOperationException ex) {
                        System.Diagnostics.Debug.WriteLine(ex);
                    } catch(SqlException ex) {
                        System.Diagnostics.Debug.WriteLine(ex);

                    } finally {
                        con.Close();
                    }
                }




            } else if(sender == DropPlayerBtn) {
                try {
                    cmd.CommandText =
                        "DELETE FROM LineupHistory " +
                        "WHERE UserId = '" + User.Identity.GetUserId() + "' AND PlayerId = " + playerId +
                        " AND Month = " + DateTime.Now.Month;

                    con.Open();
                    cmd.ExecuteScalar();

                    cmd.CommandText = "UPDATE Players SET Owned = 'False' WHERE PlayerId = " + playerId;
                    cmd.ExecuteScalar();
                    Response.Redirect("/Team/TeamOverview");
                } catch(InvalidOperationException ex) {
                    System.Diagnostics.Debug.WriteLine(ex);
                } catch(SqlException ex) {
                    System.Diagnostics.Debug.WriteLine(ex);

                } finally {
                    con.Close();
                }
            }
        }
    }
}