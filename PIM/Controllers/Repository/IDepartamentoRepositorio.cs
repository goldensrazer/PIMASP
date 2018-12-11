using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models;

namespace PIM.Controllers.Repository
{
    public interface IDepartamentoRepositorio
    {
        IEnumerable<Departamento> GetDepartamento();
        Departamento GetDepartamentoPorID(int departamentoID);
        void InserirDepartamento(Departamento _departamento);
        void DeletarDepartamento(int departamentoID);
        void AtualizarDepartamento(Departamento _departamento);
    }
}
