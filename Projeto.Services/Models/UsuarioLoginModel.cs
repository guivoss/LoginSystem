using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações no MVC e API

namespace Projeto.Services.Models
{
    public class UsuarioLoginModel
    {
        [Required] //campo obrigatorio
        public string Login { get; set; }

        [Required]  //campo obrigatorio
        public string Senha { get; set; }
        

    }
}
