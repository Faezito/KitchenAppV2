using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z1.Model
{
    public class IngredienteModel
    {
        public int? IngredienteID { get; set; }
        public string IngredienteDescr { get; set; }
        public string CategoriaIngrediente { get; set; }
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal Custo { get; set; }
        public int UsuarioID { get; set; }
        public int? ProdutoID { get; set; }
    }
}
