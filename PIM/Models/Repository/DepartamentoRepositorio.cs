using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;

namespace PIM.Models.Repository
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private DALDataContext dal_DataContext;

        public DepartamentoRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }

        public IEnumerable<Departamento> GetDepartamento()
        {
            IList<Departamento> departamentoLista = new List<Departamento>();
            var consulta = from a in dal_DataContext.DEPARTAMENTOs
                           join b in dal_DataContext.SETORs on a.SETOR equals b.ID
                           select new { ID = a.ID, NOME = a.NOME, SETORID = b.ID, SETORNOME = b.NOME};

            try
            {
                var departamento = consulta.ToList();
                foreach (var departamentoDados in departamento)
                {
                    departamentoLista.Add(new Departamento()
                    {
                        id = departamentoDados.ID,
                        Nome = departamentoDados.NOME,
                        Setor = new Setor() { id = departamentoDados.SETORID, Nome = departamentoDados.SETORNOME }
                    });
                }
                return departamentoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Departamento GetDepartamentoPorID(int departamentoID)
        {
            var query = from a in dal_DataContext.DEPARTAMENTOs
                        join b in dal_DataContext.SETORs on a.SETOR equals b.ID
                        where a.ID == departamentoID
                        select new { ID = a.ID, NOME = a.NOME, SETORID = b.ID, SETORNOME = b.NOME};

            try
            {
                var departamentoDados = query.FirstOrDefault();
                var model = new Departamento()
                {
                    id = departamentoDados.ID,
                    Nome = departamentoDados.NOME,
                    Setor = new Setor() { id = departamentoDados.SETORID, Nome = departamentoDados.SETORNOME }
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirDepartamento(Departamento _departamento)
        {
            try
            {
                var departamentoDados = new DEPARTAMENTO()
                {
                    NOME = _departamento.Nome,
                    SETOR = _departamento.SetorID
                };
                dal_DataContext.DEPARTAMENTOs.InsertOnSubmit(departamentoDados);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarDepartamento(int departamentoID)
        {
            try
            {
                DEPARTAMENTO departamento = dal_DataContext.DEPARTAMENTOs.Where(d => d.ID == departamentoID).SingleOrDefault();
                dal_DataContext.DEPARTAMENTOs.DeleteOnSubmit(departamento);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarDepartamento(Departamento _departamento)
        {
            try
            {
                DEPARTAMENTO departamentoDados = dal_DataContext.DEPARTAMENTOs.Where(s => s.ID == _departamento.id).SingleOrDefault();
                departamentoDados.NOME = _departamento.Nome;
                departamentoDados.SETOR = _departamento.SetorID;
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}