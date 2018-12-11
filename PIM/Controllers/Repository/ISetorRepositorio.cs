using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models;

namespace PIM.Controllers.Repository
{
    public interface ISetorRepositorio
    {
        IEnumerable<Setor> GetSetor(int ID = 0);
        //Setor GetSetorPorID(int setorID);
        void InserirSetor(Setor _setor);
        void DeletarSetor(int setorID);
        void AtualizarSetor(Setor _setor);
    }
}
