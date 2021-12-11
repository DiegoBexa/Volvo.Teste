using Microsoft.AspNetCore.Mvc;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CaminhaoController : ControllerBase
    {
        private readonly ICaminhaoServico _caminhaoServico;

        public CaminhaoController(ICaminhaoServico caminhaoServico)
        {
            _caminhaoServico = caminhaoServico;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_caminhaoServico.ListarTodos());
        }

        [HttpPost]
        public IActionResult Salvar(CaminhaoViewModel caminhaoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_caminhaoServico.Adicionar(caminhaoViewModel));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_caminhaoServico.BuscarPorId(id));
        }

        [HttpPut]
        public IActionResult Editar(CaminhaoViewModel caminhaoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_caminhaoServico.Editar(caminhaoViewModel));
        }

        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            return Ok(_caminhaoServico.Deletar(id));
        }
    }
}
