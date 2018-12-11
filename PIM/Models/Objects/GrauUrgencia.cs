using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIM.Models.Objects
{
    public class GrauUrgencia
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        public string Nome { get; set; }
    }
}