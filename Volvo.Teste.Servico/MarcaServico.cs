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
    public class MarcaServico : IMarcaServico
    {
        private readonly IMarcaRepositorio _marcaRepositorio;
        private readonly IMapper mapper;

        public MarcaServico(IMarcaRepositorio marcaRepositorio, IMapper mapper)
        {
            _marcaRepositorio = marcaRepositorio;
            this.mapper = mapper;
        }
        public MarcaViewModel Adicionar(MarcaViewModel prmMarcaViewModel)
        {
            Validator.ValidateObject(prmMarcaViewModel, new System.ComponentModel.DataAnnotations.ValidationContext(prmMarcaViewModel), true);

            Marca _marca = mapper.Map<Marca>(prmMarcaViewModel);

            _marca = _marcaRepositorio.Adicionar(_marca);

            prmMarcaViewModel = mapper.Map<MarcaViewModel>(_marca);

            return prmMarcaViewModel;
        }

        public MarcaViewModel BuscarPorId(int prmId)
        {

            if (prmId == 0)
                throw new Exception("Id não pode ser igual a 0");

            Marca _marca = _marcaRepositorio.BuscarPorIdAsNoTracking(prmId);

            if (_marca == null)
                throw new Exception("Marca não encontrada");

            return mapper.Map<MarcaViewModel>(_marca);
        }

        public bool Deletar(int prmId)
        {
            if (prmId == 0)
                throw new Exception("Id não pode ser igual a 0");

            Marca _marca = _marcaRepositorio.BuscarPorId(prmId);

            if (_marca == null)
                throw new Exception("Marca não encontrada");

            _marcaRepositorio.Deletar(_marca);

            return true;
        }

        public bool Editar(MarcaViewModel prmMarcaViewModel)
        {
            if (prmMarcaViewModel.Id == 0)
                throw new Exception("Id não pode ser igual a 0");

            Marca _marca = _marcaRepositorio.BuscarPorId(prmMarcaViewModel.Id);

            if (_marca == null)
                throw new Exception("Marca não encontrada");

            var objAtualizado = mapper.Map(prmMarcaViewModel, _marca);

            _marcaRepositorio.Atualizar(objAtualizado);

            return true;
        }

        public IEnumerable<MarcaViewModel> ListarTodos()
        {
            IEnumerable<Marca> marcas = _marcaRepositorio.Listar();

            var marcasVm = mapper.Map<IEnumerable<MarcaViewModel>>(marcas);

            return marcasVm;
        }
    }
}
