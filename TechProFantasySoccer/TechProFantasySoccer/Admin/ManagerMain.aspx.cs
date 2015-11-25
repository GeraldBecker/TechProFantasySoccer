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

            //Check if the user is an Admin
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isAdmin(user))
                Response.Redirect("~/AccessDenied");

        }
        protected void ManagerButton_Click(object sender, EventArgs e) {
            if (sender.Equals(editMnthPStatBtn))
                Response.Redirect("./ChooseMonth");
            else if (sender.Equals(editPInfoBtn))
                Response.Redirect("./EditPlayerInfo");
            else if (sender.Equals(createTeamBtn))
                Response.Redirect("./ManagerMain");
            else if (sender.Equals(editLineupBtn))
                Response.Redirect("./ManagerMain");
            else if (sender.Equals(setScoringBtn))
                Response.Redirect("./EditScoringValues");
            else if (sender.Equals(addClubBtn))
                Response.Redirect("./AddClub");
            else if (sender.Equals(addPlayerBtn))
                Response.Redirect("./AddPlayer");
            else if (sender.Equals(addLeagueBtn))
                Response.Redirect("./AddLeague");
            else if(sender.Equals(selectUsersBtn))
                Response.Redirect("./SelectUsers");
            else if(sender.Equals(settingsBtn))
                Response.Redirect("./Settings");
        }
    }
}