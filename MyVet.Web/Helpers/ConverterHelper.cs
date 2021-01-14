using MyVet.Web.Data;
using MyVet.Web.Data.Entities;
using MyVet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public async Task<Pet> ToPetAsync(PetViewModel model, string path, bool isNew) //se crea el metodo asincrono porque para retornar 
                                                                           //el owner de la base de datos se usa un await
        {
            //el metodo toma un objeto de la clase petviewmodel y devuelve un objeto de la clase pet
            var pet = new Pet
            {
                Agendas = model.Agendas,
                Born = model.Born,
                Histories = model.Histories,
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                PetType = await _dataContext.PetTypes.FindAsync(model.PetTypeId),
                Race = model.Race,
                Remarks = model.Remarks
            };

            //if(model.Id != 0)
            //{
            //    pet.Id = model.Id;
            //} 

            return pet;
        }

        //Este hace la operacion inversa
        public PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Agendas = pet.Agendas,
                Born = pet.Born,
                Histories = pet.Histories,
                ImageUrl = pet.ImageUrl,
                Name = pet.Name,
                Owner = pet.Owner,
                PetType = pet.PetType,
                Race = pet.Race,
                Remarks = pet.Remarks,
                Id = pet.Id,
                OwnerId = pet.Owner.Id,
                PetTypeId = pet.PetType.Id,
                PetTypes = _combosHelper.GetComboPetTypes()
            };
        }

        //Agregamos los metodos del iconverterHelper
        public async Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew)
        {
            return new History
            {
                Date = model.Date.ToUniversalTime(),
                Description = model.Description,
                Id = isNew ? 0 : model.Id, //si el registro es nuevo le mandamo de id 0 si estamos editando le mandamos el id que sera editado
                Pet = await _dataContext.Pets.FindAsync(model.PetId), //el modelo solo trasladamos el id, entonces buscamos por el id el objeto(Pet) y lo asignamos como Pet
                Remarks = model.Remarks,
                ServiceType = await _dataContext.ServiceTypes.FindAsync(model.ServiceTypeId) //lo mismo que Pet
            };
        }

        public HistoryViewModel ToHistoryViewModel(History history)
        {
            return new HistoryViewModel
            {
                Date = history.Date,
                Description = history.Description,
                Id = history.Id,
                PetId = history.Pet.Id,
                Remarks = history.Remarks,
                ServiceTypeId = history.ServiceType.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes() //aqui le mandamos el get combo de PetTypes para que lo marque
            };
        }
    }
}
