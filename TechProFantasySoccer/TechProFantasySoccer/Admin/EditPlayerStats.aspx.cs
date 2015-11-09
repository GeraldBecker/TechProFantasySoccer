using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace TechProFantasySoccer.Admin {
    public partial class EditPlayerStats : System.Web.UI.Page {
        int month;
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Check if the user is an Admin
            string user = User.Identity.GetUserId();
            cmd.CommandText =
                "SELECT Access " +
                "FROM AccessLevel " +
                "WHERE UserId = '" + user + "'";

            cmd.Connection = con;
            try {
                con.Open();
                int accessLevel = (int)cmd.ExecuteScalar();

                if(accessLevel != 1)
                    Response.Redirect("~/");

            } catch(System.Data.SqlClient.SqlException ex) {
                Response.Redirect("~/");
            } finally {
                con.Close();
            }

            try {
                if(Request.QueryString["month"] == null) {
                    month = DateTime.Now.Month;
                    Response.Redirect("EditPlayerStats.aspx?month=" + month);
                } else {
                    month = int.Parse(Request.QueryString["month"]);
                }
            } catch(FormatException ex) {
                month = DateTime.Now.Month;
                Response.Redirect("EditPlayerStats.aspx?month=" + month);
            }
            SqlDataSource1.SelectParameters["Month"].DefaultValue = month.ToString();
            SqlDataSource1.UpdateParameters["Month"].DefaultValue = month.ToString();
        }

        protected void UpdateButton_Click(object sender, EventArgs e) {
            try {

                foreach(DataListItem item in DataList1.Items) {
                    //String RecordId = DataList1.DataKeys[item.ItemIndex].ToString();
                    String StatId = DataList1.DataKeys[item.ItemIndex].ToString();
                    String Goals = ((TextBox)item.FindControl("GoalsTextBox")).Text;
                    String Shots = ((TextBox)item.FindControl("ShotsTextBox")).Text;
                    String Assists = ((TextBox)item.FindControl("AssistsTextBox")).Text;
                    String MinPlayed = ((TextBox)item.FindControl("MinPlayedTextBox")).Text;
                    String Fouls = ((TextBox)item.FindControl("FoulsTextBox")).Text;
                    String YellowCards = ((TextBox)item.FindControl("YCTextBox")).Text;
                    String RedCards = ((TextBox)item.FindControl("RCTextBox")).Text;
                    String GoalsAllowed = ((TextBox)item.FindControl("GATextBox")).Text;
                    String SavesMade = ((TextBox)item.FindControl("SavesTextBox")).Text;
                    String CleanSheets = ((TextBox)item.FindControl("CSTextBox")).Text;



                    String prevGoals = ((Label)item.FindControl("GoalsLabel")).Text;
                    String prevShots = ((Label)item.FindControl("ShotsLabel")).Text;
                    String prevAssists = ((Label)item.FindControl("AssistsLabel")).Text;
                    String prevMinPlayed = ((Label)item.FindControl("MinPlayedLabel")).Text;
                    String prevFouls = ((Label)item.FindControl("FoulsLabel")).Text;
                    String prevYC = ((Label)item.FindControl("YCLabel")).Text;
                    String prevRC = ((Label)item.FindControl("RCLabel")).Text;
                    String prevGA = ((Label)item.FindControl("GALabel")).Text;
                    String prevSaves = ((Label)item.FindControl("SavesLabel")).Text;
                    String prevCS = ((Label)item.FindControl("CSLabel")).Text;


                    if(Goals != prevGoals || Shots != prevShots || Assists != prevAssists || MinPlayed != prevMinPlayed
                        || Fouls != prevFouls || YellowCards != prevYC || RedCards != prevRC 
                        || GoalsAllowed != prevGA || SavesMade != prevSaves || CleanSheets != prevCS) {
                        //SqlDataSource1.UpdateParameters["PlayerId"].DefaultValue = RecordId;
                        SqlDataSource1.UpdateParameters["StatIdParam"].DefaultValue = StatId;
                        SqlDataSource1.UpdateParameters["GoalsParam"].DefaultValue = Goals;
                        SqlDataSource1.UpdateParameters["ShotsParam"].DefaultValue = Shots;
                        SqlDataSource1.UpdateParameters["AssistsParam"].DefaultValue = Assists;
                        SqlDataSource1.UpdateParameters["MinPlayedParam"].DefaultValue = MinPlayed;
                        SqlDataSource1.UpdateParameters["FoulsParam"].DefaultValue = Fouls;
                        SqlDataSource1.UpdateParameters["YCParam"].DefaultValue = YellowCards;
                        SqlDataSource1.UpdateParameters["RCParam"].DefaultValue = RedCards;
                        SqlDataSource1.UpdateParameters["GAParam"].DefaultValue = GoalsAllowed;
                        SqlDataSource1.UpdateParameters["SavesParam"].DefaultValue = SavesMade;
                        SqlDataSource1.UpdateParameters["CSParam"].DefaultValue = CleanSheets;

                        SqlDataSource1.Update();
                        //System.Diagnostics.Debug.WriteLine("Updated Value:" + RecordId);
                    }

                }

                DataList1.DataBind();
            } catch(FormatException) {
            } catch(NullReferenceException ex) {
            } finally {

            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e) {
            try {
                foreach(DataListItem item in DataList1.Items) {
                    String StatId = DataList1.DataKeys[item.ItemIndex].ToString();

                    bool isChecked = ((CheckBox)item.FindControl("DeleteBox")).Checked;

                    if(isChecked) {
                        SqlDataSource1.DeleteParameters["StatIdParam"].DefaultValue = StatId;

                        SqlDataSource1.Delete();
                        //System.Diagnostics.Debug.WriteLine("Deleted Value:" + StatId);
                    }
                }
                DataList1.EditItemIndex = -1;
                DataList1.DataBind();
            } catch(FormatException) {
                
            } finally {

            }
        }

        protected void GetPlayersButton_Click(object sender, EventArgs e) {

            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Populate the grid of players with fantasy points
            cmd.CommandText =
                "INSERT INTO PlayerStats (PlayerId, Month) " +
                "SELECT Players.PlayerId, " +
                month + " " +
                "FROM Players " +
                "WHERE Players.PlayerId NOT IN " +
                "    (SELECT PlayerId " +
                "     FROM PlayerStats " +
                "     WHERE Month = " + month + ")";


            cmd.Connection = con;
            try {
                con.Open();
                int numAdded = cmd.ExecuteNonQuery();
                GetPlayersResult.Text = "Total number of players added: " + numAdded.ToString() + "<br />" +
                    "Please refresh the page!";
            } catch(InvalidCastException ex) {

            }
        }

        protected void DataList1_EditCommand(object source, DataListCommandEventArgs e) {

        }

        protected void DataList1_CancelCommand(object source, DataListCommandEventArgs e) {

        }

        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e) {
            try {
                String RecordId = DataList1.DataKeys[e.Item.ItemIndex].ToString();
                String Goals = ((TextBox)e.Item.FindControl("GoalsTextBox")).Text;
                String Shots = ((TextBox)e.Item.FindControl("ShotsTextBox")).Text;

                System.Diagnostics.Debug.WriteLine("you got: " + RecordId + " " + Goals + " " + Shots);

                SqlDataSource1.UpdateParameters["PlayerId"].DefaultValue = RecordId;
                SqlDataSource1.UpdateParameters["GoalsParam"].DefaultValue = Goals;
                SqlDataSource1.UpdateParameters["ShotsParam"].DefaultValue = Shots;
                SqlDataSource1.Update();

                DataList1.EditItemIndex = -1;
                DataList1.DataBind();


            } catch(FormatException) {
                Response.Write( "Invalid Field Entry!");
            }
        }

        protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e) {
            String StatId = DataList1.DataKeys[e.Item.ItemIndex].ToString();
            SqlDataSource1.DeleteParameters["StatIdParam"].DefaultValue = StatId;

            SqlDataSource1.Delete();

            DataList1.EditItemIndex = -1;
            DataList1.DataBind();
        }
    }
}