namespace EnviaEmail.Models
{
    public class EmailModel
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Senha { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
    }
}
