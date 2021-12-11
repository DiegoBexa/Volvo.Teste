using Microsoft.AspNetCore.Mvc;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModeloController : Controller
    {
        private readonly IModeloServico _modeloServico;

        public ModeloController(IModeloServico modeloServico)
        {
            _modeloServico = modeloServico;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_modeloServico.ListarTodos());
        }

        [HttpPost]
        public IActionResult Salvar(ModeloViewModel modeloViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_modeloServico.Adicionar(modeloViewModel));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_modeloServico.BuscarPorId(id));
        }

        [HttpPut]
        public IActionResult Editar(ModeloViewModel modeloViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_modeloServico.Editar(modeloViewModel));
        }

        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            return Ok(_modeloServico.Deletar(id));
        }
    }
}
