using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Data.Entities
{
    public class PetType
    {
        public int Id { get; set; }

        [Display(Name = "Pet Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        //Relacion con la tabla Pet. se pretende que PetType almacene muchos tipos de mascotas
        //por ello se crea una coleccion 
        public ICollection<Pet> Pets { get; set; }
    }
}
