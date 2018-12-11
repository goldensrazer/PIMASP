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
    public class ChamadoController : BaseController
    {
        private IChamadoRepositorio _repositorio;

        public ChamadoController() :this(new ChamadoRepositorio())
        {
        }

        public ChamadoController(IChamadoRepositorio repositorio)
        {
            ViewBag.Title = "Chamados";
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {
            var chamado = _repositorio.GetChamado();
            return View(chamado);
        }

        public ActionResult Details(int id)
        {
            Chamado model = _repositorio.GetChamado(id).First();
            return View(model);
        }

        public ActionResult Create()
        {
            List<SelectListItem> grauItems = new List<SelectListItem>();
            List<SelectListItem> departamentoItems = new List<SelectListItem>();
            List<SelectListItem> clienteItems = new List<SelectListItem>();
            List<SelectListItem> funcionarioItems = new List<SelectListItem>();
            List<SelectListItem> atribuicaoItems = new List<SelectListItem>();

            IGrauUrgenciaRepositorio _grauRepo = new GrauUrgenciaRepositorio();
            foreach (var GP in _grauRepo.GetGrauUrgencia())
            {
                grauItems.Add(new SelectListItem { Text = GP.Nome, Value = GP.id.ToString() });
            }

            IDepartamentoRepositorio _depRepo = new DepartamentoRepositorio();
            foreach (var DR in _depRepo.GetDepartamento())
            {
                departamentoItems.Add(new SelectListItem { Text = DR.Nome, Value = DR.id.ToString() });
            }

            IClienteRepositorio _clienteRepo = new ClienteRepositorio();
            foreach (var CR in _clienteRepo.GetCliente())
            {
                clienteItems.Add(new SelectListItem { Text = CR.Nome, Value = CR.id.ToString() });
            }

            IFuncionarioRepositorio _funcRepo = new FuncionarioRepositorio();
            foreach (var FR in _funcRepo.GetFuncionario())
            {
                funcionarioItems.Add(new SelectListItem { Text = FR.Nome, Value = FR.id.ToString() });
            }

            IAtribuicaoRepositorio _atribRepo = new AtribuicaoRepositorio();
            foreach (var AR in _atribRepo.GetAtribuicao())
            {
                atribuicaoItems.Add(new SelectListItem { Text = AR.Nome, Value = AR.id.ToString() });
            }

            ViewBag.GrauUrgenciaID = grauItems;
            ViewBag.DepartamentoID = departamentoItems;
            ViewBag.ClienteID = clienteItems;
            ViewBag.FuncionarioID = funcionarioItems;
            ViewBag.AtribuicaoID = atribuicaoItems;

            return View(new Chamado());
        }
        [HttpPost]
        public ActionResult Create(Chamado chamado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.InserirChamado(chamado);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(chamado);
        }

        public ActionResult Edit(int id)
        {
            //Chamado model = _repositorio.GetChamadoPorID(id);
            Chamado model = _repositorio.GetChamado(ID: id).First();

            /*List<SelectListItem> items = new List<SelectListItem>();

            ISetorRepositorio _setorRepo = new SetorRepositorio();
            foreach (var SR in _setorRepo.GetSetor())
            {
                items.Add(new SelectListItem { Text = SR.Nome, Value = SR.id.ToString(), Selected = (SR.id == model.Setor.id) ? true : false });
            }
            ViewBag.SetorID = items;*/

            List<SelectListItem> grauItems = new List<SelectListItem>();
            List<SelectListItem> departamentoItems = new List<SelectListItem>();
            List<SelectListItem> clienteItems = new List<SelectListItem>();
            List<SelectListItem> funcionarioItems = new List<SelectListItem>();
            List<SelectListItem> atribuicaoItems = new List<SelectListItem>();

            /*grauItems.Add(new SelectListItem { Text = "Muito Baixo", Value = "1", Selected = (1 == model.GrauUrgencia.id) ? true : false });
            grauItems.Add(new SelectListItem { Text = "Baixo", Value = "2", Selected = (2 == model.GrauUrgencia.id) ? true : false });
            grauItems.Add(new SelectListItem { Text = "Médio", Value = "3", Selected = (3 == model.GrauUrgencia.id) ? true : false });
            grauItems.Add(new SelectListItem { Text = "Alto", Value = "4", Selected = (4 == model.GrauUrgencia.id) ? true : false });
            grauItems.Add(new SelectListItem { Text = "Muito Alto", Value = "5", Selected = (5 == model.GrauUrgencia.id) ? true : false });*/

            /*clienteItems.Add(new SelectListItem { Text = "Bruno", Value = "1", Selected = (1 == model.Cliente.id) ? true : false });
            clienteItems.Add(new SelectListItem { Text = "Erick", Value = "2", Selected = (2 == model.Cliente.id) ? true : false });

            funcionarioItems.Add(new SelectListItem { Text = "Adilanne", Value = "1", Selected = (1 == model.Funcionario.id) ? true : false });
            funcionarioItems.Add(new SelectListItem { Text = "Kelvin", Value = "2", Selected = (2 == model.Funcionario.id) ? true : false });
            funcionarioItems.Add(new SelectListItem { Text = "Welison", Value = "3", Selected = (3 == model.Funcionario.id) ? true : false });*/

            IGrauUrgenciaRepositorio _grauRepo = new GrauUrgenciaRepositorio();
            foreach(var GP in _grauRepo.GetGrauUrgencia())
            {
                grauItems.Add(new SelectListItem { Text = GP.Nome, Value = GP.id.ToString(), Selected = (GP.id == model.GrauUrgencia.id) ? true : false }); 
            }

            IDepartamentoRepositorio _depRepo = new DepartamentoRepositorio();
            foreach (var DR in _depRepo.GetDepartamento())
            {
                departamentoItems.Add(new SelectListItem { Text = DR.Nome, Value = DR.id.ToString(), Selected = (DR.id == model.Departamento.id) ? true : false });
            }

            IClienteRepositorio _clienteRepo = new ClienteRepositorio();
            foreach (var CR in _clienteRepo.GetCliente())
            {
                clienteItems.Add(new SelectListItem { Text = CR.Nome, Value = CR.id.ToString(), Selected = (CR.id == model.Cliente.id) ? true : false });
            }

            IFuncionarioRepositorio _funcRepo = new FuncionarioRepositorio();
            foreach (var FR in _funcRepo.GetFuncionario())
            {
                funcionarioItems.Add(new SelectListItem { Text = FR.Nome, Value = FR.id.ToString(), Selected = (FR.id == model.Funcionario.id) ? true : false });
            }

            IAtribuicaoRepositorio _atribRepo = new AtribuicaoRepositorio();
            foreach (var AR in _atribRepo.GetAtribuicao())
            {
                atribuicaoItems.Add(new SelectListItem { Text = AR.Nome, Value = AR.id.ToString(), Selected = (AR.id == model.Atribuicao.id) ? true : false });
            }

            ViewBag.GrauUrgenciaID = grauItems;
            ViewBag.DepartamentoID = departamentoItems;
            ViewBag.ClienteID = clienteItems;
            ViewBag.FuncionarioID = funcionarioItems;
            ViewBag.AtribuicaoID = atribuicaoItems;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Chamado chamado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.AtualizarChamado(chamado);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Problemas ao salvar os dados...");
            }
            return View(chamado);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Problema ao deletar dados";
            }
            //Chamado chamado = _repositorio.GetChamadoPorID(id);
            Chamado chamado = _repositorio.GetChamado(ID: id).First();
            return View(chamado);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repositorio.DeletarChamado(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}