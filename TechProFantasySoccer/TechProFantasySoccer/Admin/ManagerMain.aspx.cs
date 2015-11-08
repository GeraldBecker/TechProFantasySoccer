using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer
{
    public partial class ManagerMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
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
        }
    }
}