using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Volvo.Teste.Repositorio.Interface
{
    public interface IBaseRepositorio<T> where T : class
    {
        T Adicionar(T entity);
        bool Deletar(T entity);
        bool Atualizar(T entity);
        IEnumerable<T> Listar();
        T Buscar(Expression<Func<T, bool>> predicate);
    }
}
