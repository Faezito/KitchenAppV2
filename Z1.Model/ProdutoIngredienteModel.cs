using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z1.Model
{
    public class ProdutoIngredienteModel
    {
        public int ProdutoID { get; set; }
        public int IngredienteID { get; set; }
        public string? IngredienteNome { get; set; }
        public decimal Quantidade { get; set; }
    }
}
