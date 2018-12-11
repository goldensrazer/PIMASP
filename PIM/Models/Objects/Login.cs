using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIM.Models.Objects
{
    public class Login
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}