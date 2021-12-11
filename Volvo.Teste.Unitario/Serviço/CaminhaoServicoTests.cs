using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volvo.Teste.Dominio;
using Volvo.Teste.Repositorio.Interface;
using Volvo.Teste.Servico;
using Volvo.Teste.Servico.AutoMapper;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;
using Xunit;

namespace Volvo.Teste.Unitario.Serviço
{
    public class CaminhaoServicoTests
    {
        private CaminhaoServico caminhaoServico;

        public CaminhaoServicoTests()
        {
            caminhaoServico = new CaminhaoServico(new Mock<ICaminhaoRepositorio>().Object,
                                                  new Mock<IMapper>().Object,
                                                  new Mock<IModeloRepositorio>().Object,
                                                  new Mock<IMarcaRepositorio>().Object);
        }

        #region Validação Enviando ID


        [Fact]
        public void BuscarPorId_EnviandoInvalido()
        {
            var exception = Assert.Throws<Exception>(() => caminhaoServico.BuscarPorId(0));
            Assert.Equal("Id não pode ser igual a 0", exception.Message);
        }

        [Fact]
        public void Editar_EnviandoIdInvalido()
        {
            var exception = Assert.Throws<Exception>(() => caminhaoServico.Editar(new CaminhaoViewModel()));
            Assert.Equal("Id não pode ser igual a 0", exception.Message);
        }

        [Fact]
        public void Deletar_EnviandoIdInvalido()
        {
            var exception = Assert.Throws<Exception>(() => caminhaoServico.Deletar(0));
            Assert.Equal("Id não pode ser igual a 0", exception.Message);
        }

        [Fact]
        public void Adicionar_MarcaInvalido()
        {
            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = 1,
                Descricao = "Caminhão Teste",
                MarcaId = 0,
                ModeloId = 1,
                AnoFabricacao = 2021,
                AnoModelo = 2021
            };


            var exception = Assert.Throws<Exception>(() => caminhaoServico.Adicionar(caminhaoViewModel));
            Assert.Equal("Marca inválida !", exception.Message);
        }

        [Fact]
        public void Adicionar_ModeloInvalido()
        {
            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = 1,
                Descricao = "Caminhão Teste",
                MarcaId = 1,
                ModeloId = 0,
                AnoFabricacao = 2021,
                AnoModelo = 2021
            };

            Marca marca = new Marca { Id = 1, Descricao = "Outra Marca" };

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();
            _marcaRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.MarcaId)).Returns(marca);


            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _modeloRepositorio = new Mock<IModeloRepositorio>();

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);

            var exception = Assert.Throws<Exception>(() => caminhaoServico.Adicionar(caminhaoViewModel));
            Assert.Equal("Modelo inválido !", exception.Message);
        }


        #endregion

        #region [ Validação Objeto Enviado]

        [Fact]
        public void Adicionar_EnviandoModeloNaoPermitido()
        {
            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = 1,
                Descricao = "Caminhão Teste",
                MarcaId = 1,
                ModeloId = 1,
                AnoFabricacao = 2021,
                AnoModelo = 2021
            };

            Modelo modelo = new Modelo { Id = 1, Descricao = "Outros", MarcaId = 1, ModeloPermitido = false };
            Marca marca = new Marca { Id = 1, Descricao = "Outra Marca" };

            var _modeloRepositorio = new Mock<IModeloRepositorio>();
            _modeloRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.ModeloId)).Returns(modelo);

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();
            _marcaRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.MarcaId)).Returns(marca);

            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);

            var exception = Assert.Throws<Exception>(() => caminhaoServico.Adicionar(caminhaoViewModel));

            Assert.Equal("Modelo não permitido!", exception.Message);

        }

        [Fact]
        public void Adicionar_EnviandoModeloNaoRelacionadoMarca()
        {
            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = 1,
                Descricao = "Caminhão Teste",
                MarcaId = 99,
                ModeloId = 1,
                AnoFabricacao = 2021,
                AnoModelo = 2021
            };

            Modelo modelo = new Modelo { Id = 1, Descricao = "Outros", MarcaId = 1, ModeloPermitido = false };
            Marca marca = new Marca { Id = 99, Descricao = "Outra Marca" };

            var _modeloRepositorio = new Mock<IModeloRepositorio>();
            _modeloRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.ModeloId)).Returns(modelo);

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();
            _marcaRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.MarcaId)).Returns(marca);

            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);

            var exception = Assert.Throws<Exception>(() => caminhaoServico.Adicionar(caminhaoViewModel));

            Assert.Equal("Marca não pertence ao modelo !", exception.Message);

        }

        [Fact]
        public void Editar_EnviandoModeloNaoPermitido()
        {
            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = 1,
                Descricao = "Caminhão Teste",
                MarcaId = 1,
                ModeloId = 1,
                AnoFabricacao = 2021,
                AnoModelo = 2021
            };

            Modelo modelo = new Modelo { Id = 1, Descricao = "Outros", MarcaId = 1, ModeloPermitido = false };
            Marca marca = new Marca { Id = 1, Descricao = "Outra Marca" };

            var _modeloRepositorio = new Mock<IModeloRepositorio>();
            _modeloRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.ModeloId)).Returns(modelo);

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();
            _marcaRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.MarcaId)).Returns(marca);

            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);

            var exception = Assert.Throws<Exception>(() => caminhaoServico.Adicionar(caminhaoViewModel));

            Assert.Equal("Modelo não permitido!", exception.Message);

        }

        [Fact]
        public void Editar_EnviandoModeloNaoRelacionadoMarca()
        {
            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = 1,
                Descricao = "Caminhão Teste",
                MarcaId = 99,
                ModeloId = 1,
                AnoFabricacao = 2021,
                AnoModelo = 2021
            };

            Modelo modelo = new Modelo { Id = 1, Descricao = "Outros", MarcaId = 1, ModeloPermitido = false };
            Marca marca = new Marca { Id = 99, Descricao = "Outra Marca" };

            var _modeloRepositorio = new Mock<IModeloRepositorio>();
            _modeloRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.ModeloId)).Returns(modelo);

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();
            _marcaRepositorio.Setup(x => x.Buscar(x => x.Id == caminhaoViewModel.MarcaId)).Returns(marca);

            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);

            var exception = Assert.Throws<Exception>(() => caminhaoServico.Adicionar(caminhaoViewModel));

            Assert.Equal("Marca não pertence ao modelo !", exception.Message);

        }

        #endregion

        #region[Validação de Busca]


        [Fact]
        public void BuscarTodos()
        {
            List<Caminhao> caminhaos = new List<Caminhao>();
            caminhaos.Add(new Caminhao { Id = 1, Descricao = "Caminhão 1", MarcaId = 1, ModeloId = 1, AnoModelo = 2021, AnoFabricacao = 2022 });
            caminhaos.Add(new Caminhao { Id = 2, Descricao = "Caminhão 2", MarcaId = 1, ModeloId = 1, AnoModelo = 2021, AnoFabricacao = 2022 });

            var _modeloRepositorio = new Mock<IModeloRepositorio>();


            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);
            _caminhaoRepositorio.Setup(x => x.ListarTodos()).Returns(caminhaos);

            var result = caminhaoServico.ListarTodos();

            //Validando se o retorno contém uma lista com objetos.
            Assert.True(result.Count() > 0);

        }

        [Fact]
        public void BuscarPorId()
        {
            List<Caminhao> caminhaos = new List<Caminhao>();
            caminhaos.Add(new Caminhao { Id = 1, Descricao = "Caminhão 1", MarcaId = 1, ModeloId = 1, AnoModelo = 2021, AnoFabricacao = 2022 });
            caminhaos.Add(new Caminhao { Id = 2, Descricao = "Caminhão 2", MarcaId = 1, ModeloId = 1, AnoModelo = 2021, AnoFabricacao = 2022 });

            var _modeloRepositorio = new Mock<IModeloRepositorio>();


            var _autoMapperProfile = new AutoMapperSetup();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var _marcaRepositorio = new Mock<IMarcaRepositorio>();

            var _caminhaoRepositorio = new Mock<ICaminhaoRepositorio>();
            caminhaoServico = new CaminhaoServico(_caminhaoRepositorio.Object, _mapper, _modeloRepositorio.Object, _marcaRepositorio.Object);
            _caminhaoRepositorio.Setup(x => x.BuscarPorId(1)).Returns(caminhaos.FirstOrDefault(x => x.Id == 1));

            var result = caminhaoServico.BuscarPorId(1);

            //Validando se o retorno contém uma lista com objetos.
            Assert.Equal(1, result.Id);

        }

        #endregion

        #region [Validação Data Annotations]

        [Fact]
        public void Adicionar_EnviandoObjetoInvalido()
        {
            var exception = Assert.Throws<ValidationException>(() => caminhaoServico.Adicionar(
                new CaminhaoViewModel { MarcaId = 1, ModeloId = 1,AnoFabricacao = 2021, AnoModelo = 2022 }));
            
            Assert.Equal("Campo Descrição Obrigatório!", exception.Message);
        }

        [Fact]
        public void Adicionar_FabricacaoDiferenteAnoAtual()
        {
            var exception = Assert.Throws<ValidationException>(() => caminhaoServico.Adicionar(
                new CaminhaoViewModel {Descricao = "Caminhão A", MarcaId = 1, ModeloId = 1, AnoFabricacao = 2022, AnoModelo = 2022 }));

            Assert.Equal("Ano inválido, ano tem que ser o atual!", exception.Message);
        }

        [Fact]
        public void Adicionar_AnoModeloDiferenteSubsequente()
        {
            var exception = Assert.Throws<ValidationException>(() => caminhaoServico.Adicionar(
                new CaminhaoViewModel { Descricao = "Caminhão A", MarcaId = 1, ModeloId = 1, AnoFabricacao = 2021, AnoModelo = 2023 }));

            Assert.Equal("Ano inválido, ano tem que ser o atual ou subsequente!", exception.Message);
        }


        [Fact]
        public void Editar_EnviandoObjetoInvalido()
        {
            var exception = Assert.Throws<ValidationException>(() => caminhaoServico.Editar(
                new CaminhaoViewModel {Id = 1, MarcaId = 1, ModeloId = 1, AnoFabricacao = 2021, AnoModelo = 2022 }));

            Assert.Equal("Campo Descrição Obrigatório!", exception.Message);
        }

        [Fact]
        public void Editar_FabricacaoDiferenteAnoAtual()
        {
            var exception = Assert.Throws<ValidationException>(() => caminhaoServico.Editar(
                new CaminhaoViewModel { Id = 1, Descricao = "Caminhão A", MarcaId = 1, ModeloId = 1, AnoFabricacao = 2022, AnoModelo = 2022 }));

            Assert.Equal("Ano inválido, ano tem que ser o atual!", exception.Message);
        }

        [Fact]
        public void Editar_AnoModeloDiferenteSubsequente()
        {
            var exception = Assert.Throws<ValidationException>(() => caminhaoServico.Editar(
                new CaminhaoViewModel { Id = 1, Descricao = "Caminhão A", MarcaId = 1, ModeloId = 1, AnoFabricacao = 2021, AnoModelo = 2023 }));

            Assert.Equal("Ano inválido, ano tem que ser o atual ou subsequente!", exception.Message);
        }

        #endregion
    }
}
