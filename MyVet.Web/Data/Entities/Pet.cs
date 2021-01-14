using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Data.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The {0} field is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Race { get; set; } //La mejor practica seria agregar una tabla razas para elegir la raza desde alli
        
        [Display(Name = "Born")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Born { get; set; }
        public string Remarks { get; set; }

        //aqui colocamos la ruta del repositorio de las imagenes
        //para no guardarlas en la base de datos
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null //operador ternario. Es un if que dice: si no tiene img devuelva null
            : $"https://TDB.azurewebsites.net{ImageUrl.Substring(1)}";

        [Display(Name = "Born")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime BornLocal => Born.ToLocalTime();

        //Relacion con la tabla PerType
        public PetType PetType { get; set; }
        public Owner Owner { get; set; }
        public ICollection<History> Histories { get; set; }

        public ICollection<Agenda> Agendas { get; set; }

    }
}
