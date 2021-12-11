using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Volvo.Teste.Repositorio.ContextoDb;
using Volvo.Teste.Repositorio.Interface;

namespace Volvo.Teste.Repositorio
{
    public abstract class BaseRepositorio<T> : IBaseRepositorio<T>, IDisposable
           where T : class
    {
        protected readonly Contexto _objContexto;
        public BaseRepositorio(Contexto objContexto)
        {
            this._objContexto = objContexto;
        }

        public T Adicionar(T entidade)
        {
            _objContexto.Set<T>().Add(entidade);
            _objContexto.SaveChanges();

            return entidade;
        }

        public bool Atualizar(T entidade)
        {
            _objContexto.Set<T>().Update(entidade);
            return _objContexto.SaveChanges() > 0;
        }

        public T Buscar(Expression<Func<T, bool>> predicato)
        {
            return _objContexto.Set<T>().FirstOrDefault(predicato);
        }

        public bool Deletar(T entidade)
        {
            _objContexto.Set<T>().Remove(entidade);
            int linhasAfetadas = _objContexto.SaveChanges();
            _objContexto.Entry(entidade).Reload();

            return linhasAfetadas > 0;
        }



        public IEnumerable<T> Listar()
        {
            return _objContexto.Set<T>();
        }

        public void Dispose()
        {
            _objContexto.Dispose();
        }


    }
}
