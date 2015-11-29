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

/// <summary>
/// Author: Wilson 
/// </summary>
namespace TechProFantasySoccer.Admin {
    /// <summary>
    /// Adds a league to the fantasy website. 
    /// </summary>
    public partial class AddLeague : System.Web.UI.Page {
        /// <summary>
        /// Displays all the current leagues and provides text boxes for adding a new one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is an Admin
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isAdmin(user))
                Response.Redirect("~/AccessDenied");

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText =
                "SELECT " +
                "Leagues.LeagueName AS 'League Name' " +
                "FROM Leagues " +
                "ORDER BY Leagues.LeagueName";
            cmd.Connection = con;

            try {
                DataTable temp = new DataTable();
                con.Open();
                LeagueGridView.EmptyDataText = "No Records Found";
                temp.Load(cmd.ExecuteReader());

                LeagueGridView.DataSource = temp;
                LeagueGridView.DataBind();
            } catch (System.Data.SqlClient.SqlException ex) {
                ;
            } finally {
                con.Close();
            }

            ModifyRows();
        }

        
        /// <summary>
        /// Alternates the row colours. 
        /// </summary>
        private void ModifyRows() {
            for (int i = 0; i < LeagueGridView.Rows.Count; i++) {
                string classList = "selectedblackout";

                LeagueGridView.Rows[i].Attributes.Add("data-href", "../Players/ViewPlayer.aspx?player=" +
                    LeagueGridView.Rows[i].Cells[0].Text);

                if ((i % 2) == 1)
                    classList += " alternaterow";

                LeagueGridView.Rows[i].Attributes.Add("class", classList);

            }
        }

        /// <summary>
        /// Inserts the new league.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e) {
            string leagueName = LeagueNameTextBox.Text;
            LeagueNameTextBox.Text = "";

            FantasyDataSource.InsertParameters["LeagueName"].DefaultValue = leagueName;

            FantasyDataSource.Insert();

            Response.Redirect("./AddLeague");
        }

        /// <summary>
        /// Controls the paging of the leagues.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LeagueGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            LeagueGridView.PageIndex = e.NewPageIndex;
            LeagueGridView.DataBind();
            ModifyRows();
        }
    }
}