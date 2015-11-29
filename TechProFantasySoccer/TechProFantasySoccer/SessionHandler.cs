using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Providers.Entities;

namespace TechProFantasySoccer {
    public class SessionHandler {
        public static string _ActiveDefendersKey = "ActiveDefenders";
        public static string _BenchDefendersKey = "BenchDefenders";

        public static string _ActiveMidfieldersKey = "ActiveMidfielders";
        public static string _BenchMidfieldersKey = "BenchMidfielders";

        public static string _ActiveStrikersKey = "ActiveStrikers";
        public static string _BenchStrikersKey = "BenchStrikers";

        public static string _ActiveGoaliesKey = "ActiveGoalies";
        public static string _BenchGoaliesKey = "BenchGoalies";

        //Goalies
        public static List<string> ActiveGoalies {
            get { return (List<string>)HttpContext.Current.Session[_ActiveGoaliesKey]; }
            set { HttpContext.Current.Session[_ActiveGoaliesKey] = (List<string>)value; }
        }
        public static List<string> BenchGoalies {
            get { return (List<string>)HttpContext.Current.Session[_BenchGoaliesKey]; }
            set { HttpContext.Current.Session[_BenchGoaliesKey] = (List<string>)value; }
        }

        //Defenders
        public static List<string> ActiveDefenders {
            get { return (List<string>)HttpContext.Current.Session[_ActiveDefendersKey]; }
            set { HttpContext.Current.Session[_ActiveDefendersKey] = (List<string>)value; }
        }
        public static List<string> BenchDefenders {
            get { return (List<string>)HttpContext.Current.Session[_BenchDefendersKey]; }
            set { HttpContext.Current.Session[_BenchDefendersKey] = (List<string>)value; }
        }



        //Midfielders
        public static List<string> ActiveMidfielders {
            get { return (List<string>)HttpContext.Current.Session[_ActiveMidfieldersKey]; }
            set { HttpContext.Current.Session[_ActiveMidfieldersKey] = (List<string>)value; }
        }
        public static List<string> BenchMidfielders {
            get { return (List<string>)HttpContext.Current.Session[_BenchMidfieldersKey]; }
            set { HttpContext.Current.Session[_BenchMidfieldersKey] = (List<string>)value; }
        }

       
        //Strikers
        public static List<string> ActiveStrikers {
            get { return (List<string>)HttpContext.Current.Session[_ActiveStrikersKey]; }
            set { HttpContext.Current.Session[_ActiveStrikersKey] = (List<string>)value; }
        }
        public static List<string> BenchStrikers {
            get { return (List<string>)HttpContext.Current.Session[_BenchStrikersKey]; }
            set { HttpContext.Current.Session[_BenchStrikersKey] = (List<string>)value; }
        }
    }
}
