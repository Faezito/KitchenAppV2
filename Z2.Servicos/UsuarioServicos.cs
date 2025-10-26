using System.Reflection;
using Z1.Model;
using Z3.DataAccess;

namespace Z2.Servicos
{
    public interface IUsuarioServicos
    {
        Task<List<UsuarioModel>> Listar(int? id);
        Task<int?> Cadastrar(UsuarioModel model);
        Task Deletar(UsuarioModel model);
        Task<UsuarioModel> Obter(int id);
        Task<UsuarioModel> Login(string Email, string Senha);
    }

    public class UsuarioServicos : IUsuarioServicos
    {
        private readonly IUsuarioDataAccess _daUsuario;

        public UsuarioServicos(IUsuarioDataAccess daUsuario)
        {
            _daUsuario = daUsuario;
        }

        public async Task<int?> Cadastrar(UsuarioModel model)
        {
            if (model.Id.HasValue)
            {
                await _daUsuario.Atualizar(model);
                return model.Id;
            }
            return await _daUsuario.Adicionar(model);
        }

        public async Task Deletar(UsuarioModel model)
        {
            await _daUsuario.Deletar(model.Id);
        }

        public async Task<List<UsuarioModel>> Listar(int? id)
        {
            List<UsuarioModel> lst = await _daUsuario.Listar(id);
            return lst;
        }

        public async Task<UsuarioModel> Login(string Email, string Senha)
        {
            var user = await _daUsuario.ObterPorEmail(Email);

            if (user != null)
            {
                if (user.Senha == Senha)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<UsuarioModel> Obter(int id)
        {
            List<UsuarioModel> lst = await Listar(id);
            return lst.FirstOrDefault();
        }
    }
}
