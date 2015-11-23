using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

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
        }
    }
}