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
        Task<List<ProdutoModel>> Listar(int? ProdutoID, int UsuarioID);
        Task<int?> Cadastrar(ProdutoModel model);
        Task Deletar(ProdutoModel model);
<<<<<<< HEAD
        Task AtivarDesativar(ProdutoModel model);
        Task<ProdutoModel> Obter(int id, int usuarioID);
=======
        Task<ProdutoModel> Obter(int id);
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)

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

<<<<<<< HEAD
        public async Task AtivarDesativar(ProdutoModel model)
        {
            await _daProduto.AtivarDesativar(model);
        }

        public async Task<int?> Cadastrar(ProdutoModel model)
        {
            if (model.ProdutoID.HasValue)
            {
                await _daProduto.Atualizar(model);
                await InserirIngredientes(model);
                return model.ProdutoID;
            }
            var produtoID = await _daProduto.Adicionar(model);
            model.ProdutoID = produtoID;

            await InserirIngredientes(model);
            return model.ProdutoID;
=======
        public Task AtribuirIngredientes(int ProdutoID, List<int> ingredientesIds)
        {
            throw new NotImplementedException();
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
        }

        public Task<int?> Cadastrar(ProdutoModel model)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(ProdutoModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProdutoModel>> Listar(int? ProdutoID, int UsuarioID)
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

        private async Task InserirIngredientes(ProdutoModel model)
        {
            if (model.Ingredientes == null || !model.Ingredientes.Any())
                return;
            await _daProduto.LimparIngredientes(model);
            foreach (var i in model.Ingredientes)
            {
                ProdutoIngredienteModel PImodel = new ProdutoIngredienteModel
                {
                    IngredienteID = i.IngredienteID.Value,
                    ProdutoID = model.ProdutoID.Value,
                    Quantidade = i.Quantidade
                };
                await _daProduto.InserirProdutoIngredientes(PImodel);
            }
        }
    }
}
