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
using System.Collections; 

namespace TechProFantasySoccer {
    public partial class SetLineup : System.Web.UI.Page {
        private const int STRIKER_SLOTS = 2;
        private const int MIDFIELDER_SLOTS = 4;
        private const int DEFENDER_SLOTS = 4;
        private const int GOALIE_SLOTS = 1;

        DropDownList[] defenderddls;
        DropDownList[] midfielderddls;
        DropDownList[] strikerddls;
        DropDownList[] goalieddls;
        static String strConnString1 = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
        SqlConnection con1 = new SqlConnection(strConnString1);
        DataRow[] activeDefenders;
        DataRow[] inActiveDefenders;
        DataRow[] activeMidfielders;
        DataRow[] inActiveMidfielders;      
        DataRow[] activeStrikers;
        DataRow[] inActiveStrikers;
        DataRow[] activeGoalies;
        DataRow[] inActiveGoalies;

        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is a member of the fantasy pool
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isUser(user))
                Response.Redirect("~/AccessDenied");

            SqlCommand getPlayersCommand = new SqlCommand();

            if (!Page.IsPostBack) {
                getPlayersCommand.CommandText =
                    "SELECT concat(Players.FirstName, ' ', Players.LastName) AS Name, Players.PositionRef AS Position, " + 
                    "LineupHistory.Active AS Active, " + "Players.FirstName, Players.LastName " +
                    "FROM Players " +
                    "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                    "JOIN Positions ON Positions.PositionRef = Players.PositionRef " +
                    "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                    "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                    "ORDER BY Name";
                DataTable playersTable = new DataTable();
                try {
                    getPlayersCommand.Connection = con1;
                    con1.Open();
                    playersTable.Load(getPlayersCommand.ExecuteReader());
                } catch (SqlException ex) {
                    NotifyLabel.Text = "There was an issue connecting to the database. Please try again later";
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                } finally {
                    con1.Close();
                }


                //------------------------------------------------------------------------
                //DEFENDERS
                //------------------------------------------------------------------------

                //Get the defenders that are currently active
                activeDefenders = playersTable.Select("Active = 1 AND Position = 3");
                SessionHandler.ActiveDefenders = new ArrayList();
                SetSessionData(activeDefenders, SessionHandler.ActiveDefenders);

                //Get the bench defenders and add them into a string array session variable
                inActiveDefenders = playersTable.Select("Active = 0 AND Position = 3");
                SessionHandler.BenchDefenders = new ArrayList();
                SetSessionData(inActiveDefenders, SessionHandler.BenchDefenders);

                //------------------------------------------------------------------------
                //MIDFIELDERS
                //------------------------------------------------------------------------

                //get the active midfielders and add them into a string array session variable
                activeMidfielders = playersTable.Select("Active = 1 AND Position = 2");
                SessionHandler.ActiveMidfielders = new ArrayList();
                SetSessionData(activeMidfielders, SessionHandler.ActiveMidfielders);

                //Get the bench midfielders and add them into a string array session variable
                inActiveMidfielders = playersTable.Select("Active = 0 AND Position = 2");
                SessionHandler.BenchMidfielders = new ArrayList();
                SetSessionData(inActiveMidfielders, SessionHandler.BenchMidfielders);

                //---------------------------------------------------------------------------
                //STRIKERS
                //---------------------------------------------------------------------------

                //get the active strikers and add them into a string array session variable
                activeStrikers = playersTable.Select("Active = 1 AND Position = 1");
                SessionHandler.ActiveStrikers = new ArrayList();
                SetSessionData(activeStrikers, SessionHandler.ActiveStrikers);
                
                //Get the bench strikers
                inActiveStrikers = playersTable.Select("Active = 0 AND Position = 1");
                SessionHandler.BenchStrikers = new ArrayList();
                SetSessionData(inActiveStrikers, SessionHandler.BenchStrikers);

                //-----------------------------------------------------------------------------
                //GOALIES
                //-----------------------------------------------------------------------------

                activeGoalies = playersTable.Select("Active = 1 AND Position = 4");
                SessionHandler.ActiveGoalies = new ArrayList();
                SetSessionData(activeGoalies, SessionHandler.ActiveGoalies);

                inActiveGoalies = playersTable.Select("Active = 0 AND Position = 4");
                SessionHandler.BenchGoalies = new ArrayList();
                SetSessionData(inActiveGoalies, SessionHandler.BenchGoalies);
            } 

            InitDropDownLists();
        }

        private void SetSessionData(DataRow[] data, ArrayList sessionList) {
            sessionList.Clear();
            for (int i = 0; i < data.Length; i++) {
                sessionList.Add(data[i]["Name"].ToString());
            }
        }

        private void InitDropDownLists() {
            //Create arrays of ListItems to be added to the dropdown lists
            ListItem[] benchDefenderList = new ListItem[SessionHandler.BenchDefenders.Count];
            PopulateListItemArray(benchDefenderList, SessionHandler.BenchDefenders);

            ListItem[] benchMidfielderList = new ListItem[SessionHandler.BenchMidfielders.Count];
            PopulateListItemArray(benchMidfielderList, SessionHandler.BenchMidfielders);

            ListItem[] benchStrikersList = new ListItem[SessionHandler.BenchStrikers.Count];
            PopulateListItemArray(benchStrikersList, SessionHandler.BenchStrikers);

            ListItem[] benchGoaliesList = new ListItem[SessionHandler.BenchGoalies.Count];
            PopulateListItemArray(benchGoaliesList, SessionHandler.BenchGoalies);

            ArrayList bench = new ArrayList(SessionHandler.BenchDefenders);
            bench.AddRange(SessionHandler.BenchMidfielders);
            bench.AddRange(SessionHandler.BenchStrikers);
            bench.AddRange(SessionHandler.BenchGoalies);
            tbBench.DataSource = bench;
            tbBench.DataBind();

            //Add the dropdown lists for defenders and populate them
            defenderddls = new DropDownList[DEFENDER_SLOTS];
            for (int i = 0; i < DEFENDER_SLOTS; i++) {
                DropDownList ddl = new DropDownList();
                SetDropDownList(benchDefenderList, i, ddl, SessionHandler.ActiveDefenders, defenderddls);

                DefenderPanel.Controls.Add(ddl);
            }

            //Add the dropdown lists for midfielders and populate them
            midfielderddls = new DropDownList[MIDFIELDER_SLOTS];
            for (int i = 0; i < MIDFIELDER_SLOTS; i++) {
                DropDownList ddlMidfielder = new DropDownList();
                SetDropDownList(benchMidfielderList, i, ddlMidfielder, SessionHandler.ActiveMidfielders, midfielderddls);

                MidfielderPanel.Controls.Add(ddlMidfielder);
            }

            //Add the dropdown lists for strikers and populate them
            strikerddls = new DropDownList[STRIKER_SLOTS];
            for (int i = 0; i < STRIKER_SLOTS; i++) {
                DropDownList ddlStriker = new DropDownList();
                SetDropDownList(benchStrikersList, i, ddlStriker, SessionHandler.ActiveStrikers, strikerddls);

                StrikerPanel.Controls.Add(ddlStriker);

            }

            goalieddls = new DropDownList[GOALIE_SLOTS];
            for (int i = 0; i < GOALIE_SLOTS; i++) {
                DropDownList ddlGoalie = new DropDownList();
                SetDropDownList(benchGoaliesList, i, ddlGoalie, SessionHandler.ActiveGoalies, goalieddls);

                GoaliePanel.Controls.Add(ddlGoalie);

            }
        }

        private void SetDropDownList(ListItem[] benchStrikersList, int index, DropDownList ddlStriker, ArrayList playerList, DropDownList[] ddlArray) {
            ddlStriker.Width = 300;
            ddlStriker.Height = 30;
            ddlStriker.Items.AddRange(benchStrikersList);
            ddlStriker.SelectedIndexChanged += playerddls_SelectedIndexChanged;
            ddlStriker.AutoPostBack = true;
            ddlArray[index] = ddlStriker;

            //If there are active players, set the initial value to those players
            if (playerList.Count > index) {
                ddlStriker.Items.Insert(0, playerList[index].ToString());
            } else {
                ddlStriker.Items.Insert(0, "Select a player");
            }
        }

        private void playerddls_SelectedIndexChanged(Object sender, EventArgs e) {
            DropDownList ddl = (DropDownList)sender;

            for (int i = 0; i < DEFENDER_SLOTS; i++) {
                if (ddl == defenderddls[i])
                    UpdateActiveAndBench(i, SessionHandler.BenchDefenders, SessionHandler.ActiveDefenders, defenderddls);
            }

            for (int i = 0; i < MIDFIELDER_SLOTS; i++) {
                if (ddl == midfielderddls[i])
                    UpdateActiveAndBench(i, SessionHandler.BenchMidfielders, SessionHandler.ActiveMidfielders, midfielderddls);
            }

            for (int i = 0; i < STRIKER_SLOTS; i++) {
                if (ddl == strikerddls[i])
                    UpdateActiveAndBench(i, SessionHandler.BenchStrikers, SessionHandler.ActiveStrikers, strikerddls);
            }

            for (int i = 0; i < GOALIE_SLOTS; i++) {
                if (ddl == goalieddls[i])
                    UpdateActiveAndBench(i, SessionHandler.BenchGoalies, SessionHandler.ActiveGoalies, goalieddls);
            }
            UpdateDropDownLists();
        }

        /// <summary>
        /// Update both the bench and active players lists.
        /// </summary>
        /// <param name="index">The index of the dropdownlist to use to modify the lists.</param>
        /// <param name="benchList">The list of bench players to be updated.</param>
        /// <param name="activeList">The list of active players to be updated.</param>
        /// <param name="ddlArray">The array of dropdownlists.</param>
        private void UpdateActiveAndBench(int index, ArrayList benchList, ArrayList activeList, DropDownList[] ddlArray) {
            if (ddlArray[index].Items[0].ToString() != "Select a player") {
                benchList.Add(ddlArray[index].Items[0].ToString());
                activeList.Insert(activeList.IndexOf(ddlArray[index].Items[0].ToString()), ddlArray[index].SelectedValue);
                activeList.Remove(ddlArray[index].Items[0].ToString());
            } else {
                activeList.Add(ddlArray[index].SelectedValue);
            }
            benchList.Remove(ddlArray[index].SelectedValue);
            
        }

        /// <summary>
        /// Populate an array of ListItems based on an ArrayList.
        /// </summary>
        /// <param name="benchList">The array to be populated</param>
        /// <param name="Bench">The list to base the array on</param>
        private static void PopulateListItemArray(ListItem[] benchList, ArrayList Bench) {
            for (int i = 0; i < benchList.Length; i++) {
                benchList[i] = new ListItem(Bench[i].ToString());
            }
        }

        private void UpdateDropDownLists() {
            //Create arrays of ListItems to be added to the dropdown lists
            ListItem[] benchDefenderList = new ListItem[SessionHandler.BenchDefenders.Count];
            PopulateListItemArray(benchDefenderList, SessionHandler.BenchDefenders);

            ListItem[] benchMidfielderList = new ListItem[SessionHandler.BenchMidfielders.Count];
            PopulateListItemArray(benchMidfielderList, SessionHandler.BenchMidfielders);

            ListItem[] benchStrikersList = new ListItem[SessionHandler.BenchStrikers.Count];
            PopulateListItemArray(benchStrikersList, SessionHandler.BenchStrikers);

            ListItem[] benchGoaliesList = new ListItem[SessionHandler.BenchGoalies.Count];
            PopulateListItemArray(benchGoaliesList, SessionHandler.BenchGoalies);

            ArrayList bench = new ArrayList(SessionHandler.BenchDefenders);
            bench.AddRange(SessionHandler.BenchMidfielders);
            bench.AddRange(SessionHandler.BenchStrikers);
            bench.AddRange(SessionHandler.BenchGoalies);
            tbBench.DataSource = bench;
            tbBench.DataBind();


            for (int i = 0; i < 4; i++) {
                ResetDropDownList(benchDefenderList, i, SessionHandler.ActiveDefenders, defenderddls);
            }
            
            for (int i = 0; i < 4; i++) {
                ResetDropDownList(benchMidfielderList, i, SessionHandler.ActiveMidfielders, midfielderddls);
            }

            //Add the dropdown lists for strikers and populate them
            for (int i = 0; i < 2; i++) {
                ResetDropDownList(benchStrikersList, i, SessionHandler.ActiveStrikers, strikerddls);
            }

            //Add the dropdown lists for goalies and populate them
            for (int i = 0; i < 1; i++) {
                ResetDropDownList(benchGoaliesList, i, SessionHandler.ActiveGoalies, goalieddls);
            }
        }

        private void ResetDropDownList(ListItem[] benchDefenderList, int index, ArrayList playerList, DropDownList[] ddlArray) {
            ddlArray[index].ClearSelection();
            ddlArray[index].Items.Clear();
            ddlArray[index].Items.AddRange(benchDefenderList);

            //If there are active players, set the initial value of the dropdown to those players
            if (playerList.Count > index) {
                ddlArray[index].Items.Insert(0, playerList[index].ToString());
            } else {
                ddlArray[index].Items.Insert(0, "Select a player");
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e) {
            SqlConnection con = new SqlConnection(strConnString1);
            try {
                con.Open();
                SqlCommand updateCommand = new SqlCommand();
                updateCommand.Connection = con;

                //Update Defenders
                foreach (Object player in SessionHandler.ActiveDefenders) {
                    UpdatePlayers(updateCommand, player, true);
                }
                foreach (Object player in SessionHandler.BenchDefenders) {
                    UpdatePlayers(updateCommand, player, false);
                }

                //Update Midfielders
                foreach (Object player in SessionHandler.ActiveMidfielders) {
                    UpdatePlayers(updateCommand, player, true);
                }
                foreach (Object player in SessionHandler.BenchMidfielders) {
                    UpdatePlayers(updateCommand, player, false);
                }

                //Update Strikers
                foreach (Object player in SessionHandler.ActiveStrikers) {
                    UpdatePlayers(updateCommand, player, true);
                }
                foreach (Object player in SessionHandler.BenchStrikers) {
                    UpdatePlayers(updateCommand, player, false);
                }

                //Update Goalies
                foreach (Object player in SessionHandler.ActiveGoalies) {
                    UpdatePlayers(updateCommand, player, true);
                }
                foreach (Object player in SessionHandler.BenchGoalies) {
                    UpdatePlayers(updateCommand, player, false);
                }
            } catch (SqlException ex) {
                NotifyLabel.Text = "There was an issue updating your lineup. Your lineup has not been set. Please try again later.";
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return;
            } finally {
                con.Close();
            }
            NotifyLabel.Text = "Your lineup has been set!";
        }

        private bool CheckSelections(DropDownList[] ddls) {
            foreach (DropDownList name in ddls) {
                if (name.SelectedValue == "Select a player") {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Update a player's status to either active or inactive.
        /// </summary>
        /// <param name="updateCommand">The command used to update the database.</param>
        /// <param name="player">The player whose status is to be updated.</param>
        /// <param name="active">True to set the player to active, false for inactive.</param>
        private static void UpdatePlayers(SqlCommand updateCommand, Object player, bool active) {
            int i = (active) ? 1 : 0;
            updateCommand.CommandText =
                 "UPDATE LineupHistory " +
                 "SET LineupHistory.Active = " + i + " " +
                 "FROM LineupHistory " +
                 "JOIN Players ON LineupHistory.PlayerId = Players.PlayerId " +
                 "WHERE concat(Players.FirstName, ' ', Players.LastName) " +
                     " = '" + player.ToString() + "'";
            try { 
                updateCommand.ExecuteNonQuery();
            } catch (SqlException ex) {
                throw (ex);
            } 
        }

        /// <summary>
        /// RESET ALL SESSION DATA TO ORIGINAL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e) {
            Response.Redirect(Request.RawUrl);
        }
    }
}