using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z1.Model
{
    public class ProdutoModel
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public decimal Custo { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
        public bool Ativo { get; set; }
        public int UsuarioID { get; set; }
<<<<<<< HEAD

        //public List<ProdutoIngredienteModel> ProdutoIngredientes { get; set; } = new List<ProdutoIngredienteModel>();
        public List<IngredienteModel> Ingredientes { get; set; } = new List<IngredienteModel>();
=======
>>>>>>> parent of ac7b9c1 (Cadastro de produtos com ingredientes 0.01)
    }
}
