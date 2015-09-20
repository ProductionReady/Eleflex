using System.Web.Mvc;

namespace WebClient.Controllers
{
    /// <summary>
    /// Eleflex home controller.
    /// </summary>
    public class EleflexHomeController : Controller
    {

        /// <summary>
        /// Default.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About page.
        /// </summary>
        /// <returns></returns>        
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// GPL page.
        /// </summary>
        /// <returns></returns>
        public ActionResult GPL()
        {
            return View();
        }

    }
}
