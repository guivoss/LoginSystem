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
using Microsoft.AspNetCore.Authorization;//inportando para utilizar o Authorize

namespace Projeto.Services.Controllers
{
    //exije autenticação Bearer (JWT) que está configurado na Startup.cs
    //em auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()

    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        //atributo
        private IContaRepository repository;

        //construtor
        public ContaController(IContaRepository repository)
        {
            this.repository = repository;
        }

        //CADASTRO
        [HttpPost]  //requisição do Tipo POST
        public IActionResult Post(ContaCadastroModel model)
        {
            //Verifica se os dados da model passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {

                    Conta conta = new Conta();
                    conta.Nome = model.Nome;
                    conta.Data = model.Data;
                    conta.Valor = model.Valor;
                    conta.Tipo = model.Tipo;
                    conta.IdUsuario = model.IdUsuario;


                    //gravar no banco de dados
                    repository.Inserir(conta);

                    //retornar status de sucesso 200 (OK)
                    return StatusCode(200, $"Conta {conta.Nome}, cadastrada com sucesso. ");

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


        //EDICAO
        [HttpPut]  //requisição do Tipo PUT
        public IActionResult Post(ContaEdicaoModel model)
        {
            //Verifica se os dados da model passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {

                    Conta conta = new Conta();
                    conta.IdConta = model.IdConta;
                    conta.Nome = model.Nome;
                    conta.Data = model.Data;
                    conta.Valor = model.Valor;
                    conta.Tipo = model.Tipo;
                    conta.IdUsuario = model.IdUsuario;


                    //gravar no banco de dados
                    repository.Alterar(conta);

                    //retornar status de sucesso 200 (OK)
                    return StatusCode(200, $"Conta {conta.Nome}, cadastrada com sucesso. ");

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

        //DELETE
        [HttpDelete("{idUsuario}")]
        public IActionResult Delete(int idConta )
        {
            try
            {
                repository.Excluir(idConta); //excluindo a conta..

                return StatusCode(200, "Conta excluída com sucesso.");
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        //
        [HttpGet("{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            try
            {
                List<ContaConsultaModel> lista = new List<ContaConsultaModel>();

                foreach (var item in repository.ObterTodos(idUsuario))
                {
                    ContaConsultaModel model = new ContaConsultaModel();

                    model.IdConta = item.IdConta;
                    model.Nome = item.Nome;
                    model.Data = item.Data;
                    item.Valor = item.Valor;
                    model.Tipo = item.Tipo;

                    lista.Add(model);
                }

                return StatusCode(200, lista);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}