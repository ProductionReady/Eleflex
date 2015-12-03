using System.Web.Mvc;

namespace $rootnamespace$.Controllers.Admin
{
    /// <summary>
    /// Root controller for assembly.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public partial class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}
