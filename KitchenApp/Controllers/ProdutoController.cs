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
<<<<<<< HEAD
        private readonly Sessao _sessao;
        private readonly LoginUsuario _login;
        private readonly IIngredienteServicos _seIngrediente;

        public ProdutoController(IProdutoServicos seProduto, Sessao sessao, LoginUsuario loginUsuario, IIngredienteServicos seIngrediente)
        {
            _seProduto = seProduto;
            _sessao = sessao;
            _login = loginUsuario;
            _seIngrediente = seIngrediente;
=======

        public ProdutoController(IProdutoServicos seProduto)
        {
            _seProduto = seProduto;
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
        }
        public async Task<IActionResult> Index(int? ProdutoID, int UsuarioID)
        {
            List<ProdutoModel> lst = await _seProduto.Listar(ProdutoID, null);
            return View(lst);
        }
<<<<<<< HEAD

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            ProdutoModel model;
            var usuario = _login.GetCliente();
            int usuarioID = usuario.Id.Value;

            if (id.HasValue)
            {
                model = await _seProduto.Obter(id.Value, usuarioID);
                List<IngredienteModel> lst = await _seIngrediente.ListarIngredientesDoProduto(model.ProdutoID.Value);
                model.Ingredientes = lst;

                //TODO: CONFERIR COMO TRAZER A LISTA PARA DENTRO DO MODEL DO PRODUTO

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
            ModelState.Keys
                .Where(k =>
                    k.Contains("IngredienteDescr") ||
                    k.Contains("UnidadeMedida") ||
                    k.Contains("CategoriaIngrediente"))
                .ToList()
                .ForEach(k => ModelState.Remove(k));

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

        [HttpPost]
        public async Task<IActionResult> AtivarDesativar(int id)
        {
            var usuario = _login.GetCliente();
            int usuarioID = usuario.Id.Value;

            ProdutoModel model = await _seProduto.Obter(id, usuarioID);

            if (model.Ativo)
                model.Ativo = false;
            else
                model.Ativo = true;

            model.UsuarioID = usuario.Id.Value;

            await _seProduto.AtivarDesativar(model);
            return RedirectToAction(nameof(Index));
        }
=======
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
    }
}
