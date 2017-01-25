using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Blog.Services.Interfaces;
using Blog.ViewModels.HomeViewModels;


namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}