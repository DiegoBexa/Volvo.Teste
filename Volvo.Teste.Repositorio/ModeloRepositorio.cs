using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Volvo.Teste.Dominio;
using Volvo.Teste.Repositorio.ContextoDb;
using Volvo.Teste.Repositorio.Interface;

namespace Volvo.Teste.Repositorio
{
    public class ModeloRepositorio : BaseRepositorio<Modelo>, IModeloRepositorio
    {
        public ModeloRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public IEnumerable<Modelo> ListarTodos()
        {
            return _objContexto.Modelo.Include(x => x.Marca);
        }

        public IEnumerable<Modelo> ListarTodosPermitidos(int prmMarcaId)
        {
            return _objContexto.Modelo.Where(x => x.MarcaId == prmMarcaId && x.ModeloPermitido);
        }
    }
}
