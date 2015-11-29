using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

/// <summary>
/// Author: Gerald
/// </summary>
namespace TechProFantasySoccer {
    /// <summary>
    /// A page that tells the user that they do not have access to the website until the admin adds them.
    /// </summary>
    public partial class AccessDenied : System.Web.UI.Page {
        public string UserName = "";

        protected void Page_Load(object sender, EventArgs e) {
            if(HttpContext.Current.User.Identity.IsAuthenticated) 
                UserName = User.Identity.GetUserName();
        }
    }
}