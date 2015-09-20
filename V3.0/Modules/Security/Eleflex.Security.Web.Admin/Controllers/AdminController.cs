using System.Web.Mvc;

namespace Eleflex.Security.Web.Admin.Controllers
{
    /// <summary>
    /// Security admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>        
        public ActionResult Index()
        {            
            return View();
        }

    }
}
