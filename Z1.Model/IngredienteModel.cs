using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z1.Model
{
    public class IngredienteModel
    {
<<<<<<< HEAD
        public int? IngredienteID { get; set; }
        public string IngredienteDescr { get; set; }
=======
        public int IngredienteID { get; set; }
        public string Nome { get; set; }
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
        public string CategoriaIngrediente { get; set; }
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal Custo { get; set; }
        public int UsuarioID { get; set; }
        public int? ProdutoID { get; set; }
    }
}
