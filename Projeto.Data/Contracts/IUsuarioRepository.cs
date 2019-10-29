using System;
using System.Collections.Generic;
using System.Text;
using Projeto.Data.Entities; //entidades

namespace Projeto.Data.Contracts
{
    public interface IUsuarioRepository :IBaseRepository<Usuario>
    {
        Usuario ObterPorLogin(string login);
        Usuario ObterPorLoginESenha(string login, string senha);



    }
}
