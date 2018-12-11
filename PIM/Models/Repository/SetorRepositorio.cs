using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;

namespace PIM.Models.Repository
{
    public class SetorRepositorio: ISetorRepositorio
    {
        private DALDataContext dal_DataContext;

        public SetorRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }

        public IEnumerable<Setor> GetSetor(int ID = 0)
        {
            IList<Setor> setorLista = new List<Setor>();
            var consulta = from q in dal_DataContext.SETORs
                           where q.ID == ID 
                                || ID == 0
                           select q;

            try
            {
                var setor = consulta.ToList();
                foreach (var setorDados in setor)
                {
                    setorLista.Add(new Setor()
                    {
                        id = setorDados.ID,
                        Nome = setorDados.NOME
                    });
                }
                return setorLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public Setor GetSetorPorID(int setorID)
        {
            var query = from s in dal_DataContext.SETORs
                        where s.ID == setorID
                        select s;

            try
            {
                var setorDados = query.FirstOrDefault();
                var model = new Setor()
                {
                    id = setorDados.ID,
                    Nome = setorDados.NOME
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public void InserirSetor(Setor setor)
        {
            try
            {
                var setorDados = new SETOR()
                {
                    //ID = setor.id,
                    NOME = setor.Nome
                };
                dal_DataContext.SETORs.InsertOnSubmit(setorDados);
                dal_DataContext.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarSetor(int setorID)
        {
            try
            {
                SETOR setor = dal_DataContext.SETORs.Where(s => s.ID == setorID).SingleOrDefault();
                dal_DataContext.SETORs.DeleteOnSubmit(setor);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarSetor(Setor setor)
        {
            try
            {
                SETOR setorDados = dal_DataContext.SETORs.Where(s => s.ID == setor.id).SingleOrDefault();
                setorDados.NOME = setor.Nome;
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}