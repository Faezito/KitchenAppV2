using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z1.Model
{
    public class ProdutoModel
    {
        public int? ProdutoID { get; set; }
        public string Nome { get; set; }
        public decimal Custo { get; set; }
        public decimal Preco { get; set; }
        public string? Categoria { get; set; }
        public bool Ativo { get; set; } = true;
        public int UsuarioID { get; set; }

        public List<ProdutoIngredienteModel> Ingredientes { get; set; } = new List<ProdutoIngredienteModel>();
    }
}
