using KitchenApp.Libraries.Login;
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
        private readonly LoginUsuario _login;

        public DropIngredientes(IIngredienteServicos seIngredientes, LoginUsuario login)
        {
            _seIngredientes = seIngredientes;
            _login = login;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuario = _login.GetCliente();
            int usuarioID = usuario.Id.Value;

            var lst = await _seIngredientes.Listar(null, usuarioID);
            return View("Default", lst);
        }
    }
}
