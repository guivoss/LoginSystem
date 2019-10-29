using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
    public class Conta
    {
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public int IdUsuario { get; set; }

        //Relacionamento TER-1
        public Usuario Usuario { get; set; }

    }
}
