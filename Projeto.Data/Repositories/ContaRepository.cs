using System;
using System.Collections.Generic;
using System.Text;
using Projeto.Data.Entities; //entidades
using Projeto.Data.Contracts; //interfaces
using System.Data.SqlClient;
using Dapper; //framework para acessoao banco de dados
using System.Linq; //para fazer as consultas

namespace Projeto.Data.Repositories
{
    public class ContaRepository : IContaRepository
    {
        //atributo
        private string connectionString;

        //seleciona a linha do atributo e botão da direita para gerar o construtor com o parametro
        public ContaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Conta obj)
        {
            string query = "insert into Conta(Nome, Data, Valor, Tipo, IdUsuario) "
                        + "values(@Nome, @Data, @Valor, @Tipo, @IdUsuario)";

            //deve instalar pelo Nuget o System.Sql.Client
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Alterar(Conta obj)
        {
            string query = "update Conta set Nome = @Nome, Data = @Data, Valor = @Valor, Tipo = @Tipo, "
                        + "IdUsuario = @IdUsuario where IdConta = @IdConta";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(int id)
        {
            string query = "delete from Conta where IdConta = @IdConta";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { IdConta = id });
            }
        }

        public List<Conta> Obtertodos()
        {
            string query = "select * from Conta";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Conta>(query).ToList();
            }
        }             

        public Conta ObterPorId(int id)
        {
            string query = "select * from Conta where IdConta = @IdConta";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Conta>(query, new { IdConta = id }).SingleOrDefault();
            }
        }

        public List<Conta> ObterTodos(int idUsuario)
        {
            string query = "select * from Conta where IdUsuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Conta>(query, new { IdUsuario = idUsuario}).ToList();
            }
        }
    }
}
