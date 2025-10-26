using KitchenApp.Libraries.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace KitchenApp.Controllers
{
    public class UsuarioController : Controller
    {
        [ValidateHttpRefererAttributes]
        public IActionResult Index()
        {
            return View();
        }
    }
}
