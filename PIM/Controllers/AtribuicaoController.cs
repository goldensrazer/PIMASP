using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIM.Models.Objects;
using PIM.Controllers.Repository;
using PIM.Models.Repository;
using System.Data;

namespace PIM.Controllers
{
    public class AtribuicaoController : BaseController
    {
        private IAtribuicaoRepositorio _repositorio;

        public AtribuicaoController() :this(new AtribuicaoRepositorio())
        {
        }

        public AtribuicaoController(IAtribuicaoRepositorio repositorio)
        {
            ViewBag.Title = "Atribuições";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var atribuicao = _repositorio.GetAtribuicao();
            return View(atribuicao);
        }

        public ActionResult Details(int id)
        {
            Atribuicao model = _repositorio.GetAtribuicaoPorID(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new Atribuicao());
        }
        [HttpPost]
        public ActionResult Create(Atribuicao atribuicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirAtribuicao(atribuicao);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(atribuicao);
        }

        public ActionResult Edit(int id)
        {
            Atribuicao model = _repositorio.GetAtribuicaoPorID(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Atribuicao atribuicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarAtribuicao(atribuicao);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(atribuicao);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            Atribuicao atribuicao = _repositorio.GetAtribuicaoPorID(id);
            return View(atribuicao);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //ClienteModel cliente = _repositorio.GetClientePorID(id);
                _repositorio.DeletarAtribuicao(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}