using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models.Objects;

namespace PIM.Controllers.Repository
{
    public interface IClienteRepositorio
    {
        IEnumerable<Cliente> GetCliente(int ID = 0);
        Cliente GetClienteLogin(Login login);
        void InserirCliente(Cliente _cliente);
        void DeletarCliente(int clienteID);
        void AtualizarCliente(Cliente _cliente);
    }
}
