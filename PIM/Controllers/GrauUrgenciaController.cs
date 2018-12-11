using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIM.Models.Repository;
using PIM.Models.Objects;
using PIM.Controllers.Repository;
using System.Data;

namespace PIM.Controllers
{
    public class GrauUrgenciaController : BaseController
    {
        private IGrauUrgenciaRepositorio _repositorio;

        public GrauUrgenciaController() :this(new GrauUrgenciaRepositorio())
        {
        }

        public GrauUrgenciaController(IGrauUrgenciaRepositorio repositorio)
        {
            ViewBag.Title = "Graus de Urgencia";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var grauUrgencia = _repositorio.GetGrauUrgencia();
            return View(grauUrgencia);
        }

        public ActionResult Details(int id)
        {
            GrauUrgencia model = _repositorio.GetGrauUrgenciaPorID(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new GrauUrgencia());
        }
        [HttpPost]
        public ActionResult Create(GrauUrgencia grauUrgencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirGrauUrgencia(grauUrgencia);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(grauUrgencia);
        }

        public ActionResult Edit(int id)
        {
            GrauUrgencia model = _repositorio.GetGrauUrgenciaPorID(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(GrauUrgencia grauUrgencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarGrauUrgencia(grauUrgencia);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(grauUrgencia);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            GrauUrgencia grauUrgencia = _repositorio.GetGrauUrgenciaPorID(id);
            return View(grauUrgencia);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //ClienteModel cliente = _repositorio.GetClientePorID(id);
                _repositorio.DeletarGrauUrgencia(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}