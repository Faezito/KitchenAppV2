namespace Z1.Model
{
    public class UsuarioModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtivacao { get; set; }
        public DateTime DataDesativacao { get; set; }
    }
}
