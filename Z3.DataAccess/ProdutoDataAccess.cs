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
    public interface IProdutoDataAccess
    {
        Task<List<ProdutoModel>> Listar(int? ProdutoID, int? UsuarioID);
        Task<int?> Adicionar(ProdutoModel model);
        Task Atualizar(ProdutoModel model);
        Task Deletar(int? ProdutoID);

    }

    public class ProdutoDataAccess : IProdutoDataAccess
    {
        private readonly IDapper _dapper;

        public ProdutoDataAccess(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<int?> Adicionar(ProdutoModel model)
        {
            string sql = @"
INSERT INTO KitchenApp.dbo.Produtos (
Nome
,Custo
,Preco
,Categoria
,Ativo
,UsuarioID
)
VALUES (
@Nome,
@Custo,
@Preco,
@Categoria,
@Ativo
)
 SELECT SCOPE_IDENTITY()
";
            return await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Atualizar(ProdutoModel model)
        {
            string sql = @"
UPDATE KitchenApp.dbo.Produtos
SET Nome = COALESCE(@Nome, Nome),
Custo = COALESCE(@Custo, Custo),
Preco = COALESCE(@Preco, Preco),
Ativo = COALESCE(@Ativo, Ativo),
Categoria = COALESCE(@Categoria, Categoria),
Ativo = COALESCE(@Ativo, Ativo)
WHERE ProdutoID = @ProdutoID
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Deletar(int? ProdutoID)
        {
            string sql = @"
UPDATE KitchenApp.dbo.Produtos
SET DELETADO = 1
WHERE ProdutoID = @ProdutoID
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: ProdutoID);
        }

        public async Task<List<ProdutoModel>> Listar(int? ProdutoID, int? UsuarioID)
        {
            string sql = @"
SELECT ProdutoID
,Nome
,Custo
,Preco
,Categoria
,Ativo
,UsuarioID
FROM KitchenApp.dbo.Produtos
WHERE DELETADO = 0 
AND (@UsuarioID IS NULL OR UsuarioID = @UsuarioID)
AND (@ProdutoID IS NULL OR ProdutoID = @ProdutoID)

";
            return await _dapper.QueryAsync<ProdutoModel>(sql: sql, commandType: System.Data.CommandType.Text, param: new { ProdutoID, UsuarioID });
        }


        public async Task InserirProdutoIngredientes(ProdutoIngredienteModel model)
        {
            string sql = @"
        INSERT INTO KitchenApp.dbo.ProdutoIngredientes (ProdutoID, IngredienteID, Quantidade)
        VALUES (@ProdutoID, @IngredienteID, @Quantidade);";

            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }
        }
    }
