using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer {
    public partial class _Default : Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }
        }
        protected void MainPageBtn_Click(object sender, EventArgs e) {
            if(sender.Equals(TeamOverviewBtn))
                Response.Redirect("./Team/TeamOverview");
            else if(sender.Equals(PlayerSearchBtn))
                Response.Redirect("./Players/PlayerSearch");
            else if(sender.Equals(SetLineupBtn))
                Response.Redirect("./SetLineup");
        }
    }
}