using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIM.Models.Repository;
using PIM.Models;
using PIM.Controllers.Repository;
using System.Data;

namespace PIM.Controllers
{
    public class SetorController : BaseController
    {
        private ISetorRepositorio _repositorio;

        public SetorController() :this(new SetorRepositorio())
        {
        }

        public SetorController(ISetorRepositorio repositorio)
        {
            ViewBag.Title = "Setores";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var setor = _repositorio.GetSetor();
            return View(setor);
        }

        public ActionResult Details(int id)
        {
            //Setor model = _repositorio.GetSetorPorID(id);
            Setor model = _repositorio.GetSetor(ID: id).First();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new Setor());
        }
        [HttpPost]
        public ActionResult Create(Setor setor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirSetor(setor);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(setor);
        }

        public ActionResult Edit(int id)
        {
            Setor model = _repositorio.GetSetor(ID: id).First();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Setor setor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarSetor(setor);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(setor);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            //Setor setor = _repositorio.GetSetorPorID(id);
            Setor setor = _repositorio.GetSetor(ID: id).First();
            return View(setor);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //ClienteModel cliente = _repositorio.GetClientePorID(id);
                _repositorio.DeletarSetor(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}