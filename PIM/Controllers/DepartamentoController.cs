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
    public class DepartamentoController : BaseController
    {

        private IDepartamentoRepositorio _repositorio;

        public DepartamentoController() :this(new DepartamentoRepositorio())
        {
        }

        public DepartamentoController(IDepartamentoRepositorio repositorio)
        {
            ViewBag.Title = "Departamento";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var departamento = _repositorio.GetDepartamento();
            return View(departamento);
        }

        public ActionResult Details(int id)
        {
            Departamento model = _repositorio.GetDepartamentoPorID(id);
            return View(model);
        }

        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            ISetorRepositorio _setorRepo = new SetorRepositorio();
            foreach (var SR in _setorRepo.GetSetor())
            {
                items.Add(new SelectListItem { Text = SR.Nome, Value = SR.id.ToString() });
            }
            ViewBag.SetorID = items;
            return View(new Departamento());
        }
        [HttpPost]
        public ActionResult Create(Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirDepartamento(departamento);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(departamento);
        }

        public ActionResult Edit(int id)
        {
            Departamento model = _repositorio.GetDepartamentoPorID(id);

            List<SelectListItem> items = new List<SelectListItem>();

            ISetorRepositorio _setorRepo = new SetorRepositorio();
            foreach (var SR in _setorRepo.GetSetor())
            {
                items.Add(new SelectListItem { Text = SR.Nome, Value = SR.id.ToString(), Selected = (SR.id == model.Setor.id) ? true : false });
            }
            ViewBag.SetorID = items;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarDepartamento(departamento);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(departamento);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            Departamento departamento = _repositorio.GetDepartamentoPorID(id);
            return View(departamento);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //ClienteModel cliente = _repositorio.GetClientePorID(id);
                _repositorio.DeletarDepartamento(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}