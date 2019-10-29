using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Entities; //importando
using Projeto.Data.Contracts; //importando
using Projeto.Services.Models; //importando
using Projeto.Util; //importando
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt; //importando para gerar o token
using System.Security.Principal; //importando para gerar o token
using Microsoft.IdentityModel.Tokens; //importando para gerar o token

namespace Projeto.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //atributo
        private IUsuarioRepository repository;

        public LoginController(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        //metodo POST para realizar o Login do usuario
        [HttpPost]
        public IActionResult Post(UsuarioLoginModel model, 
            [FromServices] TokenConfiguration tokenConfiguration, 
            [FromServices] LoginConfiguration loginConfiguration)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    //instanciando a classe de criptografia
                    Criptografia criptografia = new Criptografia();
                    
                    //buscar no banco de dados o usuario que possui o login e senha informados.
                    Usuario usuario = repository.ObterPorLoginESenha(model.Login, criptografia.MD5Encrypt(model.Senha));

                    //verificar se o usuario foi encontrado
                    if (usuario !=null)
                    {
                        //retornar um TOKEN!!

                        #region Criando as credenciais do usuario


                        //criando as credenciais do usuario
                        var identity = new ClaimsIdentity(new GenericIdentity(usuario.Login, "Login"),
                            new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login)
                            });

                        //gerando o token
                        var dataCriacao = DateTime.Now;
                        var dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfiguration.Seconds);

                        var handler = new JwtSecurityTokenHandler();
                        var securityToken = handler.CreateToken(
                            new SecurityTokenDescriptor
                            {
                                Issuer = tokenConfiguration.Issuer,
                                Audience = tokenConfiguration.Audience,
                                SigningCredentials = loginConfiguration.SigningCredentials,
                                Subject = identity,
                                NotBefore = dataCriacao,
                                Expires = dataExpiracao
                            }
                        );

                        //processando o token
                        var token = handler.WriteToken(securityToken);

                        //criando um objeto para retornar o resultado da API
                        var result = new
                        {
                            authenticated = true,
                            created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                            expires = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                            accessToken = token
                        };


                        return StatusCode(200, result);

                        #endregion

                    }
                    else
                    {
                        return StatusCode(400, "Acesso negado. Usuário inválido.");
                    }
                }
                catch (Exception e)
                {

                    //retorna um status de erro 500 (Erro interno de servidor)
                    return StatusCode(500, e.Message);
                }
            }
            else
            {                
                //retornar um status de erro 400 (BadRequest)
                return StatusCode(400, "Ocorreram erros de validação.");
            }
        }
        

    }
}