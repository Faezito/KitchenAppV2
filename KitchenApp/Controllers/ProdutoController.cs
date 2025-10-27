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

        public ProdutoController(IProdutoServicos seProduto)
        {
            _seProduto = seProduto;
        }
        public async Task<IActionResult> Index(int? ProdutoID, int? UsuarioID)
        {
            List<ProdutoModel> lst = await _seProduto.Listar(ProdutoID, null);
            return View(lst);
        }
    }
}
