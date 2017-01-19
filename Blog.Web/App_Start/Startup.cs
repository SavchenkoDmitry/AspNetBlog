using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Blog.Services.Services;
using Microsoft.AspNet.Identity;
using Blog.Services.Interfaces;

[assembly: OwinStartup(typeof(Blog.Web.App_Start.Startup))]

namespace Blog.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
