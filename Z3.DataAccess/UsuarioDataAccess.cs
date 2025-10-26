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
    public interface IUsuarioDataAccess
    {
        Task<List<UsuarioModel>> Listar(int? id);
        Task<int?> Adicionar(UsuarioModel model);
        Task Atualizar(UsuarioModel model);
        Task Deletar(int? id);
        Task<UsuarioModel?> ObterPorEmail(string email);

    }

    public class UsuarioDataAccess : IUsuarioDataAccess
    {
        private readonly IDapper _dapper;

        public UsuarioDataAccess(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<int?> Adicionar(UsuarioModel model)
        {
            string sql = @"
INSERT INTO KitchenApp.dbo.Usuarios (
Nome
,Email
,Senha
,Tipo
,Ativo
,DataCriacao
,DataAtivacao
,DataDesativacao)
VALUES (
@Nome,
@Email,
@Senha,
@Tipo,
@Ativo,
GETDATE(),
GETDATE(),
DATEADD(DAY, 14, GETDATE())
)
 SELECT SCOPE_IDENTITY()
";
            return await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param:model);
        }

        public async Task Atualizar(UsuarioModel model)
        {
            string sql = @"
UPDATE KitchenApp.dbo.Usuarios
SET Email = COALESCE(@Email, Email),
Senha = COALESCE(@Senha, Senha),
Tipo = COALESCE(@Tipo, Tipo),
Ativo = COALESCE(@Ativo, Ativo),
DataAtivacao = COALESCE(@DataAtivacao, DataAtivacao),
DataDesativacao = COALESCE(@DataDesativacao, DataDesativacao)
WHERE ID = @id
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: model);
        }

        public async Task Deletar(int? id)
        {
            string sql = @"
DELETE FROM KitchenApp.dbo.Usuarios
WHERE ID = @id
";
            await _dapper.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text, param: id);
        }

        public async Task<List<UsuarioModel>> Listar(int? id)
        {
            string sql = @"
SELECT ID
,Nome
,Email
,Senha
,Tipo
,Ativo
,DataCriacao
,DataAtivacao
,DataDesativacao
FROM KitchenApp.dbo.Usuarios
WHERE (@id IS NULL OR ID = @id)
";
            return await _dapper.QueryAsync<UsuarioModel>(sql: sql, commandType: System.Data.CommandType.Text, param: id);
        }

        public async Task<UsuarioModel?> ObterPorEmail(string email)
        {
            string sql = @"
SELECT ID
,Nome
,Email
,Senha
,Tipo
,Ativo
,DataCriacao
,DataAtivacao
,DataDesativacao
FROM KitchenApp.dbo.Usuarios
WHERE (Email = @email)
";
            var usuario = await _dapper.QueryAsync<UsuarioModel>(sql: sql, commandType: System.Data.CommandType.Text, param: new { email });
            return usuario.FirstOrDefault();
        }
    }
}
