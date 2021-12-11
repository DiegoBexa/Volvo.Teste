using System.Collections.Generic;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.Servico.Interface
{
    public interface IMarcaServico
    {
        IEnumerable<MarcaViewModel> ListarTodos();

        MarcaViewModel Adicionar(MarcaViewModel prmMarcaViewModel);

        bool Editar(MarcaViewModel prmMarcaViewModel);

        bool Deletar(int prmId);

        MarcaViewModel BuscarPorId(int prmId);
    }
}
