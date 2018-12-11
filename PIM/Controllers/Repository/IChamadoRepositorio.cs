using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models.Objects;

namespace PIM.Controllers.Repository
{
    public interface IChamadoRepositorio
    {
        IEnumerable<Chamado> GetChamado(int ID = 0);
        //Chamado GetChamadoPorID(int chamadoID);
        void InserirChamado(Chamado _chamado);
        void DeletarChamado(int chamadoID);
        void AtualizarChamado(Chamado _chamado);
    }
}
