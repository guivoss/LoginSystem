using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //precisa para as validações

namespace Projeto.Services.Models
{
    public class ContaCadastroModel
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public int IdUsuario { get; set; }
    }
}
