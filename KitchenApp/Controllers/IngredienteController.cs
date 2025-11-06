using GenerativeAI.Types;
using KitchenApp.Libraries.Login;
using KitchenApp.Libraries.Sessao;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Z1.Model;
using Z2.Servicos;
using Z3.DataAccess;

namespace KitchenApp.Controllers
{
    public class IngredienteController : Controller
    {
        private readonly IIngredienteServicos _seIngrediente;
        private readonly Sessao _sessao;
        private readonly LoginUsuario _login;

        public IngredienteController(IIngredienteServicos seIngrediente, Sessao sessao, LoginUsuario loginUsuario)
        {
            _seIngrediente = seIngrediente;
            _sessao = sessao;
            _login = loginUsuario;
        }
        public async Task<IActionResult> Index(int? IngredienteID, int UsuarioID)
        {
            var usuario = _login.GetCliente();
            UsuarioID = usuario.Id.Value;

            List<IngredienteModel> lst = await _seIngrediente.Listar(IngredienteID, UsuarioID);
            return View(lst);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            IngredienteModel model;
            var usuario = _login.GetCliente();
            int usuarioID = usuario.Id.Value;

            if (id.HasValue)
            {
                model = await _seIngrediente.Obter(id.Value, usuarioID);

                if (model == null)
                    return NotFound();
            }
            else
            {
                model = new IngredienteModel();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(IngredienteModel model)
        {
            var usuario = _login.GetCliente();
            model.UsuarioID = usuario.Id.Value;

            if (usuario == null)
            {
                ModelState.AddModelError("", "Sessão expirada. Faça login novamente.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                await _seIngrediente.Cadastro(model);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = _login.GetCliente();
            int usuarioID = usuario.Id.Value;

            IngredienteModel model = await _seIngrediente.Obter(id, usuarioID);
            model.UsuarioID = usuarioID;

            await _seIngrediente.Deletar(id, usuarioID);
            return RedirectToAction(nameof(Index));
        }
    }
}
