using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Models.Objects;

namespace PIM.Controllers.Repository
{
    public interface IGrauUrgenciaRepositorio
    {
        IEnumerable<GrauUrgencia> GetGrauUrgencia();
        GrauUrgencia GetGrauUrgenciaPorID(int grauUrgenciaID);
        void InserirGrauUrgencia(GrauUrgencia _grauUrgencia);
        void DeletarGrauUrgencia(int grauUrgenciaID);
        void AtualizarGrauUrgencia(GrauUrgencia _grauUrgencia);
    }
}
