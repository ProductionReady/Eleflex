using System.Web.Mvc;

namespace WebClient.Controllers
{
    /// <summary>
    /// Represents an MVC controller handling Error pages.
    /// </summary>
    public class ErrorController : Controller
    {

        /// <summary>
        /// Index. Default page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}