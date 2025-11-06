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
        Task<ProdutoModel> Obter(int id, int usuarioID);

        // RELAÇÃO PRODUTO x INGREDIENTES
        //Task AtribuirIngredientes(int ProdutoID, List<int> ingredientesIds);
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

        //public async Task AtribuirIngredientes(int ProdutoID, List<int> ingredientesIds)
        //{
        //    foreach (var i in ingredientesIds)
        //    {
        //        await _daProduto.InserirProdutoIngredientes(ProdutoID, i);
        //    }
        //}

        public async Task<int?> Cadastrar(ProdutoModel model)
        {
            foreach(var i in model.Ingredientes)
            {
                ProdutoIngredienteModel PImodel = new ProdutoIngredienteModel
                {
                    IngredienteID = i.IngredienteID,
                    ProdutoID = model.ProdutoID.Value,
                    Quantidade = i.Quantidade
                };
                await _daProduto.InserirProdutoIngredientes(PImodel);
            }


            if (model.ProdutoID.HasValue)
            {
                await _daProduto.Atualizar(model);
                return model.ProdutoID;
            }
            await _daProduto.Adicionar(model);
            return model.ProdutoID;
        }

        public async Task Deletar(ProdutoModel model)
        {
            await _daProduto.Deletar(model.ProdutoID.Value);
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

        public async Task<ProdutoModel?> Obter(int id, int usuarioID)
        {
            List<ProdutoModel> lst = await _daProduto.Listar(id, usuarioID);
            return lst.FirstOrDefault();
        }
    }
}
