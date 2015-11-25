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
        private static string _ActiveDefendersKey = "ActiveDefenders";
        private static string _BenchDefendersKey = "BenchDefenders";

        private static string _ActiveMidfieldersKey = "ActiveMidfielders";
        private static string _BenchMidfieldersKey = "BenchMidfielders";

        private static string _ActiveStrikersKey = "ActiveStrikers";
        private static string _BenchStrikersKey = "BenchStrikers";

        private static string _ActiveGoaliesKey = "ActiveGoalies";
        private static string _BenchGoaliesKey = "BenchGoalies";

        //Goalies
        public static ArrayList ActiveGoalies {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._ActiveGoaliesKey]; }
            set { HttpContext.Current.Session[SessionHandler._ActiveGoaliesKey] = (ArrayList)value; }
        }
        public static ArrayList BenchGoalies {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._BenchGoaliesKey]; }
            set { HttpContext.Current.Session[SessionHandler._BenchGoaliesKey] = (ArrayList)value; }
        }

        //Defenders
        public static ArrayList ActiveDefenders {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._ActiveDefendersKey]; }
            set { HttpContext.Current.Session[SessionHandler._ActiveDefendersKey] = (ArrayList)value; }
        }
        public static ArrayList BenchDefenders {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._BenchDefendersKey]; }
            set { HttpContext.Current.Session[SessionHandler._BenchDefendersKey] = (ArrayList)value; }
        }



        //Midfielders
        public static ArrayList ActiveMidfielders {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._ActiveMidfieldersKey]; }
            set { HttpContext.Current.Session[SessionHandler._ActiveMidfieldersKey] = (ArrayList)value; }
        }
        public static ArrayList BenchMidfielders {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._BenchMidfieldersKey]; }
            set { HttpContext.Current.Session[SessionHandler._BenchMidfieldersKey] = (ArrayList)value; }
        }

       
        //Strikers
        public static ArrayList ActiveStrikers {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._ActiveStrikersKey]; }
            set { HttpContext.Current.Session[SessionHandler._ActiveStrikersKey] = (ArrayList)value; }
        }
        public static ArrayList BenchStrikers {
            get { return (ArrayList)HttpContext.Current.Session[SessionHandler._BenchStrikersKey]; }
            set { HttpContext.Current.Session[SessionHandler._BenchStrikersKey] = (ArrayList)value; }
        }
    }
}
