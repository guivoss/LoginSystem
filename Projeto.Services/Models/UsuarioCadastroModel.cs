using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //precisa para as validações



namespace Projeto.Services.Models
{
    public class UsuarioCadastroModel
    {
        [Required]
        public string Nome { get; set; }

        [MinLength(6)]
        [MaxLength(20)]
        [Required]
        public string Login { get; set; }

        [MinLength(6)]
        [MaxLength(12)]
        [Required]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha")]
        public string SenhaConfirm { get; set; }


    }
}
