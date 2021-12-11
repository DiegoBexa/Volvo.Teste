using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.MVC.Controllers
{
    public class ModeloController : Controller
    {
        private readonly IModeloServico _modeloServico;
        private readonly IMarcaServico _marcaServico;

        public ModeloController(IModeloServico modeloServico,
                                IMarcaServico marcaServico)
        {
            _modeloServico = modeloServico;
            _marcaServico = marcaServico;
        }

        public IActionResult Index()
        {
            var modelos = _modeloServico.ListarTodos();

            return View(modelos);
        }

        public IActionResult Create()
        {
    
            var marcas = _marcaServico.ListarTodos();

            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ModeloViewModel modeloViewModel)
        {
            if (ModelState.IsValid)
            {
                _modeloServico.Adicionar(modeloViewModel);

                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> items = new List<SelectListItem>();

            var marcas = _marcaServico.ListarTodos();
            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            return View(modeloViewModel);
        }

    

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelo = _modeloServico.BuscarPorId(id.Value);

            if (modelo == null)
            {
                return NotFound();
            }

            var marcas = _marcaServico.ListarTodos();
            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ModeloViewModel modeloViewModel)
        {
            if (ModelState.IsValid)
            {
                _modeloServico.Editar(modeloViewModel);

                return RedirectToAction(nameof(Index));
            }

            var marcas = _marcaServico.ListarTodos();
            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            return View(modeloViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelo = _modeloServico.BuscarPorId(id.Value);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _modeloServico.Deletar(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelo = _modeloServico.BuscarPorId(id.Value);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }
    }
}
