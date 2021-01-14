using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Models
{
    public class PetViewModel : Pet
    {
        public int OwnerId { get; set; } //a nivel de controlador no se envia el objeto completo sino el id, por eso se crea el OwnerID
        // y el PetTypeID

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Pet Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a pet type.")] //empezamos desde el 1 para que no seleccione la mascota 0
        public int PetTypeId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; } //aqui es donde se almacenara la imagen (en memoria)
        public IEnumerable<SelectListItem> PetTypes { get; set; } //SelectListItem es el origen de datos para los comboBox
    }
}
