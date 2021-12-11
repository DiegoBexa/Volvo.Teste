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
    public class CaminhaoServico : ICaminhaoServico
    {
        private readonly ICaminhaoRepositorio _caminhaoRepositorio;
        private readonly IModeloRepositorio _modeloRepositorio;
        private readonly IMarcaRepositorio _marcaRepositorio;
        private readonly IMapper mapper;

        public CaminhaoServico(ICaminhaoRepositorio caminhaoRepositorio, IMapper mapper, IModeloRepositorio modeloRepositorio,
                               IMarcaRepositorio marcaRepositorio)
        {
            _caminhaoRepositorio = caminhaoRepositorio;
            _modeloRepositorio = modeloRepositorio;
            _marcaRepositorio = marcaRepositorio;
            this.mapper = mapper;
        }

        public CaminhaoViewModel Adicionar(CaminhaoViewModel prmCaminhaoViewModel)
        {
            Validator.ValidateObject(prmCaminhaoViewModel, new System.ComponentModel.DataAnnotations.ValidationContext(prmCaminhaoViewModel), true);

            ValidacaoDados(prmCaminhaoViewModel);

            Caminhao _caminhao = mapper.Map<Caminhao>(prmCaminhaoViewModel);

            var adicionadoCaminhao = _caminhaoRepositorio.Adicionar(_caminhao);

            prmCaminhaoViewModel = mapper.Map<CaminhaoViewModel>(adicionadoCaminhao);

            return prmCaminhaoViewModel;
        }


        public CaminhaoViewModel BuscarPorId(int prmId)
        {
            if (prmId == 0)
                throw new Exception("Id não pode ser igual a 0");

            Caminhao _caminhao = _caminhaoRepositorio.BuscarPorIdAsNoTracking(prmId);

            if (_caminhao == null)
                throw new Exception("Caminhão não encontrado");

            return mapper.Map<CaminhaoViewModel>(_caminhao);
        }

        public bool Deletar(int prmId)
        {
            if (prmId == 0)
                throw new Exception("Id não pode ser igual a 0");

            Caminhao _caminhao = _caminhaoRepositorio.BuscarPorId(prmId);

            if (_caminhao == null)
                throw new Exception("Caminhão não encontrado");

            return _caminhaoRepositorio.Deletar(_caminhao);

        }

        public bool Editar(CaminhaoViewModel prmCaminhaoViewModel)
        {
            if (prmCaminhaoViewModel.Id == 0)
                throw new Exception("Id não pode ser igual a 0");

            Validator.ValidateObject(prmCaminhaoViewModel, new System.ComponentModel.DataAnnotations.ValidationContext(prmCaminhaoViewModel), true);

            ValidacaoDados(prmCaminhaoViewModel);

            Caminhao _caminhao = _caminhaoRepositorio.BuscarPorId(prmCaminhaoViewModel.Id);

            if (_caminhao == null)
                throw new Exception("Caminhão não encontrado");

            var objAtualizado = mapper.Map(prmCaminhaoViewModel, _caminhao);

            return _caminhaoRepositorio.Atualizar(objAtualizado);

        }

        public IEnumerable<CaminhaoViewModel> ListarTodos()
        {
            IEnumerable<Caminhao> caminhao = _caminhaoRepositorio.ListarTodos();

            var caminhaoVm = mapper.Map<IEnumerable<CaminhaoViewModel>>(caminhao);

            return caminhaoVm;
        }



        private void ValidacaoDados(CaminhaoViewModel prmCaminhaoViewModel)
        {


            if (prmCaminhaoViewModel.MarcaId == 0)
                throw new Exception("Marca inválida !");
            else
            {
                var marca = _marcaRepositorio.BuscarPorIdAsNoTracking(prmCaminhaoViewModel.MarcaId);

                if (marca == null)
                    throw new Exception("Marca não encontrada !");
            }

            if (prmCaminhaoViewModel.ModeloId == 0)
                throw new Exception("Modelo inválido !");
            else
            {
                var modelo = _modeloRepositorio.BuscarPorIdAsNoTracking(prmCaminhaoViewModel.ModeloId);

                if (modelo == null)
                    throw new Exception("Modelo não encontrado !");
                else
                {
                    if(modelo.MarcaId != prmCaminhaoViewModel.MarcaId)
                        throw new Exception("Marca não pertence ao modelo !");
                }
            }


            if (!ValidarModeloPermitido(prmCaminhaoViewModel))
                throw new Exception("Modelo não permitido!");
        }

        private bool ValidarModeloPermitido(CaminhaoViewModel prmCaminhaoViewModel)
        {
            Modelo modelo = null;

            if (prmCaminhaoViewModel.ModeloId > 0)
                modelo = _modeloRepositorio.BuscarPorIdAsNoTracking( prmCaminhaoViewModel.ModeloId);
            else if (!string.IsNullOrEmpty(prmCaminhaoViewModel.Modelo?.Descricao))
                modelo = _modeloRepositorio.Buscar(x => x.Descricao == prmCaminhaoViewModel.Modelo.Descricao);

            return modelo == null ? false : modelo.ModeloPermitido;
        }

    }
}
