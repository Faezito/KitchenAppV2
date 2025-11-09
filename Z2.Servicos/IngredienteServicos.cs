using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z1.Model;
using Z3.DataAccess;

namespace Z2.Servicos
{
    public interface IIngredienteServicos
    {
        Task<List<IngredienteModel>> Listar(int? IngredienteID, int usuarioID);
        Task<IngredienteModel> Obter(int IngredienteID, int usuarioID);
        Task<int?> Cadastro(IngredienteModel model);
        Task Deletar(int IngredienteID, int UsuarioID);
        Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID);
    }
    public class IngredienteServicos : IIngredienteServicos
    {
        private readonly IIngredienteDataAccess _da;

        public IngredienteServicos(IIngredienteDataAccess da)
        {
            _da = da;
        }

        public async Task<int?> Cadastro(IngredienteModel model)
        {
            try
            {
                if (model.IngredienteID.HasValue)
                {
                    await _da.Atualizar(model);
                    return model.IngredienteID;
                }
                return await _da.Adicionar(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Deletar(int IngredienteID, int UsuarioID)
        {
            await _da.Deletar(IngredienteID, UsuarioID);
        }

        public async Task<List<IngredienteModel>> Listar(int? IngredienteID, int usuarioID)
        {
            List<IngredienteModel> lst = await _da.Listar(IngredienteID, usuarioID);
            return lst;
        }

        public async Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID)
        {
            List<IngredienteModel> lst = await _da.ListarIngredientesDoProduto(ProdutoID);
            return lst;
        }

        public async Task<IngredienteModel> Obter(int IngredienteID, int usuarioID)
        {
            List<IngredienteModel> lst = await _da.Listar(IngredienteID, usuarioID);
            return lst.FirstOrDefault();
        }
    }
}
