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
        Task<List<IngredienteModel>> Listar(int? IngredienteID, int usuarioID);
        Task<int?> Adicionar(IngredienteModel model);
        Task Atualizar(IngredienteModel model);
        Task Deletar(int? IngredienteID, int usuarioID);
        Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID);

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
,UsuarioID
)
VALUES (
@IngredienteDescr
,@CategoriaIngrediente
,@Quantidade
,@UnidadeMedida
,@Custo
,@UsuarioID
)
 SELECT SCOPE_IDENTITY()
";
            return await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Atualizar(IngredienteModel model)
        {
            string sql = @"
UPDATE KitchenApp.dbo.Ingredientes
SET Nome = COALESCE(@IngredienteDescr, Nome),
CategoriaIngrediente = COALESCE(@CategoriaIngrediente, CategoriaIngrediente),
Quantidade = COALESCE(@Quantidade, Quantidade),
UnidadeMedida = COALESCE(@UnidadeMedida, UnidadeMedida),
Custo = COALESCE(@Custo, Custo),
WHERE IngredienteID = @IngredienteID
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Deletar(int? IngredienteID, int usuarioID)
        {
            string sql = @"
DELETE FROM KitchenApp.dbo.Ingredientes
WHERE IngredienteID = @IngredienteID
AND UsuarioID = @usuarioID
";
<<<<<<< HEAD
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: new { IngredienteID, usuarioID });
=======
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: IngredienteID);
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
        }

        public async Task<List<IngredienteModel>> Listar(int? IngredienteID, int usuarioID)
        {
            string sql = @"
SELECT IngredienteID
,Nome AS IngredienteDescr
,CategoriaIngrediente
,Quantidade
,UnidadeMedida
,Custo
FROM KitchenApp.dbo.Ingredientes
WHERE (@IngredienteID IS NULL OR IngredienteID = @IngredienteID)
AND UsuarioID = @usuarioID
";
<<<<<<< HEAD
            return await _dapper.QueryAsync<IngredienteModel>(sql: sql, commandType: System.Data.CommandType.Text, param: new { IngredienteID, usuarioID });
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
=======
            return await _dapper.QueryAsync<IngredienteModel>(sql: sql, commandType: System.Data.CommandType.Text, param: IngredienteID);
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
        }

        public async Task<List<IngredienteModel>> ListarIngredientesDoProduto(int ProdutoID)
        {
            string sql = @"
SELECT 
P.ProdutoID,
<<<<<<< HEAD
P.Nome AS ProdutoDescr,
=======
P.Titulo,
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
P.Custo,
P.Preco,
I.Nome AS IngredienteDescr,
I.IngredienteID,
PrIn.Quantidade,
I.UnidadeMedida,
P.Categoria,
I.CategoriaIngrediente,
P.Ativo
FROM KitchenApp.dbo.Produtos P
LEFT JOIN KitchenApp.dbo.ProdutoIngredientes PrIn ON P.ProdutoID = PrIn.ProdutoID 
LEFT JOIN KitchenApp.dbo.Ingredientes I ON PrIn.IngredienteID = I.IngredienteID
WHERE PrIn.ProdutoID = @ProdutoID
";
            return await _dapper.QueryAsync<IngredienteModel>(sql: sql, commandType: System.Data.CommandType.Text, param: ProdutoID);

        }
    }
}
