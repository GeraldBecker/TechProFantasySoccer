using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace TechProFantasySoccer {
    public partial class AccessDenied : System.Web.UI.Page {
        public string UserName = "";

        protected void Page_Load(object sender, EventArgs e) {
            if(HttpContext.Current.User.Identity.IsAuthenticated) 
                UserName = User.Identity.GetUserName();
        }
    }
}