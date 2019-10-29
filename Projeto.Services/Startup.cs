using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Projeto.Data.Repositories; //incluir repositorios
using Projeto.Data.Contracts; //importando 
using Swashbuckle.AspNetCore.Swagger; //swagger
using Microsoft.AspNetCore.Authorization; //importando para o token
using Microsoft.AspNetCore.Authentication.JwtBearer;  //importando para o token


//classe de inicialização do projeto Asp.Net CORE
namespace Projeto.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region configuração para geração do TOKEN

            //configuração para geração do TOKEN de autenticação
            //configuração para definir a política de autenticação por JWT
            var signingConfigurations = new LoginConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfiguration"))
                    //appsettings.json
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults
                                 .AuthenticationScheme;

                authOptions.DefaultChallengeScheme = JwtBearerDefaults
                                .AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes
                    (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion

            

            //código (1) para o SWAGGER
            //mapeando o Swagger (documentação API)
            services.AddSwaggerGen(
                    swagger =>
                    {
                        swagger.SwaggerDoc("v1",
                            new Info
                            {
                                Title = "Sistema Asp.Net Web API",
                                Version = "v1",
                                Description = "Projeto desenvolvido em aula - C# WebDeveloper",
                                Contact = new Contact
                                {
                                    Name = "COTI Informática",
                                    Url = "http://www.cotiinformatica.com.br",
                                    Email = "contato@cotiinformatica.com.br"
                                }
                            });

                        //inclui o campo para token no swagger
                        swagger.OperationFilter<TokenParameter>();
                    }
                );
                                 

            #region Configuração da ConnectionString

            //configura para enviar a connectionString para as classes
            //resgatar a string de conexão do banco de dados
            //no arquivo appsettings.json
            string connectionString = Configuration.GetConnectionString("Aula");

            //configurar as classes de repositorio para receber a connectionString
            //colocar a Interface e a respectiva classe
            services.AddTransient<IUsuarioRepository, UsuarioRepository>
                (map => new UsuarioRepository(connectionString));

            services.AddTransient<IContaRepository, ContaRepository>
                (map => new ContaRepository(connectionString));

            #endregion



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //código (2) para o SWAGGER
            app.UseSwagger(); //definindo o uso do Swagger para o projeto
            app.UseSwaggerUI(
                    swagger =>
                    {
                        swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto");
                    }
                );

            app.UseMvc();
        }
    }
}
