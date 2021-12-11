using System.Collections.Generic;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.Servico.Interface
{
    public interface ICaminhaoServico
    {
        IEnumerable<CaminhaoViewModel> ListarTodos();

        CaminhaoViewModel Adicionar(CaminhaoViewModel prmCaminhaoViewModel);

        bool Editar(CaminhaoViewModel prmCaminhaoViewModel);

        bool Deletar(int Id);

        CaminhaoViewModel BuscarPorId(int prmId);
    }
}
