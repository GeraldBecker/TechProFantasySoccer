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

                getPlayersCommand.Connection = con1;

                DataTable playersTable = new DataTable();
                con1.Open();

                playersTable.Load(getPlayersCommand.ExecuteReader());

                //------------------------------------------------------------------------
                //DEFENDERS
                //------------------------------------------------------------------------

                //Get the defenders that are currently active
                activeDefenders = playersTable.Select("Active = 1 AND Position = 3");
                SessionHandler.ActiveDefenders = new ArrayList();
                SetSessionData(activeDefenders, SessionHandler.ActiveDefenders);

                //////////////////////////////////////////////////////////////////////////////
                
                /////////////////////////////////////////////////////////////////////////////////

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
            defenderddls = new DropDownList[4];
            for (int i = 0; i < 4; i++) {
                DropDownList ddl = new DropDownList();
                ddl.Width = 300;
                ddl.Height = 30;
                ddl.Items.AddRange(benchDefenderList);
                ddl.SelectedIndexChanged += playerddls_SelectedIndexChanged;
                ddl.AutoPostBack = true;
                defenderddls[i] = ddl;

                //If there are active players, set the initial value of the dropdown to those players
                if (SessionHandler.ActiveDefenders.Count > i) {
                    ddl.Items.Insert(0, SessionHandler.ActiveDefenders[i].ToString());
                } else {
                    ddl.Items.Insert(0, "Select a player");
                }

                DefenderPanel.Controls.Add(ddl);
            }

            //Add the dropdown lists for midfielders and populate them
            midfielderddls = new DropDownList[4];
            for (int i = 0; i < 4; i++) {
                DropDownList ddlMidfielder = new DropDownList();
                ddlMidfielder.Width = 300;
                ddlMidfielder.Height = 30;
                ddlMidfielder.Items.AddRange(benchMidfielderList);
                ddlMidfielder.SelectedIndexChanged += playerddls_SelectedIndexChanged;
                ddlMidfielder.AutoPostBack = true;
                midfielderddls[i] = ddlMidfielder;

                //If there are active players, set the initial value to those players
                if (SessionHandler.ActiveMidfielders.Count > i) {
                    ddlMidfielder.Items.Insert(0, SessionHandler.ActiveMidfielders[i].ToString());
                } else {
                    ddlMidfielder.Items.Insert(0, "Select a player");
                }

                MidfielderPanel.Controls.Add(ddlMidfielder);
            }

            //Add the dropdown lists for strikers and populate them
            strikerddls = new DropDownList[2];
            for (int i = 0; i < 2; i++) {
                DropDownList ddlStriker = new DropDownList();
                ddlStriker.Width = 300;
                ddlStriker.Height = 30;
                ddlStriker.Items.AddRange(benchStrikersList);
                ddlStriker.SelectedIndexChanged += playerddls_SelectedIndexChanged;
                ddlStriker.AutoPostBack = true;
                strikerddls[i] = ddlStriker;

                //If there are active players, set the initial value to those players
                if (SessionHandler.ActiveStrikers.Count > i) {
                    ddlStriker.Items.Insert(0, SessionHandler.ActiveStrikers[i].ToString());
                } else {
                    ddlStriker.Items.Insert(0, "Select a player");
                }

                StrikerPanel.Controls.Add(ddlStriker);

            }

            goalieddls = new DropDownList[1];
            for (int i = 0; i < 1; i++) {
                DropDownList ddlGoalie = new DropDownList();
                ddlGoalie.Width = 300;
                ddlGoalie.Height = 30;
                ddlGoalie.Items.AddRange(benchGoaliesList);
                ddlGoalie.SelectedIndexChanged += playerddls_SelectedIndexChanged;
                ddlGoalie.AutoPostBack = true;
                goalieddls[i] = ddlGoalie;

                //If there are active players, set the initial value to those players
                if (SessionHandler.ActiveGoalies.Count > i) {
                    ddlGoalie.Items.Insert(0, SessionHandler.ActiveGoalies[i].ToString());
                } else {
                    ddlGoalie.Items.Insert(0, "Select a player");
                }

                GoaliePanel.Controls.Add(ddlGoalie);

            }
        }

        private void playerddls_SelectedIndexChanged(Object sender, EventArgs e) {
            DropDownList ddl = (DropDownList)sender;

            if (ddl == defenderddls[0]) {
                UpdateActiveAndBench(0, SessionHandler.BenchDefenders, SessionHandler.ActiveDefenders, defenderddls);
            } if (ddl == defenderddls[1]) {
                UpdateActiveAndBench(1, SessionHandler.BenchDefenders, SessionHandler.ActiveDefenders, defenderddls);
            } if (ddl == defenderddls[2]) {
                UpdateActiveAndBench(2, SessionHandler.BenchDefenders, SessionHandler.ActiveDefenders, defenderddls);
            } if (ddl == defenderddls[3]) {
                UpdateActiveAndBench(3, SessionHandler.BenchDefenders, SessionHandler.ActiveDefenders, defenderddls);
            }

            if (ddl == midfielderddls[0]) {
                UpdateActiveAndBench(0, SessionHandler.BenchMidfielders, SessionHandler.ActiveMidfielders, midfielderddls);
            } if (ddl == midfielderddls[1]) {
                UpdateActiveAndBench(1, SessionHandler.BenchMidfielders, SessionHandler.ActiveMidfielders, midfielderddls);
            } if (ddl == midfielderddls[2]) {
                UpdateActiveAndBench(2, SessionHandler.BenchMidfielders, SessionHandler.ActiveMidfielders, midfielderddls);
            } if (ddl == midfielderddls[3]) {
                UpdateActiveAndBench(3, SessionHandler.BenchMidfielders, SessionHandler.ActiveMidfielders, midfielderddls);
            }

            if (ddl == strikerddls[0]) {
                UpdateActiveAndBench(0, SessionHandler.BenchStrikers, SessionHandler.ActiveStrikers, strikerddls);
            } if (ddl == strikerddls[1]) {
                UpdateActiveAndBench(1, SessionHandler.BenchStrikers, SessionHandler.ActiveStrikers, strikerddls);
            }

            if (ddl == goalieddls[0]) {
                UpdateActiveAndBench(0, SessionHandler.BenchGoalies, SessionHandler.ActiveGoalies, goalieddls);
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
                activeList.Remove(ddlArray[index].Items[0].ToString());
            }
            benchList.Remove(ddlArray[index].SelectedValue);
            activeList.Add(ddlArray[index].SelectedValue);
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
                defenderddls[i].ClearSelection();
                defenderddls[i].Items.Clear();
                defenderddls[i].Items.AddRange(benchDefenderList);

                //If there are active players, set the initial value of the dropdown to those players
                if (SessionHandler.ActiveDefenders.Count > i) {
                    defenderddls[i].Items.Insert(0, SessionHandler.ActiveDefenders[i].ToString());
                } else {
                    defenderddls[i].Items.Insert(0, "Select a player");
                }
            }
            
            for (int i = 0; i < 4; i++) {
                midfielderddls[i].ClearSelection();
                midfielderddls[i].Items.Clear();
                midfielderddls[i].Items.AddRange(benchMidfielderList);

                //If there are active players, set the initial value to those players
                if (SessionHandler.ActiveMidfielders.Count > i) {
                    midfielderddls[i].Items.Insert(0, SessionHandler.ActiveMidfielders[i].ToString());
                } else {
                    midfielderddls[i].Items.Insert(0, "Select a player");
                }
            }

            //Add the dropdown lists for strikers and populate them
            for (int i = 0; i < 2; i++) {
                strikerddls[i].ClearSelection();
                strikerddls[i].Items.Clear();
                strikerddls[i].Items.AddRange(benchStrikersList);

                //If there are active players, set the initial value to those players
                if (SessionHandler.ActiveStrikers.Count > i) {
                    strikerddls[i].Items.Insert(0, SessionHandler.ActiveStrikers[i].ToString());
                } else {
                    strikerddls[i].Items.Insert(0, "Select a player");
                }
            }

            //Add the dropdown lists for goalies and populate them
            for (int i = 0; i < 1; i++) {
                goalieddls[i].ClearSelection();
                goalieddls[i].Items.Clear();
                goalieddls[i].Items.AddRange(benchGoaliesList);

                //If there are active players, set the initial value to those players
                if (SessionHandler.ActiveGoalies.Count > i) {
                    goalieddls[i].Items.Insert(0, SessionHandler.ActiveGoalies[i].ToString());
                } else {
                    goalieddls[i].Items.Insert(0, "Select a player");
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e) {
            SqlConnection con = new SqlConnection(strConnString1);
            con.Open();
            SqlCommand updateCommand = new SqlCommand();
            updateCommand.Connection = con;

            if (!CheckSelections(defenderddls) || !CheckSelections(midfielderddls) || 
                        !CheckSelections(strikerddls)  || !CheckSelections(goalieddls)) {
                NotifyLabel.Text = "Please select a player for all active slots";
                return;
            }

            //Update Defenders
            foreach (Object player in SessionHandler.ActiveDefenders) {
                UpdateActive(updateCommand, player);
            }
            foreach (Object player in SessionHandler.BenchDefenders) {
                UpdateInactive(updateCommand, player);
            }

            //Update Midfielders
            foreach (Object player in SessionHandler.ActiveMidfielders) {
                UpdateActive(updateCommand, player);
            }
            foreach (Object player in SessionHandler.BenchMidfielders) {
                UpdateInactive(updateCommand, player);
            }

            //Update Strikers
            foreach (Object player in SessionHandler.ActiveStrikers) {
                UpdateActive(updateCommand, player);
            }
            foreach (Object player in SessionHandler.BenchStrikers) {
                UpdateInactive(updateCommand, player);
            }

            //Update Goalies
            foreach (Object player in SessionHandler.ActiveGoalies) {
                UpdateActive(updateCommand, player);
            }
            foreach (Object player in SessionHandler.BenchGoalies) {
                UpdateInactive(updateCommand, player);
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

        private static void UpdateInactive(SqlCommand updateCommand, Object player) {
            updateCommand.CommandText =
                 "UPDATE LineupHistory " +
                 "SET LineupHistory.Active = 0 " +
                 "FROM LineupHistory " +
                 "JOIN Players ON LineupHistory.PlayerId = Players.PlayerId " +
                 "WHERE concat(Players.FirstName, ' ', Players.LastName) " +
                     " = '" + player.ToString() + "'";

            updateCommand.ExecuteNonQuery();
        }

        private static void UpdateActive(SqlCommand updateCommand, Object player) {
            updateCommand.CommandText =
                "UPDATE LineupHistory " +
                "SET LineupHistory.Active = 1 " +
                "FROM LineupHistory " +
                "JOIN Players ON LineupHistory.PlayerId = Players.PlayerId " +
                "WHERE concat(Players.FirstName, ' ', Players.LastName) " +
                    " = '" + player.ToString() + "'";

            updateCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// RESET ALL SESSION DATA TO ORIGINAL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e) {
            //SetSessionData(activeDefenders, SessionHandler.ActiveDefenders);
            //SetSessionData(inActiveDefenders, SessionHandler.BenchDefenders);

            //SetSessionData(activeMidfielders, SessionHandler.ActiveMidfielders);
            //SetSessionData(inActiveMidfielders, SessionHandler.BenchMidfielders);

            //SetSessionData(activeStrikers, SessionHandler.ActiveStrikers);
            //SetSessionData(inActiveStrikers, SessionHandler.BenchStrikers);

            //SetSessionData(activeGoalies, SessionHandler.ActiveGoalies);
            //SetSessionData(inActiveGoalies, SessionHandler.BenchGoalies);
            
            Response.Redirect(Request.RawUrl);
        }
    }
}