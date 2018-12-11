using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models.Objects;

namespace PIM.Controllers.Repository
{
    public interface IAtribuicaoRepositorio
    {
        IEnumerable<Atribuicao> GetAtribuicao();
        Atribuicao GetAtribuicaoPorID(int atribuicaoID);
        void InserirAtribuicao(Atribuicao _atribuicao);
        void DeletarAtribuicao(int atribuicaoID);
        void AtualizarAtribuicao(Atribuicao _atribuicao);
    }
}
