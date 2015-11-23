using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.SqlClient;

namespace TechProFantasySoccer.Admin {
    public partial class SelectUsers : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect("/Account/Login");
            }

            //Check if the user is an Admin
            string user = User.Identity.GetUserId();
            if(!AuthLevelCheck.isAdmin(user))
                Response.Redirect("~/AccessDenied");
        }

        protected void UpdateButton_Click(object sender, EventArgs e) {
            try {

                foreach(DataListItem item in DataList1.Items) {
                    String AccessId = DataList1.DataKeys[item.ItemIndex].ToString();
                    String access = ((TextBox)item.FindControl("AccessTextBox")).Text;

                    String userId = ((Label)item.FindControl("UserIDLabel")).Text;
                    String prevAccess = ((Label)item.FindControl("AccessLabel")).Text;


                    if(access != prevAccess) {
                        if(AccessId == "") {
                            System.Diagnostics.Debug.WriteLine("OBJECT WAS NULL... " + userId + " will have this access: " + access);
                            SqlDataSource1.InsertParameters["UserIdParam"].DefaultValue = userId;
                            SqlDataSource1.InsertParameters["AccessParam"].DefaultValue = access;
                            SqlDataSource1.Insert();

                        } else {
                            System.Diagnostics.Debug.WriteLine("OBJECT WAS : " + AccessId);
                            SqlDataSource1.UpdateParameters["AccessIdParam"].DefaultValue = AccessId;
                            SqlDataSource1.UpdateParameters["AccessParam"].DefaultValue = access;

                            SqlDataSource1.Update();

                        }


                    }

                }

                DataList1.DataBind();
            } catch(FormatException) {
            } catch(NullReferenceException ex) {
            } finally {

            }
        }

        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e) {
            /*try {
                String RecordId = DataList1.DataKeys[e.Item.ItemIndex].ToString();
                String Goals = ((TextBox)e.Item.FindControl("GoalsTextBox")).Text;
                String Shots = ((TextBox)e.Item.FindControl("ShotsTextBox")).Text;

                System.Diagnostics.Debug.WriteLine("you got: " + RecordId + " " + Goals + " " + Shots);

                SqlDataSource1.UpdateParameters["PlayerId"].DefaultValue = RecordId;
                SqlDataSource1.UpdateParameters["GoalsParam"].DefaultValue = Goals;
                SqlDataSource1.UpdateParameters["ShotsParam"].DefaultValue = Shots;
                SqlDataSource1.Update();

                DataList1.EditItemIndex = -1;
                DataList1.DataBind();


            } catch(FormatException) {
                Response.Write("Invalid Field Entry!");
            }*/
        }


    }
}