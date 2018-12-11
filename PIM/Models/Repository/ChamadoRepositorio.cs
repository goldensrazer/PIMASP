using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;
using PIM.Models.Objects;

namespace PIM.Models.Repository
{
    public class ChamadoRepositorio : IChamadoRepositorio
    {
        private DALDataContext dal_DataContext;

        public ChamadoRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }

        public IEnumerable<Chamado> GetChamado(int ID = 0)
        {
            IList<Chamado> chamadoLista = new List<Chamado>();

            var consulta = from a in dal_DataContext.CHAMADOs
                               join b in dal_DataContext.DEPARTAMENTOs on a.DEPARTAMENTO equals b.ID
                               join c in dal_DataContext.GRAU_URGENCIAs on a.GRAU_URGENCIA equals c.ID
                               join d in dal_DataContext.CLIENTEs on a.CLIENTE equals d.ID
                               join e in dal_DataContext.FUNCIONARIOs on a.FUNCIONARIO equals e.ID
                               join f in dal_DataContext.ATRIBUICAOs on a.ATRIBUICAO equals f.ID
                           where a.ID == ID
                                || ID == 0
                           select new
                           {
                               ID = a.ID,
                               TITULO = a.TITULO,
                               DESCRICAO = a.DESCRICAO,
                               CONCLUSAO = a.CONCLUSAO,

                               DEPARTAMENTOID = b.ID,
                               DEPARTAMENTONOME = b.NOME,
                               GRAUURGENCIAID = c.ID,
                               GRAUURGENCIANOME = c.NOME,
                               CLIENTEID = d.ID,
                               CLIENTENOME = d.NOME,
                               FUNCIONARIOID = e.ID,
                               FUNCIONARIONOME = e.NOME,
                               ATRIBUICAOID = f.ID,
                               ATRIBUICAONOME = f.NOME
                           };

            try
            {
                var chamado = consulta.ToList();
                foreach (var chamadoDados in chamado)
                {
                    chamadoLista.Add(new Chamado()
                    {
                        id = chamadoDados.ID,
                        Titulo = chamadoDados.TITULO,
                        Descricao = chamadoDados.DESCRICAO,
                        Conclusao = chamadoDados.CONCLUSAO,

                        DepartamentoID = chamadoDados.DEPARTAMENTOID,
                        GrauUrgenciaID = chamadoDados.GRAUURGENCIAID,
                        ClienteID = chamadoDados.GRAUURGENCIAID,
                        FuncionarioID = chamadoDados.FUNCIONARIOID,
                        AtribuicaoID = chamadoDados.ATRIBUICAOID,

                        Departamento = new Departamento() { id = chamadoDados.DEPARTAMENTOID, Nome = chamadoDados.DEPARTAMENTONOME },
                        GrauUrgencia = new GrauUrgencia() { id = chamadoDados.GRAUURGENCIAID, Nome = chamadoDados.GRAUURGENCIANOME },
                        Cliente = new Cliente() { id = chamadoDados.CLIENTEID, Nome = chamadoDados.CLIENTENOME},
                        Funcionario = new Funcionario() { id = chamadoDados.FUNCIONARIOID, Nome = chamadoDados.FUNCIONARIONOME},
                        Atribuicao = new Atribuicao() { id = chamadoDados.ATRIBUICAOID, Nome = chamadoDados.ATRIBUICAONOME}
                    });
                }
                return chamadoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public Chamado GetChamadoPorID(int chamadoID)
        {
            var query = from a in dal_DataContext.CHAMADOs
                           join b in dal_DataContext.DEPARTAMENTOs on a.DEPARTAMENTO equals b.ID
                           join c in dal_DataContext.GRAU_URGENCIAs on a.GRAU_URGENCIA equals c.ID
                           join d in dal_DataContext.CLIENTEs on a.CLIENTE equals d.ID
                           join e in dal_DataContext.FUNCIONARIOs on a.FUNCIONARIO equals e.ID
                           join f in dal_DataContext.ATRIBUICAOs on a.ATRIBUICAO equals f.ID
                        where a.ID == chamadoID
                           select new
                           {
                               ID = a.ID,
                               TITULO = a.TITULO,
                               DESCRICAO = a.DESCRICAO,
                               CONCLUSAO = a.CONCLUSAO,

                               DEPARTAMENTOID = b.ID,
                               DEPARTAMENTONOME = b.NOME,
                               GRAUURGENCIAID = c.ID,
                               GRAUURGENCIANOME = c.NOME,
                               CLIENTEID = d.ID,
                               CLIENTENOME = d.NOME,
                               FUNCIONARIOID = e.ID,
                               FUNCIONARIONOME = e.NOME,
                               ATRIBUICAOID = f.ID,
                               ATRIBUICAONOME = f.NOME
                           };

            try
            {
                var chamadoDados = query.FirstOrDefault();
                var model = new Chamado() { 
                    id = chamadoDados.ID,
                    Titulo = chamadoDados.TITULO,
                    Descricao = chamadoDados.DESCRICAO,
                    Conclusao = chamadoDados.CONCLUSAO,

                    DepartamentoID = chamadoDados.DEPARTAMENTOID,
                    GrauUrgenciaID = chamadoDados.GRAUURGENCIAID,
                    ClienteID = chamadoDados.GRAUURGENCIAID,
                    FuncionarioID = chamadoDados.FUNCIONARIOID,
                    AtribuicaoID = chamadoDados.ATRIBUICAOID,

                    Departamento = new Departamento() { id = chamadoDados.DEPARTAMENTOID, Nome = chamadoDados.DEPARTAMENTONOME },
                    GrauUrgencia = new GrauUrgencia() { id = chamadoDados.GRAUURGENCIAID, Nome = chamadoDados.GRAUURGENCIANOME },
                    Cliente = new Cliente() { id = chamadoDados.CLIENTEID, Nome = chamadoDados.CLIENTENOME },
                    Funcionario = new Funcionario() { id = chamadoDados.FUNCIONARIOID, Nome = chamadoDados.FUNCIONARIONOME },
                    Atribuicao = new Atribuicao() { id = chamadoDados.ATRIBUICAOID, Nome = chamadoDados.ATRIBUICAONOME }
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public void InserirChamado(Chamado _chamado)
        {
            try
            {
                var chamadoDados = new CHAMADO()
                {
                    TITULO = _chamado.Titulo,
                    DESCRICAO = _chamado.Descricao,

                    DEPARTAMENTO = _chamado.DepartamentoID,
                    GRAU_URGENCIA = _chamado.GrauUrgenciaID,
                    CLIENTE = _chamado.ClienteID,
                    FUNCIONARIO = _chamado.FuncionarioID,
                    ATRIBUICAO = _chamado.AtribuicaoID
                };
                dal_DataContext.CHAMADOs.InsertOnSubmit(chamadoDados);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarChamado(int chamadoID)
        {
            try
            {
                CHAMADO chamado = dal_DataContext.CHAMADOs.Where(c => c.ID == chamadoID).SingleOrDefault();
                dal_DataContext.CHAMADOs.DeleteOnSubmit(chamado);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarChamado(Chamado _chamado)
        {
            try
            {
                CHAMADO chamadoDados = dal_DataContext.CHAMADOs.Where(c => c.ID == _chamado.id).SingleOrDefault();
                chamadoDados.TITULO = _chamado.Titulo;
                chamadoDados.DESCRICAO = _chamado.Descricao;

                chamadoDados.DEPARTAMENTO = _chamado.DepartamentoID;
                chamadoDados.GRAU_URGENCIA = _chamado.GrauUrgenciaID;
                chamadoDados.CLIENTE = _chamado.ClienteID;
                chamadoDados.FUNCIONARIO = _chamado.FuncionarioID;
                chamadoDados.ATRIBUICAO = _chamado.AtribuicaoID;

                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}