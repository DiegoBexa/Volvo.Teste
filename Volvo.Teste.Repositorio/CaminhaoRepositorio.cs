using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Volvo.Teste.Dominio;
using Volvo.Teste.Repositorio.ContextoDb;
using Volvo.Teste.Repositorio.Interface;

namespace Volvo.Teste.Repositorio
{
    public class CaminhaoRepositorio : BaseRepositorio<Caminhao>, ICaminhaoRepositorio
    {

        public CaminhaoRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public Caminhao BuscarPorId(int prmId)
        {
            return _objContexto.Caminhao.FirstOrDefault(x => x.Id == prmId);
        }

        public Caminhao BuscarPorIdAsNoTracking(int prmId)
        {
            return _objContexto.Caminhao.AsNoTracking().Include(x => x.Marca)
                                                       .Include(x => x.Modelo)
                                                       .FirstOrDefault(x => x.Id == prmId);
        }

        public IEnumerable<Caminhao> ListarTodos()
        {
            return _objContexto.Caminhao.Include(x => x.Marca)
                                        .Include(x => x.Modelo);
        }
    }
}
