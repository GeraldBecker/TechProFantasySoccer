using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechProFantasySoccer
{
    public partial class ChooseMonth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }
        }

        protected void MonthButton_Click(object sender, EventArgs e) {
            if(sender.Equals(sepBtn))
                Response.Redirect("./EditPlayerStats?month=9");
            else if(sender.Equals(octBtn))
                Response.Redirect("./EditPlayerStats?month=10");
            else if(sender.Equals(novBtn))
                Response.Redirect("./EditPlayerStats?month=11");
            else if(sender.Equals(decBtn))
                Response.Redirect("./EditPlayerStats?month=12");
            else if(sender.Equals(janBtn))
                Response.Redirect("./EditPlayerStats?month=1");
            else if(sender.Equals(febBtn))
                Response.Redirect("./EditPlayerStats?month=2");
        }
    }
}