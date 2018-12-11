using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIM.Models.Objects
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Senha { get; set; }
        public int DepartamentoID { get; set; }

        public virtual Departamento Departamento { get; set; }
    }
}