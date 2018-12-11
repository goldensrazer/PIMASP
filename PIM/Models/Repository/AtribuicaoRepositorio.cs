using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;
using PIM.Models.Objects;

namespace PIM.Models.Repository
{
    public class AtribuicaoRepositorio: IAtribuicaoRepositorio
    {
        private DALDataContext dal_DataContext;

        public AtribuicaoRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }
        public IEnumerable<Atribuicao> GetAtribuicao()
        {
            IList<Atribuicao> atribuicaoLista = new List<Atribuicao>();
            var consulta = from q in dal_DataContext.ATRIBUICAOs
                           select q;

            try
            {
                var atribuicao = consulta.ToList();
                foreach (var atribuicaoDados in atribuicao)
                {
                    atribuicaoLista.Add(new Atribuicao()
                    {
                        id = atribuicaoDados.ID,
                        Nome = atribuicaoDados.NOME
                    });
                }
                return atribuicaoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Atribuicao GetAtribuicaoPorID(int atribuicaoID)
        {
            var query = from s in dal_DataContext.ATRIBUICAOs
                        where s.ID == atribuicaoID
                        select s;

            try
            {
                var atribuicaoDados = query.FirstOrDefault();
                var model = new Atribuicao()
                {
                    id = atribuicaoDados.ID,
                    Nome = atribuicaoDados.NOME
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirAtribuicao(Atribuicao atribuicao)
        {
            try
            {
                var atribuicaoDados = new ATRIBUICAO()
                {
                    NOME = atribuicao.Nome
                };
                dal_DataContext.ATRIBUICAOs.InsertOnSubmit(atribuicaoDados);
                dal_DataContext.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarAtribuicao(int atribuicaoID)
        {
            try
            {
                ATRIBUICAO atribuicao = dal_DataContext.ATRIBUICAOs.Where(s => s.ID == atribuicaoID).SingleOrDefault();
                dal_DataContext.ATRIBUICAOs.DeleteOnSubmit(atribuicao);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarAtribuicao(Atribuicao atribuicao)
        {
            try
            {
                ATRIBUICAO atribuicaoDados = dal_DataContext.ATRIBUICAOs.Where(s => s.ID == atribuicao.id).SingleOrDefault();
                atribuicaoDados.NOME = atribuicao.Nome;
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}