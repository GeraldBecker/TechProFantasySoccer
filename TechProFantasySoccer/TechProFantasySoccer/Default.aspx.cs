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

namespace TechProFantasySoccer {
    public partial class _Default : Page {

        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is a member of the fantasy pool
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isUser(user))
                Response.Redirect("~/AccessDenied");
            
            TeamName.Text = HttpContext.Current.User.Identity.Name;
        }

        protected void MainPageBtn_Click(object sender, EventArgs e) {
            if(sender.Equals(TeamOverviewBtn))
                Response.Redirect("./Team/TeamOverview");
            else if(sender.Equals(PlayerSearchBtn))
                Response.Redirect("./Players/PlayerSearch");
            else if(sender.Equals(SetLineupBtn))
                Response.Redirect("./SetLineup");
            else if(sender.Equals(StandingsBtn))
                Response.Redirect("./Team/Standings");
        }
    }
}