using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Z1.Model;
using Z3.DataAccess;

namespace KitchenApp.Components
{
    public class DropIngredienteCategorias : ViewComponent
    {
        private readonly IIngredienteDataAccess _daIngrediente;

        public DropIngredienteCategorias(IIngredienteDataAccess daProduto)
        {
            _daIngrediente = daProduto;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lst = await _daIngrediente.ListarCategorias();
            return View("Default", lst);
        }
    }
}
