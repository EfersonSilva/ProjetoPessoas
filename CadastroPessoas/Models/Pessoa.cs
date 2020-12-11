using System;

namespace ProjetoPloomers.Models
{
    public class Pessoa
    {
        public int Id_Pessoa { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public char Sexo { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public long CPF { get; set; }
        public string Endereco { get; set; }
        public bool Ativo { get; set; }
    }
}
