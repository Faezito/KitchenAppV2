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
        public string Titulo { get; set; }
        public decimal Custo { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
        public bool Ativo { get; set; }
        public int UsuarioID { get; set; }
    }
}
