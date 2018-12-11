using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace PIM.Models
{
    public class Departamento
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Setor")]
        public int SetorID { get; set; }

        public virtual Setor Setor { get; set; }
    }
}