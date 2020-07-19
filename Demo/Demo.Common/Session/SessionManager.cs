using System.Web;

namespace Demo.Common.Session
{
    /// <summary>
    /// Session manager class
    /// Contains current session Value
    /// </summary>
    public class SessionManager
    {
        // Private constructor
        private SessionManager()
        {

        }

        // Gets the current session.
        public static SessionManager Current
        {
            get
            {
                var session = (SessionManager)HttpContext.Current.Session["DemoSession"];
                if (session != null)
                {
                    return session;
                }
                session = new SessionManager();
                HttpContext.Current.Session["DemoSession"] = session;
                return session;
            }
        }

        // Add session properties here

        /// <summary>
        /// current user name
        /// </summary>
        public string UserName { get; set; }
    }
}
