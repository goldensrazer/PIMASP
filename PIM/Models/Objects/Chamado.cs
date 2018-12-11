using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIM.Models.Objects
{
    public class Chamado
    {
        [Display(Name = "Código")]
        public int id { get; set; }

        [Display(Name = "Departamento")]
        public int DepartamentoID { get; set; }

        [Display(Name = "Grau de Urgência")]
        public int GrauUrgenciaID { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }

        [Display(Name = "Técnico")]
        public int FuncionarioID { get; set; }

        [Display(Name = "Atribuição")]
        public int AtribuicaoID { get; set; }

        [Display(Name = "Conclusão")]
        public DateTime? Conclusao { get; set; }

        public virtual Departamento Departamento { get; set; }

        [Display(Name = "Grau de Urgência")]
        public virtual GrauUrgencia GrauUrgencia { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Atribuicao Atribuicao { get; set; }

        [Display(Name = "Funcionário")]
        public virtual Funcionario Funcionario { get; set; }
    }
}