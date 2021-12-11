using Microsoft.AspNetCore.Mvc;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.MVC.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IMarcaServico _marcaServico;

        public MarcaController(IMarcaServico marcaServico)
        {
            _marcaServico = marcaServico;
        }

        public IActionResult Index()
        {
            var listaMarcas = _marcaServico.ListarTodos();

            return View(listaMarcas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                _marcaServico.Adicionar(marcaViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(marcaViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = _marcaServico.BuscarPorId(id.Value);

            if (marca == null)
            {
                return NotFound();
            }


            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {

                _marcaServico.Editar(marcaViewModel);

                return RedirectToAction(nameof(Index));
            }         

            return View(marcaViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = _marcaServico.BuscarPorId(id.Value);

            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _marcaServico.Deletar(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = _marcaServico.BuscarPorId(id.Value);

            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }
    }
}
