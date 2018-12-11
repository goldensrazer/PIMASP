using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIM.Models.Objects;

namespace PIM.Controllers
{
    public class AcessoController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            Repository.IFuncionarioRepositorio _rf;
            Repository.IClienteRepositorio _rc;

            _rf = new Models.Repository.FuncionarioRepositorio(new DAL.DALDataContext());
            _rc = new Models.Repository.ClienteRepositorio(new DAL.DALDataContext());

            Funcionario f = _rf.GetFuncionarioLogin(login);
            if (f != null)
            {
                Session["usuarioLogadoID"] = f.id;
                Session["usuarioLogadoNome"] = f.Nome;
                Session["usuarioLogadoTipo"] = "f";
                Session["ErroLogin"] = null;
            }
            else
            {
                Cliente c = _rc.GetClienteLogin(login);
                if (c != null)
                {
                    Session["usuarioLogadoID"] = c.id;
                    Session["usuarioLogadoNome"] = c.Nome;
                    Session["usuarioLogadoTipo"] = "c";
                    Session["ErroLogin"] = null;
                }
                else
                {
                    Session["ErroLogin"] = "E-mail e Senha não encontrados!";
                    return RedirectToAction("Login", "Acesso");
                }
            }
            return RedirectToAction("Index", "Chamado");
        }

        public ActionResult Erro404()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            Session["usuarioLogadoID"] = null;
            return RedirectToAction("Login", "Acesso");
        }

        // GET: Acesso
        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}