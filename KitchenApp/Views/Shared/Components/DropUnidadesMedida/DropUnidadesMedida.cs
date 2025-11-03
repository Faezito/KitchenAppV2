using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Z1.Model;
using Z3.DataAccess;

namespace KitchenApp.Components
{
    public class DropUnidadesMedida : ViewComponent
    {
        private readonly IProdutoDataAccess _daProduto;

        public DropUnidadesMedida(IProdutoDataAccess daProduto)
        {
            _daProduto = daProduto;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var lst = await _daProduto.ListarCategorias();
            return View("Default");
        }
    }
}
