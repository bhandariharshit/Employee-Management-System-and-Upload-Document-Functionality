using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationDBTest.Common
{
    public class ApplicationContext
    {
        private static ApplicationContext appContext;
        public string email { get; set; }
        private ApplicationContext()
        {
        }
        public static ApplicationContext GetApplicationState()
        {
            if (appContext == null)
            {
                appContext = new ApplicationContext();
                appContext.email = "a@b.com";
            }
            return appContext;
        }

    }
}