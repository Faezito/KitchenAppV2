using KitchenApp.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Z1.Model;
using Z2.Servicos;

namespace KitchenApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioServicos _seUsuario;
        private readonly LoginUsuario _login;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IUsuarioServicos seUsuario, LoginUsuario login, IHttpContextAccessor httpContextAccessor)
        {
            _seUsuario = seUsuario;
            _login = login;
            _httpContextAccessor = httpContextAccessor;
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] UsuarioModel model)
        {
            UsuarioModel user = new();
            user = await _seUsuario.Login(model.Email, model.Senha);

            if (user != null)
            {
                if (user.Ativo)
                {
                    _login.Login(user);
                    _httpContextAccessor.HttpContext.Session.SetInt32("ID", user.Id.Value);
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewData["MSG_E"] = "Acesso negado! Contate um administrador.";
                    return View();
                }
            }
            else
            {
                ViewData["MSG_E"] = "Dados inválidos";
                return View();
            }
        }

    

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
