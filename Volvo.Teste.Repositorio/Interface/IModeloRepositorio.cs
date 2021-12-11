using System.Collections.Generic;
using Volvo.Teste.Dominio;

namespace Volvo.Teste.Repositorio.Interface
{
    public interface IModeloRepositorio : IBaseRepositorio<Modelo>
    {
        IEnumerable<Modelo> ListarTodosPermitidos(int prmMarcaId);
        IEnumerable<Modelo> ListarTodos();
    }
}
