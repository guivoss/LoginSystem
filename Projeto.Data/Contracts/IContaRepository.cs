using System;
using System.Collections.Generic;
using System.Text;
using Projeto.Data.Entities;

namespace Projeto.Data.Contracts
{
    public interface IContaRepository :IBaseRepository<Conta>        
    {
        List<Conta> ObterTodos(int idUsuario);


    }
}
