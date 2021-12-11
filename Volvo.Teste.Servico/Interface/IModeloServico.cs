using System.Collections.Generic;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.Servico.Interface
{
    public interface IModeloServico
    {

        IEnumerable<ModeloViewModel> ListarTodos();

        ModeloViewModel Adicionar(ModeloViewModel prmModeloViewModel);

        bool Editar(ModeloViewModel prmModeloViewModel);

        bool Deletar(int prmId);

        ModeloViewModel BuscarPorId(int prmId);

        IEnumerable<ModeloViewModel> ListarTodosPermitidos(int prmIdMarca);
    }
}
