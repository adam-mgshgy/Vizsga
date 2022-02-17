using System;
using System.Collections.Generic;
using System.Text;
using Owin;
using System.Web.Http;
using System.Net.Http;

namespace MoveYourBody.WebAPI.Tests
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration CONFIG = new HttpConfiguration();
            CONFIG.EnableCors();
            CONFIG.Routes.MapHttpRoute(
                name: "createUserApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            appBuilder.UseWebApi(CONFIG);
        }
    }
}
