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
namespace TechProFantasySoccer.Admin {
    /// <summary>
    /// Allows the admin to change various settings used throughout the app.
    /// </summary>
    public partial class ToggleTransactions : System.Web.UI.Page {

        /// <summary>
        /// Checks only if the user is an admin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is an Admin
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isAdmin(user))
                Response.Redirect("~/AccessDenied");
        }

        /// <summary>
        /// Updates any values that were changed in the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateButton_Click(object sender, EventArgs e) {
            try {

                foreach(DataListItem item in DataList1.Items) {
                    String KeyId = DataList1.DataKeys[item.ItemIndex].ToString();
                    String value = ((TextBox)item.FindControl("ValueTextBox")).Text;
                    String prevValue = ((Label)item.FindControl("ValueLabel")).Text;


                    if(value != prevValue) {
                        SqlDataSource1.UpdateParameters["KeyIdParam"].DefaultValue = KeyId;
                        SqlDataSource1.UpdateParameters["ValueParam"].DefaultValue = value;

                        SqlDataSource1.Update();
                    }

                }

                DataList1.DataBind();
            } catch(FormatException) {
            } catch(NullReferenceException ex) {
            } finally {

            }
        }

    }
}