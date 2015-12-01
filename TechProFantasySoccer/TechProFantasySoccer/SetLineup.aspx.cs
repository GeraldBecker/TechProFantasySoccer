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
    /// <summary>
    /// The page used to update a user's lineup
    /// 
    /// Author: Wilson Carpenter
    /// </summary>
    public partial class SetLineup : System.Web.UI.Page {
        /// <summary>
        /// Number of slots for active players by position
        /// </summary>
        private const int STRIKER_SLOTS = 2;
        private const int MIDFIELDER_SLOTS = 4;
        private const int DEFENDER_SLOTS = 4;
        private const int GOALIE_SLOTS = 1;
        
        /// <summary>
        /// Dropdown list arrays for each position
        /// </summary>
        private DropDownList[] defenderddls;
        private DropDownList[] midfielderddls;
        private DropDownList[] strikerddls;
        private DropDownList[] goalieddls;

        /// <summary>
        /// 
        /// </summary>
        static String strConnString1 = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
        SqlConnection con1 = new SqlConnection(strConnString1);
        private DataRow[] activeDefenders;
        private DataRow[] inActiveDefenders;
        private DataRow[] activeMidfielders;
        private DataRow[] inActiveMidfielders;      
        private DataRow[] activeStrikers;
        private DataRow[] inActiveStrikers;
        private DataRow[] activeGoalies;
        private DataRow[] inActiveGoalies;

        protected void Page_Load(object sender, EventArgs e) {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is a member of the fantasy pool
            string user = User.Identity.GetUserId();
            if (!AuthLevelCheck.isUser(user))
                Response.Redirect("~/AccessDenied");

            SqlCommand getPlayersCommand = new SqlCommand();
            DataTable playersTable = new DataTable();
            DataRow[] allActive;
            if (!Page.IsPostBack) {
                getPlayersCommand.CommandText =
                    "SELECT concat(Players.FirstName, ' ', Players.LastName) AS Name, Players.PositionRef AS Position, " +
                    "LineupHistory.Active AS Active, " + "Players.FirstName, Players.LastName " +
                    "FROM Players " +
                    "JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId " +
                    "JOIN Positions ON Positions.PositionRef = Players.PositionRef " +
                    "WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE()) " +
                    "AND LineupHistory.UserId = '" + User.Identity.GetUserId() + "' " +
                    "ORDER BY Position DESC";
                
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

                // Add the active players for the current month to the listbox
                allActive = playersTable.Select("Active = 1");
                foreach (DataRow row in allActive) {
                    ListItem listItem = new ListItem(row["Name"].ToString());
                    lbActivePlayers.Items.Add(listItem);
                }

                //------------------------------------------------------------------------
                //DEFENDERS
                //------------------------------------------------------------------------

                //Get the defenders that are currently active
                activeDefenders = playersTable.Select("Active = 1 AND Position = 3");
                SessionHandler.ActiveDefenders = new List<string>();
                SetSessionData(activeDefenders, SessionHandler.ActiveDefenders);

                //Get the bench defenders and add them into a string array session variable
                inActiveDefenders = playersTable.Select("Active = 0 AND Position = 3");
                SessionHandler.BenchDefenders = new List<string>();
                SetSessionData(inActiveDefenders, SessionHandler.BenchDefenders);

                //------------------------------------------------------------------------
                //MIDFIELDERS
                //------------------------------------------------------------------------

                //get the active midfielders and add them into a string array session variable
                activeMidfielders = playersTable.Select("Active = 1 AND Position = 2");
                SessionHandler.ActiveMidfielders = new List<string>();
                SetSessionData(activeMidfielders, SessionHandler.ActiveMidfielders);

                //Get the bench midfielders and add them into a string array session variable
                inActiveMidfielders = playersTable.Select("Active = 0 AND Position = 2");
                SessionHandler.BenchMidfielders = new List<string>();
                SetSessionData(inActiveMidfielders, SessionHandler.BenchMidfielders);

                //---------------------------------------------------------------------------
                //STRIKERS
                //---------------------------------------------------------------------------

                //get the active strikers and add them into a string array session variable
                activeStrikers = playersTable.Select("Active = 1 AND Position = 1");
                SessionHandler.ActiveStrikers = new List<string>();
                SetSessionData(activeStrikers, SessionHandler.ActiveStrikers);

                //Get the bench strikers
                inActiveStrikers = playersTable.Select("Active = 0 AND Position = 1");
                SessionHandler.BenchStrikers = new List<string>();
                SetSessionData(inActiveStrikers, SessionHandler.BenchStrikers);

                //-----------------------------------------------------------------------------
                //GOALIES
                //-----------------------------------------------------------------------------

                activeGoalies = playersTable.Select("Active = 1 AND Position = 4");
                SessionHandler.ActiveGoalies = new List<string>();
                SetSessionData(activeGoalies, SessionHandler.ActiveGoalies);

                inActiveGoalies = playersTable.Select("Active = 0 AND Position = 4");
                SessionHandler.BenchGoalies = new List<string>();
                SetSessionData(inActiveGoalies, SessionHandler.BenchGoalies);
            }
            int i = 0;
            foreach (ListItem item in lbActivePlayers.Items) {
                if (i % 2 == 0)
                    item.Attributes["class"] = "player";
                else
                    item.Attributes["class"] = "player2";
                i++;
            }

            InitDropDownLists();
        }

        /// <summary>
        /// Populate a session list from a datarow array
        /// </summary>
        /// <param name="data">Data from the database</param>
        /// <param name="sessionList">The session list to be populated</param>
        private void SetSessionData(DataRow[] data, List<string> sessionList) {
            sessionList.Clear();
            for (int i = 0; i < data.Length; i++) {
                sessionList.Add(data[i]["Name"].ToString());
            }
        }

        /// <summary>
        /// Initialize all the dropdown lists and populate them.
        /// </summary>
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

            // Set the bench
            List<string> bench = new List<string>();
            foreach (string name in SessionHandler.BenchDefenders) {
                bench.Add(name + " - Defender");
            }
            foreach (string name in SessionHandler.BenchMidfielders) {
                bench.Add(name + " - Midfielder");
            }
            foreach (string name in SessionHandler.BenchStrikers) {
                bench.Add(name + " - Striker");
            }
            foreach (string name in SessionHandler.BenchGoalies) {
                bench.Add(name + " - Goalie");
            }
            //bench.AddRange(SessionHandler.BenchMidfielders);
            //bench.AddRange(SessionHandler.BenchStrikers);
            //bench.AddRange(SessionHandler.BenchGoalies);
            tbBench.DataSource = bench;
            tbBench.DataBind();
            int j = 0;
            foreach (ListItem item in tbBench.Items) {
                if (j % 2 == 0)
                    item.Attributes["class"] = "player";
                else
                    item.Attributes["class"] = "player2";
                j++;
            }

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

            // Update the bench
            List<string> bench = new List<string>();
            foreach (string name in SessionHandler.BenchDefenders) {
                bench.Add(name + " - Defender");
            }
            foreach (string name in SessionHandler.BenchMidfielders) {
                bench.Add(name + " - Midfielder");
            }
            foreach (string name in SessionHandler.BenchStrikers) {
                bench.Add(name + " - Striker");
            }
            foreach (string name in SessionHandler.BenchGoalies) {
                bench.Add(name + " - Goalie");
            }
            tbBench.DataSource = bench;
            tbBench.DataBind();
            int j = 0;
            foreach (ListItem item in tbBench.Items) {
                if (j % 2 == 0)
                    item.Attributes["class"] = "player";
                else
                    item.Attributes["class"] = "player2";
                j++;
            }



            // Repopulate all the dropdown lists
            for (int i = 0; i < 4; i++) {
                ResetDropDownList(benchDefenderList, i, SessionHandler.ActiveDefenders, defenderddls);
            }

            for (int i = 0; i < 4; i++) {
                ResetDropDownList(benchMidfielderList, i, SessionHandler.ActiveMidfielders, midfielderddls);
            }

            for (int i = 0; i < 2; i++) {
                ResetDropDownList(benchStrikersList, i, SessionHandler.ActiveStrikers, strikerddls);
            }

            for (int i = 0; i < 1; i++) {
                ResetDropDownList(benchGoaliesList, i, SessionHandler.ActiveGoalies, goalieddls);
            }
        }

        /// <summary>
        /// Configure and populate a dropdown list.
        /// </summary>
        /// <param name="benchStrikersList">The array used to populate the dropdown list</param>
        /// <param name="index">The current index</param>
        /// <param name="ddl">The dropdown list to configure</param>
        /// <param name="playerList">Active players list</param>
        /// <param name="ddlArray">The array of dropdown lists to add to</param>
        private void SetDropDownList(ListItem[] benchStrikersList, int index, DropDownList ddl, List<string> playerList, DropDownList[] ddlArray) {
            ddl.Width = 300;
            ddl.Height = 30;
            ddl.Items.AddRange(benchStrikersList);
            ddl.SelectedIndexChanged += playerddls_SelectedIndexChanged;
            ddl.AutoPostBack = true;
            ddlArray[index] = ddl;

            //If there are active players, set the initial value to those players
            if (playerList.Count > index) {
                ddl.Items.Insert(0, playerList[index].ToString());
            } else {
                ddl.Items.Insert(0, "Select a player");
            }
        }

        /// <summary>
        /// Update the bench list and active list when the user selects a new player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            // After the lists are updated, repopulate the dropdown lists
            UpdateDropDownLists();
        }

        /// <summary>
        /// Update both the bench and active players lists to be stored in sessions.
        /// </summary>
        /// <param name="index">The index of the dropdownlist to use to modify the lists.</param>
        /// <param name="benchList">The list of bench players to be updated.</param>
        /// <param name="activeList">The list of active players to be updated.</param>
        /// <param name="ddlArray">The array of dropdownlists.</param>
        private void UpdateActiveAndBench(int index, List<string> benchList, List<string> activeList, DropDownList[] ddlArray) {
            // Get the currently active player and the selected player
            string oldActive = ddlArray[index].Items[0].ToString();
            string newActive = ddlArray[index].SelectedValue;

            // If there is a currently active player in this spot, we need to add that player to the bench, and activate 
            // the selected player
            if (oldActive != "Select a player") {
                benchList.Add(oldActive);
                // we want to make sure we add the new player to the same index as the one we remove
                activeList.Insert(activeList.IndexOf(oldActive), newActive);
                activeList.Remove(oldActive);
            } else {
                // Otherwise, we just need to add the selected player to the end of the list
                activeList.Add(newActive);
            }
            // Either way, the activated player needs to be removed from the bench
            benchList.Remove(newActive);
        }

        /// <summary>
        /// Populate an array of ListItems based on a List.
        /// </summary>
        /// <param name="benchList">The array to be populated</param>
        /// <param name="Bench">The list to base the array on</param>
        private static void PopulateListItemArray(ListItem[] benchList, List<string> Bench) {
            for (int i = 0; i < benchList.Length; i++) {
                benchList[i] = new ListItem(Bench[i].ToString());
            }
        }

        /// <summary>
        /// Repopulate the dropdownlists based on the new bench lists and active lists, and display the new bench
        /// </summary>


        /// <summary>
        /// Repopulate a dropdown list.
        /// </summary>
        /// <param name="benchList">The list of bench players to add.</param>
        /// <param name="index"></param>
        /// <param name="playerList"></param>
        /// <param name="ddlArray"></param>
        private void ResetDropDownList(ListItem[] benchList, int index, List<string> playerList, DropDownList[] ddlArray) {
            // Clear everything from the dropdown lists and add the bench defenders
            ddlArray[index].ClearSelection();
            ddlArray[index].Items.Clear();
            ddlArray[index].Items.AddRange(benchList);

            //If there are active players, set the initial value of the dropdown to those players
            if (playerList.Count > index) {
                ddlArray[index].Items.Insert(0, playerList[index].ToString());
            } else {
                ddlArray[index].Items.Insert(0, "Select a player");
            }
        }

        /// <summary>
        /// Checks if there are any unselected players
        /// </summary>
        /// <param name="ddls"></param>
        /// <returns></returns>
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
        /// <param name="isActive">True to set the player to active, false for inactive.</param>
        private  void UpdatePlayers(Object player, bool isActive) {
            int active = (isActive) ? 1 : 0;
            int nextMonth = 0;
            SqlConnection con = new SqlConnection(strConnString1);
            SqlCommand selectCommand = new SqlCommand();
            SqlCommand insertCommand = new SqlCommand();
            SqlCommand updateCommand = new SqlCommand();
            DataTable resultTable = new DataTable();
            if (DateTime.Now.Month == 12) {
                nextMonth = 1;
            } else {
                nextMonth = DateTime.Now.Month + 1;
            }
            try {
                con.Open();
                selectCommand.Connection = con;
                insertCommand.Connection = con;
                updateCommand.Connection = con;

                // Check if the row already exists in the database.
                selectCommand.CommandText =
                    "SELECT * FROM LineupHistory " +
                    "WHERE userId = '" + User.Identity.GetUserId() + "' AND " +
                          "PlayerId = (SELECT PlayerId FROM Players " +
                                      "WHERE concat(FirstName, ' ', LastName) = '" + player.ToString() + "' AND " +
                                      "Month = DATEPART(MONTH, GETDATE()) + " + nextMonth + ")";
                // The insert command, to be executed if the row does not exist in the database.
                insertCommand.CommandText =
                     "INSERT INTO LineupHistory (UserId, PlayerId, Month, Active) " +
                     "VALUES ('" + User.Identity.GetUserId() + "', " +
                            "(SELECT PlayerId FROM Players " +
                             "WHERE concat(FirstName, ' ', LastName) = '" + player.ToString() + "'), " +
                             "DATEPART(MONTH, GETDATE()) + " + nextMonth + ", " +
                     active + ")";
                // the update command, to be executed if the row already exists
                updateCommand.CommandText =
                     "UPDATE LineupHistory " +
                     "SET   LineupHistory.Active = " + active + " " +
                     "FROM  LineupHistory " +
                     "JOIN  Players ON LineupHistory.PlayerId = Players.PlayerId " +
                     "WHERE concat(Players.FirstName, ' ', Players.LastName) " + " = '" + player.ToString() + "'" +
                           "AND Month = DATEPART(MONTH, GETDATE()) + " + nextMonth;


                resultTable.Load(selectCommand.ExecuteReader());
                // If the row exists, update, otherwise insert
                if (resultTable.Rows.Count != 0) {
                    updateCommand.ExecuteNonQuery();
                } else {
                    insertCommand.ExecuteNonQuery();
                }
            } catch (SqlException ex) {
                NotifyLabel.Text = "There was an error updating the database. Please try again later";
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            } finally {
                con.Close();
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

        /// <summary>
        /// Submit the lineup selction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e) {
            try {

                //Update Defenders
                foreach (Object player in SessionHandler.ActiveDefenders) {
                    UpdatePlayers(player, true);
                }
                foreach (Object player in SessionHandler.BenchDefenders) {
                    UpdatePlayers(player, false);
                }

                //Update Midfielders
                foreach (Object player in SessionHandler.ActiveMidfielders) {
                    UpdatePlayers(player, true);
                }
                foreach (Object player in SessionHandler.BenchMidfielders) {
                    UpdatePlayers(player, false);
                }

                //Update Strikers
                foreach (Object player in SessionHandler.ActiveStrikers) {
                    UpdatePlayers(player, true);
                }
                foreach (Object player in SessionHandler.BenchStrikers) {
                    UpdatePlayers(player, false);
                }

                //Update Goalies
                foreach (Object player in SessionHandler.ActiveGoalies) {
                    UpdatePlayers(player, true);
                }
                foreach (Object player in SessionHandler.BenchGoalies) {
                    UpdatePlayers(player, false);
                }
            } catch (SqlException ex) {
                NotifyLabel.Text = "There was an issue updating your lineup. Your lineup has not been set. Please try again later.";
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            } finally {
            }
            NotifyLabel.Text = "Your lineup for next month has been set. Changes will not take effect until the next month has begun.";
        }
    }
}