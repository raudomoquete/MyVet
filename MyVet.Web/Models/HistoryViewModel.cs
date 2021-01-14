using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Models
{
    public class HistoryViewModel : History
    {
        //el modelo history tiene una relacion con la tabla Pet, pero es el objeto completo
        public int PetId { get; set; } //cuando se envia el modelo entre vista y controlador no se mandan objetos completos sino el Id

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Service Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a service type.")]
        public int ServiceTypeId { get; set; }

        public IEnumerable<SelectListItem> ServiceTypes { get; set; }
    }
}
