using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity obj);
        void Alterar(TEntity obj);
        void Excluir(int id);

        List<TEntity> Obtertodos();
        TEntity ObterPorId(int id);


    }
}
