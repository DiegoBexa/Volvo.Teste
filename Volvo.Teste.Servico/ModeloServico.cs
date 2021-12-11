using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volvo.Teste.Dominio;
using Volvo.Teste.Repositorio.Interface;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.Servico
{
    public class ModeloServico : IModeloServico
    {
        private readonly IMarcaRepositorio _marcaRepositorio;
        private readonly IModeloRepositorio _modeloRepositorio;
        private readonly IMapper mapper;

        public ModeloServico(IMarcaRepositorio marcaRepositorio, IMapper mapper, 
                             IModeloRepositorio modeloRepositorio)
        {
            _marcaRepositorio = marcaRepositorio;
            _modeloRepositorio = modeloRepositorio;
            this.mapper = mapper;
        }

        public ModeloViewModel Adicionar(ModeloViewModel prmModeloViewModel)
        {
            Validator.ValidateObject(prmModeloViewModel, new System.ComponentModel.DataAnnotations.ValidationContext(prmModeloViewModel), true);

            Modelo _modelo = mapper.Map<Modelo>(prmModeloViewModel);

            _modelo = _modeloRepositorio.Adicionar(_modelo);

            prmModeloViewModel = mapper.Map<ModeloViewModel>(_modelo);

            return prmModeloViewModel;
        }

        public ModeloViewModel BuscarPorId(int prmId)
        {
            if (prmId == 0)
                throw new Exception("Id não pode ser igual a 0");

            Modelo _modelo = _modeloRepositorio.BuscarPorId(prmId);

            if (_modelo == null)
                throw new Exception("Modelo não encontrado");

            return mapper.Map<ModeloViewModel>(_modelo);
        }

        public bool Deletar(int prmId)
        {

            if (prmId == 0)
                throw new Exception("Id não pode ser igual a 0");

            Modelo _modelo = _modeloRepositorio.BuscarPorId(prmId);

            if (_modelo == null)
                throw new Exception("Modelo não encontrado");

            return _modeloRepositorio.Deletar(_modelo);
        }

        public bool Editar(ModeloViewModel prmModeloViewModel)
        {
            if (prmModeloViewModel.Id == 0)
                throw new Exception("Id não pode ser igual a 0");

            Modelo _modelo = _modeloRepositorio.BuscarPorId(prmModeloViewModel.Id);

            if (_modelo == null)
                throw new Exception("Modelo não encontrado");

            var objAtualizado = mapper.Map(prmModeloViewModel, _modelo);

            return _modeloRepositorio.Atualizar(objAtualizado);

        }

        public IEnumerable<ModeloViewModel> ListarTodos()
        {
            IEnumerable<Modelo> modelos = _modeloRepositorio.ListarTodos();

            var modeloVm = mapper.Map<IEnumerable<ModeloViewModel>>(modelos);

            return modeloVm;
        }

        public IEnumerable<ModeloViewModel> ListarTodosPermitidos(int prmIdMarca)
        {
            IEnumerable<Modelo> modelos = _modeloRepositorio.ListarTodosPermitidos(prmIdMarca);

            var modeloVm = mapper.Map<IEnumerable<ModeloViewModel>>(modelos);

            return modeloVm;
        }
    }
}
