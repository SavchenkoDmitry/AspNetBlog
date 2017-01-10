using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Blog.Services.Services;
using Microsoft.AspNet.Identity;
using Blog.Services.Interfaces;

[assembly: OwinStartup(typeof(Blog.App_Start.Startup))]

namespace Blog.App_Start
{
    public class Startup
    {
        //IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        //private IUserService CreateUserService()
        //{
        //    return serviceCreator.CreateUserService("DefaultConnection");
        //}
    }
}