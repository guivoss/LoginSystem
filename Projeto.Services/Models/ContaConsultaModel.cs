using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models
{
    public class ContaConsultaModel
    {
       
        public int IdConta { get; set; }
        
        public string Nome { get; set; }
        
        public DateTime Data { get; set; }                
        
        public string Tipo { get; set; }

    }
}
