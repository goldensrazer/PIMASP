using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.DAL;
using PIM.Controllers.Repository;
using PIM.Models.Objects;

namespace PIM.Models.Repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private DALDataContext dal_DataContext;

        public ClienteRepositorio()
        {
            dal_DataContext = new DALDataContext();
        }

        public ClienteRepositorio(DALDataContext d)
        {
            dal_DataContext = d;
        }

        public IEnumerable<Cliente> GetCliente(int ID = 0)
        {
            IList<Cliente> clienteLista = new List<Cliente>();

            var consulta = from a in dal_DataContext.CLIENTEs
                           join b in dal_DataContext.DEPARTAMENTOs on a.DEPARTAMENTO equals b.ID
                           where a.ID == ID 
                                || ID == 0
                           select new
                           {
                               ID = a.ID,
                               NOME = a.NOME,
                               EMAIL = a.EMAIL,
                               SENHA = a.SENHA,

                               DEPARTAMENTOID = b.ID,
                               DEPARTAMENTONOME = b.NOME,
                           };

            try
            {
                var cliente = consulta.ToList();
                foreach (var clienteDados in cliente)
                {
                    clienteLista.Add(new Cliente()
                    {
                        id = clienteDados.ID,
                        Nome = clienteDados.NOME,
                        Email = clienteDados.EMAIL,
                        Senha = clienteDados.SENHA,
                        DepartamentoID = clienteDados.DEPARTAMENTOID,
                        Departamento = new Departamento() { id = clienteDados.DEPARTAMENTOID, Nome = clienteDados.DEPARTAMENTONOME }
                    });
                }
                return clienteLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cliente GetClienteLogin(Login login)
        {
            var consulta = from a in dal_DataContext.CLIENTEs
                           where a.EMAIL == login.Email && a.SENHA == login.Senha
                           select new
                           {
                               ID = a.ID,
                               NOME = a.NOME,
                               EMAIL = a.EMAIL
                           };

            try
            {
                var cliente = consulta.FirstOrDefault();
                Cliente c = new Cliente()
                {
                    id = cliente.ID,
                    Nome = cliente.NOME,
                    Email = cliente.EMAIL
                };

                return c;
            }
            catch
            {
                return null;
            }
        }

        public void InserirCliente(Cliente _cliente)
        {
            try
            {
                var clienteDados = new CLIENTE()
                {
                    NOME = _cliente.Nome,
                    EMAIL = _cliente.Email,
                    SENHA = _cliente.Senha,
                    DEPARTAMENTO = _cliente.DepartamentoID
                };
                dal_DataContext.CLIENTEs.InsertOnSubmit(clienteDados);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeletarCliente(int clienteID)
        {
            try
            {
                CLIENTE cliente = dal_DataContext.CLIENTEs.Where(c => c.ID == clienteID).SingleOrDefault();
                dal_DataContext.CLIENTEs.DeleteOnSubmit(cliente);
                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarCliente(Cliente _cliente)
        {
            try
            {
                CLIENTE cliente = dal_DataContext.CLIENTEs.Where(c => c.ID == _cliente.id).SingleOrDefault();
                cliente.NOME = _cliente.Nome;
                cliente.EMAIL = _cliente.Email;
                //cliente.SENHA = _cliente.Senha;
                cliente.DEPARTAMENTO = _cliente.DepartamentoID;

                dal_DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}