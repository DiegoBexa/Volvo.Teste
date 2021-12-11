using Volvo.Teste.Dominio;

namespace Volvo.Teste.Repositorio.Interface
{
    public interface IMarcaRepositorio : IBaseRepositorio<Marca>
    {
        Marca BuscarPorId(int prmId);
    }
}
