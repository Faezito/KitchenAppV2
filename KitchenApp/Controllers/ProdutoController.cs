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
    public class ProdutoController : Controller
    {
        private readonly IProdutoServicos _seProduto;
        private readonly Sessao _sessao;
        private readonly LoginUsuario _login;

        public ProdutoController(IProdutoServicos seProduto, Sessao sessao, LoginUsuario loginUsuario)
        {
            _seProduto = seProduto;
            _sessao = sessao;
            _login = loginUsuario;
        }
        public async Task<IActionResult> Index(int? ProdutoID, int? UsuarioID)
        {
            var usuario = _login.GetCliente();
            UsuarioID = usuario.Id.Value;

            List<ProdutoModel> lst = await _seProduto.Listar(ProdutoID, UsuarioID);
            return View(lst);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            ProdutoModel model;
            var usuario = _login.GetCliente();
            int usuarioID = usuario.Id.Value;

            if (id.HasValue)
            {
                model = await _seProduto.Obter(id.Value, usuarioID);

                if (model == null)
                    return NotFound();
            }
            else
            {
                model = new ProdutoModel();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoModel model)
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
                await _seProduto.Cadastrar(model);

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

            ProdutoModel model = await _seProduto.Obter(id, usuarioID);
            model.UsuarioID = usuario.Id.Value;

            await _seProduto.Deletar(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
