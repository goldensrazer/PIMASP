using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;
using PIM.Models.Objects;

namespace PIM.Models.Repository
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private DALDataContext dal_DataContext;

        public FuncionarioRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }

        public FuncionarioRepositorio(DALDataContext d)
        {
            dal_DataContext = d;
        }

        public IEnumerable<Funcionario> GetFuncionario(int ID = 0)
        {
            IList<Funcionario> funcionarioLista = new List<Funcionario>();

            var consulta = from a in dal_DataContext.FUNCIONARIOs
                           //join b in dal_DataContext.DEPARTAMENTOs on a.DEPARTAMENTO equals b.ID
                           where a.ID == ID 
                                || ID == 0
                           select new
                           {
                               ID = a.ID,
                               NOME = a.NOME,
                               EMAIL = a.EMAIL,
                               SENHA = a.SENHA/*,

                               DEPARTAMENTOID = b.ID,
                               DEPARTAMENTONOME = b.NOME,*/
                           };

            try
            {
                var funcionario = consulta.ToList();
                foreach (var funcionarioDados in funcionario)
                {
                    funcionarioLista.Add(new Funcionario()
                    {
                        id = funcionarioDados.ID,
                        Nome = funcionarioDados.NOME,
                        Email = funcionarioDados.EMAIL,
                        Senha = funcionarioDados.SENHA/*,
                        DepartamentoID = funcionarioDados.DEPARTAMENTOID,
                        Departamento = new Departamento() { id = funcionarioDados.DEPARTAMENTOID, Nome = funcionarioDados.DEPARTAMENTONOME }*/
                    });
                }
                return funcionarioLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Funcionario GetFuncionarioLogin(Login login)
        {
            var consulta = from a in dal_DataContext.FUNCIONARIOs
                           where a.EMAIL == login.Email && a.SENHA == login.Senha
                           select new
                           {
                               ID = a.ID,
                               NOME = a.NOME,
                               EMAIL = a.EMAIL
                           };

            try
            {
                var funcionario = consulta.FirstOrDefault();
                Funcionario f = new Funcionario()
                {
                    id = funcionario.ID,
                    Nome = funcionario.NOME,
                    Email = funcionario.EMAIL
                };

                return f;
            }
            catch
            {
                return null;
            }
        }

        public void InserirFuncionario(Funcionario _funcionario)
        {
            try
            {
                var funcionarioDados = new FUNCIONARIO()
                {
                    NOME = _funcionario.Nome,
                    EMAIL = _funcionario.Email,
                    SENHA = _funcionario.Senha/*, 
                    DEPARTAMENTO = _funcionario.DepartamentoID*/
                };
                dal_DataContext.FUNCIONARIOs.InsertOnSubmit(funcionarioDados);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeletarFuncionario(int funcionarioID)
        {
            try
            {
                FUNCIONARIO funcionario = dal_DataContext.FUNCIONARIOs.Where(c => c.ID == funcionarioID).SingleOrDefault();
                dal_DataContext.FUNCIONARIOs.DeleteOnSubmit(funcionario);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarFuncionario(Funcionario _funcionario)
        {
            try
            {
                FUNCIONARIO funcionario = dal_DataContext.FUNCIONARIOs.Where(c => c.ID == _funcionario.id).SingleOrDefault();
                funcionario.NOME = _funcionario.Nome;
                funcionario.EMAIL = _funcionario.Email;
                //funcionario.SENHA = _funcionario.Senha;
                //funcionario.DEPARTAMENTO = _funcionario.DepartamentoID;

                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}