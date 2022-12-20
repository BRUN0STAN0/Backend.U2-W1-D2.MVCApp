using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Dipendente
    {
        public int IDDipendente { get; set; }


        [Display(Name = "Nome Dipendente")]
        [Required(ErrorMessage= "Nome Obbligatorio")]
        public string Nome { get; set; }

        [Display(Name = "Cognome Dipendente")]
        [Required(ErrorMessage = "Cognome Obbligatorio")]
        public string Cognome { get; set; }

        [Display(Name = "Stipendio Dipendente in €")]
        [Required(ErrorMessage = "Droppa sto stipendio")]
        public double Stipendio { get; set; }

        
        public bool Coniugato { get; set;  }
    }
}