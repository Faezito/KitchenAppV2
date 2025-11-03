using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z1.Model;
using Z3.DataAccess.Database;

namespace Z3.DataAccess
{
    public interface IIngredienteDataAccess
    {
        Task<List<IngredienteModel>> Listar(int? IngredienteID);
        Task<int?> Adicionar(IngredienteModel model);
        Task Atualizar(IngredienteModel model);
        Task Deletar(int? IngredienteID);
        Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID);
        Task<List<CategoriasModel>> ListarCategorias();

    }

    public class IngredienteDataAccess : IIngredienteDataAccess
    {
        private readonly IDapper _dapper;

        public IngredienteDataAccess(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<int?> Adicionar(IngredienteModel model)
        {
            string sql = @"
INSERT INTO KitchenApp.dbo.Ingredientes (
Nome
,CategoriaIngrediente
,Quantidade
,UnidadeMedida
,Custo
)
VALUES (
@Nome
,@CategoriaIngrediente
,@Quantidade
,@UnidadeMedida
,@Custo
)
 SELECT SCOPE_IDENTITY()
";
            return await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Atualizar(IngredienteModel model)
        {
            string sql = @"
UPDATE KitchenApp.dbo.Ingredientes
SET Nome = COALESCE(@Nome, Nome),
CategoriaIngrediente = COALESCE(@CategoriaIngrediente, CategoriaIngrediente),
Quantidade = COALESCE(@Quantidade, Quantidade),
UnidadeMedida = COALESCE(@UnidadeMedida, UnidadeMedida),
Custo = COALESCE(@Custo, Custo),
WHERE IngredienteID = @IngredienteID
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Deletar(int? IngredienteID)
        {
            string sql = @"
UPDATE KitchenApp.dbo.Ingredientes
SET DELETADO = 1
WHERE IngredienteID = @IngredienteID
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: new { IngredienteID });
        }

        public async Task<List<IngredienteModel>> Listar(int? IngredienteID)
        {
            string sql = @"
SELECT IngredienteID
,Nome
,CategoriaIngrediente
,Quantidade
,UnidadeMedida
,Custo
FROM KitchenApp.dbo.Ingredientes
WHERE (@IngredienteID IS NULL OR IngredienteID = @IngredienteID)
";
            return await _dapper.QueryAsync<IngredienteModel>(sql: sql, commandType: System.Data.CommandType.Text, param: new { IngredienteID });
        }

        public async Task<List<CategoriasModel>> ListarCategorias()
        {
            try
            {
                string sql = @"
SELECT CategoriaID
      ,Categoria
  FROM KitchenApp.dbo.IngredienteCategorias
";
            return await _dapper.QueryAsync<CategoriasModel>(sql: sql, commandType: System.Data.CommandType.Text, param: null);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID)
        {
            string sql = @"
SELECT 
P.ProdutoID,
P.Nome,
P.Custo,
P.Preco,
I.Nome,
PrIn.Quantidade,
P.Categoria,
P.Ativo
FROM KitchenApp.dbo.Produtos P
LEFT JOIN KitchenApp.dbo.ProdutoIngredientes PrIn ON P.ProdutoID = PrIn.ProdutoID 
LEFT JOIN KitchenApp.dbo.Ingredientes I ON PrIn.ProdutoID = I.IngredienteID
WHERE PrIn.ProdutoID = @ProdutoID
";
            return await _dapper.QueryAsync<IngredienteModel>(sql: sql, commandType: System.Data.CommandType.Text, param: new { ProdutoID });

        }
    }
}
