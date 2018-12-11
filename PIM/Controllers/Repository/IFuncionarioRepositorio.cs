using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models.Objects;

namespace PIM.Controllers.Repository
{
    public interface IFuncionarioRepositorio
    {
        IEnumerable<Funcionario> GetFuncionario(int ID = 0);
        Funcionario GetFuncionarioLogin(Login login);
        void InserirFuncionario(Funcionario _funcionario);
        void DeletarFuncionario(int funcionarioID);
        void AtualizarFuncionario(Funcionario _funcionario);
    }
}
