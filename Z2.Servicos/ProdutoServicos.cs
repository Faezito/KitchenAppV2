using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z1.Model;
using Z3.DataAccess;

namespace Z2.Servicos
{
    public interface IProdutoServicos
    {
        Task<List<ProdutoModel>> Listar(int? ProdutoID, int? UsuarioID);
        Task<int?> Cadastrar(ProdutoModel model);
        Task Deletar(ProdutoModel model);
        Task<ProdutoModel> Obter(int id);

        // RELAÇÃO PRODUTO x INGREDIENTES
        Task AtribuirIngredientes(int ProdutoID, List<int> ingredientesIds);
        Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID);
    }

    public class ProdutoServicos : IProdutoServicos
    {
        private readonly IProdutoDataAccess _daProduto;
        private readonly IIngredienteDataAccess _daIngrediente;

        public ProdutoServicos(IProdutoDataAccess daProduto, IIngredienteDataAccess daIngrediente)
        {
             _daProduto = daProduto;
             _daIngrediente = daIngrediente; 
        }

        public Task AtribuirIngredientes(int ProdutoID, List<int> ingredientesIds)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Cadastrar(ProdutoModel model)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(ProdutoModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProdutoModel>> Listar(int? ProdutoID, int? UsuarioID)
        {
            return await _daProduto.Listar(ProdutoID, UsuarioID);
        }

        public async Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID)
        {
            List<IngredienteModel> lst = await _daIngrediente.ListarIngredientesDoProduto(ProdutoID);
            return lst;
        }

        public async Task<ProdutoModel?> Obter(int id)
        {
            List<ProdutoModel> lst = await _daProduto.Listar(id, null);
            return lst.FirstOrDefault();
        }
    }
}
