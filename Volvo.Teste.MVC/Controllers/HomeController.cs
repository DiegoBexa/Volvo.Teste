using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Volvo.Teste.MVC.Models;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Servico.ViewModel;

namespace Volvo.Teste.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICaminhaoServico _caminhaoServico;
        private readonly IMarcaServico _marcaServico;
        private readonly IModeloServico _modeloServico;

        public HomeController(ICaminhaoServico caminhaoServico,
                              IMarcaServico marcaServico,
                              IModeloServico modeloServico)
        {
            _caminhaoServico = caminhaoServico;
            _marcaServico = marcaServico;
            _modeloServico = modeloServico;
        }

        public IActionResult Index()
        {
            var listaCaminhoes = _caminhaoServico.ListarTodos();

            return View(listaCaminhoes);
        }

        public IActionResult Create()
        {

            var marcas = _marcaServico.ListarTodos();

            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CaminhaoViewModel caminhaoViewModel)
        {
            if (ModelState.IsValid)
            {
                caminhaoViewModel = _caminhaoServico.Adicionar(caminhaoViewModel);

                if (caminhaoViewModel.Id > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return BadRequest();
            }

            var marcas = _marcaServico.ListarTodos();
            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            if (caminhaoViewModel.MarcaId > 0)
            {
                var modelos = _modeloServico.ListarTodosPermitidos(caminhaoViewModel.MarcaId);

                ViewData["ModeloId"] = new SelectList(modelos, "Id", "Descricao");
            }

            return View(caminhaoViewModel);
        }

        [HttpPost]
        public IActionResult ListarModeloAjax(int prmIdMarca)
        {
            List<SelectListItem> listaRetorno = new List<SelectListItem>();

            var modelos = _modeloServico.ListarTodosPermitidos(prmIdMarca);

            if (modelos.Any())
            {
                listaRetorno = modelos.Select(x => new SelectListItem
                {
                    Text = x.Descricao,
                    Value = x.Id.ToString()

                }).ToList();
            }


            return Json(listaRetorno);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = _caminhaoServico.BuscarPorId(id.Value);

            if (caminhao == null)
            {
                return NotFound();
            }


            var marcas = _marcaServico.ListarTodos();

            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            var modelos = _modeloServico.ListarTodosPermitidos(caminhao.MarcaId);

            ViewData["ModeloId"] = new SelectList(modelos, "Id", "Descricao");

            return View(caminhao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CaminhaoViewModel caminhaoViewModel)
        {
            if (ModelState.IsValid)
            {
                _caminhaoServico.Editar(caminhaoViewModel);

                return RedirectToAction(nameof(Index));
            }


            var marcas = _marcaServico.ListarTodos();
            ViewData["MarcaId"] = new SelectList(marcas, "Id", "Descricao");

            if (caminhaoViewModel.MarcaId > 0)
            {
                var modelos = _modeloServico.ListarTodosPermitidos(caminhaoViewModel.MarcaId);

                ViewData["ModeloId"] = new SelectList(modelos, "Id", "Descricao");
            }

            return View(caminhaoViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = _caminhaoServico.BuscarPorId(id.Value);

            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _caminhaoServico.Deletar(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = _caminhaoServico.BuscarPorId(id.Value);

            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
