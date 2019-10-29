using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services
{
    public class TokenConfiguration
    {
        //codigo de have publica gerada pelo TOKEN
        public string Audience { get; set; }

        //dominio da aplicação (proprio sistema) que está gerando o TOKEN
        public string Issuer { get; set; }

        //tempo de expiração em segundos
        public int Seconds { get; set; }
    }
}
