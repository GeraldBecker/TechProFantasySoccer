using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Author: Gerald
/// </summary>
namespace TechProFantasySoccer {
    /// <summary>
    /// Checks the authorization level of a user to see if they are an admin or if they are just a regular user.
    /// </summary>
    public class AuthLevelCheck {
        /// <summary>
        /// Checks if the user is an admin and has elevated privelages. 
        /// </summary>
        /// <param name="user">The user id of the user logged in.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if the user is a member of the fantasy league.
        /// </summary>
        /// <param name="user">The user Id. </param>
        /// <returns></returns>
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