using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PIM.DAL;

namespace PIM.Models
{
    public class Setor
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        public string Nome { get; set; }
    }
}