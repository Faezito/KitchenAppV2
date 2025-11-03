using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Z1.Model;
using Z2.Servicos;
using Z3.DataAccess;

namespace KitchenApp.Components
{
    public class DropIngredientes : ViewComponent
    {
        private readonly IIngredienteServicos _seIngredientes;

        public DropIngredientes(IIngredienteServicos seIngredientes)
        {
            _seIngredientes = seIngredientes;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lst = await _seIngredientes.Listar(null);
            return View("Default", lst);
        }
    }
}
