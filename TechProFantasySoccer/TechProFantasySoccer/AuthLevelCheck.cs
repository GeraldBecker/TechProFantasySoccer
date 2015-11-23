using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace TechProFantasySoccer {
    public class AuthLevelCheck {
        public static bool isAdmin(string user) {
            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Check if the user is an Admin
            cmd.CommandText =
                "SELECT Access " +
                "FROM AccessLevel " +
                "WHERE UserId = '" + user + "'";

            cmd.Connection = con;
            try {
                con.Open();
                int accessLevel = (int)cmd.ExecuteScalar();

                if(accessLevel == 1)
                    return true;

            } catch(System.Data.SqlClient.SqlException ex) {
            } catch(NullReferenceException ex) {
            } finally {
                con.Close();
            }

            return false;
        }

        public static bool isUser(string user) {
            String strConnString = ConfigurationManager.ConnectionStrings["FantasySoccerConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            //Check if the user is an Admin
            cmd.CommandText =
                "SELECT Access " +
                "FROM AccessLevel " +
                "WHERE UserId = '" + user + "'";

            cmd.Connection = con;
            try {
                con.Open();
                int accessLevel = (int)cmd.ExecuteScalar();

                if(accessLevel == 1 || accessLevel == 2)
                    return true;

            } catch(System.Data.SqlClient.SqlException ex) {
            } catch(NullReferenceException ex) {
            } finally {
                con.Close();
            }

            return false;
        }
    }
}