using Microsoft.AspNetCore.Mvc;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MarcaController : Controller
    {
        private readonly IMarcaServico _marcaServico;

        public MarcaController(IMarcaServico marcaServico)
        {
            _marcaServico = marcaServico;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_marcaServico.ListarTodos());
        }

        [HttpPost]
        public IActionResult Salvar(MarcaViewModel marcaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_marcaServico.Adicionar(marcaViewModel));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_marcaServico.BuscarPorId(id));
        }

        [HttpPut]
        public IActionResult Editar(MarcaViewModel marcaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_marcaServico.Editar(marcaViewModel));
        }

        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            return Ok(_marcaServico.Deletar(id));
        }
    }
}
