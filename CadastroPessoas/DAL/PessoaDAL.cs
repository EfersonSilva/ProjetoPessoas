using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoPloomers.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProjetoPloomers.Repository
{
    public class PessoaDAL
    {
        private readonly string _connectionString;
        public PessoaDAL(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("STRING_CONNECTION");
        }

        public bool Insert(Pessoa pessoa)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(" INSERT INTO [dbo].[Pessoas]( ");
                    query.Append(" Nome, ");
                    query.Append(" Telefone, ");
                    query.Append(" Sexo, Email, ");
                    query.Append(" DataNascimento, ");
                    query.Append(" CPF, ");
                    query.Append(" Endereco, ");
                    query.Append(" Ativo ");
                    query.Append(" ) VALUES (");
                    query.Append(" @Nome,");
                    query.Append(" @Telefone,");
                    query.Append(" @Sexo,");
                    query.Append(" @Email,");
                    query.Append(" @DataNascimento,");
                    query.Append(" @CPF,");
                    query.Append(" @Endereco,");
                    query.Append(" @Ativo) ");
                    pessoa = conn.Query<Pessoa>(query.ToString(), pessoa).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }

            return true;
        }

        public Pessoa Update(Pessoa pessoa)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var returnPessoa = connection.Query<Pessoa>(
                    " UPDATE [dbo].[Pessoas] SET Nome = @Nome," +
                    " Telefone = @Telefone," +
                    " Sexo = @Sexo," +
                    " Email = @Email," +
                    " DataNascimento = @DataNascimento," +
                    " CPF = @CPF," +
                    " Endereco = @Endereco," +
                    " Ativo = @Ativo" +
                    " OUTPUT inserted.Id_Pessoa," +
                    " inserted.Nome, " +
                    " inserted.Telefone," +
                    " inserted.Sexo, " +
                    " inserted.Email, " +
                    " inserted.DataNascimento, " +
                    " inserted.CPF, " +
                    " inserted.Endereco, " +
                    " inserted.Ativo " +
                    "WHERE Id_Pessoa = @Id_Pessoa OR CPF = @CPF", pessoa).FirstOrDefault();

                return returnPessoa ?? new Pessoa();
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public bool Delete(Pessoa pessoa)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var query = " UPDATE [dbo].[Pessoas] SET " +
                    " Ativo = @Ativo " +
                    " WHERE CPF = @CPF";
                var result = connection.Execute(query, pessoa).ToString();

                return pessoa.Ativo;
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }

        }

        public Pessoa Search(Pessoa pessoa)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var query = new StringBuilder();

                query.Append("  SELECT Id_pessoa,");
                query.Append(" Nome, Telefone, Sexo, ");
                query.Append(" Email, DataNascimento, ");
                query.Append(" CPF, ");
                query.Append(" Endereco, ");
                query.Append(" Ativo  ");
                query.Append(" From [dbo].[Pessoas] ");
                query.Append(" WHERE Id_pessoa = @Id_Pessoa OR CPF = @CPF AND Ativo = 1 ");

                var returnPessoa = connection.Query<Pessoa>(query.ToString(), pessoa).FirstOrDefault();

                return returnPessoa ?? new Pessoa();
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}
