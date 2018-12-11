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
    public class FuncionarioController : BaseController
    {
        private IFuncionarioRepositorio _repositorio;

        public FuncionarioController() :this(new FuncionarioRepositorio())
        {
        }

        public FuncionarioController(IFuncionarioRepositorio repositorio)
        {
            ViewBag.Title = "Funcionarios";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var funcionario = _repositorio.GetFuncionario();
            return View(funcionario);
        }

        public ActionResult Details(int id)
        {
            Funcionario model = _repositorio.GetFuncionario(ID: id).First();
            return View(model);
        }

        public ActionResult Create()
        {
            /*List<SelectListItem> items = new List<SelectListItem>();

            IDepartamentoRepositorio _depRepo = new DepartamentoRepositorio();
            foreach (var DP in _depRepo.GetDepartamento())
            {
                items.Add(new SelectListItem { Text = DP.Nome, Value = DP.id.ToString() });
            }
            ViewBag.DepartamentoID = items;*/

            return View(new Funcionario());
        }
        [HttpPost]
        public ActionResult Create(Funcionario funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirFuncionario(funcionario);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(funcionario);
        }

        public ActionResult Edit(int id)
        {
            Funcionario model = _repositorio.GetFuncionario(ID: id).First();

            /*List<SelectListItem> items = new List<SelectListItem>();

            IDepartamentoRepositorio _depRepo = new DepartamentoRepositorio();
            foreach (var DP in _depRepo.GetDepartamento())
            {
                items.Add(new SelectListItem { Text = DP.Nome, Value = DP.id.ToString(), Selected = (DP.id == model.Departamento.id) ? true : false });
            }
            ViewBag.DepartamentoID = items;*/

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Funcionario funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarFuncionario(funcionario);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(funcionario);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            Funcionario funcionario = _repositorio.GetFuncionario(ID: id).First();
            return View(funcionario);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repositorio.DeletarFuncionario(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }

    }
}