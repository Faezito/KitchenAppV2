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
        Task<List<IngredienteModel>> Listar(int? IngredienteID);
        Task<IngredienteModel> Obter(int IngredienteID);
        Task<int?> Cadastro(IngredienteModel model);
        Task Deletar(int IngredienteID);
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

        public async Task Deletar(int IngredienteID)
        {
            await _da.Deletar(IngredienteID);
        }

        public async Task<List<IngredienteModel>> Listar(int? IngredienteID)
        {
            List<IngredienteModel> lst = await _da.Listar(IngredienteID);
            return lst;
        }

        public async Task<IngredienteModel> Obter(int IngredienteID)
        {
            List<IngredienteModel> lst = await _da.Listar(IngredienteID);
            return lst.FirstOrDefault();
        }
    }
}
