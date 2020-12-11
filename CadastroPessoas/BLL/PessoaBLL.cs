using Microsoft.Extensions.Configuration;
using ProjetoPloomers.Models;
using ProjetoPloomers.Repository;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace ProjetoPloomers.BLL
{
    public class PessoaBLL
    {
        public IConfiguration AppSettings()
        {

            Console.WriteLine(Directory.GetCurrentDirectory());
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            Console.OutputEncoding = Encoding.UTF8;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

            return configuration;
        }
        public bool Insert(Pessoa pessoa)
        {
            var result = false;
            var configuration = AppSettings();

            result = new PessoaDAL(configuration).Insert(pessoa);

            bool email_ativo = Convert.ToBoolean(configuration.GetSection("EMAIL").GetSection("EMAIL_ATIVO").Value);

            if (email_ativo == true)
            {
                EnviaEmail(pessoa);
            }
            return result;
        }

        public void EnviaEmail(Pessoa pessoa) 
        {
            var configuration = AppSettings();
            var email = new EnviaEmail.Models.EmailModel();

            string host = Convert.ToString(configuration.GetSection("EMAIL").GetSection("HOST").Value);
            string porta = Convert.ToString(configuration.GetSection("EMAIL").GetSection("PORTA").Value);
            email.Origem = Convert.ToString(configuration.GetSection("EMAIL").GetSection("ORIGEM").Value);
            email.Senha = Convert.ToString(configuration.GetSection("EMAIL").GetSection("SENHA").Value);
            email.Assunto = Convert.ToString(configuration.GetSection("EMAIL").GetSection("ASSUNTO").Value);
            email.Destino = pessoa.Email;
            email.Porta = Convert.ToInt32(porta);
            email.Host = host;

            EnviaEmail.BLL.EnviaEmailBLL emailBll = new EnviaEmail.BLL.EnviaEmailBLL();
            emailBll.EnviaEmail(email);
        }

        public Pessoa Search(Pessoa pessoa)
        {
            var configuration = AppSettings();

            var result = new PessoaDAL(configuration).Search(pessoa);

            return result;

        }

        public Pessoa Update(Pessoa pessoa)
        {
            var configuration = AppSettings();
            var result = new PessoaDAL(configuration).Update(pessoa);

            return result;
        }

        public bool Delete(Pessoa pessoa)
        {
            var configuration = AppSettings();
            new PessoaDAL(configuration).Delete(pessoa);

            var result = Search(pessoa);

            return result.Ativo;
        }
    }
}
