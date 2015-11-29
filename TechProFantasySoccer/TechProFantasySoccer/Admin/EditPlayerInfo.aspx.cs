using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace TechProFantasySoccer
{
    public partial class EditPlayerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if(Request.QueryString["player"] == null) {
                Response.Redirect("/Admin/ManagerMain");
            }
            string player = Request.QueryString["player"];
            Session["Player"] = player;

            //Get player information
            cmd.CommandText =
                "SELECT " +
                "FirstName AS First, " +
                "LastName AS Last, " +
                "Cost, " +
                "Clubs.ClubName, " +
                "Clubs.ClubId, " +
                "Leagues.LeagueName, " +
                "Players.Owned, " +
                "Positions.PositionName AS Position, " +
                "Positions.PositionRef AS PositionRef " +
                "FROM Players " +
                "LEFT OUTER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef " +
                "LEFT OUTER JOIN Clubs ON Players.ClubId = Clubs.ClubId " +
                "LEFT OUTER JOIN Leagues ON Clubs.LeagueId = Leagues.LeagueId " +
                "WHERE Players.PlayerId = " + player;


            DataTable temp;
            cmd.Connection = con;
            try {
                temp = new DataTable();
                con.Open();
                temp.Load(cmd.ExecuteReader());

                if(temp.Rows.Count > 0) {
                    if(!IsPostBack) {
                        PlayerFNameTextBox.Text = temp.Rows[0]["First"].ToString();
                        PlayerLNameTextBox.Text = temp.Rows[0]["Last"].ToString();

                        LeagueLabel.Text = (string)temp.Rows[0]["LeagueName"];

                        ClubDropDown.SelectedValue = temp.Rows[0]["ClubId"].ToString();

                        //int position = int.Parse(temp.Rows[0]["PositionRef"].ToString());
                        PositionDropDown.SelectedValue = temp.Rows[0]["PositionRef"].ToString();
                        //PositionDropDown.SelectedIndex = (position);


                        costTextBox.Text = ((int)temp.Rows[0]["Cost"]).ToString();
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

        }

        //protected void ImageUpload_Click(object sender, EventArgs e)
        //{
        //    StartUpload();
        //}

        //private void StartUpload()
        //{
        //    // get the files name of image
        //    string imgName = fileupload.FileName;

        //    // sets image path
        //    string imgPath = "Images/" + imgName;

        //    // get the size in bytes 
        //    int imgSize = fileupload.PostedFile.ContentLength;

        //    // validates the posted file before saving
        //    if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        //    {
        //        // 10240 kb mean 10mb
        //        if (fileupload.PostedFile.ContentLength > 1000000000)
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);
        //        }
        //        else
        //        {
        //            // save to folder
        //            fileupload.SaveAs(Server.MapPath(imgPath));
        //            image.ImageUrl = "~/" + imgPath;
        //            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image saved!')", true);
        //        }
        //    }
        //}

        protected void editBtn_Click(object sender, EventArgs e) {
            if(sender == saveBtn) {
                ClubsDataSource.UpdateParameters["FirstName"].DefaultValue = PlayerFNameTextBox.Text;
                ClubsDataSource.UpdateParameters["LastName"].DefaultValue = PlayerLNameTextBox.Text;
                ClubsDataSource.UpdateParameters["Cost"].DefaultValue = costTextBox.Text;
                ClubsDataSource.UpdateParameters["Position"].DefaultValue = PositionDropDown.SelectedValue;
                ClubsDataSource.UpdateParameters["Club"].DefaultValue = ClubDropDown.SelectedValue;
                ClubsDataSource.UpdateParameters["PlayerId"].DefaultValue = Session["Player"].ToString();

                ClubsDataSource.Update();

                Response.Redirect("./AddPlayer");
                //System.Diagnostics.Debug.WriteLine("POSITION:" + PositionDropDown.SelectedValue);
                //System.Diagnostics.Debug.WriteLine("CLUB:" + ClubDropDown.SelectedValue);
            }
        }
        
    }
}