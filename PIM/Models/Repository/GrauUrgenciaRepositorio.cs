using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;
using PIM.Models.Objects;

namespace PIM.Models.Repository
{
    public class GrauUrgenciaRepositorio: IGrauUrgenciaRepositorio
    {
        private DALDataContext dal_DataContext;

        public GrauUrgenciaRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }

        public IEnumerable<GrauUrgencia> GetGrauUrgencia()
        {
            IList<GrauUrgencia> grauUrgenciaLista = new List<GrauUrgencia>();
            var consulta = from q in dal_DataContext.GRAU_URGENCIAs
                           select q;

            try
            {
                var grauUrgencia = consulta.ToList();
                foreach (var grauUrgenciaDados in grauUrgencia)
                {
                    grauUrgenciaLista.Add(new GrauUrgencia()
                    {
                        id = grauUrgenciaDados.ID,
                        Nome = grauUrgenciaDados.NOME
                    });
                }
                return grauUrgenciaLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GrauUrgencia GetGrauUrgenciaPorID(int grauUrgenciaID)
        {
            var query = from s in dal_DataContext.GRAU_URGENCIAs
                        where s.ID == grauUrgenciaID
                        select s;

            try
            {
                var grauUrgenciaDados = query.FirstOrDefault();
                var model = new GrauUrgencia()
                {
                    id = grauUrgenciaDados.ID,
                    Nome = grauUrgenciaDados.NOME
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirGrauUrgencia(GrauUrgencia grauUrgencia)
        {
            try
            {
                var grauUrgenciaDados = new GRAU_URGENCIA()
                {
                    //ID = grauUrgencia.id,
                    NOME = grauUrgencia.Nome
                };
                dal_DataContext.GRAU_URGENCIAs.InsertOnSubmit(grauUrgenciaDados);
                dal_DataContext.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarGrauUrgencia(int grauUrgenciaID)
        {
            try
            {
                GRAU_URGENCIA grauUrgencia = dal_DataContext.GRAU_URGENCIAs.Where(s => s.ID == grauUrgenciaID).SingleOrDefault();
                dal_DataContext.GRAU_URGENCIAs.DeleteOnSubmit(grauUrgencia);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarGrauUrgencia(GrauUrgencia grauUrgencia)
        {
            try
            {
                GRAU_URGENCIA grauUrgenciaDados = dal_DataContext.GRAU_URGENCIAs.Where(s => s.ID == grauUrgencia.id).SingleOrDefault();
                grauUrgenciaDados.NOME = grauUrgencia.Nome;
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}