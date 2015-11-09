using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Configuration; 

namespace TechProFantasySoccer
{
    public partial class ManagerMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        }
        protected void ManagerButton_Click(object sender, EventArgs e) {
            if(sender.Equals(editMnthPStatBtn))
                Response.Redirect("./ChooseMonth");
            else if(sender.Equals(editPInfoBtn))
                Response.Redirect("./EditPlayerInfo");
            else if(sender.Equals(createTeamBtn))
                Response.Redirect("./ManagerMain");
            else if(sender.Equals(editLineupBtn))
                Response.Redirect("./ManagerMain");
            else if(sender.Equals(setScoringBtn))
                Response.Redirect("./ManagerMain");
            else if(sender.Equals(addClubBtn))
                Response.Redirect("./AddClub");
            else if(sender.Equals(addPlayerBtn))
                Response.Redirect("./AddPlayer");
        }
    }
}