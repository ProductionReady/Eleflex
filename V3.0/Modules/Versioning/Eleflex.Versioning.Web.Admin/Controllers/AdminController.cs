using System.Linq;
using System.Web.Mvc;
using Eleflex.Versioning.Services.WCF.Message;

namespace Eleflex.Versioning.Web.Admin.Controllers
{
    /// <summary>
    /// Logging admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        protected readonly IModuleServiceRepository _moduleServiceRepository;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="moduleServiceRepository"></param>
        public AdminController(IModuleServiceRepository moduleServiceRepository)
        {
            _moduleServiceRepository = moduleServiceRepository;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var resp = _moduleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = new StorageQuery() });
            if (resp != null && resp.Items != null)
                resp.Items = resp.Items.OrderBy(x => x.Name).ToList();
            return View(resp);
        }

    }
}
