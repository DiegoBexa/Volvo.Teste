using System.Collections.Generic;
using Volvo.Teste.Dominio;

namespace Volvo.Teste.Repositorio.Interface
{
    public interface ICaminhaoRepositorio : IBaseRepositorio<Caminhao>
    {
        IEnumerable<Caminhao> ListarTodos();
        Caminhao BuscarPorId(int prmId);
    }
}
