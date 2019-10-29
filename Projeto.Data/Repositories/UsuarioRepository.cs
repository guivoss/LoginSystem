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
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private string connectionString;

        //seleciona a linha do atributo e botão da direita para gerar o construtor com o parametro
        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Usuario obj)
        {
            string query = "insert into Usuario(Nome, Login, Senha, DataCriacao) "
                        + "values(@Nome, @Login, @Senha, @DataCriacao)";

            //deve instalar pelo Nuget o System.Sql.Client
            using(SqlConnection connection = new SqlConnection(connectionString))
            { 
                connection.Execute(query, obj);
            }

        }

        public void Alterar(Usuario obj)
        {
            string query = "update Usuario set Nome = @Nome, Login = @Login, Senha = @Senha, DataCriacao = @DataCriacao) "
                        +"where IdUsuario = @IdUsuario";
                        

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(int id)
        {
            string query = "delete from Usuario where IdUsuario = @IdUsuario";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { IdUsuario = id});
            }
        }

        public List<Usuario> Obtertodos()
        {
            string query = "select * from Usuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }

        public Usuario ObterPorId(int id)
        {
            string query = "select * from Usuario where IdUsuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query, new { IdUsuario = id }).SingleOrDefault();
            }
        }

        public Usuario ObterPorLogin(string login)
        {
            string query = "select * from Usuario where Login = @Login";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query, new { Login = login }).SingleOrDefault();
            }
        }

        public Usuario ObterPorLoginESenha(string login, string senha)
        {
            string query = "select * from Usuario where Login = @Login and Senha = @Senha";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query, new { Login = login, Senha = senha }).SingleOrDefault();
            }
        }
    }
}
