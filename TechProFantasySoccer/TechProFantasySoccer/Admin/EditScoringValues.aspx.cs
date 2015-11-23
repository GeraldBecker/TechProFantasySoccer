using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace TechProFantasySoccer.Admin {
    public partial class EditScoringValues : System.Web.UI.Page {
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

                foreach (DataListItem item in DataList1.Items) {
                    //String RecordId = DataList1.DataKeys[item.ItemIndex].ToString();
                    String ValueId = DataList1.DataKeys[item.ItemIndex].ToString();
                    
                    String Value = ((TextBox)item.FindControl("ValueTextBox")).Text;
                    String prevValue = ((Label)item.FindControl("ValueLabel")).Text;

                    if (Value != prevValue) {
                        ScoringValuesDataSource.UpdateParameters["ScoringIdParam"].DefaultValue = ValueId;
                        ScoringValuesDataSource.UpdateParameters["ValueParam"].DefaultValue = (Value);

                        ScoringValuesDataSource.Update();
                        //System.Diagnostics.Debug.WriteLine("Updated Value:" + RecordId);
                    }

                }
                DataList1.DataBind();
            } catch (FormatException ex) {
                Response.Write(ex);
            } catch (NullReferenceException ex) {
                
            } finally {
               // Response.Write(ScoringValuesDataSource.UpdateCommand);
            }
        }

        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e) {
            try {
                String RecordId = DataList1.DataKeys[e.Item.ItemIndex].ToString();
                String Value = ((TextBox)e.Item.FindControl("Value")).Text;

                System.Diagnostics.Debug.WriteLine("you got: " + RecordId + " " + Value);

                ScoringValuesDataSource.UpdateParameters["PlayerId"].DefaultValue = RecordId;
                ScoringValuesDataSource.UpdateParameters["ValueParam"].DefaultValue = Value;
                ScoringValuesDataSource.Update();

                DataList1.EditItemIndex = -1;
                DataList1.DataBind();


            } catch (FormatException) {
                Response.Write("Invalid Field Entry!");
            }
        }
    }
}