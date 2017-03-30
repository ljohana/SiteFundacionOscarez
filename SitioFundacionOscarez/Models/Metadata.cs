using System;
using System.ComponentModel.DataAnnotations;

namespace SitioFundacionOscarez.Models
{
    public class ContactenosMetadata
    {
        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un formato correcto, ejemplo: micorreo@dominio.com")]
        public string email { get; set; }

        [Display(Name = "Numero Teléfonico")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Mensaje { get; set; }
    }
}