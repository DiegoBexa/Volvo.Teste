using System.Linq;
using Volvo.Teste.Dominio;
using Volvo.Teste.Repositorio.ContextoDb;
using Volvo.Teste.Repositorio.Interface;

namespace Volvo.Teste.Repositorio
{
    public class MarcaRepositorio: BaseRepositorio<Marca>, IMarcaRepositorio
    {
        public MarcaRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public Marca BuscarPorId(int prmId)
        {
            return _objContexto.Marca.FirstOrDefault(x => x.Id == prmId);
        }
    }
}
