using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Entities; //importando
using Projeto.Data.Contracts; //importando
using Projeto.Util; //importando
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    //endpoint - endereço de acesso a api
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //atributo
        private IUsuarioRepository repository;

        //construtor
        
        public UsuarioController(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Post(UsuarioCadastroModel model)
        {
            //Verifica se os dados da model passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //verificar se o login informado existe no banco de dados
                    if (repository.ObterPorLogin(model.Login) != null)
                    {
                        return StatusCode(400, "Login já encontra-se cadastrado, tente outro.");
                    }
                    else
                    {
                        Criptografia criptografia = new Criptografia();

                        Usuario usuario = new Usuario();
                        usuario.Nome = model.Nome;
                        usuario.Login = model.Login;
                        usuario.Senha = criptografia.MD5Encrypt(model.Senha);
                        usuario.DataCriacao = DateTime.Now;

                        //gravar no banco de dados
                        repository.Inserir(usuario);

                        //retornar status de sucesso 200 (OK)
                        return StatusCode(200, $"Usuário {usuario.Nome}, cadastrado com sucesso. ");
                    }
                }
                catch (Exception e)
                {
                    //retornar um status de erro 500 (Erro Interno do Servidor) 
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                //retornar um status de erro 400 (Requisição inválida)
                return StatusCode(400, "Ocorreram erros de validação.");                
            }

            

        }

    }
}