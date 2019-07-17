using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using XmlWebservice.Infrastructure;

namespace XmlWebservice
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DiConfig.Configure();
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
