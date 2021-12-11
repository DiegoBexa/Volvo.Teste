using AutoMapper;
using Volvo.Teste.Dominio;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.Servico.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region ViewModelToDomain

            CreateMap<CaminhaoViewModel, Caminhao>();
            CreateMap<MarcaViewModel, Marca>();
            CreateMap<ModeloViewModel, Modelo>();

            #endregion

            #region DomainToViewModel

            CreateMap<Caminhao, CaminhaoViewModel>();
            CreateMap<Marca, MarcaViewModel>();
            CreateMap<Modelo, ModeloViewModel>();

            #endregion
        }
    }
}
