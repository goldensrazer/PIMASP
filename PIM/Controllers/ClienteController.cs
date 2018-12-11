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
    public class ClienteController : BaseController
    {
        private IClienteRepositorio _repositorio;

        public ClienteController() :this(new ClienteRepositorio())
        {
        }

        public ClienteController(IClienteRepositorio repositorio)
        {
            ViewBag.Title = "Clientes";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var cliente = _repositorio.GetCliente();
            return View(cliente);
        }

        public ActionResult Details(int id)
        {
            Cliente model = _repositorio.GetCliente(ID: id).First();
            return View(model);
        }

        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            IDepartamentoRepositorio _depRepo = new DepartamentoRepositorio();
            foreach (var DP in _depRepo.GetDepartamento())
            {
                items.Add(new SelectListItem { Text = DP.Nome, Value = DP.id.ToString() });
            }
            ViewBag.DepartamentoID = items;

            return View(new Cliente());
        }
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(cliente);
        }

        public ActionResult Edit(int id)
        {
            Cliente model = _repositorio.GetCliente(ID: id).First();

            List<SelectListItem> items = new List<SelectListItem>();

            IDepartamentoRepositorio _depRepo = new DepartamentoRepositorio();
            foreach (var DP in _depRepo.GetDepartamento())
            {
                items.Add(new SelectListItem { Text = DP.Nome, Value = DP.id.ToString(), Selected = (DP.id == model.Departamento.id) ? true : false });
            }
            ViewBag.DepartamentoID = items;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(cliente);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            Cliente cliente = _repositorio.GetCliente(ID: id).First();
            return View(cliente);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repositorio.DeletarCliente(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }

    }
}